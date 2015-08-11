﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class AssignScreening
	{
		string m_MasterAccessionNo;
        int m_AssignedToId;

		YellowstonePathology.Business.Test.AccessionOrder m_CytologyAccessionOrder;
		YellowstonePathology.Business.Test.PanelSetOrderCytology m_PanelSetOrderCytology;

        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		public AssignScreening()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_Rule.ActionList.Add(this.Assign);
        }        

        public void Assign()
        {
			this.m_CytologyAccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(this.m_MasterAccessionNo);
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.PanelSetOrderCytology)this.m_CytologyAccessionOrder.PanelSetOrderCollection.GetPAP();
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Persistence.ObjectTracker();
			objectTracker.RegisterObject(this.m_CytologyAccessionOrder);

			this.m_PanelSetOrderCytology.AssignedToId = this.m_AssignedToId;
			foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in this.m_PanelSetOrderCytology.PanelOrderCollection)
            {
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (cytologyPanelOrder.Accepted == false)
                    {
                        cytologyPanelOrder.AssignedToId = this.m_AssignedToId;
                    }
                }
            }

			objectTracker.SubmitChanges(this.m_CytologyAccessionOrder);
        }

		public void Execute(string masterAccessionNo, int assignToId, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_AssignedToId = assignToId;
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(executionStatus);
		}
	}
}