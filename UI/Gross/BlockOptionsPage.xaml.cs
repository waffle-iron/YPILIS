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

namespace YellowstonePathology.UI.Gross
{
	/// <summary>
	/// Interaction logic for BlockOptionsPage.xaml
	/// </summary>
	public partial class BlockOptionsPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges
	{
        public delegate void NextEventHandler(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event BackEventHandler Back;

        public delegate void ShowBlockColorSelectionPageEventHandler(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event ShowBlockColorSelectionPageEventHandler ShowBlockColorSelectionPage;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;

		public BlockOptionsPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_SpecimenOrder = specimenOrder;
            this.m_AliquotOrder = aliquotOrder;			
			this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_ObjectTracker = objectTracker;			

			InitializeComponent();
			DataContext = this;
		}		

		private void ButtonReprintBlock_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            if (panelSetOrder == null) panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection[0];

			this.m_AliquotOrder.Printed = false;
			YellowstonePathology.Business.Common.BlockCollection blockCollection = new Business.Common.BlockCollection();
			string patientInitials = YellowstonePathology.Business.Helper.PatientHelper.GetPatientInitials(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName);
			YellowstonePathology.Business.Test.AliquotOrderCollection blocksToPrintCollection = this.m_SpecimenOrder.AliquotOrderCollection.GetUnPrintedBlocks();
			blockCollection.FromAliquotOrderItemCollection(blocksToPrintCollection, panelSetOrder.ReportNo, patientInitials, this.m_AccessionOrder.PrintMateColumnNumber, true);
			YellowstonePathology.Business.Common.PrintMate.Print(blockCollection);
			blocksToPrintCollection.SetPrinted();

            CustomEventArgs.SpecimenOrderReturnEventArgs specimenOrderReturnEventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder);
            this.Next(this, specimenOrderReturnEventArgs);
		}

		private void ButtonChangeFSToIC_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_AliquotOrder.AliquotType == "FrozenBlock")
			{
				this.DeleteAliquot();
				this.AddIC();

                CustomEventArgs.SpecimenOrderReturnEventArgs specimenOrderReturnEventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder);
                this.Next(this, specimenOrderReturnEventArgs);
			}
			else
			{
				MessageBox.Show("The selected block is not a Frozen Block");
			}
		}		

		private void DeleteAliquot()
		{
            YellowstonePathology.Business.Visitor.RemoveAliquotOrderVisitor removeAliquotOrderVisitor = new Business.Visitor.RemoveAliquotOrderVisitor(this.m_AliquotOrder);
            this.m_AccessionOrder.TakeATrip(removeAliquotOrderVisitor);            
		}

		private void AddIC()
		{
			YellowstonePathology.Business.Test.Model.Test iCTest = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests().GetTest(194);
			string patientInitials = YellowstonePathology.Business.Helper.PatientHelper.GetPatientInitials(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName);

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.AddBlock(this.m_SpecimenOrder, YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint, this.m_AccessionOrder.AccessionDate.Value);
			YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo, iCTest, iCTest.OrderComment, null, false, this.m_AliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection, this.m_SystemIdentity);
            this.m_AccessionOrder.TakeATrip(orderTestVisitor);

			YellowstonePathology.Business.Common.BlockCollection blockCollection = new Business.Common.BlockCollection();
			YellowstonePathology.Business.Test.AliquotOrderCollection blocksToPrintCollection = this.m_SpecimenOrder.AliquotOrderCollection.GetUnPrintedBlocks();
			blockCollection.FromAliquotOrderItemCollection(blocksToPrintCollection, this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo, patientInitials, this.m_AccessionOrder.PrintMateColumnNumber, true);
			YellowstonePathology.Business.Common.PrintMate.Print(blockCollection);
			blocksToPrintCollection.SetPrinted();
		}

		public void Save()
		{
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);	
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void UpdateBindingSources()
		{
		}

        private void ButtonChangeBlockColor_Click(object sender, RoutedEventArgs e)
        {
            CustomEventArgs.SpecimenOrderReturnEventArgs eventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder);
            if (this.ShowBlockColorSelectionPage != null) this.ShowBlockColorSelectionPage(this, eventArgs);
        }        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            CustomEventArgs.SpecimenOrderReturnEventArgs eventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder);
            if(this.Back != null) this.Back(this, eventArgs);
        }
	}
}
