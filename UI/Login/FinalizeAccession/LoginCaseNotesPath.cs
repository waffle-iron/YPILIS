﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class LoginCaseNotesPath
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private LoginPageWindow m_LoginPageWindow;
		private YellowstonePathology.Business.Domain.Lock m_Lock;

		public LoginCaseNotesPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
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
				YellowstonePathology.Business.Domain.CaseNotesKeyCollection caseNotesKeyCollection = new YellowstonePathology.Business.Domain.CaseNotesKeyCollection(this.m_AccessionOrder);
				CaseNotesPage caseNotesPage = new CaseNotesPage(this.m_LoginPageWindow.PageNavigator, this.m_SystemIdentity, caseNotesKeyCollection);
				caseNotesPage.Return += new CaseNotesPage.ReturnEventHandler(CaseNotesPage_Return);
				this.m_LoginPageWindow.PageNavigator.Navigate(caseNotesPage);
			}
			else
			{
				this.m_LoginPageWindow.Close();
			}
		}

		private void CaseNotesPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
					this.m_LoginPageWindow.Close();
					break;
			}
		}
	}
}