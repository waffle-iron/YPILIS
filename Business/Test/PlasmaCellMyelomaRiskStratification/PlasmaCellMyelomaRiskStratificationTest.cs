﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification
{
	public class PlasmaCellMyelomaRiskStratificationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public PlasmaCellMyelomaRiskStratificationTest()
		{
			this.m_PanelSetId = 173;
			this.m_PanelSetName = "Plasma Cell Myeloma Risk Stratification";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;


			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationTestOrder).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials and send to Neogenomics.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskRefernceLabSendout(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}