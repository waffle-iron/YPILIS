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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login.Receiving
{	
	public partial class ClientOrderDetailsPage : UserControl, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void SaveClientOrderDetailEventHandler(object sender, EventArgs e);
        public event SaveClientOrderDetailEventHandler SaveClientOrderDetail;        
		
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		private string m_PageHeaderText = "Specimen Details Page";
        private ObservableCollection<string> m_FixationTypeCollection;
        private string m_SpecialInstructions;        

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private ObservableCollection<string> m_TimeToFixationTypeCollection;
        private YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;

		public ClientOrderDetailsPage(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, 
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail,
            string specialInstructions,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_PageNavigator = pageNavigator;
            this.m_ClientOrderDetail = clientOrderDetail;
            this.m_SpecialInstructions = specialInstructions;
            this.m_SystemIdentity = systemIdentity;

            this.m_TimeToFixationTypeCollection = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.GetTimeToFixationTypeCollection();
            this.m_FixationTypeCollection = YellowstonePathology.Business.Specimen.Model.FixationType.GetFixationTypeCollection();
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_SpecimenCollection = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.GetAll();

            this.DataContext = this;
			InitializeComponent();
            
            this.Loaded +=new RoutedEventHandler(ClientOrderDetailsPage_Loaded);
            this.Unloaded += new RoutedEventHandler(ClientOrderDetailsPage_Unloaded);            
		}

        private void ClientOrderDetailsPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
        }

        private void ClientOrderDetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;                
            this.TextBoxAccessionAs.Focus();
            if (string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccession) == false)
            {
                if (this.m_ClientOrderDetail.DescriptionToAccession.ToUpper().Contains("THIN PREP") == true)
                {
                    this.TextBoxSpecimenSource.Focus();
                }
            }

            this.ComboBoxReceivedIn.SelectionChanged += new SelectionChangedEventHandler(ComboBoxReceivedIn_SelectionChanged);
            this.CheckBoxClientAccessioned.Checked +=new RoutedEventHandler(CheckBoxClientAccessioned_Checked);
            this.CheckBoxClientAccessioned.Unchecked +=new RoutedEventHandler(CheckBoxClientAccessioned_Unchecked);
        }

        private void ComboBoxReceivedIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_ClientOrderDetail.SetFixationStartTime();
            this.NotifyPropertyChanged(string.Empty);
        }

		private void ContainerScanReceived(YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                if (string.IsNullOrEmpty(this.m_ClientOrderDetail.ContainerId) == true)
                {
                    this.m_ClientOrderDetail.ContainerIdBinding = containerBarcode.ToString();
                    this.m_ClientOrderDetail.Receive();
                }
                else
                {
                    MessageBox.Show("Unable to set the Container ID because it is already set.");
                }                
            }
            ));
        }

        public ObservableCollection<string> TimeToFixationTypeCollection
        {
            get { return this.m_TimeToFixationTypeCollection; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return this.m_SpecimenCollection; }
        }

        public string SpecialInstructions
        {
            get { return this.m_SpecialInstructions; }
            set { this.m_SpecialInstructions = value; }
        }

        public ObservableCollection<string> FixationTypeCollection
        {
            get { return this.m_FixationTypeCollection; }
        }			

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}
        
		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
		{
			get { return this.m_ClientOrderDetail; }
		}		
        
		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
            if(this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            this.m_ClientOrderDetail.ValidateObject();
            if (this.m_ClientOrderDetail.ValidationErrors.Count > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("There are validation errors on this form.  Are you sure you want to continue?", "Validation Errors", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
                    this.Next(this, new EventArgs());                    
                }
            }
            else
            {
                this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
                this.Next(this, new EventArgs());                    
            }           
		}        

        private YellowstonePathology.Business.Rules.MethodResult IsOkToNavigate()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Success = true;
                        
            if (this.m_ClientOrderDetail.FixationStartTimeManuallyEntered == true && string.IsNullOrEmpty(this.m_ClientOrderDetail.FixationComment) == true)
            {
                result.Success = false;
                result.Message = "The Fixation Start Time has been manually set so you must provide a comment before continuing.";
            }            

            if (string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccession) == false)
            {
                if (this.m_ClientOrderDetail.DescriptionToAccession.ToUpper().Contains("PROSTATE") == true)
                {
                    if (string.IsNullOrEmpty(this.m_ClientOrderDetail.SpecimenId) == true)
                    {
                        result.Success = false;
                        result.Message = "You must select the specimen id for prostate specimens.";
                    }
                }
                else if (this.m_ClientOrderDetail.DescriptionToAccession.ToUpper().Contains("APPENDIX, EXCISION") == true)
                {
                    if (string.IsNullOrEmpty(this.m_ClientOrderDetail.SpecimenId) == true)
                    {
                        result.Success = false;
                        result.Message = "You must select the specimen id for Appendix, excision specimens.";
                    }
                }
            }            

            return result;
        }        

        private bool HandleContainerIdValidation()
        {
            bool result = false;

            if (string.IsNullOrEmpty(this.m_ClientOrderDetail.ContainerId) == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("The Container ID is blank. Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }
       
		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
            if (this.SaveClientOrderDetail != null) this.SaveClientOrderDetail(this, new EventArgs());
		}

		public void UpdateBindingSources()
		{
         
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
       
        private void ComboBoxSpecimenId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxSpecimenId.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccession) == true)
                {
                    if (this.TextBoxAccessionAs != null)
                    {
                        YellowstonePathology.Business.Specimen.Model.Specimen specimen = (YellowstonePathology.Business.Specimen.Model.Specimen)this.ComboBoxSpecimenId.SelectedItem;
                        this.m_ClientOrderDetail.DescriptionToAccessionBinding = specimen.Description;
                        this.m_ClientOrderDetail.LabFixationBinding = specimen.LabFixation;
                        this.m_ClientOrderDetail.ClientFixationBinding = specimen.ClientFixation;
                        this.m_ClientOrderDetail.RequiresGrossExamination = specimen.RequiresGrossExamination;

                        this.HandleTemplatedSpecimen();
                        this.NotifyPropertyChanged("");
                    }
                }
            }
        }

        private void HandleTemplatedSpecimen()
        {
            int positionOfFirstBracket = this.TextBoxAccessionAs.Text.IndexOf('[');
            if (positionOfFirstBracket != -1)
            {
                int positionOfLastBracket = this.TextBoxAccessionAs.Text.IndexOf(']', positionOfFirstBracket);
                if (positionOfLastBracket != -1)
                {
                    this.TextBoxAccessionAs.Focus();
                    this.TextBoxAccessionAs.SelectionStart = positionOfFirstBracket;
                    this.TextBoxAccessionAs.SelectionLength = positionOfLastBracket - positionOfFirstBracket + 1;
                }
            }                        
        }

        private void HyperLinkReceivedFresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Fresh;
            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInFormalin_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInBPlus_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.BPlusFixative;
            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInCytolyt_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Cytolyt;
            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Cytolyt;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceived95PercentIPA_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.NinetyFivePercentIPA;
            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.NinetyFivePercentIPA;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInNotApplicable_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable;
            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInPreservCyt_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.PreservCyt;
            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.PreservCyt;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }   

        private void CheckBoxClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            this.TextBoxFixationStartTime.IsEnabled = false;
            this.m_ClientOrderDetail.FixationStartTimeBinding = null;

            this.m_ClientOrderDetail.ClientFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.ComboBoxReceivedIn.IsEnabled = false;

            this.m_ClientOrderDetail.LabFixationBinding = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.ComboBoxProcessedIn.IsEnabled = false;            
        }

        private void CheckBoxClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {
            this.TextBoxFixationStartTime.IsEnabled = true;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperlinkClearContainerId_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ClientOrderDetail.Accessioned == false)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to clear the Container ID.", "Clear Container ID", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    this.m_ClientOrderDetail.ContainerId = null;
                    this.m_ClientOrderDetail.Received = false;
                    this.m_ClientOrderDetail.DateReceived = null;
                    this.m_ClientOrderDetail.FixationStartTime = null;
                    this.NotifyPropertyChanged(string.Empty);
                }
            }
            else
            {
                MessageBox.Show("The Container Id cannot be cleared because the specimen has been accessioned.");
            }
        }
                
	}
}