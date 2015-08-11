﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	public class TrichomonasTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public TrichomonasTest()
		{
			this.m_PanelSetId = 61;
			this.m_PanelSetName = "Trichomonas Vaginalis";
            this.m_Abbreviation = "TRICH";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;            
			this.m_AllowMultiplePerAccession = true;            

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder).AssemblyQualifiedName;
            string taskDescription = "Gather materials and perform testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_HasSplitCPTCode = false;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT87798(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTRCHMNAA());

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid thinPrepFluid = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid();
            this.OrderTargetTypeCollectionRestrictions.Add(thinPrepFluid);
		}
	}
}