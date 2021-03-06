﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCytologySAComments : NonpersistentTableDef
    {
        public NonpersistentTableDefCytologySAComments()
        {
            this.m_TableName = "tblCytologySAComments";
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("CommentID", "int", "11", null, false));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("Comment", "varchar", "1000", "NULL", true));
            this.m_ColumnDefinitions.Add(new NonpersistentColumnDef("ObjectId", "varchar", "50", "NULL", true));

            this.SetKeyField("CommentID");
            this.SetSelectStatement();
            this.SetInsertColumnsStatement();
            this.IsAutoIncrement = true;
        }
    }
}
