using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Flow
{
    public partial class MarkerItem
    {
        public MarkerItem()
        {

        }

		public void Save()
		{
			base.SaveOld(this);
		}
	}
}
				