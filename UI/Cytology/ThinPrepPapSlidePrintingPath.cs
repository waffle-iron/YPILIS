﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Cytology
{
	public class ThinPrepPapSlidePrintingPath
	{
        private PrintSlideDialog m_PrintSlideDialog;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;        

		public ThinPrepPapSlidePrintingPath()
        {
            
        }

        public void Start()
        {
			this.m_PrintSlideDialog = new PrintSlideDialog();
            this.ShowScanSecurityBadgePage();
			this.m_PrintSlideDialog.ShowDialog();
        }        

        private void ShowScanSecurityBadgePage()
        {
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new Login.ScanSecurityBadgePage(System.Windows.Visibility.Visible);
			this.m_PrintSlideDialog.PageNavigator.Navigate(scanSecurityBadgePage);
            scanSecurityBadgePage.AuthentificationSuccessful += new Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            scanSecurityBadgePage.Close += new Login.ScanSecurityBadgePage.CloseEventHandler(ScanSecurityBadgePage_Close);
        }

        private void ScanSecurityBadgePage_Close(object sender, EventArgs e)
        {
			this.m_PrintSlideDialog.Close();
        }

        private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
        {
            this.m_SystemIdentity = e.SystemIdentity;
            this.ShowScanContainerPage();
        }

		private void ShowScanContainerPage()
        {
            YellowstonePathology.UI.Gross.ScanContainerPage scanContainerPage = new YellowstonePathology.UI.Gross.ScanContainerPage(this.m_SystemIdentity, "Please Scan Container");
            scanContainerPage.UseThisContainer += new YellowstonePathology.UI.Gross.ScanContainerPage.UseThisContainerEventHandler(ScanContainerPage_UseThisContainer);
            scanContainerPage.PageTimedOut += new YellowstonePathology.UI.Gross.ScanContainerPage.PageTimedOutEventHandler(ScanContainerPage_PageTimedOut);
            scanContainerPage.BarcodeWontScan += new YellowstonePathology.UI.Gross.ScanContainerPage.BarcodeWontScanEventHandler(ScanContainerPage_BarcodeWontScan);
            scanContainerPage.SignOut += new YellowstonePathology.UI.Gross.ScanContainerPage.SignOutEventHandler(ScanContainerPage_SignOut);

            this.m_PrintSlideDialog.PageNavigator.Navigate(scanContainerPage);
        }

        private void ScanContainerPage_SignOut(object sender, EventArgs e)
        {
            this.m_SystemIdentity = null;
            this.ShowScanSecurityBadgePage();
        }

        private void ScanContainerPage_PageTimedOut(object sender, EventArgs e)
        {
            this.ShowScanSecurityBadgePage();
        }

        private void ScanContainerPage_BarcodeWontScan(object sender, EventArgs e)
        {
            //Login.ReceiveSpecimen.BarcodeManualEntryPage containerManualEntryPage = new Login.ReceiveSpecimen.BarcodeManualEntryPage();
            //containerManualEntryPage.Return += new Login.ReceiveSpecimen.BarcodeManualEntryPage.ReturnEventHandler(BarcodeManualEntryPage_Return);
            //containerManualEntryPage.Back += new Login.ReceiveSpecimen.BarcodeManualEntryPage.BackEventHandler(ContainerManualEntryPage_Back);
            //this.m_HistologyGrossDialog.PageNavigator.Navigate(containerManualEntryPage);
        }

        private void ScanContainerPage_UseThisContainer(object sender, string containerId)
        {
            /*
            this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByContainerId(containerId);
            this.m_ObjectTracker = new Persistence.ObjectTracker();

            if (this.m_AccessionOrder == null)
            {
                System.Windows.MessageBox.Show("The scanned container was not found.");
                this.ShowScanContainerPage();
            }
            else
            {
                this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(containerId);

                this.AddMaterialTrackingLog(specimenOrder);

                if (this.m_HistologyGrossDialog.PageNavigator.HasDualMonitors() == true)
                {
                    DictationTemplatePage dictationTemplatePage = new DictationTemplatePage(specimenOrder.SpecimenId);
                    this.m_SecondaryWindow.PageNavigator.Navigate(dictationTemplatePage);
                }

                if (string.IsNullOrEmpty(specimenOrder.ProcessorRunId) == true)
                {
                    YellowstonePathology.Business.Surgical.ProcessorRunCollection processorRunCollection = YellowstonePathology.Business.Surgical.ProcessorRunCollection.GetAll(false);
                    YellowstonePathology.Business.Surgical.ProcessorRun processorRun = processorRunCollection.Get(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference);
                    specimenOrder.SetProcessor(processorRun);
                    specimenOrder.SetFixationDuration();
                }

                if (this.m_AccessionOrder.PrintMateColumnNumber == 0 && this.m_AccessionOrder.PanelSetOrderCollection.HasTestBeenOrdered(48) == false)
                {
                    this.ShowBlockColorSelectionPage(specimenOrder);
                }
                else
                {
                    this.ShowPrintBlockPage(specimenOrder);
                }
            }
            */
        }

		private void ScanContainerPage_ContainerScannedReceived(object sender, Business.BarcodeScanning.ContainerBarcode containerBarcode)
		{
			this.ShowThinPrepPapSlidePrintingPage(containerBarcode.ID);
		}

		private void ScanContainerPage_Next(object sender, EventArgs e)
		{
			this.m_PrintSlideDialog.Close();
		}

		private void ShowThinPrepPapSlidePrintingPage(string containerId)
		{
			ThinPrepPapSlidePrintingPage thinPrepPapSlidePrintingPage = new ThinPrepPapSlidePrintingPage();
			thinPrepPapSlidePrintingPage.Next += new ThinPrepPapSlidePrintingPage.NextEventHandler(ThinPrepPapSlidePrintingPage_Next);
			this.m_PrintSlideDialog.PageNavigator.Navigate(thinPrepPapSlidePrintingPage);
		}

		private void ThinPrepPapSlidePrintingPage_Next(object sender, EventArgs e)
		{
			this.ShowScanContainerPage();
		}

        private void PageTimedOut(object sender, EventArgs eventArgs)
        {
            this.ShowScanSecurityBadgePage();
        }
	}
}