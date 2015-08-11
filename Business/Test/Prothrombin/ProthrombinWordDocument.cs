﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Prothrombin
{
	public class ProthrombinWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			ProthrombinTestOrder testOrder = (ProthrombinTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Prothrombin.6.xml";

			this.OpenTemplate();
			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			if (this.m_PanelSetOrder.Final)
			{
				this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);
			}

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			//remove unused comment line
			if (string.IsNullOrEmpty(testOrder.Comment) == true)
			{
				this.DeleteRow("report_comment");
			}
			else
			{
				this.ReplaceText("report_comment", testOrder.Comment);
			}

			this.SetXmlNodeData("report_result", testOrder.Result);
			this.ReplaceText("result_description", testOrder.ResultDescription);
			this.ReplaceText("report_interpretation", testOrder.Interpretation);
			this.ReplaceText("report_indication", testOrder.Indication);
			this.ReplaceText("report_method", testOrder.Method);
			this.ReplaceText("test_development", testOrder.TestDevelopment);
			this.ReplaceText("report_references", testOrder.References);

			YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
			amendment.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}