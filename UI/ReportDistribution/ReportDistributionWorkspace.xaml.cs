using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;

namespace YellowstonePathology.UI.ReportDistribution
{    
    public partial class ReportDistributionWorkspace : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private YellowstonePathology.Business.Mongo.LocalServer m_LocalServer;

        private System.Windows.Threading.DispatcherTimer m_Timer;
        private TimeSpan m_TimerIntervalFast = new TimeSpan(0, 0, 10);
        private TimeSpan m_TimerIntervalSlow = new TimeSpan(1, 0, 0);

        private DateTime m_PublishClock;
        private DateTime m_ScheduleClock;
        private YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntryCollection m_ReportDistributionLogEntryCollection;
        //private YellowstonePathology.Business.DocumentCollectionTracker m_DocumentCollectionTracker;
        private bool m_TimerRunning;        

        public ReportDistributionWorkspace()
        {
            //this.m_LocalServer = new Business.Mongo.LocalServer(Business.Mongo.LocalServer.LocalLISDatabaseName);

            this.m_PublishClock = DateTime.Now;
            this.m_ScheduleClock = DateTime.Now;

            this.m_ReportDistributionLogEntryCollection = new Business.ReportDistribution.Model.ReportDistributionLogEntryCollection();
            //this.m_ReportDistributionLogEntryCollection = YellowstonePathology.Business.Mongo.Gateway.GetReportDistributionLogEntryCollectionGTETime(DateTime.UtcNow.AddHours(-8));
            //this.m_DocumentCollectionTracker = new Business.DocumentCollectionTracker(this.m_ReportDistributionLogEntryCollection, this.m_LocalServer);
            
            this.m_Timer = new System.Windows.Threading.DispatcherTimer();
            this.m_Timer.Interval = this.m_TimerIntervalFast;            
            this.m_Timer.Tick += new EventHandler(Timer_Tick);

            this.m_TimerRunning = true;

            InitializeComponent();

            this.DataContext = this;
            this.m_Timer.Start();

            this.SetStatus("Idle");
        }

        public bool TimmerRunning
        {
            get { return this.m_TimerRunning; }
            set { this.m_TimerRunning = value; }
        }

        public YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntryCollection ReportDistributionLogEntryCollection
        {
            get { return this.m_ReportDistributionLogEntryCollection; }
        }

