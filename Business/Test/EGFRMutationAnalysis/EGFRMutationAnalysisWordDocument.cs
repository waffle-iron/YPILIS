﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
	public class EGFRMutationAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;

			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);

			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);            

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\EGFR.5.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(egfrMutationAnalysisTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(egfrMutationAnalysisTestOrder.OrderedOn, egfrMutationAnalysisTestOrder.OrderedOnId);

            string specimenDescription = specimenOrder.Description + ", Block " + aliquotOrder.Label;
            this.ReplaceText("specimen_description", specimenDescription);            

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXMLNodeParagraphData("report_result", egfrMutationAnalysisTestOrder.Result);
            this.SetXMLNodeParagraphData("report_comment", egfrMutationAnalysisTestOrder.Comment);
            this.SetXMLNodeParagraphData("report_interpretation", egfrMutationAnalysisTestOrder.Interpretation);

            this.ReplaceText("report_indication", egfrMutationAnalysisTestOrder.Indication);
            this.ReplaceText("tumor_nuclei_percentage", egfrMutationAnalysisTestOrder.TumorNucleiPercentage);

            if (egfrMutationAnalysisTestOrder.MicrodisectionPerformed == true)
            {
                this.ReplaceText("microdisection_performed", "Yes");
            }
            else
            {
                this.ReplaceText("microdisection_performed", "No");
            }

            this.SetXMLNodeParagraphData("report_method", egfrMutationAnalysisTestOrder.Method);
            this.SetXMLNodeParagraphData("report_references", egfrMutationAnalysisTestOrder.References);
			
			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));			
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}