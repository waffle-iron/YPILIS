﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{	
	public partial class SpecimenMappingPage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText = "Specimen Mapping Page";
		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
        private ObservableCollection<string> m_TimeToFixationTypeCollection;

        public SpecimenMappingPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
			this.m_ObjectTracker = objectTracker;
			this.m_AccessionOrder = accessionOrder;

            this.m_TimeToFixationTypeCollection = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.GetTimeToFixationTypeCollection();

			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);			
			InitializeComponent();

			DataContext = this;			
		}		

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
		{
			get { return this.m_PathologistUsers; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

        public ObservableCollection<string> TimeToFixationTypeCollection
        {
            get { return this.m_TimeToFixationTypeCollection; }
        }

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.IsOkToGoNext() == true)
            {
                this.Next(this, new EventArgs());
            }
		}

        private bool IsOkToGoNext()
        {
            bool result = true;
            if (this.m_AccessionOrder.ClientAccessioned == true)
            {
                if (string.IsNullOrEmpty(this.m_AccessionOrder.ClientAccessionNo) == true)
                {
                    MessageBox.Show("The Client Accession No cannot be blank.");
                    result = false;
                }
                else
                {
                    foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                    {
                        if (specimenOrder.ClientAccessioned == true)
                        {
                            if (specimenOrder.ClientSpecimenNumber == 0)
                            {
                                MessageBox.Show("The Client Specimen Number cannot be zero.");
                                result = false;
                                break;
                            }
                            else
                            {
                                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                                {
                                    if (aliquotOrder.ClientAccessioned == true)
                                    {
                                        if (string.IsNullOrEmpty(aliquotOrder.ClientLabel) == true)
                                        {
                                            MessageBox.Show("The Client Label for: " + aliquotOrder.Label + " cannot be blank.");
                                            result = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
			this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
		}

		public void UpdateBindingSources()
		{

		}

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            this.PrintClientAccessionedLabels();
        }

        private void PrintClientAccessionedLabels()
        {
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (specimenOrder.ClientAccessioned == true)
                {
                    foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                    {                        
                        if (aliquotOrder.ClientAccessioned == true)
                        {
                            YellowstonePathology.Business.Test.BlockLabelPrinter blockLabelPrinter = new Business.Test.BlockLabelPrinter(aliquotOrder.AliquotOrderId, aliquotOrder.Label, this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName);
                            blockLabelPrinter.Print();

                            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                            {
                                if (slideOrder.ClientAccessioned == true)
                                {
                                    YellowstonePathology.Business.Label.Model.HistologySlidePaperLabel paperLabel = new Business.Label.Model.HistologySlidePaperLabel(slideOrder.SlideOrderId,
                                        this.m_AccessionOrder.MasterAccessionNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation);

                                    YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter printer = new Business.Label.Model.HistologySlidePaperLabelPrinter();
                                    printer.Queue.Enqueue(paperLabel);
                                    printer.Print();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CheckBoxSlideClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)checkBox.Tag;
            slideOrder.Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.ClientAccessioned.ToString();
        }

        private void CheckBoxSlideClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)checkBox.Tag;
            slideOrder.Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Created.ToString();
        }
	}
}