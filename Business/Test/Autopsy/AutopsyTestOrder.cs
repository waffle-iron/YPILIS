﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Autopsy
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class AutopsyTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		public AutopsyTestOrder()
		{

		}

		public AutopsyTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
		{
		}

		public override void Finalize(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus, Business.User.SystemIdentity systemIdentity)
        {
            this.m_Final = true;
            this.m_FinalDate = DateTime.Today;
            this.m_FinalTime = DateTime.Now;
            this.m_FinaledById = systemIdentity.User.UserId;
            this.m_Signature = systemIdentity.User.Signature;
        }
	}
}