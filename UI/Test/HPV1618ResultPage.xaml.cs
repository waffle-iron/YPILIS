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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for HPV1618ResultPage.xaml
	/// </summary>
	public partial class HPV1618ResultPage : UserControl, INotifyPropertyChanged, Shared.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private string m_PageHeaderText;		

		private YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 m_PanelSetOrderHPV1618;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private List<string> m_IndicationList;

		public HPV1618ResultPage(YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_PanelSetOrderHPV1618 = panelSetOrderHPV1618;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_ObjectTracker = objectTracker;
			this.m_PageNavigator = pageNavigator;

            this.m_IndicationList = YellowstonePathology.Business.Test.HPV1618.HPV1618Indication.GetIndicationList();
			this.m_PageHeaderText = "HPV Genotypes 16 and 18 Results For: " + this.m_AccessionOrder.PatientDisplayName;
			
			InitializeComponent();

			DataContext = this;                      
		}

        public List<string> IndicationList
        {
            get { return this.m_IndicationList; }
        }

		public YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 PanelSetOrder
		{
			get { return this.m_PanelSetOrderHPV1618; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		
		public string HyperlinkFinalizeText
		{
			get
			{
				string result = "Finalize";
				if (this.m_PanelSetOrderHPV1618.Final == true) result = "Unfinalize";
				return result;
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
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

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrderHPV1618.IsOkToFinalize();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection resultCollection = YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection.GetAllResults();
				YellowstonePathology.Business.Test.HPV1618.HPV1618Result hpv1618Result = resultCollection.GetResult(this.m_PanelSetOrderHPV1618.ResultCode);
				hpv1618Result.FinalizeResults(this.m_PanelSetOrderHPV1618, this.m_SystemIdentity);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                if (this.m_AccessionOrder.PanelSetOrderCollection.WomensHealthProfileExists() == true)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile().SetExptectedFinalTime(this.m_AccessionOrder);
                }
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrderHPV1618.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.HPV1618.PanelOrderHPV1618 panelOrder = (YellowstonePathology.Business.Test.HPV1618.PanelOrderHPV1618)this.m_PanelSetOrderHPV1618.PanelOrderCollection.GetLastAcceptedPanelOrder();
				YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection resultCollection = YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection.GetAllResults();
				YellowstonePathology.Business.Test.HPV1618.HPV1618Result hpv1618Result = resultCollection.GetResult(panelOrder.ResultCode);
				hpv1618Result.UnFinalizeResults(this.m_PanelSetOrderHPV1618);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrderHPV1618.IsOkToUnaccept();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection resultCollection = YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection.GetAllResults();
				YellowstonePathology.Business.Test.HPV1618.HPV1618Result hpv1618Result = resultCollection.GetResult(this.m_PanelSetOrderHPV1618.ResultCode);
				hpv1618Result.UnacceptResults(this.m_PanelSetOrderHPV1618);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618.HPV1618Result.IsOkToAccept(this.m_PanelSetOrderHPV1618);
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection resultCollection = YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection.GetAllResults();
				YellowstonePathology.Business.Test.HPV1618.HPV1618Result hpv1618Result = resultCollection.GetResult(this.m_PanelSetOrderHPV1618.ResultCode);
				hpv1618Result.AcceptResults(this.m_PanelSetOrderHPV1618, this.m_SystemIdentity);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
			YellowstonePathology.Business.Test.HPV1618.HPV1618WordDocument report = new Business.Test.HPV1618.HPV1618WordDocument();
			report.Render(this.m_PanelSetOrderHPV1618.MasterAccessionNo, this.m_PanelSetOrderHPV1618.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrderHPV1618.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkBothNegative_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618.HPV1618Result.IsOkToSetResult(this.m_PanelSetOrderHPV1618);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618.HPV1618BothNegativeResult result = new Business.Test.HPV1618.HPV1618BothNegativeResult();
				result.SetResult(this.m_PanelSetOrderHPV1618);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLink16Positive18Negative_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618.HPV1618Result.IsOkToSetResult(this.m_PanelSetOrderHPV1618);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618.HPV16PositiveHPV18NegativeResult result = new Business.Test.HPV1618.HPV16PositiveHPV18NegativeResult();
				result.SetResult(this.m_PanelSetOrderHPV1618);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLink16Negative18Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618.HPV1618Result.IsOkToSetResult(this.m_PanelSetOrderHPV1618);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618.HPV16NegativeHPV18PositiveResult result = new Business.Test.HPV1618.HPV16NegativeHPV18PositiveResult();
				result.SetResult(this.m_PanelSetOrderHPV1618);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
        }

        private void HyperLinkBothPositive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618.HPV1618Result.IsOkToSetResult(this.m_PanelSetOrderHPV1618);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618.HPV1618BothPositiveResult result = new Business.Test.HPV1618.HPV1618BothPositiveResult();
				result.SetResult(this.m_PanelSetOrderHPV1618);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
        }

        private void HyperLinkIndeterminate_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618.HPV1618Result.IsOkToSetResult(this.m_PanelSetOrderHPV1618);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618.HPV1618IndeterminateResult result = new Business.Test.HPV1618.HPV1618IndeterminateResult();
				result.SetResult(this.m_PanelSetOrderHPV1618);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLinkProvider_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_PanelSetOrderHPV1618.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            providerDistributionPath.Back += new Login.FinalizeAccession.ProviderDistributionPath.BackEventHandler(ProviderDistributionPath_Back);
            providerDistributionPath.Next += new Login.FinalizeAccession.ProviderDistributionPath.NextEventHandler(ProviderDistributionPath_Next);
            providerDistributionPath.Start();
        }

        private void ProviderDistributionPath_Next(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void ProviderDistributionPath_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }        
	}
}