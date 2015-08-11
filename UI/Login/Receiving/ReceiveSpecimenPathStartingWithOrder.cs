﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
	public class ReceiveSpecimenPathStartingWithOrder
	{        
        private YellowstonePathology.UI.Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.UI.Login.Receiving.ClientOrderReceivingHandler m_ClientOrderReceivingHandler;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        public ReceiveSpecimenPathStartingWithOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_ClientOrder = clientOrder;
            this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.Blank);
            this.m_LoginPageWindow = new Receiving.LoginPageWindow();                        
        }

        public void Start()
        {                       
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
				this.SetClient();
			}
			this.m_LoginPageWindow.Show();
		}

        private void ShowScanSecurityBadgePage()
        {
			YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			scanSecurityBadgePage.AuthentificationSuccessful += new ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            this.m_LoginPageWindow.PageNavigator.Navigate(scanSecurityBadgePage);
        }

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
        {
			this.m_SystemIdentity = e.SystemIdentity;
			this.SetClient();			
        }

		private void SetClient()
		{			
			this.m_ClientOrderReceivingHandler = new Receiving.ClientOrderReceivingHandler(this.m_SystemIdentity);
			this.m_ClientOrderReceivingHandler.IFoundAClientOrder(this.m_ClientOrder);			

			YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_ClientOrderReceivingHandler.ClientOrder.ClientId);
			client.ClientLocationCollection.SetCurrentLocation(client.ClientLocationCollection[0].ClientLocationId);
			this.m_ClientOrderReceivingHandler.IFoundAClient(client);
            
			this.ShowItemsReceivedPage();
		}
		
		private void ShowItemsReceivedPage()
		{
            Receiving.ItemsReceivedPage itemsReceivedPage = new Receiving.ItemsReceivedPage(this.m_ClientOrderReceivingHandler);
            itemsReceivedPage.Next += new ItemsReceivedPage.NextEventHandler(ItemsReceivedPage_Next);
            itemsReceivedPage.Back += new ItemsReceivedPage.BackEventHandler(ItemsReceivedPage_Back);
            itemsReceivedPage.BarcodeWontScan += new ItemsReceivedPage.BarcodeWontScanEventHandler(ItemsReceivedPage_BarcodeWontScan);            
            itemsReceivedPage.ContainerScannedReceived += new ItemsReceivedPage.ContainScanReceivedEventHandler(ItemsReceivedPage_ContainerScannedReceived);
			itemsReceivedPage.ShowClientOrderDetailsPage += new ItemsReceivedPage.ShowClientOrderDetailsPageEventHandler(ItemsReceivedPage_ShowClientOrderDetailsPage);
			this.m_LoginPageWindow.PageNavigator.Navigate(itemsReceivedPage);
		}

        private void ItemsReceivedPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

		private void ItemsReceivedPage_ContainerScannedReceived(object sender, YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.ReceiveContainerScan(containerBarcode.ToString());
        }        

        private void ItemsReceivedPage_BarcodeWontScan(object sender, EventArgs e)
        {
            YellowstonePathology.UI.Login.ReceiveSpecimen.BarcodeManualEntryPage containerManualEntryPage = new YellowstonePathology.UI.Login.ReceiveSpecimen.BarcodeManualEntryPage();
            containerManualEntryPage.Return += new ReceiveSpecimen.BarcodeManualEntryPage.ReturnEventHandler(ContainerManualEntryPage_Return);
            containerManualEntryPage.Back += new ReceiveSpecimen.BarcodeManualEntryPage.BackEventHandler(ContainerManualEntryPage_Back);
            this.m_LoginPageWindow.PageNavigator.Navigate(containerManualEntryPage);						
        }

        private void ContainerManualEntryPage_Back()
        {
            this.ShowItemsReceivedPage();
        }        

        private void ItemsReceivedPage_Next(object sender, EventArgs e)
        {
			this.m_ClientOrderReceivingHandler.Save();
			this.StartReviewClientOrderPath();
        }

		private void ItemsReceivedPage_ShowClientOrderDetailsPage(object sender, CustomEventArgs.ClientOrderDetailReturnEventArgs e)
		{
			this.m_ClientOrderReceivingHandler.ResetTheSelectedClientOrderDetailToThisOne(e.ClientOrderDetail);
			this.ShowClientOrderDetailsPage();
		}

        private void ContainerManualEntryPage_Return(object sender, string containerId)
        {
            if (string.IsNullOrEmpty(containerId) == false)
            {
                this.ReceiveContainerScan(containerId);
            }           
        }

		private void ReceiveContainerScan(string containerId)
		{
            YellowstonePathology.UI.Login.ReceiveSpecimen.IFoundAContainerResult result = this.m_ClientOrderReceivingHandler.IFoundAContainer(containerId);
			if (result.OkToReceive == true)
			{
				this.ShowClientOrderDetailsPage();
			}
			else
			{
				System.Windows.MessageBox.Show(result.Message);
			}
		}

		private void ShowClientOrderDetailsPage()
		{
            Receiving.ClientOrderDetailsPage clientOrderDetailsPage = new Receiving.ClientOrderDetailsPage(this.m_LoginPageWindow.PageNavigator, this.m_ClientOrderReceivingHandler.CurrentClientOrderDetail, this.m_ClientOrderReceivingHandler.ClientOrder.SpecialInstructions, this.m_ClientOrderReceivingHandler.SystemIdentity);
            clientOrderDetailsPage.Next += new ClientOrderDetailsPage.NextEventHandler(ClientOrderDetailsPage_Next);
            clientOrderDetailsPage.Back += new ClientOrderDetailsPage.BackEventHandler(ClientOrderDetailsPage_Back);
            clientOrderDetailsPage.SaveClientOrderDetail += new ClientOrderDetailsPage.SaveClientOrderDetailEventHandler(ClientOrderDetailsPage_SaveClientOrderDetail);            
			this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderDetailsPage);            
		}

        private void ClientOrderDetailsPage_SaveClientOrderDetail(object sender, EventArgs e)
        {
            this.m_ClientOrderReceivingHandler.Save();
        }        

        private void ClientOrderDetailsPage_Back(object sender, EventArgs e)
        {
            this.ShowItemsReceivedPage();
        }

        private void ClientOrderDetailsPage_Next(object sender, EventArgs e)
        {
            this.ShowItemsReceivedPage();
        }			

		private void HandleCommand(UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch ((ReceiveSpecimen.ReceiveSpecimenCommandTypeEnum)e.Data)
			{
				case ReceiveSpecimen.ReceiveSpecimenCommandTypeEnum.Finalize:
                    this.m_LoginPageWindow.Close();
					break;
			}
		}

		private void StartReviewClientOrderPath()
		{
			ReviewClientOrderPath reviewClientOrderPath = new ReviewClientOrderPath(this.m_LoginPageWindow.PageNavigator, this.m_ClientOrderReceivingHandler);
			reviewClientOrderPath.Return += new ReviewClientOrderPath.ReturnEventHandler(ReviewClientOrderPath_Return);
			reviewClientOrderPath.Back += new ReviewClientOrderPath.BackEventHandler(ReviewClientOrderPath_Back);
			reviewClientOrderPath.Finish += new ReviewClientOrderPath.FinishEventHandler(ReviewClientOrderPath_Finish);
			reviewClientOrderPath.Start();
		}

		private void ReviewClientOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_LoginPageWindow.Close();
		}

		private void ReviewClientOrderPath_Back(object sender, EventArgs e)
		{
			this.ShowItemsReceivedPage();
		}


		private void ReviewClientOrderPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Command:
					this.HandleCommand(e);
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
                    this.m_LoginPageWindow.Close();
					break;
			}
		}        
	}
}