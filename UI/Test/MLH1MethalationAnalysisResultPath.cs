﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class MLH1MethalationAnalysisResultPath : ResultPath
	{
		MLH1MethalationAnalysisResultPage m_MLH1MethalationAnalysisResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis m_PanelSetOrderMLH1MethylationAnalysis;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public MLH1MethalationAnalysisResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) 
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

        private void ShowResultPage()
        {
			this.m_MLH1MethalationAnalysisResultPage = new MLH1MethalationAnalysisResultPage(this.m_PanelSetOrderMLH1MethylationAnalysis, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_MLH1MethalationAnalysisResultPage.Next += new MLH1MethalationAnalysisResultPage.NextEventHandler(MLH1MethalationAnalysisResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_MLH1MethalationAnalysisResultPage);
        }

		private void MLH1MethalationAnalysisResultPage_Next(object sender, EventArgs e)
        {
			if (this.ShowReflexTestPage() == false)
			{
				this.Finished();
			}
		}

		private bool ShowReflexTestPage()
		{
			bool result = false;
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSetLse = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest panelSetcccp = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(panelSetLse.PanelSetId) == true)
			{
				result = true;
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLse.PanelSetId).ReportNo;
				YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath resultPath = new YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath(reportNo,
					this.m_AccessionOrder, this.m_ObjectTracker, this.m_PageNavigator, System.Windows.Visibility.Visible);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Back += new LynchSyndromeEvaluationResultPath.BackEventHandler(ResultPath_Back);
				resultPath.Start();
			}
			else if (this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(panelSetcccp.PanelSetId) == true)
			{
				result = true;
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetcccp.PanelSetId).ReportNo;
				YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath resultPath = new YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath(reportNo,
					this.m_AccessionOrder, this.m_ObjectTracker, this.m_PageNavigator, System.Windows.Visibility.Collapsed);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Back += new ComprehensiveColonCancerProfilePath.BackEventHandler(ResultPath_Back);
				resultPath.Start();
			}
			return result;
		}

		private void ResultPath_Back(object sender, EventArgs e)
		{
			this.ShowReflexTestPage();
		}

		private void ResultPath_Finish(object sender, EventArgs e)
		{
			base.Finished();
		}
	}
}