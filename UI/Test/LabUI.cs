﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.UI.Test
{
	public class LabUI : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_CurrentClientOrder;
		private YellowstonePathology.Business.Test.SearchEngine m_SearchEngine;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		YellowstonePathology.Business.Search.PathologistSearch m_PathologistSearch;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		YellowstonePathology.Business.User.SystemUserCollection m_MedTechUsers;
		YellowstonePathology.Business.User.SystemUserCollection m_LogUsers;

		YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection m_PanelSetCaseTypeCollection;

		YellowstonePathology.Business.FileList m_DigeneImportFileList;
		YellowstonePathology.Business.BatchTypeList m_BatchTypeList;

		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;				

		private bool m_HasDataError;

		YellowstonePathology.Business.Domain.XElementFromSql m_AcknowledgeOrders;
		private string m_PanelOrderIds;
		YellowstonePathology.Business.Common.FieldEnabler m_FieldEnabler;
		YellowstonePathology.Business.Domain.Lock m_Lock;
		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public LabUI(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{            
            this.m_SystemIdentity = systemIdentity;
			this.m_Lock = new YellowstonePathology.Business.Domain.Lock(this.m_SystemIdentity);
			
			this.m_SearchEngine = new Business.Test.SearchEngine();
			this.m_MedTechUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.MedTech, true);
			this.m_LogUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Log, true);

			this.m_BatchTypeList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetBatchTypeList();
			
			this.m_DigeneImportFileList = new YellowstonePathology.Business.FileList();
            			
			this.m_AcknowledgeOrders = new YellowstonePathology.Business.Domain.XElementFromSql();
			this.m_PanelOrderIds = string.Empty;
			this.m_HasDataError = false;

			this.m_PathologistSearch = new YellowstonePathology.Business.Search.PathologistSearch(this.m_SystemIdentity);

			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);			

			this.m_FieldEnabler = new YellowstonePathology.Business.Common.FieldEnabler();

			this.m_PanelSetCaseTypeCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPanelSetCaseTypeCollection();            
		}                        

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection ClientOrderCollection
        {
            get { return this.m_ClientOrderCollection; }
            set
            {
                this.m_ClientOrderCollection = value;
                this.NotifyPropertyChanged("ClientOrderCollection");
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder CurrentClientOrder
        {
            get { return this.m_CurrentClientOrder; }
            set
            {
                this.m_CurrentClientOrder = value;
                this.NotifyPropertyChanged("CurrentClientOrder");
            }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }			
		}

		public YellowstonePathology.Business.Domain.Lock Lock
		{
			get { return this.m_Lock; }
			set { this.m_Lock = value; }
		}

		public YellowstonePathology.Business.Common.FieldEnabler FieldEnabler
		{
			get { return this.m_FieldEnabler; }
			set { this.m_FieldEnabler = value; }
		}

		public void ClearLock()
		{
			this.Lock.ReleaseLock();
		}			

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public void SetBatchTestOrderResultsAsNormal()
        {
			foreach (YellowstonePathology.Business.Search.ReportSearchItem item in this.CaseList)
			{
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(item.ReportNo);
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				objectTracker.RegisterObject(accessionOrder);
				YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);

                YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();

				foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
				{
					panelOrder.SetResultsAsNormal(this.AccessionOrder, ruleExecutionStatus);
				}
				objectTracker.SubmitChanges(accessionOrder);
            }            
        }

		public YellowstonePathology.Business.Search.ReportSearchList CaseList
        {
			get { return this.m_SearchEngine.ReportSearchList; }
        }

		public YellowstonePathology.Business.User.SystemUserCollection MedTechUsers
        {
            get { return this.m_MedTechUsers; }
        }

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUsers; }
        }

		public YellowstonePathology.Business.User.SystemUserCollection LogUsers
		{
			get { return this.m_LogUsers; }
		}

		public YellowstonePathology.Business.BatchTypeList BatchTypeList
		{
			get { return this.m_BatchTypeList; }
		}

		public YellowstonePathology.Business.Panel.Model.PanelOrderBatchList BatchList
		{
			get { return this.m_SearchEngine.PanelOrderBatchList; }
		}

        public YellowstonePathology.Business.AutomatedOrderList AutomatedOrderList
		{
			get { return this.m_SearchEngine.AutomatedOrderList; }
		}

		public YellowstonePathology.Business.FileList DigeneImportFileList
		{
			get { return m_DigeneImportFileList; }
		}

		public YellowstonePathology.Business.Domain.XElementFromSql AcknowledgeOrders
		{
			get { return this.m_AcknowledgeOrders; }
		}
       
		public void GetAccessionOrder(string reportNo)
		{
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportNo);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);

			this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.AccessionOrder, reportNo);
			this.m_Lock.SetLockable(this.AccessionOrder);						
			this.RunWorkspaceEnableRules();
			this.NotifyPropertyChanged("");
		}

		public void AddPanelOrderBatch(YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch)
		{
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			objectTracker.RegisterRootInsert(panelOrderBatch);
			objectTracker.SubmitChanges(panelOrderBatch);
			this.FillBatchList();
		}

		public YellowstonePathology.Business.Panel.Model.PanelOrderBatch AddPanelOrderBatch(YellowstonePathology.Business.BatchTypeListItem batchTypeListItem)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch = new Business.Panel.Model.PanelOrderBatch(objectId);
			panelOrderBatch.BatchTypeId = batchTypeListItem.BatchTypeId;
			panelOrderBatch.Description = batchTypeListItem.BatchTypeDescription;
			panelOrderBatch.BatchDate = DateTime.Today;
			panelOrderBatch.RunDate = DateTime.Today;
			this.AddPanelOrderBatch(panelOrderBatch);

			YellowstonePathology.Business.Panel.Model.PanelOrderBatch result = null;
			foreach (YellowstonePathology.Business.Panel.Model.PanelOrderBatch batch in this.m_SearchEngine.PanelOrderBatchList)
			{
				if (batch.BatchDate == DateTime.Today && batch.RunDate == DateTime.Today && batch.PanelOrderBatchId > 0)
				{
					result = batch;
					break;
				}
			}
			return result;
		}

		public void RemovePanelOrderBatch(YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch)
		{
			this.m_SearchEngine.PanelOrderBatchList.Remove(panelOrderBatch);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			objectTracker.RegisterRootDelete(panelOrderBatch);
			objectTracker.SubmitChanges(panelOrderBatch);
		}

		public void PrintCurrentBatchLog(YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch, YellowstonePathology.Business.Search.ReportSearchList selectedItemList)
		{
			YellowstonePathology.Business.MolecularTesting.BatchLogReport report = new YellowstonePathology.Business.MolecularTesting.BatchLogReport();
			report.Print(selectedItemList, panelOrderBatch);
		}

		public void MoveIndeterminateHpvCasesToUnassignedBatch()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "pHpvUnBatchIndeterminate";
			cmd.CommandType = CommandType.StoredProcedure;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				cmd.ExecuteNonQuery();
			}
		}

        /*
		public void AddSpecimen()
		{
			int panelSetId = 0;
			if (this.m_PanelSetOrder != null)
			{
				panelSetId = this.m_PanelSetOrder.PanelSetId;
			}

			if (panelSetId > 0)
			{
				Business.Specimen.Model.SpecimenOrder specimenOrder = this.AccessionOrder.SpecimenOrderCollection.Add(this.AccessionOrder.MasterAccessionNo);
                specimenOrder.Accepted = false;
                specimenOrder.AliquotRequestCount = 1;
				this.Save();
				this.GetAccessionOrder(this.m_PanelSetOrder.ReportNo);
			}
		}
        */

		/*public void AcknowledgeOrder()
		{
			DateTime acknowledgementDate = DateTime.Today;
			DateTime acknowledgementTime = DateTime.Now;
			int acknowledgementId = this.m_SystemIdentity.User.UserId;

			if (this.m_PanelOrderIds.Length > 0)
			{
				List<YellowstonePathology.Business.Test.PanelOrder> panelOrders = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPanelOrdersToAcknowledge(this.m_PanelOrderIds);
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Persistence.ObjectTracker();
				foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelOrders)
				{
					objectTracker.RegisterObject(panelOrder);
					panelOrder.Acknowledged = true;
					panelOrder.AcknowledgedById = acknowledgementId;
					panelOrder.AcknowledgedDate = acknowledgementDate;
					panelOrder.AcknowledgedTime = acknowledgementTime;
					objectTracker.SubmitChanges(panelOrder);
				}

				// make the report
				YellowstonePathology.Business.Reports.LabOrderSheet labOrderSheet = new YellowstonePathology.Business.Reports.LabOrderSheet();
				labOrderSheet.CreateReport(this.m_PanelOrderIds, acknowledgementDate, acknowledgementTime);
			}

			this.m_AcknowledgeOrders = new YellowstonePathology.Business.Domain.XElementFromSql();
			this.m_PanelOrderIds = string.Empty;
			NotifyPropertyChanged("AcknowledgeOrders");                                  
		}*/		

		/*public void FillOrderToAcknowledgeList()
		{
			this.m_AcknowledgeOrders = new YellowstonePathology.Business.Domain.XElementFromSql();
			this.m_PanelOrderIds = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPanelOrderIdsToAcknowledge();
			if (string.IsNullOrEmpty(m_PanelOrderIds) == false)
			{
				this.m_AcknowledgeOrders = YellowstonePathology.Business.Gateway.XmlGateway.GetXmlOrdersToAcknowledge(this.m_PanelOrderIds);
				NotifyPropertyChanged("AcknowledgeOrders");
			}
		}*/

		public void ViewLabOrderLog(DateTime orderDate)
		{
			string rptpath = @"\\CFileServer\documents\Reports\Lab\LabOrdersLog\YEAR\MONTH\LabOrdersLog.FILEDATE.v1.xml";

			string rptName = rptpath.Replace("YEAR", orderDate.ToString("yyyy"));
			rptName = rptName.Replace("MONTH", orderDate.ToString("MMMM"));
			rptName = rptName.Replace("FILEDATE", orderDate.ToString("MM.dd.yy"));

			YellowstonePathology.Business.Reports.LabOrdersLog labOrdersLog = new YellowstonePathology.Business.Reports.LabOrdersLog();
			labOrdersLog.CreateReport(orderDate);

			string holdRptName = string.Empty;
			do
			{
				holdRptName = rptName; 
				int vLocation = rptName.IndexOf(".v");
				int originalVersion = Convert.ToInt32(rptName.Substring(vLocation + 2, 1));
				int newVersion = originalVersion + 1;
				rptName = rptName.Replace(".v" + originalVersion.ToString(), ".v" + newVersion.ToString());
			} while (File.Exists(rptName));

			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(holdRptName);
		}

		public bool HasDataError
		{
			get { return this.m_HasDataError; }
			set	{ m_HasDataError = value; }
		}

		public void RunWorkspaceEnableRules()
		{
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
			YellowstonePathology.Business.Rules.WorkspaceEnableRules workspaceEnableRules = new YellowstonePathology.Business.Rules.WorkspaceEnableRules();
			workspaceEnableRules.Execute(this.m_AccessionOrder, this.m_PanelSetOrder, this.m_FieldEnabler, this.m_Lock, executionStatus, this.m_SystemIdentity);
		}

		public string OrderedBy
		{
			get
			{
				string name = string.Empty;
				if (AccessionOrder != null && m_PanelSetOrder != null)
				{
					Nullable<int> id = m_PanelSetOrder.OrderedById;
					if (id.HasValue)
					{
						YellowstonePathology.Business.User.SystemUser orderedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(id.Value);
						name = orderedBy.UserName;
					}
				}
				return name;
			}
		}

		public YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection PanelSetCaseTypeCollection
		{
			get { return this.m_PanelSetCaseTypeCollection; }
		}             

		public void Save()
		{
			if (this.m_AccessionOrder != null)
			{
				this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
			}
		}

		public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
		}

		public YellowstonePathology.Business.Test.SearchEngine SearchEngine
		{
			get { return this.m_SearchEngine; }
		}

		public void FillCaseList()
		{
			this.m_SearchEngine.FillSearchList();
			this.NotifyPropertyChanged("CaseList");
		}

		public void FillBatchList()
		{
			this.m_SearchEngine.FillBatchList();
			this.NotifyPropertyChanged("BatchList");
		}		

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public void RefreshCaseDocumentCollection()
		{
			this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.m_PanelSetOrder.ReportNo);
			NotifyPropertyChanged("CaseDocumentCollection");
		}

		public YellowstonePathology.Business.Persistence.ObjectTracker ObjectTracker
		{
			get { return this.m_ObjectTracker; }
		}

		public YellowstonePathology.Business.Test.Model.TestOrderCollection TestOrders
		{
			get
			{
				if(this.m_AccessionOrder != null) return this.m_PanelSetOrder.GetTestOrders();
				return null;
			}
		}

		public string ResultString
		{
			get
			{
				string result = string.Empty;
				if (this.m_AccessionOrder != null) result = this.m_AccessionOrder.ToResultString(this.m_PanelSetOrder.ReportNo);
				return result;
			}
		}

		public void SetPanelSetOrder(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
			this.m_PanelSetOrder = panelSetOrder;
		}
	}
}