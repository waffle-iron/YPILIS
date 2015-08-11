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
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for ClientFaxPage.xaml
	/// </summary>
	public partial class ClientFaxPage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private StringBuilder m_AbnEventComment;
		private StringBuilder m_InfoEventComment;
		
		private string m_PatientNameWithBirthDate;

		private bool m_FormatBold;
		private string m_TestName;		

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private string m_PageHeaderText = "Create Client Fax";

		public ClientFaxPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_ClientOrder = clientOrder;
			this.m_SystemIdentity = systemIdentity;
			
			InitializeComponent();
			this.DataContext = this;

			this.m_AbnEventComment = new StringBuilder();
			this.m_InfoEventComment = new StringBuilder();
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public string PatientName
		{
			get { return this.m_ClientOrder.PatientDisplayName; }
		}

		public string ClientName
		{
			get { return this.m_ClientOrder.ClientName; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}		

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save()
		{
		}

		public void UpdateBindingSources()
		{
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.Next(this, new EventArgs());
		}

		private void CreateLetterBody()
		{
			this.m_FormatBold = false;
			this.m_PatientNameWithBirthDate = this.PatientName;
			if (this.m_ClientOrder.PBirthdate.HasValue) m_PatientNameWithBirthDate = m_PatientNameWithBirthDate + " (DOB:" + this.m_ClientOrder.PBirthdate.Value.ToShortDateString() + ")";
			StringBuilder result = new StringBuilder();
			bool created = CreateMissingABN(result);
			if (!created) created = CreateMissingInfo(result);
			if (!created) created = CreateMissingSignature(result);
			this.TextBoxLetterBody.Text = result.ToString();
		}

		private void ButtonCreateLetterBody_Click(object sender, RoutedEventArgs e)
		{
			this.CreateLetterBody();
		}

		private void ButtonFaxLetter_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_ClientOrder.ClientId != 0)
			{
				YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_ClientOrder.ClientId);
				string letterBody = this.TextBoxLetterBody.Text;

				if (this.m_FormatBold)
				{
					MissingSignatureLetterBody missingSignatureLetterBody = new MissingSignatureLetterBody();
					StringBuilder body = new StringBuilder();
					letterBody = missingSignatureLetterBody.GetLetterBodyForFax(this.m_TestName);
					YellowstonePathology.Business.Document.ClientLetterBold clientLetterBold = new YellowstonePathology.Business.Document.ClientLetterBold();
					clientLetterBold.Create(this.m_PatientNameWithBirthDate, this.m_ClientOrder.ProviderName, client, letterBody);
				}
				else
				{
					YellowstonePathology.Business.Document.ClientLetter clientLetter = new YellowstonePathology.Business.Document.ClientLetter();
					clientLetter.Create(this.m_PatientNameWithBirthDate, client, letterBody);
				}

                YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(client.Fax, client.LongDistance, "Missing Information", YellowstonePathology.UI.Properties.Settings.Default.ClientMissingInformationLetterFileName);
                MessageBox.Show("The fax was successfully submitted.");

				if (string.IsNullOrEmpty(this.m_InfoEventComment.ToString()) == false)
				{
					YellowstonePathology.Business.Domain.OrderCommentLog.LogEvent(this.m_ClientOrder.ClientOrderId,
						this.m_InfoEventComment.ToString(), this.m_SystemIdentity.User, YellowstonePathology.Business.Domain.OrderCommentEnum.CytologyLogFaxSentRequestingInformation);
				}

				if (string.IsNullOrEmpty(this.m_AbnEventComment.ToString()) == false)
				{
					YellowstonePathology.Business.Domain.OrderCommentLog.LogEvent(this.m_ClientOrder.ClientOrderId,
						this.m_AbnEventComment.ToString(), this.m_SystemIdentity.User, YellowstonePathology.Business.Domain.OrderCommentEnum.CytologyLogFaxSentRequestingABN);
				}
			}
			else
			{
				MessageBox.Show("Client must be selected before a fax can be generated.");
			}
		}

		private void AppendToEventDescription(StringBuilder stringBuilder, string msg)
		{
			if (stringBuilder.Length > 9)
			{
				stringBuilder.Append("; ");
			}
			stringBuilder.Append(msg);
		}

		private bool CreateMissingABN(StringBuilder result)
		{
			bool res = false;
			if (this.CheckBoxABNDate.IsChecked == true ||
				this.CheckBoxABNEstimatedCost.IsChecked == true ||
				this.CheckBoxABNIdentificationNumber.IsChecked == true ||
				this.CheckBoxABNLaboratory.IsChecked == true ||
				this.CheckBoxABNNotifier.IsChecked == true ||
				this.CheckBoxABNOptionBoxChecked.IsChecked == true ||
				this.CheckBoxABNPatientName.IsChecked == true)
			{
				MissingABNLetterBody missingABNLetterBody = new MissingABNLetterBody();
				missingABNLetterBody.GetLetterBody(result, this.m_PatientNameWithBirthDate,
					this.CheckBoxABNDate.IsChecked == true,
					this.CheckBoxABNEstimatedCost.IsChecked == true,
					this.CheckBoxABNIdentificationNumber.IsChecked == true,
					this.CheckBoxABNLaboratory.IsChecked == true,
					this.CheckBoxABNNotifier.IsChecked == true,
					this.CheckBoxABNOptionBoxChecked.IsChecked == true,
					this.CheckBoxABNPatientName.IsChecked == true);

				res = true;

				if (this.CheckBoxABNDate.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Date");
				if (this.CheckBoxABNEstimatedCost.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Estimate Cost ");
				if (this.CheckBoxABNIdentificationNumber.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Identification Number ");
				if (this.CheckBoxABNLaboratory.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Laboratory Tests ");
				if (this.CheckBoxABNNotifier.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Notifier ");
				if (this.CheckBoxABNOptionBoxChecked.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Option Box Checked ");
				if (this.CheckBoxABNPatientName.IsChecked == true) this.AppendToEventDescription(m_AbnEventComment, "Patient Name ");
			}
			return res;
		}

		private bool CreateMissingInfo(StringBuilder result)
		{
			bool res = false;
			if (this.CheckBoxABN.IsChecked == true ||
				this.CheckBoxCytologyDXCode.IsChecked == true ||
				this.CheckBoxPatientDemographics.IsChecked == true ||
				this.CheckBoxNGCTDXCode.IsChecked == true ||
				this.CheckBoxTestType.IsChecked == true ||
				this.CheckBoxOrderingPhysician.IsChecked == true ||
				this.CheckBoxCollectionDate.IsChecked == true)
			{
				MissingInfoLetterBody missingInfoLetterBody = new MissingInfoLetterBody();
				missingInfoLetterBody.GetLetterBody(result, this.m_PatientNameWithBirthDate,
					this.CheckBoxABN.IsChecked == true,
					this.CheckBoxCytologyDXCode.IsChecked == true,
					this.CheckBoxPatientDemographics.IsChecked == true,
					this.CheckBoxNGCTDXCode.IsChecked == true,
					this.CheckBoxTestType.IsChecked == true,
					this.CheckBoxOrderingPhysician.IsChecked == true,
					this.CheckBoxCollectionDate.IsChecked == true);

				res = true;

				if (this.CheckBoxABN.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "ABN");
				if (this.CheckBoxCytologyDXCode.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Diagnosis Code");
				if (this.CheckBoxPatientDemographics.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Patient Demographics");
                if (this.CheckBoxNGCTDXCode.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment,  "NG/CT Diagnosis Code");                
				if (this.CheckBoxTestType.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Test Type");
				if (this.CheckBoxOrderingPhysician.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Ordering Physician");
				if (this.CheckBoxCollectionDate.IsChecked == true) this.AppendToEventDescription(m_InfoEventComment, "Collection Date");
			}
			return res;
		}

		private bool CreateMissingSignature(StringBuilder result)
		{
			bool res = false;
			this.m_TestName = string.Empty;
			if (this.CheckBoxMissingSignatureSurgical.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureSurgical.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureCytology.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureCytology.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureFlow.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureFlow.Tag.ToString();
			}

			if (this.CheckBoxMissingSignatureMolecular.IsChecked == true)
			{
				this.m_TestName = this.CheckBoxMissingSignatureMolecular.Tag.ToString();
			}
			if (this.m_TestName.Length > 0)
			{
				MissingSignatureLetterBody missingSignatureLetterBody = new MissingSignatureLetterBody();
				missingSignatureLetterBody.GetLetterBody(result, this.m_TestName, this.m_PatientNameWithBirthDate, this.m_ClientOrder.ProviderName);
				this.m_FormatBold = true;
			}
			return res;
		}
	}
}