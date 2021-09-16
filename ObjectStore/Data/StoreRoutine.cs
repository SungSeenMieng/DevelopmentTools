using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;

namespace ObjectStore.Data
{
    internal class StoreRoutine
    {
        public static void CreateTable(SQLiteConnection Connection, Type type)
        {
                string queryString = "CREATE TABLE IF NOT EXISTS " + type.FullName.Replace(".", "_") + "( [tkid] INTEGER PRIMARY KEY autoincrement NOT NULL";
                PropertyInfo[] props = type.GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    queryString += ", ";
                    string PropName = DataType.GetTypeName(props[i].PropertyType);
                    if (PropName == "Object")
                    {
                        queryString += props[i].Name + " TEXT";
                    }
                    else
                    {
                        queryString += props[i].Name + " " + PropName;
                    }
                }
                queryString += "  ) ";
                SQLiteCommand dbCommand = Connection.CreateCommand();
                dbCommand.CommandText = queryString;
                SQLiteDataReader dataReader = dbCommand.ExecuteReader();
        }

        public static int InsertObject(SQLiteConnection Connection, object obj)
        {
            Type type = obj.GetType();
            string queryString = "INSERT INTO " + type.FullName.Replace(".", "_") + " VALUES(NULL";
            PropertyInfo[] props = type.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                queryString += ", ";
                string PropName = ObjectStore.Data.DataType.GetTypeName(props[i].PropertyType);
                object PropValue = props[i].GetValue(obj);
                if (PropName == "Object")
                {
                    queryString += "'" + Newtonsoft.Json.JsonConvert.SerializeObject(PropValue).Replace("\"", "\\\"") + "'";
                }
                else
                {
                    if (PropValue == null)
                    {
                        queryString += "''";
                    }
                    else
                    {
                        queryString += "'" + PropValue.ToString() + "'";
                    }
                }
            }
            queryString += "); SELECT last_insert_rowid();";
            SQLiteCommand dbCommand = Connection.CreateCommand();
            dbCommand.CommandText = queryString;
            SQLiteDataReader dataReader = dbCommand.ExecuteReader();
            int key = -1;
            if (dataReader.Read())
            {
                key = dataReader.GetInt32(0);
            }
            return key;
        }
        public static List<T> SearchObject<T>(SQLiteConnection Connection, string Table, string condition) where T : new()
        {
            Type type = typeof(T);
            string queryString = "SELECT * FROM " + Table;
            if (!String.IsNullOrEmpty(condition))
            {
                queryString += " WHERE " + condition;
            }
            SQLiteCommand dbCommand = Connection.CreateCommand();
            dbCommand.CommandText = queryString;
            SQLiteDataReader dataReader = dbCommand.ExecuteReader();
            List<T> list = new List<T>();
            
            while (dataReader.Read())
            {
                T obj = new T();
                for (int i = 1; i < dataReader.FieldCount; i++)
                {
                    var val = dataReader.GetValue(i);
                    string PropertyName = dataReader.GetName(i);
                    Type PropertyType = dataReader.GetFieldType(i);
                    PropertyInfo property = type.GetProperty(PropertyName);
                    if (property != null)
                    {
                        string PropName = ObjectStore.Data.DataType.GetTypeName(property.PropertyType);
                        if (PropName == "Object")
                        {
                            PropertyName = PropertyName.Split('_')[PropertyName.Split('_').Length - 1];
                            property = type.GetProperty(PropertyName);
                            var function = typeof(JsonConvert).GetMethods()[38];
                            var convert = function.MakeGenericMethod(property.PropertyType);
                            object[] param = new object[] { val.ToString().Replace("\\\"", "\"") };
                            var result = convert.Invoke(null, param);
                            property.SetValue(obj, result);
                        }
                        else
                        {
                            property.SetValue(obj, val);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static int StoreObject<T>(SQLiteConnection Connection, T obj)
        {
            try
            {
                CreateTable(Connection, typeof(T));
                return InsertObject(Connection, obj);
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        public static List<T> GetObject<T>(SQLiteConnection Connection, string condition) where T : new()
        {
            try
            {
                //CreateTable(Connection, typeof(T));
                return SearchObject<T>(Connection, typeof(T).FullName.Replace('.', '_'), condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int DeleteObject(SQLiteConnection Connection, string Table, string condition)
        {
            try
            {
                string queryString = "DELETE FROM " + Table;
                if (condition != "*")
                {
                    queryString += " WHERE " + condition;
                }
                SQLiteCommand dbCommand = Connection.CreateCommand();
                dbCommand.CommandText = queryString;
                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
