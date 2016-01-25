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
	public partial class HPV1618ByPCRResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder m_HPV1618ByPCRTestOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private List<string> m_IndicationList;

		public HPV1618ByPCRResultPage(YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder hpv1618ByPCRTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_HPV1618ByPCRTestOrder = hpv1618ByPCRTestOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

            this.m_IndicationList = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.GetIndicationList();
			this.m_PageHeaderText = "HPV Genotypes 16 and 18 By PCR Results For: " + this.m_AccessionOrder.PatientDisplayName;
			
			InitializeComponent();

			DataContext = this;

            Loaded += HPV1618ByPCRResultPage_Loaded;
            Unloaded += HPV1618ByPCRResultPage_Unloaded;                      
		}

        private void HPV1618ByPCRResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.RegisterObject(this.m_AccessionOrder, this);
        }

        private void HPV1618ByPCRResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.CleanUp(this);
        }

        public List<string> IndicationList
        {
            get { return this.m_IndicationList; }
        }

		public YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder PanelSetOrder
		{
			get { return this.m_HPV1618ByPCRTestOrder; }
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
				if (this.m_HPV1618ByPCRTestOrder.Final == true) result = "Unfinalize";
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
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.SubmitChanges(this.m_AccessionOrder, this);
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
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPV1618ByPCRTestOrder.IsOkToFinalize();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResultCollection resultCollection = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResultCollection.GetAllResults();
				YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResult hpv1618Result = resultCollection.GetResult(this.m_HPV1618ByPCRTestOrder.ResultCode);
				hpv1618Result.FinalizeResults(this.m_HPV1618ByPCRTestOrder, this.m_SystemIdentity);

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
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPV1618ByPCRTestOrder.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
                this.m_HPV1618ByPCRTestOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPV1618ByPCRTestOrder.IsOkToUnaccept();
			if (methodResult.Success == true)
			{
                this.m_HPV1618ByPCRTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResult.IsOkToAccept(this.m_HPV1618ByPCRTestOrder);
			if (methodResult.Success == true)
			{
                this.m_HPV1618ByPCRTestOrder.Accept(this.m_SystemIdentity.User);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}         
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
			YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRWordDocument report = new Business.Test.HPV1618ByPCR.HPV1618ByPCRWordDocument();
			report.Render(this.m_HPV1618ByPCRTestOrder.MasterAccessionNo, this.m_HPV1618ByPCRTestOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_HPV1618ByPCRTestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkBothNegative_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResult.IsOkToSetResult(this.m_HPV1618ByPCRTestOrder);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618BothNegativeResult result = new Business.Test.HPV1618ByPCR.HPV1618BothNegativeResult();
				result.SetResult(this.m_HPV1618ByPCRTestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLink16Positive18Negative_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResult.IsOkToSetResult(this.m_HPV1618ByPCRTestOrder);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618ByPCR.HPV16PositiveHPV18NegativeResult result = new Business.Test.HPV1618ByPCR.HPV16PositiveHPV18NegativeResult();
				result.SetResult(this.m_HPV1618ByPCRTestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLink16Negative18Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResult.IsOkToSetResult(this.m_HPV1618ByPCRTestOrder);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618ByPCR.HPV16NegativeHPV18PositiveResult result = new Business.Test.HPV1618ByPCR.HPV16NegativeHPV18PositiveResult();
				result.SetResult(this.m_HPV1618ByPCRTestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
        }

        private void HyperLinkBothPositive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResult.IsOkToSetResult(this.m_HPV1618ByPCRTestOrder);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618BothPositiveResult result = new Business.Test.HPV1618ByPCR.HPV1618BothPositiveResult();
				result.SetResult(this.m_HPV1618ByPCRTestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
        }

        private void HyperLinkIndeterminate_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRResult.IsOkToSetResult(this.m_HPV1618ByPCRTestOrder);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618IndeterminateResult result = new Business.Test.HPV1618ByPCR.HPV1618IndeterminateResult();
				result.SetResult(this.m_HPV1618ByPCRTestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLinkProvider_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
            //YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_HPV1618ByPCRTestOrder.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            //providerDistributionPath.Back += new Login.FinalizeAccession.ProviderDistributionPath.BackEventHandler(ProviderDistributionPath_Back);
            //providerDistributionPath.Next += new Login.FinalizeAccession.ProviderDistributionPath.NextEventHandler(ProviderDistributionPath_Next);
            //providerDistributionPath.Start(this.m_SystemIdentity);
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
