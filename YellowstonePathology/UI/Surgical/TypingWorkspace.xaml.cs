﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;

namespace YellowstonePathology.UI.Surgical
{
	public partial class TypingWorkspace : UserControl
    {        
        public CommandBinding CommandBindingClose;        
		public CommandBinding CommandBindingShowCaseDocument;		
		public CommandBinding CommandBindingShowOrderForm;
		public CommandBinding CommandBindingShowAmendmentDialog;

        private YellowstonePathology.Business.Typing.TypingUIV2 m_TypingUI;        
        private YellowstonePathology.UI.AmendmentControlV2 m_AmendmentControl;
        private YellowstonePathology.UI.TypingShortcutUserControl m_TypingShortcutUserControl;
        private UI.DocumentWorkspace m_DocumentViewer;        

		private YellowstonePathology.UI.Common.TreeViewWorkspace m_TreeviewWorkspace;

        private YellowstonePathology.Business.DictationList m_LocalDictationList;
        private YellowstonePathology.Business.DictationList m_ServerDictationList;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private PageNavigationWindow m_SecondMonitorWindow;
        private System.Windows.Controls.TabItem m_Writer;

        public TypingWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, PageNavigationWindow secondMonitorWindow, System.Windows.Controls.TabItem writer)
		{
            this.m_Writer = writer;
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_SecondMonitorWindow = secondMonitorWindow;
            if (secondMonitorWindow != null)
            {
                this.m_SecondMonitorWindow.WindowState = WindowState.Maximized;
            }

            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;

            this.CommandBindingClose = new CommandBinding(MainWindow.ApplicationClosingCommand, HandleAppClosing);            
			this.CommandBindingShowCaseDocument = new CommandBinding(MainWindow.ShowCaseDocumentCommand, ShowCaseDocument, ItemIsPresent);			
			this.CommandBindingShowOrderForm = new CommandBinding(MainWindow.ShowOrderFormCommand, this.ShowOrderForm, ItemIsSelected);
			this.CommandBindingShowAmendmentDialog = new CommandBinding(MainWindow.ShowAmendmentDialogCommand, this.ShowAmendmentDialog, ItemIsSelected);			

            this.CommandBindings.Add(this.CommandBindingClose);            
            this.CommandBindings.Add(this.CommandBindingShowCaseDocument);			
			this.CommandBindings.Add(this.CommandBindingShowOrderForm);
			this.CommandBindings.Add(this.CommandBindingShowAmendmentDialog);                        	
            
			this.m_TypingUI = new YellowstonePathology.Business.Typing.TypingUIV2(this.m_Writer);									
			this.m_AmendmentControl = new AmendmentControlV2(this.m_SystemIdentity, string.Empty, this.m_TypingUI.AccessionOrder);			
            this.m_DocumentViewer = new DocumentWorkspace();            

            this.m_LocalDictationList = new YellowstonePathology.Business.DictationList(Business.DictationLocationEnum.Local);            
            this.m_ServerDictationList = new YellowstonePathology.Business.DictationList(Business.DictationLocationEnum.Server);            

            InitializeComponent();

            this.m_TypingShortcutUserControl = new TypingShortcutUserControl(this.m_SystemIdentity, this.m_Writer);
            this.TabItemTypingShortCuts.Content = this.m_TypingShortcutUserControl;

            this.DataContext = this.m_TypingUI;			            
            this.ContentControlDocument.Content = this.m_DocumentViewer;            

            this.ListViewLocalDictation.ItemsSource = this.m_LocalDictationList;
            this.ListViewServerDictation.ItemsSource = this.m_ServerDictationList;
         
            this.Unloaded += new RoutedEventHandler(TypingWorkspace_Unloaded);
		}

        private void TypingWorkspace_Loaded(object sender, RoutedEventArgs e)
		{
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += new MainWindowCommandButtonHandler.StartProviderDistributionPathEventHandler(MainWindowCommandButtonHandler_StartProviderDistributionPath);
            this.m_MainWindowCommandButtonHandler.Save += new MainWindowCommandButtonHandler.SaveEventHandler(MainWindowCommandButtonHandler_Save);
            this.m_MainWindowCommandButtonHandler.Refresh += new MainWindowCommandButtonHandler.RefreshEventHandler(MainWindowCommandButtonHandler_Refresh);
            this.m_MainWindowCommandButtonHandler.RemoveTab += new MainWindowCommandButtonHandler.RemoveTabEventHandler(MainWindowCommandButtonHandler_RemoveTab);
            this.m_MainWindowCommandButtonHandler.ShowMessagingDialog += new MainWindowCommandButtonHandler.ShowMessagingDialogEventHandler(MainWindowCommandButtonHandler_ShowMessagingDialog);

            if (this.m_TypingUI.SurgicalTestOrder != null) this.m_TypingUI.RunWorkspaceEnableRules();

            UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Add(this.Save);
            UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Add(this.m_TypingUI.RunWorkspaceEnableRules);
        }                

