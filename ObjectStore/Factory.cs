using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace ObjectStore
{
    public class Factory:IDisposable
    {
        public string FactoryPath { get; set; }
        private string connectString { get; set; }
        SQLiteConnection Connection;
        public Factory(string dbPath)
        {
            this.FactoryPath = dbPath;

            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }
            connectString = string.Format("Data Source={0}", dbPath);
            Connection = new SQLiteConnection(connectString);
            Connection.Open();
        }
        public int Store<T>(T obj)where T:class,new()
        {
            if (obj is IEnumerable)
            {
                return -1;
            }
            return Data.StoreRoutine.StoreObject<T>(Connection, obj);
        }
        public List<T> Get<T>(string condition="")where T:class, new()
        {
            if (typeof(T) is IEnumerable)
            {
                return null;
            }
           
            return Data.StoreRoutine.GetObject<T>(Connection, condition);
        }
        public int Remove<T>(string condition) where T : class, new()
        {
            if (typeof(T) is IEnumerable)
            {
                return -1;
            }
            if (string.IsNullOrEmpty(condition) || string.IsNullOrWhiteSpace(condition))
            {
                return -1;
            }
            string tablename = typeof(T).FullName.Replace(".", "_");
            return Data.StoreRoutine.DeleteObject(Connection, tablename,condition);
        }
        public int Alter<T>(string condition,T obj) where T : class, new()
        {
            int count = Remove<T>(condition);
            if (count> -1)
            {
                for (int i = 0; i < count; i++)
                {
                    int ret = Store<T>(obj);
                    if (ret == -1)
                    {
                        return ret;
                    }
                }
                return count;
            }
            return -1;
        }
        public void Dispose()
        {
            Connection.Shutdown();
            Connection.Close();
        }
    }
}
