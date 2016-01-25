﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class MPNStandardReflexPath : ResultPath
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		MPNStandardReflexPage m_MPNStandardReflexPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex m_PanelSetOrderMPNStandardReflex;

		public MPNStandardReflexPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderMPNStandardReflex = (YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_MPNStandardReflexPage = new MPNStandardReflexPage(this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_MPNStandardReflexPage.Back += new MPNStandardReflexPage.BackEventHandler(MPNStandardReflexPage_Back);
			this.m_MPNStandardReflexPage.Finish += new MPNStandardReflexPage.FinishEventHandler(MPNStandardReflexPage_Finish);
			this.m_MPNStandardReflexPage.OrderTest += new MPNStandardReflexPage.OrderTestEventHandler(MPNStandardReflexPage_OrderTest);
			this.m_PageNavigator.Navigate(this.m_MPNStandardReflexPage);
        }

		private void MPNStandardReflexPage_Back(object sender, EventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
			else this.Finished();
		}

		private void MPNStandardReflexPage_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }

        private void MPNStandardReflexPage_OrderTest(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs e)
		{
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
            objectTracker.RegisterObject(this.m_AccessionOrder);
            YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, objectTracker, this.m_SystemIdentity, this.m_PageNavigator, PageNavigationModeEnum.Inline);
            reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(e.TestOrderInfo);
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this.m_MPNStandardReflexPage);
		}
	}
}
