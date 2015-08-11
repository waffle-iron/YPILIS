﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class MDSExtendedPanelByFishResultPath : ResultPath
	{
		MDSExtendedPanelByFishResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.PanelSetOrderMDSExtendedByFish m_PanelSetOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public MDSExtendedPanelByFishResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) 
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.PanelSetOrderMDSExtendedByFish)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

        private void ShowResultPage()
        {
			this.m_ResultPage = new MDSExtendedPanelByFishResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_ResultPage.Next += new MDSExtendedPanelByFishResultPage.NextEventHandler(ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

		private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
	}
}