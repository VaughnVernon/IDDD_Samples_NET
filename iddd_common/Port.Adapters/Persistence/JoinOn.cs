using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace SaaSOvation.Common.Port.Adapters.Persistence
{
    public class JoinOn
    {
        public JoinOn()
        {
        }

        public JoinOn(string leftKey, string rightKey)
        {
            this.leftKey = leftKey;
            this.rightKey = rightKey;
        }

        object currentLeftQualifier;
        string leftKey;
        string rightKey;

        public bool IsSpecified
        {
            get
            {
                return this.leftKey != null && this.rightKey != null;
            }
        }

        public bool HasCurrentLeftQualifier(IDataReader dataReader)
        {
            try
            {
                var columnValue = dataReader.GetValue(dataReader.GetOrdinal(this.leftKey));
                if (columnValue == null)
                {
                    return false;
                }
                return columnValue.Equals(this.currentLeftQualifier);
            }
            catch
            {
                return false;
            }
        }

        public bool IsJoinedOn(IDataReader dataReader)
        {
            var leftColumn = default(object);
            var rightColumn = default(object);
            try
            {
                if (this.IsSpecified)
                {
                    leftColumn = dataReader.GetValue(dataReader.GetOrdinal(this.leftKey));
                    rightColumn = dataReader.GetValue(dataReader.GetOrdinal(this.rightKey));
                }
            }
            catch
            {
                // ignore
            }
            return leftColumn != null && rightColumn != null;
        }

        public void SaveCurrentLeftQualifier(string columnName, object columnValue)
        {
            if (columnName.Equals(this.leftKey))
            {
                this.currentLeftQualifier = columnValue;
            }
        }
    }
}
