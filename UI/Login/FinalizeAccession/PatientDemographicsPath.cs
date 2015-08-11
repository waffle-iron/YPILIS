﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class PatientDemographicsPath
    {
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.Business.Domain.Lock m_Lock;

        public PatientDemographicsPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new LoginPageWindow(this.m_SystemIdentity);
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
				this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
				this.ShowCaseLockPage();
			}
			this.m_LoginPageWindow.ShowDialog();
        }

        private void ShowScanSecurityBadgePage()
        {
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			this.m_LoginPageWindow.PageNavigator.Navigate(scanSecurityBadgePage);
			scanSecurityBadgePage.AuthentificationSuccessful += new ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
		}

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
		{
			this.m_SystemIdentity = e.SystemIdentity;
			this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
			this.ShowCaseLockPage();
		}

		private void ShowCaseLockPage()
		{
			this.m_Lock = new Business.Domain.Lock(this.m_SystemIdentity);
			CaseLockPage caseLockPage = new CaseLockPage(this.m_LoginPageWindow.PageNavigator, this.m_Lock, this.m_AccessionOrder);
			caseLockPage.Return += new CaseLockPage.ReturnEventHandler(CaseLockPage_Return);
			caseLockPage.AttemptCaseLock();
		}

        private void CaseLockPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            CaseLockPage caseLockPage = (CaseLockPage)sender;
            if (caseLockPage.Lock.LockAquired == true)
            {
				this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);
				FinalizeAccession.PatientDetailsPage patientDetailsPage = new FinalizeAccession.PatientDetailsPage(this.m_AccessionOrder, this.m_ObjectTracker);
                patientDetailsPage.Return += new FinalizeAccession.PatientDetailsPage.ReturnEventHandler(PatientDetailsPage_Return);
				this.m_LoginPageWindow.PageNavigator.Navigate(patientDetailsPage);
            }
            else
            {
                this.m_LoginPageWindow.Close();
            }
        }

        private void PatientDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
			this.m_Lock.ReleaseUserLocks();
            this.m_LoginPageWindow.Close();			
		}       
    }
}