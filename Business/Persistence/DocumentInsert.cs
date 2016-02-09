﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentInsert : Document
    {
        public DocumentInsert(DocumentId documentId, object value) 
            : base (documentId, false)
        {
            this.m_Value = value;            
        }

        public override YellowstonePathology.Business.Persistence.SubmissionResult Submit()
        {            
            PersistentClass persistentClassAttribute = (PersistentClass)this.m_Type.GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            PropertyInfo keyProperty = this.m_Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(this.m_Value, null);            

            InsertCommandBuilder insertCommandBuilder = new InsertCommandBuilder();
            insertCommandBuilder.Build(this.m_Value, objectSubmitter.SqlInsertCommands, objectSubmitter.SqlInsertLastCommands);
            return objectSubmitter.SubmitChanges();
        }        
    }
}
