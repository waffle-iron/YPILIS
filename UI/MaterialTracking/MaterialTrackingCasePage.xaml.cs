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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.MaterialTracking
{
	/// <summary>
	/// Interaction logic for MaterialTrackingCasePage.xaml
	/// </summary>
	public partial class MaterialTrackingCasePage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection m_MaterialTrackingLogCollection;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private string m_PageHeaderText;

		public MaterialTrackingCasePage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_MaterialTrackingLogCollection = materialTrackingLogCollection;
            this.m_SystemIdentity = systemIdentity;
			this.m_PageHeaderText = "Material Tracking For Case: " + this.m_AccessionOrder.MasterAccessionNo + " - " + this.m_AccessionOrder.PatientDisplayName;
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;			

			InitializeComponent();
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(MaterialTrackingCasePage_Loaded);
			this.Unloaded += new RoutedEventHandler(MaterialTrackingCasePage_Unloaded);
		}

		private void MaterialTrackingCasePage_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(BarcodeScanPort_HistologyBlockScanReceived);
			this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(BarcodeScanPort_HistologySlideScanReceived);
		}

        private void MaterialTrackingCasePage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.HistologyBlockScanReceived -= BarcodeScanPort_HistologyBlockScanReceived;
			this.m_BarcodeScanPort.HistologySlideScanReceived -= BarcodeScanPort_HistologySlideScanReceived;
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection MaterialTrackingLogCollection
		{
			get { return this.m_MaterialTrackingLogCollection; }
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
				this.HandleBlockScanReceived(barcode.ID);
			}
			));
		}

		private void HandleBlockScanReceived(string aliquotOrderId)
		{
			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);
			if(aliquotOrder != null)
			{
				this.AddMaterialTrackingLog(aliquotOrder);
			}
			else
			{
				MessageBox.Show("The block scanned does not belong to this case.");
			}
		}

		private void AddMaterialTrackingLog(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
		{
			YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
			YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
			YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);
			string scanLocation = "Block Scanned At " + YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, aliquotOrder.AliquotOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
				thisLocation.LocationId, thisLocation.Description, this.m_SystemIdentity.User.UserId, this.m_SystemIdentity.User.UserName, "Block Scanned", scanLocation, "Aliquot", this.m_AccessionOrder.MasterAccessionNo, aliquotOrder.Label, aliquotOrder.ClientAccessioned);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			objectTracker.RegisterRootInsert(materialTrackingLog);
			objectTracker.SubmitChanges(materialTrackingLog);
			this.m_MaterialTrackingLogCollection.Insert(0, materialTrackingLog);
		}

		private void BarcodeScanPort_HistologySlideScanReceived(Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
				this.HandleSlideScanReceived(barcode.ID);
			}
			));
		}

		private void HandleSlideScanReceived(string slideOrderId)
		{
			YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSlideOrder(slideOrderId);
			if (slideOrder != null)
			{
				this.AddMaterialTrackingLog(slideOrder);
			}
			else
			{
				MessageBox.Show("This slide does not belong to this case.");
			}
		}

		private void AddMaterialTrackingLog(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
		{
			YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
			YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
			YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);
			string scanLocation = "Slide Scanned At " + YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, slideOrder.SlideOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
				thisLocation.LocationId, thisLocation.Description, this.m_SystemIdentity.User.UserId, this.m_SystemIdentity.User.UserName, "Slide Scan", scanLocation, "SlideOrder", this.m_AccessionOrder.MasterAccessionNo, slideOrder.Label, slideOrder.ClientAccessioned);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			objectTracker.RegisterRootInsert(materialTrackingLog);
			objectTracker.SubmitChanges(materialTrackingLog);
			this.m_MaterialTrackingLogCollection.Insert(0, materialTrackingLog);
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            Window.GetWindow(this).Close();			
		}                     

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

        public void Save()
        {
            
        }

		public void UpdateBindingSources()
		{

		}
	}
}