﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CMMC
{
	public class CMMCHighRiskHpvNteView : CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public CMMCHighRiskHpvNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}		

		public override void ToXml(XElement document)
		{
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(this.m_ReportNo);            
            YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("High Risk HPV Report", document);            
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);
            
            string resultText = "Result: " + panelSetOrder.Result;
            this.AddNextNteElement(resultText, document);
            this.AddNextNteElement("Reference: Negative", document);
            this.AddBlankNteElement(document);            

            this.AddNextNteElement("Specimen: ThinPrep fluid", document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Test Information: ", document);            
            this.HandleLongString(YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.TestInformation, document);
            this.AddBlankNteElement(document);
            
            this.AddNextNteElement("References: ", document);
            this.HandleLongString(YellowstonePathology.Business.Test.HPVTWI.HPVTWIResult.References, document);
            this.AddBlankNteElement(document);

            string asrComment = "This test was performed using a US FDA approved DNA probe kit.  The FDA procedure was performed using a modified DNA extraction method for test optimization, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the responsibility for test performance.";
            this.HandleLongString(asrComment, document);            
		}        
	}
}