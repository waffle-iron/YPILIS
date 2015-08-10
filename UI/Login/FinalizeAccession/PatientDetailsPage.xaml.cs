﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for PatientDetailsPage.xaml
	/// </summary>
	public partial class PatientDetailsPage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		public PatientDetailsPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
			this.m_ObjectTracker = objectTracker;
			this.m_AccessionOrder = accessionOrder;

            this.m_PageHeaderText = accessionOrder.MasterAccessionNo + ": " + 
                accessionOrder.PFirstName + " " + accessionOrder.PLastName;

			InitializeComponent();

            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(PatientDetailsPage_Loaded);
		}        

        private void PatientDetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxSex.Focus();
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		private void ButtonHistory_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.AccessionOrder.PatientId) || this.AccessionOrder.PatientId == "0")
			{
				MessageBox.Show("History is not available until the patient is linked.", "Patient is not linked", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			YellowstonePathology.UI.Common.CaseHistoryDialog caseHistoryDialog = new Common.CaseHistoryDialog(this.m_AccessionOrder);
			caseHistoryDialog.ShowDialog();
		}

		private void ButtonLink_Click(object sender, RoutedEventArgs e)
		{
            this.Save();
		}

		private void ButtonCaseNotes_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Command, FinalizeAccessionCommandTypeEnum.ShowCaseNotes);
			this.Return(this, args);
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.DataIsValid() == true)
            {
                UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
                this.Return(this, args);
            }
		}

        private bool DataIsValid()
        {
            bool result = true;

            /*** this shoulde be moved to the audit namespace *****/
            Shared.ValidationResult accountNoValidation = this.m_AccessionOrder.IsAccountNoValid();
            Shared.ValidationResult mrnValidation = this.m_AccessionOrder.IsMedicalRecordNoValid();

            if (accountNoValidation.IsValid == false)
            {
                MessageBox.Show(accountNoValidation.Message);
                result = false;
            }
            else if (mrnValidation.IsValid == false)
            {
                MessageBox.Show(mrnValidation.Message);
                result = false;
            }
            /********/

            YellowstonePathology.Business.Audit.Model.AuditCollection auditCollection = new Business.Audit.Model.AuditCollection();
            auditCollection.Add(new YellowstonePathology.Business.Audit.Model.TallmanMedicalRecordAudit(this.m_AccessionOrder));
            auditCollection.Run();

            if (auditCollection.ActionRequired == true)
            {
                MessageBox.Show(auditCollection.Message);
                result = false;
            }

            return result;
        }

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
			this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);            
		}

		public void UpdateBindingSources()
		{
		}
	}
}
