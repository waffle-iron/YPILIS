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
	public partial class GrossEntryPage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_PanelSetOrderSurgical;
		private string m_PageHeaderText;

        public GrossEntryPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
			this.m_ObjectTracker = objectTracker;
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderSurgical = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();

			this.m_PageHeaderText = this.m_PanelSetOrderSurgical.ReportNo + ": " + 
                accessionOrder.PFirstName + " " + accessionOrder.PLastName;

			InitializeComponent();

            this.DataContext = this;                     
		}        

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}


		public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder PanelSetOrderSurgical
        {
			get { return this.m_PanelSetOrderSurgical; }
        }

		private void ButtonLink_Click(object sender, RoutedEventArgs e)
		{
            this.Save();
		}		

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.Next != null) this.Next(this, new EventArgs());
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