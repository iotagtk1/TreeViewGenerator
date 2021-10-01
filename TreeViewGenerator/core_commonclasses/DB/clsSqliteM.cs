using System;
using System.CodeDom;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

    /*
     *
     * List<OwnerUser> otherUserStore = clsSqliteM.singleton._ReqAsync<OwnerUser>( new OwnerUser() , sql:sql);
     * 
     */

    public class clsSqliteM
    {
        
        public static clsSqliteM singleton;
        
        public static clsSqliteM _sharedObject(string path) {
            if (singleton == null) {
                singleton = new clsSqliteM(path);
            }
            return singleton;
        }

        public static void _remove()
        {
            singleton = null;
        }

        public SqliteConnection con = null;
        public clsSqliteM(string path) {

            try {
                
                SqliteConnectionStringBuilder b = new SqliteConnectionStringBuilder();
                b.DataSource = path;
            
                con = new SqliteConnection(b.ConnectionString);
                
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                
                DapperExtensions.DapperExtensions.SqlDialect = new DapperExtensions.Sql.SqliteDialect();
                
                con.Open();
                
            } catch(Exception e) {
                Console.WriteLine("DBに接続できない" + e.Message);
                throw;
            }
        }

        public List<T> _Req<T>(T kata, string sql) where T : class
        {
            List<T> array = new List<T>();
            try {
                var result = con.Query<T>(sql);
                foreach (var item in result)
                {
                    array.Add(item);
                }
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return array;
        }

        public DataTable _ReqDynamic(string sql,List<string> columnName)
        {
            DataTable table = null;
            try {
                var reader = con.ExecuteReader(sql);
                
                table = new DataTable();

                while (reader.Read())
                {

                    if (table.Columns.Count == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            if (columnName.Contains(name))
                            {
                                table.Columns.Add(name);
                            }
                        }
                    }
                  
                    DataRow dr = table.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string name = reader.GetName(i);
                        if (columnName.Contains(name))
                        {
                            dr[name] = reader.GetValue(i);
                        }
                    }
                    table.Rows.Add(dr);
                    
                }

            } catch(Exception en) {
                Console.WriteLine(en.Message);
                Console.WriteLine(sql);
                throw;
            }
            return table;
        }

        public List<T> _ReqAsync<T>(T kata , string sql) where T : class
        {
            List<T> array = new List<T>();
            try {
                var result = con.QueryAsync<T>(sql);
                foreach (var item in result.Result)
                {
                    array.Add(item);
                }
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return array;
        }
        
        public object _Insert<T>(T val) where T : class
        {
            try {
                return con.Insert<T>(val);
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public object _InsertAsync<T>(T val) where T : class
        {
            try {
                return con.InsertAsync<T>(val);
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public object _Update<T>(T val) where T : class
        {
            try {
                return con.Update<T>(val);
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public object _UpdateAsync<T>(T val) where T : class
        {
            try {
                return con.UpdateAsync<T>(val);
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }

        public object _Delete<T>(T val) where T : class
        {
            try {
                return con.Delete<T>(val);
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public object _DeleteAsync<T>(T val) where T : class
        {
            try {
                return con.DeleteAsync<T>(val);
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public Int64 _lastId() {
            try {

                var sql ="SELECT last_insert_rowid() as lastId;";
 
                Int64 num = con.QueryFirst(sql);

                return num;

            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }
            return -1;
        }
        
        //外部キーを設定する
        public void _mkForeign_key() {
            try {

                con.Open();
                SqliteCommand command = con.CreateCommand();
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();

            } catch (Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
        }


    }
