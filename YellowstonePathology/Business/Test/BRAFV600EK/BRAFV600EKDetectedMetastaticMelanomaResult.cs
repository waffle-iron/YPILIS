﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKDetectedMetastaticMelanomaResult : BRAFV600EKDetectedResult
	{		
		public BRAFV600EKDetectedMetastaticMelanomaResult()
		{
            YellowstonePathology.Business.Test.IndicationMetastaticMelanoma indication = new IndicationMetastaticMelanoma();
            this.m_Indication = indication.IndicationCode;
            this.m_IndicationComment = indication.Description;
		    this.m_Interpretation = "The BRAF gene, a member of the RAS-RAF-MEK-MAPK pathway that drives tumor growth and progression, " +
		        "has been found to play a pivotal role in melanoma oncogenesis.  Recent studies have shown that approximately 50% of melanomas harbor a point mutation in codon " +
		        "600 of the BRAF gene, resulting in the substitution of glutamic acid for valine in the encoded protein.  This alteration results in constitutive activation of " +
		        "the BRAF protein kinase, and thus, the RAS-RAF-MEK-MAPK pathway.  Testing for the BRAF V600E/K mutation in metastatic melanomas is useful for guiding therapy.\r\n\r\n" +
		        "Melanomas with the V600E/K mutation have been shown to respond to the new BRAF inhibitors.  A 107-base product indicative of a BRAF V600E/K mutation was detected " +
		        "by high resolution capillary electrophoresis.  Therefore this patient may benefit from the new BRAF inhibitors.";
		   this.m_Comment = "The patient may benefit from the use of the new BRAF inhibitors.";
           this.m_References = BRAFV600EKResult.MetastaticMelanomaReference;
		}
	}
}
