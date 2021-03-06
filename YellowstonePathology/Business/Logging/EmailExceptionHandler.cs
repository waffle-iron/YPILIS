﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Logging
{
    public class EmailExceptionHandler
    {        
        public static void HandleException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", "Sid.Harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, e.Exception.ToString());
            message.To.Add("william.copland@ypii.com");
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);    
        }

        public static void HandleException(string message)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage("support@ypii.com", "Sid.Harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, message);
            mailMessage.To.Add("william.copland@ypii.com");
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(mailMessage);
        }

        public static void HandleException(Business.Test.PanelSetOrder panelSetOrder, string errorMessage)
        {
            string message = "ReportNo: " + panelSetOrder.ReportNo + ", Test: " + panelSetOrder.PanelSetName + Environment.NewLine + errorMessage;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage("support@ypii.com", "Sid.Harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, message);
            mailMessage.To.Add("william.copland@ypii.com");
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(mailMessage);
        }
    }
}
