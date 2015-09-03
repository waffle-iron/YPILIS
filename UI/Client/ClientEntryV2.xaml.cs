﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Client
{
	/// <summary>
	/// Interaction logic for ClientEntry.xaml
	/// </summary>
	public partial class ClientEntryV2 : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private YellowstonePathology.Business.Client.Model.Client m_Client;
		private YellowstonePathology.Business.Billing.InsuranceTypeCollection m_InsuranceTypeCollection;
		private List<string> m_FacilityTypes;
		private YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList m_DistributionTypeList;
		private YellowstonePathology.Business.View.ClientPhysicianView m_ClientPhysicianView;
		private YellowstonePathology.Business.Domain.PhysicianCollection m_PhysicianCollection;
		private YellowstonePathology.Business.Billing.Model.BillingRuleSetCollection m_BillingRuleSetCollection;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection m_ClientSupplyOrderCollection;

		public ClientEntryV2(YellowstonePathology.Business.Client.Model.Client client, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
			this.m_Client = client;
			this.m_ObjectTracker = objectTracker;
			this.m_ClientPhysicianView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientPhysicianViewByClientIdV2(this.m_Client.ClientId);
			this.m_InsuranceTypeCollection = new Business.Billing.InsuranceTypeCollection(true);
			
			this.m_FacilityTypes = new List<string>();
			this.m_FacilityTypes.Add("Hospital");
			this.m_FacilityTypes.Add("Hospital Owned Clinic");
			this.m_FacilityTypes.Add("Non-Grandfathered Hospital");
			this.m_FacilityTypes.Add("Non-Hospital");

            this.m_DistributionTypeList = new YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList();
			this.m_BillingRuleSetCollection = YellowstonePathology.Business.Billing.Model.BillingRuleSetCollection.GetAllRuleSets();
			this.m_ClientSupplyOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyOrderCollection(this.m_Client.ClientId);

			InitializeComponent();
			this.DataContext = this;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public ObservableCollection<YellowstonePathology.Business.Domain.Physician> Physicians
		{
			get { return this.m_ClientPhysicianView.Physicians; }
		}

		public List<string> FacilityTypes
		{
			get { return this.m_FacilityTypes; }
		}

		public YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList DistributionTypeList
		{
            get { return this.m_DistributionTypeList; }
		}

		public YellowstonePathology.Business.Billing.InsuranceTypeCollection InsuranceTypeCollection
		{
			get { return this.m_InsuranceTypeCollection; }
		}

		public YellowstonePathology.Business.Client.Model.Client Client
		{
			get { return this.m_Client; }
		}

		public YellowstonePathology.Business.Domain.PhysicianCollection PhysicianCollection
		{
			get { return this.m_PhysicianCollection; }
		}

		public YellowstonePathology.Business.Billing.Model.BillingRuleSetCollection BillingRuleSetCollection
		{
			get { return this.m_BillingRuleSetCollection; }
		}

		public YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection ClientSupplyOrderCollection
		{
			get { return this.m_ClientSupplyOrderCollection; }
		}

		private void BorderPanelSetOrderHeader_Loaded(object sender, RoutedEventArgs e)
		{
			Border border = sender as Border;
			ContentPresenter contentPresenter = border.TemplatedParent as ContentPresenter;
			contentPresenter.HorizontalAlignment = HorizontalAlignment.Stretch;
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			this.m_ObjectTracker.SubmitChanges(this.m_Client);
			Close();
		}

		private void Control_Loaded(object sender, RoutedEventArgs e)
		{
			UIElement uIElement = sender as UIElement;
			if (uIElement == null) return;
			uIElement.Focus();
		}

		private void ButtonAddToClient_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxAvailableProviders.SelectedItem != null)
			{
				YellowstonePathology.Business.Domain.Physician physician = (YellowstonePathology.Business.Domain.Physician)this.ListBoxAvailableProviders.SelectedItem;
				if (this.m_ClientPhysicianView.PhysicianExists(physician.PhysicianId) == false)
				{
					this.m_ObjectTracker.SubmitChanges(this.m_Client);

					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, objectId, physician.PhysicianId, physician.ObjectId, this.m_Client.ClientId);
					YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
					objectTracker.RegisterRootInsert(physicianClient);
					objectTracker.SubmitChanges(physicianClient);
					this.m_ClientPhysicianView.Physicians.Add(physician);
					this.NotifyPropertyChanged("Physicians");
				}
			}
		}

		private void ButtonRemoveFromClient_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxPhysicians.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Remove selected physician?", "Remove", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK)
				{
					YellowstonePathology.Business.Domain.Physician physician = (YellowstonePathology.Business.Domain.Physician)this.ListBoxPhysicians.SelectedItem;
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(physician.ObjectId, this.m_Client.ClientId);
					YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
					objectTracker.RegisterRootDelete(physicianClient);
					objectTracker.SubmitChanges(physicianClient);

					this.m_ClientPhysicianView.Physicians.Remove(physician);
					this.NotifyPropertyChanged("Physicians");
				}
			}
		}

		private void TextBoxProviderName_KeyUp(object sender, KeyEventArgs e)
		{
			if (this.TextBoxProviderName.Text.Length > 0)
			{
				string[] splitName = this.TextBoxProviderName.Text.Split(',');
				string lastName = splitName[0].Trim();
				string firstName = null;
				if (splitName.Length > 1)
				{
					firstName = splitName[1].Trim();
				}
				this.m_PhysicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysiciansByName(firstName, lastName);
				NotifyPropertyChanged("PhysicianCollection");
			}
		}

		private void ButtonNewSupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = new Business.Client.Model.ClientSupplyOrder(objectId, this.m_Client);
			ClientSupplyOrderDialog clientSupplyOrderDialog = new ClientSupplyOrderDialog(clientSupplyOrder);
			clientSupplyOrderDialog.ShowDialog();
		}

		private void ButtonDeleteSupplyOrder_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ListViewOrderDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewOrderDetails.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewOrderDetails.SelectedItem;
				ClientSupplyOrderDialog clientSupplyOrderDialog = new ClientSupplyOrderDialog(clientSupplyOrder);
				clientSupplyOrderDialog.ShowDialog();
			}
		}
	}
}