        private void MainWindowCommandButtonHandler_ShowMessagingDialog(object sender, EventArgs e)
        {
            if (this.m_TypingUI.AccessionOrder != null)
            {
                AppMessaging.MessagingPath.Instance.Start(this.m_TypingUI.AccessionOrder);
            }
        }                

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
        }

        private void MainWindowCommandButtonHandler_Refresh(object sender, EventArgs e)
        {

        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            this.Save();
        }

        private void Save()
        {
            if (this.m_TypingUI.AccessionOrder != null)
            {
                MainWindow.MoveKeyboardFocusNextThenBack();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_TypingUI.AccessionOrder, this.m_Writer);
                if (this.m_TypingUI.AccessionOrder.IsLockAquiredByMe == false)
                {
                    this.m_TypingUI.RunWorkspaceEnableRules();
                    this.m_TypingUI.NotifyPropertyChanged(string.Empty);
                }
            }
        }

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.m_TypingUI.AccessionOrder != null)
            {                
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath =
					new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath(this.m_TypingUI.SurgicalTestOrder.ReportNo, this.m_TypingUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

        private void TypingWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.MoveKeyboardFocusNextThenBack();
            this.Save(false);
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.Refresh -= MainWindowCommandButtonHandler_Refresh;
            this.m_MainWindowCommandButtonHandler.RemoveTab -= MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowMessagingDialog -= MainWindowCommandButtonHandler_ShowMessagingDialog;

            UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Remove(this.Save);
            UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Remove(this.m_TypingUI.RunWorkspaceEnableRules);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        public void ButtonRefresh_Click(object sender, RoutedEventArgs args)
        {
            this.HandleNewCaseSearch(this.TextBoxReportNoSearch.Text);
        }
        
        private void TabItemSpecimen_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TabControlRightMain.SelectedIndex = 1;
        }		

        public void HandleAppClosing(object target, ExecutedRoutedEventArgs args)
        {
            if (this.m_TypingUI.SurgicalTestOrder != null)
            {
                YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LastReportNo = this.m_TypingUI.SurgicalTestOrder.ReportNo;
                YellowstonePathology.Business.User.UserPreferenceInstance.Instance.Save();
            }            
        }        		

		public void ShowCaseDocument(object target, ExecutedRoutedEventArgs args)
		{
			this.Save(false);
			YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument report = new YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument(this.m_TypingUI.AccessionOrder, this.m_TypingUI.SurgicalTestOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_TypingUI.SurgicalTestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        public void SaveData(object target, ExecutedRoutedEventArgs args)
        {
            this.Save(false);
            MessageBox.Show("The typing workspace has been saved");
        }

        public void Save(bool releaseLock)
        {
            MainWindow.MoveKeyboardFocusNextThenBack();
            this.m_TypingUI.Save(releaseLock);
        }

        private void CanSaveData(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void SetFocus()
        {
            this.TextBoxReportNoSearch.Focus();
            this.TextBoxReportNoSearch.SelectionStart = 100;    
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs args)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Select(5000, 1);
        }

        public void TextBoxReportNoSearch_KeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Return)
            {
                this.HandleNewCaseSearch(this.TextBoxReportNoSearch.Text);
            }
        }

        private void HandleNewCaseSearch(string masterAccessionNoOrReportNo)
        {
            if (this.m_TypingUI.SurgicalTestOrder != null)
            {
                YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LastReportNo = this.m_TypingUI.SurgicalTestOrder.ReportNo;
                YellowstonePathology.Business.User.UserPreferenceInstance.Instance.Save();
            }

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(masterAccessionNoOrReportNo);
            if (orderIdParser.IsLegacyReportNo == true)
            {
                this.TextBoxReportNoSearch.Text.ToUpper();
                this.GetSurgicalCase(this.TextBoxReportNoSearch.Text);
            }            
            else if (orderIdParser.IsValidMasterAccessionNo == true)
            {
                this.TextBoxReportNoSearch.Text = orderIdParser.CreateSurgicalReportNoFromMasterAccessionNo();
                this.GetSurgicalCase(this.TextBoxReportNoSearch.Text);                
            }                
        }

		public void GetSurgicalCase(string reportNo)
        {               
            this.Save(true);
			this.m_DocumentViewer.ClearContent();
			
            this.m_TypingUI.GetAccessionOrder(reportNo);

            if (this.m_TypingUI.AccessionOrder != null)
            {
                this.ListViewDocumentList.ItemsSource = this.m_TypingUI.CaseDocumentCollection;
                this.m_TypingUI.RunWorkspaceEnableRules();

                if (this.m_TypingUI.CaseDocumentCollection.GetFirstRequisition() != null)
                {
                    if (this.m_DocumentViewer.SyncDocuments == true)
                    {
                        this.TabControlRightMain.SelectedIndex = 0;
                    }
                    this.m_DocumentViewer.ShowDocument(this.m_TypingUI.CaseDocumentCollection.GetFirstRequisition());
                }
                this.RefreshWorkspaces();

                if (System.Windows.Forms.Screen.AllScreens.Length == 2)
                {
                    YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPage providerDistributionPage = new Login.FinalizeAccession.ProviderDistributionPage(reportNo, this.m_TypingUI.AccessionOrder, this.m_SecondMonitorWindow.PageNavigator,
                        System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                    this.m_SecondMonitorWindow.PageNavigator.Navigate(providerDistributionPage);
                }
            }            
        }

        private void RefreshWorkspaces()
        {            
            this.m_TreeviewWorkspace = new Common.TreeViewWorkspace(this.m_TypingUI.AccessionOrder, this.m_SystemIdentity);
            this.m_TreeviewWorkspace.IsEnabled = this.m_TypingUI.AccessionOrder.IsLockAquiredByMe;
            this.tabItemTreeView.Content = this.m_TreeviewWorkspace;

			this.m_AmendmentControl = new AmendmentControlV2(this.m_SystemIdentity, this.m_TypingUI.SurgicalTestOrder.ReportNo, this.m_TypingUI.AccessionOrder);
            this.m_AmendmentControl.IsEnabled = this.m_TypingUI.AccessionOrder.IsLockAquiredByMe;
            this.TabItemAmendments.Content = this.m_AmendmentControl;
        }

        public void GridTyping_KeyUp(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.F7:
                    this.CheckSpelling();
                    break;
                case Key.PageUp:
                    this.GetNextCaseListItem(-1);
                    break;
                case Key.PageDown:
                    this.GetNextCaseListItem(1);
                    break;                    
            }                        
        }

        public void GetNextCaseListItem(int upDown)
        {
            if (this.ListViewSurgicalCaseList.SelectedItem != null)
            {
                if (upDown == 1)
                {
                    if (this.ListViewSurgicalCaseList.SelectedIndex < this.ListViewSurgicalCaseList.Items.Count)
                    {
                        this.ListViewSurgicalCaseList.SelectedIndex += 1;                        
						YellowstonePathology.Business.Surgical.SurgicalOrderListItem item = (YellowstonePathology.Business.Surgical.SurgicalOrderListItem)this.ListViewSurgicalCaseList.SelectedItem;
						this.TextBoxReportNoSearch.Text = item.ReportNo;                        
                    }
                }
                if (upDown == -1)
                {
                    if (this.ListViewSurgicalCaseList.SelectedIndex > 0)
                    {
                        this.ListViewSurgicalCaseList.SelectedIndex -= 1;                        
						YellowstonePathology.Business.Surgical.SurgicalOrderListItem item = (YellowstonePathology.Business.Surgical.SurgicalOrderListItem)this.ListViewSurgicalCaseList.SelectedItem;
						this.TextBoxReportNoSearch.Text = item.ReportNo;                        
                    }
                }
                this.m_TypingUI.SetIsPossibleReportableCase();                
            }
        }        

        public void CheckSpelling()
        {
            this.TextBoxReportNoSearch.Focus();
			if (this.m_TypingUI.SurgicalTestOrder != null)
            {
                YellowstonePathology.Business.Common.SpellChecker spellChecker = new YellowstonePathology.Business.Common.SpellChecker();
				YellowstonePathology.Business.Common.TypingSpellCheckList typeingSpellCheckList = new YellowstonePathology.Business.Common.TypingSpellCheckList(this.m_TypingUI.AccessionOrder, this.m_TypingUI.SurgicalTestOrder);
                spellChecker.CheckSpelling(typeingSpellCheckList);
                MessageBox.Show("Spell check is complete.");
            }            
        }

        public void ListViewDocuments_SelectionChanged(object sender, RoutedEventArgs args)
        {
			YellowstonePathology.Business.Document.CaseDocument item = (YellowstonePathology.Business.Document.CaseDocument)this.ListViewDocumentList.SelectedItem;
			if (this.m_DocumentViewer.SyncDocuments == true)
			{
				this.TabControlRightMain.SelectedIndex = 0;
			}
			this.m_DocumentViewer.ShowDocument(item);            
        }

        public void ButtonViewDocument_Click(object sender, RoutedEventArgs args)
        {
			if (this.m_TypingUI.SurgicalTestOrder != null && this.m_TypingUI.SurgicalTestOrder.ReportNo != string.Empty)
			{
				this.Save(false);
				YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument report = new YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument(this.m_TypingUI.AccessionOrder, this.m_TypingUI.SurgicalTestOrder, Business.Document.ReportSaveModeEnum.Draft);
				report.Render();
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_TypingUI.SurgicalTestOrder.ReportNo);
				string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
				YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
			}
        }

        private YellowstonePathology.Business.Rules.MethodResult DoTypingFinalChecks()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            methodResult.Success = true;            

            if (this.m_TypingUI.AccessionOrder.ClientId == 558)
            {
                string message = string.Empty;
                if (string.IsNullOrEmpty(this.m_TypingUI.AccessionOrder.SvhAccount) == true)
                {
                    message += "The SVH Account is blank.";
                    methodResult.Success = false;
                }
                if (string.IsNullOrEmpty(this.m_TypingUI.AccessionOrder.SvhMedicalRecord) == true)
                {
                    message += "The SVH MRN is blank.";
                    methodResult.Success = false;
                }                
                methodResult.Message = message;
            }

			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_TypingUI.AccessionOrder.PhysicianId);
            if (string.IsNullOrEmpty(physician.Npi) == true)
            {
                methodResult.Message += "The provider NPI is blank.";
                methodResult.Success = false;
            }

            if (this.m_TypingUI.SurgicalTestOrder.AssignedToId == 0)
            {
                methodResult.Success = false;
                methodResult.Message = "Please assign a pathologist to this case.";
            }            
            return methodResult;
        }
        
        public void CheckBoxAccepted_Click(object sender, RoutedEventArgs args)
        {                        
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.DoTypingFinalChecks();
            if (methodResult.Success == true)
            {
				if (this.m_TypingUI.SurgicalTestOrder != null)
                {
					if (this.CheckBoxAccepted.IsChecked.Value == true)
					{
						this.m_TypingUI.SurgicalTestOrder.AcceptedDate = DateTime.Parse(DateTime.Today.ToShortDateString());
						this.m_TypingUI.SurgicalTestOrder.AcceptedTime = DateTime.Parse(DateTime.Now.ToShortTimeString());

						if (this.m_TypingUI.SurgicalTestOrder.SurgicalSpecimenCollection.HasDuplicateStains() == true)
						{
							System.Windows.MessageBox.Show("Duplicate stains were found, please check to see if any need to be no charged.");
						}         
                    }
					else
					{
						this.m_TypingUI.SurgicalTestOrder.AcceptedDate = null;
						this.m_TypingUI.SurgicalTestOrder.AcceptedTime = null;
					}
                }
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        public void ContextMenuAddCptCodes_Click(object sender, RoutedEventArgs args)
        {            
            this.Save(false);

            Control ctrl = (Control)args.Source;
			YellowstonePathology.Business.View.BillingSpecimenView billingSpecimenView = (YellowstonePathology.Business.View.BillingSpecimenView)ctrl.Tag;            
			YellowstonePathology.UI.CodeSelectionV2 codeSelection = new CodeSelectionV2(this.m_TypingUI.AccessionOrder, billingSpecimenView.SurgicalSpecimen);
            codeSelection.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            codeSelection.ShowDialog();
			this.m_TypingUI.RefreshBillingSpecimenViewCollection();
        }

        public void ContextMenuDeleteCptCode_Click(object sender, RoutedEventArgs args)
        {
            this.Save(false);
            Control ctrl = (Control)args.Source;                                              
            string panelSetOrderCPTCodeId = ctrl.Tag.ToString();

			m_TypingUI.DeletePanelSetOrderCptCode(panelSetOrderCPTCodeId);            
        }

        public void ContextMenuDeleteIcd9Code_Click(object sender, RoutedEventArgs args)
        {            
            Control ctrl = (Control)args.Source;
            string icd9BillingId = ctrl.Tag.ToString();
			this.m_TypingUI.DeleteIcd9Code(icd9BillingId);            
            this.Save(false);
		}

        public void TextBox_KeyUp(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Space)
            {                
                TextBox textBox = (TextBox)args.Source;
                this.m_TypingShortcutUserControl.SetShortcut(textBox);
            }
        }        

        public void ComboBoxAcceptingUsers_SelectionChanged(object sender, RoutedEventArgs args)
        {
			if (this.ComboBoxAcceptingUsers.SelectedItem != null)
            {
				YellowstonePathology.Business.User.SystemUser item = (YellowstonePathology.Business.User.SystemUser)this.ComboBoxAcceptingUsers.SelectedItem;
				this.m_TypingUI.SurgicalTestOrder.AcceptedBy = item.DisplayName;
            }
        }

        public void ContextMenuMoveServerFileToLocal_Click(object sender, RoutedEventArgs args)
        {
            this.Save(false);
            if (this.ListViewServerDictation.SelectedItem != null)
            {
                YellowstonePathology.Business.FileListItem item = (YellowstonePathology.Business.FileListItem)this.ListViewServerDictation.SelectedItem;
                this.m_ServerDictationList.MoveServerFileToLocal(item);
                this.m_ServerDictationList.Refresh();
                this.m_LocalDictationList.Refresh();
            }
        }        

        public void ContextMenuOpenDictation_Click(object sender, RoutedEventArgs args)
        {
            if (this.ListViewLocalDictation.SelectedItem != null)
            {
                YellowstonePathology.Business.FileListItem item = (YellowstonePathology.Business.FileListItem)this.ListViewLocalDictation.SelectedItem;
                YellowstonePathology.Business.FileList.OpenFile(item);
            }
        }        

        public void ListViewDocumentList_MouseLeftButtonUp(object sender, RoutedEventArgs args)
        {
            if (this.ListViewDocumentList.SelectedItem != null)
            {
				if (this.m_DocumentViewer.SyncDocuments == true)
				{
					this.TabControlRightMain.SelectedIndex = 0;
				}
				YellowstonePathology.Business.Document.CaseDocument item = (YellowstonePathology.Business.Document.CaseDocument)this.ListViewDocumentList.SelectedItem;
				this.m_DocumentViewer.ShowDocument(item);
            }
        }

        public void ButtonRefreshDictation_Click(object sender, RoutedEventArgs args)
        {
            this.m_ServerDictationList.Refresh();
            this.m_LocalDictationList.Refresh();
        }

        public void ButtonGoBack_Click(object sender, RoutedEventArgs args)
        {            
            this.m_TypingUI.CaseListDate = this.m_TypingUI.CaseListDate.AddDays(-1);
			this.m_TypingUI.SurgicalOrderList.FillByAccessionDate(this.m_TypingUI.CaseListDate);
        }

        public void ButtonGoForward_Click(object sender, RoutedEventArgs args)
        {
            this.m_TypingUI.CaseListDate = this.m_TypingUI.CaseListDate.AddDays(1);
			this.m_TypingUI.SurgicalOrderList.FillByAccessionDate(this.m_TypingUI.CaseListDate);
        }

        public void ButtonGetList_Click(object sender, RoutedEventArgs args)
        {            
            switch (this.ComboboxListSelection.Text)
            {                
                case "Cases With Intraoperative Consulations":
					this.m_TypingUI.SurgicalOrderList.FillByIntraoperative();
                    break;
                case "Cases Not Audited":
					this.m_TypingUI.SurgicalOrderList.FillByNotAudited();
                    break;
				case "Cases No Signature":
					this.m_TypingUI.SurgicalOrderList.FillByNoSignature();
					break;
                case "Daily Accession List":
                case "Daily Final List":
                case "PQRI Cases":
                    DateTime dateOut;
                    bool validDate = DateTime.TryParse(this.TextBoxCaseListDate.Text, out dateOut);
                    if (validDate == true)
                    {
                        if (this.ComboboxListSelection.Text == "Daily Accession List")
                        {
							this.m_TypingUI.SurgicalOrderList.FillByAccessionDate(dateOut);
                        }
                        if (this.ComboboxListSelection.Text == "Daily Final List")
                        {
							this.m_TypingUI.SurgicalOrderList.FillByFinalDate(dateOut);
                        }
                        if (this.ComboboxListSelection.Text == "PQRI Cases")
                        {
                            this.m_TypingUI.SurgicalOrderList.FillByPqri(dateOut);
                        }
                    }
                    break;                    
				case "SVH Electronic Orders":
                    DateTime rptDate;
                    bool dateIsValid = DateTime.TryParse(this.TextBoxCaseListDate.Text, out rptDate);
					if (dateIsValid)
					{
						this.m_TypingUI.SurgicalOrderList.FillBySvhClientOrder(rptDate);
					}
					break;
                case "No Gross":
                    this.m_TypingUI.SurgicalOrderList.FillByNoGross();
                    break;
                case "No Clinical Info":
                    this.m_TypingUI.SurgicalOrderList.FillByNoClinicalInfo();
                    break;
            }            
        }        

        public void ButtonFilterCaseList_Click(object sender, RoutedEventArgs args)
        {
            if (this.ComboBoxCaseListPathologistUsers.SelectedItem != null)
            {
				YellowstonePathology.Business.User.SystemUser item = (YellowstonePathology.Business.User.SystemUser)this.ComboBoxCaseListPathologistUsers.SelectedItem;
				this.m_TypingUI.SurgicalOrderList.FilterByPathologistId(item.UserId);
            }
        }        

        public void MenuItemRefreshDocumentList_Click(object sender, RoutedEventArgs args)
        {
            this.Save(false);
			if (this.m_TypingUI.SurgicalTestOrder != null)
			{				
				this.m_TypingUI.RefreshCaseDocumentCollection(this.m_TypingUI.SurgicalTestOrder.ReportNo);
				this.ListViewDocumentList.ItemsSource = this.m_TypingUI.CaseDocumentCollection;
			}
        }

		private void ContextMenu_Opened(object sender, RoutedEventArgs e)
		{
			ContextMenu contextMenu = (ContextMenu)sender;
			foreach (MenuItem menuItem in contextMenu.Items)
			{
				if (menuItem.Header.ToString() == "Add Specimen" || menuItem.Header.ToString() == "Delete Specimen" || menuItem.Header.ToString() == "Add Intraoperative Consultation")
				{
					menuItem.IsEnabled = false;
				}
				else
				{
					menuItem.IsEnabled = this.m_TypingUI.FieldEnabler.IsUnprotectedEnabled;
				}
			}
		}

		private void ShowOrderForm(object target, ExecutedRoutedEventArgs args)
		{
			this.Save(false);
			YellowstonePathology.UI.Common.OrderDialog frm = new YellowstonePathology.UI.Common.OrderDialog(this.m_TypingUI.AccessionOrder, this.m_TypingUI.SurgicalTestOrder);
			frm.ShowDialog();

            string reportNo = this.m_TypingUI.AccessionOrder.PanelSetOrderCollection.GetItem(13).ReportNo;
            this.m_TypingUI.GetAccessionOrder(reportNo);
            this.RefreshWorkspaces();
		}

		private void ItemIsSelected(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected &&
				this.m_TypingUI.AccessionOrder != null &&
				this.m_TypingUI.SurgicalTestOrder != null &&
				this.m_TypingUI.SurgicalTestOrder.ReportNo != string.Empty &&
				this.m_TypingUI.AccessionOrder.IsLockAquiredByMe)
			{
				e.CanExecute = true;
			}
		}

		private void ItemIsPresent(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected &&
				this.m_TypingUI.AccessionOrder != null &&
				this.m_TypingUI.SurgicalTestOrder != null &&
				this.m_TypingUI.SurgicalTestOrder.ReportNo != string.Empty)
			{
				e.CanExecute = true;
			}
		}

        private void ListBoxSpecimen_Click(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ShowAmendmentDialog(object target, ExecutedRoutedEventArgs args)
        {
            if (this.m_TypingUI.AccessionOrder != null)
            {
                this.Save(false);
                YellowstonePathology.UI.AmendmentPageController amendmentPageController = new AmendmentPageController(this.m_TypingUI.AccessionOrder, this.m_TypingUI.SurgicalTestOrder);
                amendmentPageController.ShowDialog();

                string reportNo = this.m_TypingUI.AccessionOrder.PanelSetOrderCollection.GetItem(13).ReportNo;
                this.RefreshWorkspaces();

                this.m_TypingUI.RunWorkspaceEnableRules();
            }
		}

        private void ButtonTemplate_Click(object sender, RoutedEventArgs e)
        {

        }        		

        private void ButtonCAPLink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.cap.org/web/home/resources/cancer-reporting-tools/cancer-protocol-templates?_adf.ctrl-state=dqakeocns_55&_afrLoop=977071090428152#!%40%40%3F_afrLoop%3D977071090428152%26_adf.ctrl-state%3Djmv67olsb_30");
        }

        private void MenuItemShowRequisition_Click(object sender, RoutedEventArgs e)
        {
            Business.Document.CaseDocument caseDocument = this.m_TypingUI.CaseDocumentCollection.GetRequisition();
            this.m_DocumentViewer.ShowDocument(caseDocument);
        }        

        private void MenuItemShowAccessionDataSheet_Click(object sender, RoutedEventArgs e)
        {
            Business.Document.CaseDocument caseDocument = this.m_TypingUI.CaseDocumentCollection.GetAccessionOrderDataSheet();
            this.m_DocumentViewer.ShowDocument(caseDocument);
        }

        private void MenuItemShowPatientFaceSheet_Click(object sender, RoutedEventArgs e)
        {
            Business.Document.CaseDocument caseDocument = this.m_TypingUI.CaseDocumentCollection.GetPatientFaceSheet();
            this.m_DocumentViewer.ShowDocument(caseDocument);
        }        

        public string FixInvalidCharacters(string text)
        {
            string result = text;

            char invalidApostrophe = Convert.ToChar(8217);
            char correctApostrophe = Convert.ToChar(39);

            char invalidQuote1 = Convert.ToChar(8221);
            char invalidQuote2 = Convert.ToChar(8220);
            char correctQuote = Convert.ToChar(34);

            char invalidDash = Convert.ToChar(8211);
            char correctDash = Convert.ToChar(45);

            result = result.Replace(invalidApostrophe, correctApostrophe);            
            result = result.Replace(invalidQuote1, correctQuote);
            result = result.Replace(invalidQuote2, correctQuote);
            result = result.Replace(invalidDash, correctDash);
           
            return result;
        }       

        private void ButtonProviderEntry_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullPhysician(this.m_TypingUI.AccessionOrder.PhysicianId, this.m_Writer);
            Client.ProviderEntry providerEntry = new Client.ProviderEntry(physician);
            providerEntry.ShowDialog();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(physician, this.m_Writer);
        }

        private void ButtonPatientLinking_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker = new Business.Patient.Model.PatientLinker(this.m_TypingUI.AccessionOrder.MasterAccessionNo,
				this.m_TypingUI.SurgicalTestOrder.ReportNo, 
                this.m_TypingUI.AccessionOrder.PFirstName, 
                this.m_TypingUI.AccessionOrder.PLastName,
                this.m_TypingUI.AccessionOrder.PMiddleInitial,
				this.m_TypingUI.AccessionOrder.PSSN, this.m_TypingUI.AccessionOrder.PatientId, this.m_TypingUI.AccessionOrder.PBirthdate);
			if (patientLinker.IsOkToLink.IsValid == true)
			{
				YellowstonePathology.UI.Common.PatientLinkingDialog patientLinkingDialog = new Common.PatientLinkingDialog(this.m_TypingUI.AccessionOrder,
					Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, patientLinker);
                patientLinkingDialog.ShowDialog();
            }
            else
			{
				MessageBox.Show(patientLinker.IsOkToLink.Message, "Missing Information");
			}
		}

		private void ButtonPatientDetails_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.m_TypingUI.AccessionOrder.SvhMedicalRecord) == false)
			{
                YellowstonePathology.Business.Patient.Model.SVHBillingDataCollection svhBillingDataCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSVHBillingDataCollection(this.m_TypingUI.AccessionOrder.SvhMedicalRecord);
				if (svhBillingDataCollection.Count > 0)
				{
                    YellowstonePathology.Business.Patient.Model.SVHBillingData svhBillingDate = svhBillingDataCollection.GetMostRecent();
					YellowstonePathology.UI.Billing.PatientDetailPage patientDetailPage = new Billing.PatientDetailPage(svhBillingDate);
					patientDetailPage.Back += new Billing.PatientDetailPage.BackEventHandler(PatientDetailPage_Return);
					patientDetailPage.Next += new Billing.PatientDetailPage.NextEventHandler(PatientDetailPage_Return);

					Login.Receiving.LoginPageWindow loginPageWindow = new Login.Receiving.LoginPageWindow();
					loginPageWindow.PageNavigator.Navigate(patientDetailPage);
					loginPageWindow.ShowDialog();
				}
				else
				{
					System.Windows.MessageBox.Show("No additional data to show.");
				}
			}
			else
			{
				System.Windows.MessageBox.Show("The Medical Record Number is blank. No additional data to show.");
			}
		}

		private void PatientDetailPage_Return(object sender, EventArgs e)
		{
			YellowstonePathology.UI.Billing.PatientDetailPage patientDetailPage = (YellowstonePathology.UI.Billing.PatientDetailPage)sender;
			Window.GetWindow(patientDetailPage).Close();
		}

		private void CheckBoxAudit_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.DoTypingFinalChecks();
            if (methodResult.Success == false)
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        public void ContextMenuMoveLocalFileToDone_Click(object sender, RoutedEventArgs args)
        {
            this.Save(false);
            MessageBoxResult result = MessageBox.Show("Move this dictation file to the done folder?", "Move Dictation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                if (this.ListViewLocalDictation.SelectedItem != null)
                {
                    YellowstonePathology.Business.FileListItem item = (YellowstonePathology.Business.FileListItem)this.ListViewLocalDictation.SelectedItem;
                    this.m_LocalDictationList.MoveLocalFileToDone(item);
                    this.m_LocalDictationList.Refresh();
                }
            }
        }

        private void ContextMenuDictationToCaseFolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewLocalDictation.SelectedItem != null)
            {
                YellowstonePathology.Business.FileListItem item = (YellowstonePathology.Business.FileListItem)this.ListViewLocalDictation.SelectedItem;
                YellowstonePathology.UI.Surgical.MoveDictationToCaseFolderPage moveDictationToCaseFolder = new MoveDictationToCaseFolderPage(item.FullPath);
                YellowstonePathology.UI.PageFunctionStartWindow pageFunctionStartWindow = new PageFunctionStartWindow(moveDictationToCaseFolder);
                pageFunctionStartWindow.ShowDialog();
            }
        }

        private void ContextMenuOpenDocumentsFolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewSurgicalCaseList.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Surgical.SurgicalOrderListItem surgicalOrderListItem = (YellowstonePathology.Business.Surgical.SurgicalOrderListItem)this.ListViewSurgicalCaseList.SelectedItem;
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(surgicalOrderListItem.ReportNo);
				string folderPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
                p.StartInfo = info;
                p.Start();
            }
        }        

        private void MenuItemImportPreopDiagnosis_Click(object sender, RoutedEventArgs e)
        {            
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_TypingUI.AccessionOrder.MasterAccessionNo);
            if (clientOrderCollection.Count != 0)
            {
                BindingExpression bindingExpression = this.TextBoxClinical.GetBindingExpression(TextBox.TextProperty);
                bindingExpression.UpdateSource();

                if(clientOrderCollection[0] is YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder)
                {
                    YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder surgicalClientOrder = (YellowstonePathology.Business.ClientOrder.Model.SurgicalClientOrder)clientOrderCollection[0];
					this.m_TypingUI.AccessionOrder.ClinicalHistory += "Pre-Op Diagnosis: " + surgicalClientOrder.PreOpDiagnosis;
                }
            }
            else
            {
                MessageBox.Show("Client order not found.");
            }
        }

        private void MenuItemImportSpecialInstructions_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_TypingUI.AccessionOrder.MasterAccessionNo);
            if (clientOrderCollection.Count != 0)
            {
                BindingExpression bindingExpression = this.TextBoxClinical.GetBindingExpression(TextBox.TextProperty);
                bindingExpression.UpdateSource();

                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = clientOrderCollection[0];
				this.m_TypingUI.AccessionOrder.ClinicalHistory += clientOrder.SpecialInstructions;
            }
            else
            {
                MessageBox.Show("Client order not found.");
            }
        }

        private void ListViewSurgicalCaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (this.ListViewSurgicalCaseList.SelectedItem != null)
            {
                if (this.m_TypingUI.SurgicalTestOrder != null)
                {
                    YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LastReportNo = this.m_TypingUI.SurgicalTestOrder.ReportNo;
                    YellowstonePathology.Business.User.UserPreferenceInstance.Instance.Save();
                }

                YellowstonePathology.Business.Surgical.SurgicalOrderListItem surgicalOrderListItem = (YellowstonePathology.Business.Surgical.SurgicalOrderListItem)this.ListViewSurgicalCaseList.SelectedItem;
                this.GetSurgicalCase(surgicalOrderListItem.ReportNo);
                this.TextBoxReportNoSearch.Text = surgicalOrderListItem.ReportNo;
            }
        }                

        private void HyperLinkShowGossTemplate_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_TypingUI.AccessionOrder != null)
            {                
                DictationTemplatePage dictationTemplatePage = new DictationTemplatePage(this.m_TypingUI.AccessionOrder, this.m_SystemIdentity);
                this.m_SecondMonitorWindow.PageNavigator.Navigate(dictationTemplatePage);                
            }
        }

        private void HyperLinkShowProviderDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_TypingUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPage providerDistributionPage = new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPage(this.m_TypingUI.SurgicalTestOrder.ReportNo, this.m_TypingUI.AccessionOrder, this.m_SecondMonitorWindow.PageNavigator,
                    Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed);
                this.m_SecondMonitorWindow.PageNavigator.Navigate(providerDistributionPage);
            }
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LastReportNo) == false)
            {
                this.HandleNewCaseSearch(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LastReportNo);
            }
        }

        private void HyperLinkBuildTemplates_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder result = new StringBuilder();
            if (this.m_TypingUI.AccessionOrder != null)
            {
            	YellowstonePathology.UI.Gross.DictationTemplateCollection collection = YellowstonePathology.UI.Gross.DictationTemplateCollection.GetAll();
            	foreach(YellowstonePathology.UI.Gross.DictationTemplate template in collection)
            	{
                    result.AppendLine("Name: " + template.TemplateName);
                    result.Append(template.BuildResultText(this.m_TypingUI.AccessionOrder.SpecimenOrderCollection[0], this.m_TypingUI.AccessionOrder, this.m_SystemIdentity));
                    result.AppendLine();
                    result.AppendLine();
                    result.AppendLine();
                }
                this.m_TypingUI.TemplateText = result.ToString();
                this.m_TypingUI.NotifyPropertyChanged("TemplateText");
            }
        }

        private void HyperLinkCleanClinicalInformation_Click(object sender, RoutedEventArgs e)
        {
            this.m_TypingUI.AccessionOrder.ClinicalHistory = this.m_TypingUI.AccessionOrder.ClinicalHistory.Replace("Comprehensive Consult:->No", "");
;        }

        private void HyperLinkSpellCheck_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_TypingUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.NHunspell nhunspell = new NHunspell(this.m_TypingUI.AccessionOrder);
                nhunspell.ShowDialog();
            }
        }
    }    
}