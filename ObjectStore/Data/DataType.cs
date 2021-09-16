using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectStore.Data
{
    internal class DataType
    {
        public static string GetTypeName(Type t)
        {
            switch (t.Name)
            {
                case "DateTime":
                    return "DATETIME";
                case "Decimal":
                    return "DECIMAL";
                case "Int16":
                    return "SMALLINT";
                case "UInt16":
                    return "SMALLUINT";
                case "Int32":
                    return "INT";
                case "UInt32":
                    return "UINT";
                case "Int64":
                    return "INTEGER";
                case "UInt64":
                    return "UNSIGNEDINTEGER";
                case "Binary":
                    return "BLOB";
                case "Boolean":
                    return "Boolean";
                case "String":
                    return "TEXT";
                case "Double":
                    return "REAL";
                case "Single":
                    return "SINGLE";
                case "Byte":
                    return "TINYINT";
                case "SByte":
                    return "TINYSINT";
                case "Guid":
                    return "UNIQUEIDENTIFIER";
                case "Enum":
                    return "INT";
                default:
                    return "Object";
            }
        }
    }
}
