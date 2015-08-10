﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
	[CollectionDataContract]
	public class ICD9BillingCodeCollection : ObservableCollection<ICD9BillingCode>
	{
		public ICD9BillingCodeCollection()
        {

		}
	}
}
