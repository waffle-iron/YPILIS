﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class MissingInfoLetterBody
	{
		const string StartStatement = "We are in receipt of physician/provider orders and a patient specimen on patient_name. However, we " +
			"cannot proceed with testing of this specimen because we are missing important information. Please provide information detailed below " +
			"and fax back to Yellowstone Pathology at (406)238-6361 to ensure timely processing of the specimen: ";
		const string ABNDelayStatement = "We will delay processing for two weeks, at which time, we will process and test this specimen as usual.  If Medicare " +
			"denies payment, we will bill your office for our services.";

		// do not print signature line if only ABN is missing.  This works now as ABN is first in the list.
		private bool m_NeedSignatureLine;

		public void GetLetterBody(StringBuilder result, string patientName, bool missingABN, bool missingCytologyDXCode, bool missingPatientDemographics,
			bool missingCytologyNGCTDXCode, bool missingTestType, bool missingOrderingPhysician, bool missingCollectionDate)
		{
			result.AppendLine(StartStatement);
			result.Replace("patient_name", patientName);
			result.AppendLine();
			if (missingABN) this.MissingABN(result);
			if (missingCytologyDXCode) this.MissingCytologyDXCode(result);
			if (missingPatientDemographics) this.MissingPatientDemographics(result);
			if (missingCytologyNGCTDXCode) this.MissingCytologyNGCTDXCode(result);
			if (missingTestType) this.MissingTestType(result);
			if (missingOrderingPhysician || missingCollectionDate) this.RequisitionInformation(result);
			if (missingOrderingPhysician) this.MissingOrderingPhysician(result);
			if (missingCollectionDate) this.MissingCollectionDate(result);
			if(this.m_NeedSignatureLine) this.AddSignatureLine(result);
		}

		public void MissingABN(StringBuilder result)
		{
			result.AppendLine("Missing ABN: Although this patient is a Medicare beneficiary, the specimen was not accompanied by the required " +
				"Advance Beneficiary Notice (ABN).");
			result.AppendLine();
			result.AppendLine(ABNDelayStatement);
			result.AppendLine();
			this.m_NeedSignatureLine = false;
		}

		public void MissingCytologyDXCode(StringBuilder result)
		{
			result.AppendLine("Missing Diagnosis Code:");
			result.AppendLine();
			result.AppendLine("____________________  Routine Screening Pap - V76.2");
			result.AppendLine();
            result.AppendLine("____________________  Vag Screen, no cervix - V76.47");
            result.AppendLine();
			result.AppendLine("____________________  High Risk Screening Pap - V15.89");
			result.AppendLine();
			result.AppendLine("____________________  Diagnostic Pap (Must Supply Code)");
			result.AppendLine();
			result.AppendLine();
			this.m_NeedSignatureLine = true;
		}

		public void MissingPatientDemographics(StringBuilder result)
		{
			result.AppendLine("Missing Patient Demographics");			
			this.m_NeedSignatureLine = false;
		}

		public void MissingCytologyNGCTDXCode(StringBuilder result)
		{
			result.AppendLine("Missing NG/CT Diagnosis Code: (Please note PAP codes and office visit codes are NOT valid for NG/CT)");
			result.AppendLine();
			result.AppendLine("____________________  NG/CT Diagnosis Code");
            result.AppendLine();
            result.AppendLine("Here is a list of the most common, but not limited to,  DX used for screening NG/CT");
            result.AppendLine();
            result.AppendLine("Pap smear or office visit screening V codes are not valid for an STD/NGCT testing");
            result.AppendLine();
            result.AppendLine("It might be useful to keep around:");            
            result.AppendLine("V74.5 Venereal disease");
            result.AppendLine("V73.88 Other specified chlamydial deseases");
            result.AppendLine("V73.89 Other specified viral diseases");
            result.AppendLine("V73.98 Unspecified chlamydial diseases");
            result.AppendLine("V73.99 Unspecified viral disease");
            result.AppendLine("V01.6 Venereal diseases-persons with potential health hazards related to communicable diseases");
			result.AppendLine();
			this.m_NeedSignatureLine = true;
		}

		public void MissingTestType(StringBuilder result)
		{
			result.Append("Missing Test Type:");
			result.AppendLine();
			result.AppendLine("____________________ ThinPrep Pap Test");
			result.AppendLine();
			result.AppendLine("____________________ Reflex HPV DNA High Risk Probe");
			result.AppendLine();
			result.AppendLine("____________________ Routine HPV DNA High Risk Probe");
			result.AppendLine();
			result.AppendLine("____________________ NG/CT Screen By PCR (From ThinPrep Vial)");
			result.AppendLine();
			this.m_NeedSignatureLine = true;
		}

		public void MissingOrderingPhysician(StringBuilder result)
		{
			result.AppendLine("____________________ Ordering Physician");
			result.AppendLine();
			this.m_NeedSignatureLine = true;
		}

		public void MissingCollectionDate(StringBuilder result)
		{
			result.AppendLine("____________________ Collection Date");
			result.AppendLine();
			this.m_NeedSignatureLine = true;
		}

		public void RequisitionInformation(StringBuilder result)
		{
			result.AppendLine("Missing Requisition Information:");
			result.AppendLine();
			this.m_NeedSignatureLine = true;
		}

		private void AddSignatureLine(StringBuilder result)
		{
			result.AppendLine();
			result.AppendLine();
			result.AppendLine("____________________________________");
			result.AppendLine("Signature of person completing form.");
		}
	}
}
