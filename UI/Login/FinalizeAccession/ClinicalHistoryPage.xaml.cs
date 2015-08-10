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
	/// Interaction logic for ClinicalHistoryPage.xaml
	/// </summary>
	public partial class ClinicalHistoryPage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private string m_PageHeaderText;

		public ClinicalHistoryPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ObjectTracker = objectTracker;
			this.m_PageHeaderText = accessionOrder.MasterAccessionNo + ": " +
				accessionOrder.PFirstName + " " + accessionOrder.PLastName;

			InitializeComponent();

            this.Loaded += new RoutedEventHandler(ClinicalHistoryPage_Loaded);
			this.DataContext = this;
		}

        private void ClinicalHistoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxClinicalHistory.Focus();
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			this.Return(this, args);
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
