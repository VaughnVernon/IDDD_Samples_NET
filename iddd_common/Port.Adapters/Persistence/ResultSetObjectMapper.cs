using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SaaSOvation.Common.Port.Adapters.Persistence
{
    public class ResultSetObjectMapper<T>
    {
        public ResultSetObjectMapper(IDataReader dataReader, JoinOn joinOn, string columnPrefix = null)
        {
            this.dataReader = dataReader;
            this.joinOn = joinOn;
            this.columnPrefix = columnPrefix;
        }

        readonly IDataReader dataReader;
        readonly JoinOn joinOn;
        readonly string columnPrefix;

        public T MapResultToType()
        {
            var obj = default(T);

            var associationsToMap = new HashSet<string>();

            var fields = typeof(T).GetFields();

            foreach (var field in fields)
            {
                var columnName = FieldNameToColumnName(field.Name);
                var columnIndex = this.dataReader.GetOrdinal(columnName);
                if (columnIndex >= 0)
                {
                    var columnValue = ColumnValueFrom(columnIndex, field.FieldType);

                    this.joinOn.SaveCurrentLeftQualifier(columnName, columnValue);

                    field.SetValue(obj, columnValue);
                }
                else
                {
                    var objectPrefix = ToObjectPrefix(columnName);
                    if (!associationsToMap.Contains(objectPrefix) && HasAssociation(objectPrefix))
                    {
                        associationsToMap.Add(field.Name);
                    }
                }
            }

            if (associationsToMap.Count > 0)
            {
                MapAssociations(obj, associationsToMap);
            }

            return obj;
        }

        void MapAssociations(object obj, ISet<string> associationsToMap)
        {
            var mappedCollections = new Dictionary<string, ICollection<object>>();
            while (this.dataReader.NextResult())
            {
                foreach (var fieldName in associationsToMap)
                {
                    var associationField = typeof(T).GetField(fieldName);
                    var associationFieldType = default(Type);
                    var collection = default(ICollection<object>);

                    if (typeof(IEnumerable).IsAssignableFrom(associationField.FieldType))
                    {
                        if (!mappedCollections.TryGetValue(fieldName, out collection))
                        {
                            collection = CreateCollectionFrom(associationField.FieldType);
                            mappedCollections.Add(fieldName, collection);
                            associationField.SetValue(obj, collection);
                        }

                        var genericType = associationField.FieldType.GetGenericTypeDefinition();
                        associationFieldType = genericType.GetGenericArguments()[0];
                    }
                    else
                    {
                        associationFieldType = associationField.FieldType;
                    }

                    var columnName = FieldNameToColumnName(fieldName);

                    var mapper = new ResultSetObjectMapper<object>(this.dataReader, this.joinOn, ToObjectPrefix(columnName));

                    var associationObject = mapper.MapResultToType();

                    if (collection != null)
                    {
                        collection.Add(associationObject);
                    }
                    else
                    {
                        associationField.SetValue(obj, associationObject);
                    }
                }

            }
        }

        /// <summary>
        /// TODO: ensure correctness
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ICollection<object> CreateCollectionFrom(Type type)
        {
            var genericType = type.GetGenericTypeDefinition();
            if (typeof(IList<>).IsAssignableFrom(genericType))
            {
                return new List<object>();
            }
            else if (typeof(ISet<>).IsAssignableFrom(genericType))
            {
                return new HashSet<object>();
            }
            else
            {
                return null;
            }
        }

        bool HasAssociation(string objectPrefix)
        {
            var fieldCount = this.dataReader.FieldCount;
            for (var i = 0; i < fieldCount; i++)
            {
                var columnName = this.dataReader.GetName(i);
                if (columnName.StartsWith(objectPrefix) && this.joinOn.IsJoinedOn(this.dataReader))
                {

                    return true;
                }
            }
            return false;
        }

        string ToObjectPrefix(string columnName)
        {
            return "o_" + columnName + "_";
        }

        object ColumnValueFrom(int columnIndex, Type columnType)
        {
            switch (Type.GetTypeCode(columnType))
            {
                case TypeCode.Int32:
                    return this.dataReader.GetInt32(columnIndex);
                case TypeCode.Int64:
                    return this.dataReader.GetInt64(columnIndex);
                case TypeCode.Boolean:
                    return this.dataReader.GetBoolean(columnIndex);
                case TypeCode.Int16:
                    return this.dataReader.GetInt16(columnIndex);
                case TypeCode.Single:
                    return this.dataReader.GetFloat(columnIndex);
                case TypeCode.Double:
                    return this.dataReader.GetDouble(columnIndex);
                case TypeCode.Byte:
                    return this.dataReader.GetByte(columnIndex);
                case TypeCode.Char:
                    return this.dataReader.GetChar(columnIndex);
                case TypeCode.String:
                    return this.dataReader.GetString(columnIndex);
                case TypeCode.DateTime:
                    return this.dataReader.GetDateTime(columnIndex);
                default:
                    throw new InvalidOperationException("Unsupported type.");
            }
        }

        string FieldNameToColumnName(string fieldName)
        {
            var sb = new StringBuilder();

            if (this.columnPrefix != null)
            {
                sb.Append(this.columnPrefix);
            }

            foreach (var ch in fieldName)
            {
                if (char.IsLetter(ch) && char.IsUpper(ch))
                {
                    sb.Append('_').Append(char.ToLower(ch));
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

    }
}
