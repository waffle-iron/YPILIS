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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for BRAFV600EResultPage.xaml
	/// </summary>
	public partial class BRAFV600EKResultPage : UserControl, INotifyPropertyChanged, Shared.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private string m_PageHeaderText;
		private string m_OrderedOnDescription;
        private YellowstonePathology.Business.Test.IndicationCollection m_IndicationCollection;
        private YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection m_ResultCollection;
		private YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder m_PanelSetOrder;
		private System.Windows.Visibility m_BackButtonVisibility;

		public BRAFV600EKResultPage(YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			System.Windows.Visibility backButtonVisibility)
		{
			this.m_PanelSetOrder = panelSetOrderBraf;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_ObjectTracker = objectTracker;
			this.m_PageNavigator = pageNavigator;
			this.m_BackButtonVisibility = backButtonVisibility;

			this.m_PageHeaderText = "BRAF V600E/K Mutation Analysis Results For: " + this.m_AccessionOrder.PatientDisplayName + " (" + panelSetOrderBraf.ReportNo + ")";
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetByAliquotOrderId(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description + ": " + aliquotOrder.Label;

            this.m_IndicationCollection = YellowstonePathology.Business.Test.IndicationCollection.GetAll();
            this.m_ResultCollection = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection.GetUniqueResultChoices();

			InitializeComponent();

			DataContext = this;                      

		}

        public YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection ResultCollection
        {
            get { return this.m_ResultCollection; }
        }

		public YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public YellowstonePathology.Business.Test.IndicationCollection IndicationCollection
		{
			get { return this.m_IndicationCollection; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
		}

		public System.Windows.Visibility BackButtonVisibility
		{
			get { return this.m_BackButtonVisibility; }
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

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.Next(this, new EventArgs());
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult.IsOkToSetResult(this.m_PanelSetOrder);
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection.GetAll();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
				result.SetResults(this.m_PanelSetOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult.IsOkToFinal(this.m_AccessionOrder, this.m_PanelSetOrder);
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection.GetAll();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
				result.FinalizeResults(this.m_PanelSetOrder, this.m_SystemIdentity);

                YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
				if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId) == true)
				{
					YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder krasStandardReflexTestOrder = (YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId);
					krasStandardReflexTestOrder.UpdateFromChildren(this.m_AccessionOrder, this.m_PanelSetOrder);
				}
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FResult.IsOkToUnFinalize(this.m_PanelSetOrder);
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection.GetAll();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
				result.UnFinalizeResults(this.m_PanelSetOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult.IsOkToAccept(this.m_PanelSetOrder);
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.PanelOrder panelOrder = this.m_PanelSetOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection.GetAll();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
				result.AcceptResults(this.m_PanelSetOrder, panelOrder, this.m_SystemIdentity);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult.IsOkToUnaccept(this.m_PanelSetOrder);
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.PanelOrder panelOrder = this.m_PanelSetOrder.PanelOrderCollection.GetLastAcceptedPanelOrder();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResultCollection.GetAll();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
				result.UnacceptResults(this.m_PanelSetOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.PanelOrderCollection.GetUnacceptedPanelCount() == 0)
			{
				this.Save();
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKWordDocument report = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKWordDocument();
				report.Render(this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
				string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
				YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
			}
			else
			{
				MessageBox.Show("The results must be accepted before the document can be viewed.", "Accept then view");
			}
		}

		private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ComboBoxResult.SelectedItem != null)
			{
				YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
				this.m_PanelSetOrder.ResultCode = testResult.ResultCode;
			}
		}
	}
}