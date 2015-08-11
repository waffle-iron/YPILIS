﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class DoNotPublishReport : CaseReportV2
    {
		public DoNotPublishReport()
		{
			this.m_NativeDocumentFormat = NativeDocumentFormatEnum.XPS;
		}

        public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }

		public override Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }

		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
        }

        public override void Publish()
        {
        }
	}
}