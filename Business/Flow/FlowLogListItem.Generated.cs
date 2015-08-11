using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YellowstonePathology.Persistence;

namespace YellowstonePathology.Business.Flow
{
	public partial class FlowLogListItem
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in FlowLogListItem");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in FlowLogListItem");
		}
		#endregion

		#region Fields
		private string m_ObjectId;
		private string m_ReportNo;
		private string m_PLastName;
		private string m_PFirstName;
		private Nullable<DateTime> m_AccessionDate;
		private Nullable<DateTime> m_FinalDate;
		private string m_TestName;
		#endregion

		#region properties
		[PersistentDocumentIdProperty]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set { this.m_ObjectId = value; }
		}

		[PersistentProperty]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set { this.m_ReportNo = value; }
		}

		[PersistentProperty]
		public string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if (value != this.m_PLastName)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
				}
			}
		}

		[PersistentProperty]
		public string PFirstName
		{
			get { return this.m_PFirstName; }
			set
			{
				if (value != this.m_PFirstName)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
				}
			}
		}

		[PersistentProperty]
		public Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				if (value != this.m_AccessionDate)
				{
					this.m_AccessionDate = value;
					this.NotifyPropertyChanged("AccessionDate");
				}
			}
		}

		[PersistentProperty]
		public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
			set
			{
				if (value != this.m_FinalDate)
				{
					this.m_FinalDate = value;
					this.NotifyPropertyChanged("FinalDate");
				}
			}
		}

		[PersistentProperty]
		public string TestName
		{
			get { return this.m_TestName; }
			set
			{
				if (value != this.m_TestName)
				{
					this.m_TestName = value;
					this.NotifyPropertyChanged("TestName");
				}
			}
		}
		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_PLastName = propertyWriter.WriteString("PLastName");
			this.m_PFirstName = propertyWriter.WriteString("PFirstName");
			this.m_AccessionDate = propertyWriter.WriteNullableDateTime("AccessionDate");
			this.m_FinalDate = propertyWriter.WriteNullableDateTime("FinalDate");
			this.m_TestName = propertyWriter.WriteString("TestName");
		}
		#endregion
	}
}