        private void SetStatus(string status)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        this.TextBlockStatus.Text = status;
                    }));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.SetStatus("Processing");

            DateTime dailyStartTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 05:00");
            DateTime dailyEndTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 20:00");

            if (DateTime.Now >= dailyStartTime && DateTime.Now <= dailyEndTime)
            {                
                this.m_Timer.Stop();

                this.HandleUnscheduledAmendments();
                this.HandleUnsetDistribution();
                this.HandleUnscheduledPublish();
                this.HandleUnscheduledDistribution();                
                this.PublishNext();

                this.SetStatus("Idle Office Hours. Next process starts: " + DateTime.Now.Add(this.m_TimerIntervalFast));
                this.m_Timer.Interval = this.m_TimerIntervalFast;
                this.m_Timer.Start();                
            }
            else
            {                
                this.m_Timer.Interval = this.m_TimerIntervalSlow;
                this.SetStatus("Idle After Hours");
            }

            //this.m_DocumentCollectionTracker.SubmitChanges();
            this.m_Timer.Start();            
        }

        private void HandleUnscheduledAmendments()
        {
			List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> testOrderReportDistributionList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetUnscheduledAmendments();

            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionList)
            {
                this.ScheduleDistribution(testOrderReportDistribution);
            }    
        }

        private void HandleUnsetDistribution()
        {
			List<YellowstonePathology.Business.Test.PanelSetOrderView> panelSetOrderViewList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetUnsetDistributions();
            foreach (YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView in panelSetOrderViewList)
            {
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(panelSetOrderView.ReportNo);
				YellowstonePathology.Business.Persistence.ObjectTracker ot = new YellowstonePathology.Business.Persistence.ObjectTracker();
                ot.RegisterObject(accessionOrder);

                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetOrderView.ReportNo);
                YellowstonePathology.Business.Client.PhysicianClientDistributionCollection physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(accessionOrder.PhysicianId, accessionOrder.ClientId);

                if (physicianClientDistributionCollection.Count != 0)
                {
                    physicianClientDistributionCollection.SetDistribution(panelSetOrder, accessionOrder);
                    ot.SubmitChanges(accessionOrder);

                    this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Handle Unset Distribution", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo,
                        accessionOrder.PhysicianName, accessionOrder.ClientName, "Distribution Set");
                }
                else
                {
                    this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Handle Unset Distribution", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo,
                        accessionOrder.PhysicianName, accessionOrder.ClientName, "No Distribution Defined");

                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Support@ypii.com", "Support@ypii.com", System.Windows.Forms.SystemInformation.UserName, "No Distribution Defined: " + panelSetOrder.ReportNo);
                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");
                    client.Credentials = new System.Net.NetworkCredential("Administrator", "p0046e");
                    client.Send(message);
                }
            }            
        }

        private void HandleUnscheduledDistribution()
        {
			List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> testOrderReportDistributionList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetUnscheduledDistribution();            

            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionList)
            {
                this.ScheduleDistribution(testOrderReportDistribution);
            }    
        }

        private void ScheduleDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();           

            objectTracker.RegisterObject(testOrderReportDistribution);
            testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(15);
            objectTracker.SubmitChanges(testOrderReportDistribution);

			YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCaseToSchedule(testOrderReportDistribution.ReportNo);
            objectTracker.RegisterObject(panelSetOrderView);

            panelSetOrderView.Published = false;
            panelSetOrderView.ScheduledPublishTime = DateTime.Now.AddMinutes(15);
            objectTracker.SubmitChanges(panelSetOrderView);

            this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Schedule Distribution", testOrderReportDistribution.DistributionType, panelSetOrderView.ReportNo, 
                panelSetOrderView.MasterAccessionNo, testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, "Distribution Scheduled");
        }

        private void HandleUnscheduledPublish()
        {
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();

			List<YellowstonePathology.Business.Test.PanelSetOrderView> panelSetOrderViewList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCasesToSchedule();

            foreach (YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView in panelSetOrderViewList)
            {                                
                objectTracker.RegisterObject(panelSetOrderView);

                DateTime scheduleTime = DateTime.Now;
                if (panelSetOrderView.FinalTime > DateTime.Now.AddMinutes(-15))
                {
                    scheduleTime = panelSetOrderView.FinalTime.Value.AddMinutes(15);
                }

                panelSetOrderView.ScheduledPublishTime = scheduleTime;
                objectTracker.SubmitChanges(panelSetOrderView);
                
                this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Handle Unschedule Publish", null, panelSetOrderView.ReportNo, 
                    panelSetOrderView.MasterAccessionNo, null, null, "PanelSet Publish Sceduled");

                if (panelSetOrderView.Distribute == true)
                {
					List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> testOrderReportDistributionList = 
						YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetUnscheduledDistribution(panelSetOrderView.MasterAccessionNo);
                    foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionList)
                    {
                        this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Handle Unschedule Publish", testOrderReportDistribution.DistributionType, panelSetOrderView.ReportNo,
                            panelSetOrderView.MasterAccessionNo, testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, "TestOrderReportDistribution Sceduled");

                        objectTracker.RegisterObject(testOrderReportDistribution);
                        testOrderReportDistribution.ScheduledDistributionTime = scheduleTime;
                        objectTracker.SubmitChanges(testOrderReportDistribution);                        
                    }                 
                }                
            }
        }

        private bool TryPublish(YellowstonePathology.Business.Interface.ICaseDocument caseDocument, 
            YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            bool result = true;

            try
            {
                caseDocument.Render(panelSetOrderView.MasterAccessionNo, panelSetOrderView.ReportNo, Business.Document.ReportSaveModeEnum.Normal);
                caseDocument.Publish();

                this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Publish Next", null, panelSetOrderView.ReportNo, panelSetOrderView.MasterAccessionNo,
                null, null, "PanelSetOrder Published");                              
            }
            catch (Exception publishException)
            {
                this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Publish Next", null, panelSetOrderView.ReportNo, panelSetOrderView.MasterAccessionNo,
                null, null, publishException.Message);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Support@ypii.com", "Support@ypii.com", System.Windows.Forms.SystemInformation.UserName, publishException.Message);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");
                client.Credentials = new System.Net.NetworkCredential("Administrator", "p0046e");
                client.Send(message);

                this.DelayPublishAndDistribution(15, publishException.Message, panelSetOrderView, objectTracker);

                result = false;
            }

            return result;
        }

        public bool TryDelete(YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView, YellowstonePathology.Business.Interface.ICaseDocument caseDocument,
			YellowstonePathology.Business.OrderIdParser orderIdParser, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            bool result = true;
            
            YellowstonePathology.Business.Rules.MethodResult methodResult = caseDocument.DeleteCaseFiles(orderIdParser);

            if (methodResult.Success == false)
            {
                this.DelayPublishAndDistribution(15, "Not able to delete files prior to publishing.", panelSetOrderView, objectTracker);

                this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Publish Next", null, panelSetOrderView.ReportNo, panelSetOrderView.MasterAccessionNo,
                                null, null, "Not able to delete files prior to publishing.");

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Support@ypii.com", "Support@ypii.com", System.Windows.Forms.SystemInformation.UserName, "Not able to delete files prior to publishing: " + panelSetOrderView.ReportNo);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");
                client.Credentials = new System.Net.NetworkCredential("Administrator", "p0046e");
                client.Send(message);

                result = false;
            }

            return result;
        }

        private void DelayPublishAndDistribution(int delayMinutes, string delayMessage, YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            panelSetOrderView.Published = false;
            panelSetOrderView.TimeLastPublished = null;
            panelSetOrderView.ScheduledPublishTime = DateTime.Now.AddMinutes(delayMinutes);
            objectTracker.SubmitChanges(panelSetOrderView);

            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> testOrderReportDistributionList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetScheduledDistribution(panelSetOrderView.ReportNo);
            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionList)
            {
                objectTracker.RegisterObject(testOrderReportDistribution);
                testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(delayMinutes);
                testOrderReportDistribution.Rescheduled = true;
                testOrderReportDistribution.RescheduledMessage = delayMessage;
                objectTracker.SubmitChanges(testOrderReportDistribution);
            }
        }

        private void PublishNext()
        {            
			List<YellowstonePathology.Business.Test.PanelSetOrderView> panelSetOrderViewList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNextCasesToPublish();            

            int maxProcessCount = 2;
            int processCount = 0;

            foreach (YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView in panelSetOrderViewList)
            {
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(panelSetOrderView);

                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrderView.PanelSetId);

                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(panelSetOrderView.PanelSetId);
				YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(panelSetOrderView.ReportNo);                

                if (this.TryDelete(panelSetOrderView, caseDocument, orderIdParser, objectTracker) == true)
                {
                    if (this.TryPublish(caseDocument, panelSetOrderView, objectTracker) == true)
                    {
                        if (panelSetOrderView.Distribute == true)
                        {
                            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> testOrderReportDistributionList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetScheduledDistribution(panelSetOrderView.ReportNo);
                            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionList)
                            {
                                objectTracker.RegisterObject(testOrderReportDistribution);
                                YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = this.Distribute(testOrderReportDistribution);

                                if (distributionResult.IsComplete == true)
                                {
                                    testOrderReportDistribution.TimeOfLastDistribution = DateTime.Now;
                                    testOrderReportDistribution.ScheduledDistributionTime = null;
                                    testOrderReportDistribution.Distributed = true;
                                    objectTracker.SubmitChanges(testOrderReportDistribution);

                                    string testOrderReportDistributionLogId = Guid.NewGuid().ToString();
                                    string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                                    YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog testOrderReportDistributionLog = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog(testOrderReportDistributionLogId, objectId);
                                    objectTracker.RegisterRootInsert(testOrderReportDistributionLog);
                                    testOrderReportDistributionLog.FromTestOrderReportDistribution(testOrderReportDistribution);
                                    testOrderReportDistributionLog.TimeDistributed = DateTime.Now;
                                    objectTracker.SubmitChanges(testOrderReportDistributionLog);

                                    this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Publish Next", testOrderReportDistribution.DistributionType, panelSetOrderView.ReportNo, panelSetOrderView.MasterAccessionNo,
                                        testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, "TestOrderReportDistribution Distributed");
                                }
                                else
                                {
                                    testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(30);
                                    testOrderReportDistribution.Rescheduled = true;
                                    testOrderReportDistribution.RescheduledMessage = distributionResult.Message;
                                    objectTracker.SubmitChanges(testOrderReportDistribution);

                                    this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Publish Next", testOrderReportDistribution.DistributionType, panelSetOrderView.ReportNo, panelSetOrderView.MasterAccessionNo,
                                        testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, distributionResult.Message);

                                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Sid.Harder@ypii.com", "Sid.Harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, distributionResult.Message);
                                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");
                                    client.Credentials = new System.Net.NetworkCredential("Administrator", "p0046e");
                                    client.Send(message);
                                }
                            }
                        }

                        this.HandleNotificationEmail(panelSetOrderView);

                        panelSetOrderView.Published = true;
                        panelSetOrderView.TimeLastPublished = DateTime.Now;
                        panelSetOrderView.ScheduledPublishTime = null;                        
                        objectTracker.SubmitChanges(panelSetOrderView);                        
                    }
                }                             

                processCount += 1;
                if (processCount == maxProcessCount) break;
            }                        
        }

        private void HandleNotificationEmail(YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView)
        {
            YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByMasterAccessionNo(panelSetOrderView.MasterAccessionNo);
            if (physician.SendPublishNotifications == true)
            {
                if (panelSetOrderView.Distribute == true)
                {
                    string subject = "You have a result ready for review: " + panelSetOrderView.PanelSetName;
                    string body = "You have a patient report ready. You can review the report by using YPI Connect.  If you don't have access to YPI Connect please call us at (406)238-6360.";
                    
                    System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("Results@YPII.com");
                    System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(physician.PublishNotificationEmailAddress);
                    System.Net.Mail.MailAddress bcc = new System.Net.Mail.MailAddress("Results@YPII.com");

                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
                    message.Subject = subject;
                    message.Body = body;
                    message.Bcc.Add(bcc);
                    
                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");
                    client.Credentials = new System.Net.NetworkCredential("Results", "p0046ep0046e");
                    client.Send(message);

                    panelSetOrderView.TimeOfLastPublishNotification = DateTime.Now;
                    panelSetOrderView.PublishNotificationSent = true;
                }
            }
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult Distribute(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = null;

            switch (testOrderReportDistribution.DistributionType)
            {
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX:
                    result = this.HandleFaxDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    result = this.HandleEPICDistribution(testOrderReportDistribution);
                    break;                
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    result = this.HandleECWDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    result = this.HandleATHENADistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    result = this.HandleMeditechDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE:
                    result = this.HandleWebServiceDistribution(testOrderReportDistribution);
                    break;                
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.PRINT:
                    result = this.HandlePrintDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH:
                    result = this.HandleMTDOHDistribution(testOrderReportDistribution);
                    break;            
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH:
                    result = this.HandleWYDOHDistribution(testOrderReportDistribution);
                    break;
                default:
                    result = this.HandleNoImplemented(testOrderReportDistribution);
                    break;
            }

            return result;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleWYDOHDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = true;            
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleMTDOHDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            YellowstonePathology.Business.HL7View.CDC.MTDohResultView mtDohResultView = new Business.HL7View.CDC.MTDohResultView(testOrderReportDistribution.ReportNo);
            mtDohResultView.CanSend(result);
            mtDohResultView.Send(result);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = result.Success;
            distributionResult.Message = result.Message;
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandlePrintDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.PrintDistribution printDistribution = new Business.ReportDistribution.Model.PrintDistribution();
            return printDistribution.Distribute(testOrderReportDistribution.ReportNo);            
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleWebServiceDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = new Business.ReportDistribution.Model.DistributionResult();
            result.IsComplete = true;
            return result;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleNoImplemented(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = false;
            distributionResult.Message = "Not Implemented";
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleATHENADistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            YellowstonePathology.Business.HL7View.CMMC.CMMCResultView cmmcResultView = new Business.HL7View.CMMC.CMMCResultView(testOrderReportDistribution.ReportNo);
            YellowstonePathology.Business.Rules.MethodResult MmthodResult = new Business.Rules.MethodResult();
            cmmcResultView.Send(methodResult);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = methodResult.Success;
            distributionResult.Message = methodResult.Message;
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleMeditechDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.MeditechDistribution meditechDistribution = new Business.ReportDistribution.Model.MeditechDistribution();            
            return meditechDistribution.Distribute(testOrderReportDistribution.ReportNo);                        
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleECWDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {            
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();            
            YellowstonePathology.Business.HL7View.ECW.ECWResultView resultView = new Business.HL7View.ECW.ECWResultView(testOrderReportDistribution.ReportNo, false);
            resultView.Send(methodResult);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = methodResult.Success;
            distributionResult.Message = methodResult.Message;
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleFaxDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(testOrderReportDistribution.ReportNo);
            string tifCaseFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
            return YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(testOrderReportDistribution.FaxNumber, testOrderReportDistribution.LongDistance, testOrderReportDistribution.ReportNo, tifCaseFileName);
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleEPICDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(testOrderReportDistribution.ReportNo, testOrderReportDistribution.ClientId, false);
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = new Business.ReportDistribution.Model.DistributionResult();
            result.IsComplete = methodResult.Success;
            result.Message = methodResult.Message;
            return result;
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void CheckBoxTimeRunning_Checked(object sender, RoutedEventArgs e)
        {
            this.m_Timer.Start();
        }

        private void CheckBoxTimerRunning_Unchecked(object sender, RoutedEventArgs e)
        {
            this.m_Timer.Stop();
        }     
    }
}