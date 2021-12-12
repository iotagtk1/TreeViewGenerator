using System;
using System.CodeDom;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

    /*
     * SQLitePCLRaw.bundle_e_sqlite3
     * Dapper
     *
     *
     *  clsIniFile._sharedObject(configFileName);
        clsSqliteM._sharedObject(clsIniFile.singlton["config", "dbPath"]);
        clsSqliteM.singleton._mkForeign_key();     
        clsDapper._init();
        string sql = "Select * from otherUser Where effective = '1' order by create_date asc ; ;";
        List<OtherUser> OtherUserResult = clsSqliteM.singleton._ReqAsync<OtherUser>( new OtherUser() , sql:sql);
        clsSqliteM.singleton._UpdateAsync<OtherUser>(OwnerUser1);
        clsSqliteM.singleton._DeleteAsync<OtherUser>(OwnerUser1);
        clsSqliteM.singleton._InsertAsync<OtherUser>(OwnerUser1);
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
                
                con.Close();
                con.Open();

            } catch(Exception en) {
                Console.WriteLine(en.Message);
                Console.WriteLine(sql);
                throw;
            }
            return table;
        }

        public async Task<List<T>> _ReqAsync<T>(T kata , string sql) where T : class
        {
            List<T> array = new List<T>();
            try {
                var result = await con.QueryAsync<T>(sql);
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
        
        public async Task<object> _InsertAsync<T>(T val) where T : class
        {
            try {
                return await con.InsertAsync<T>(val);
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
        
        public async Task<object> _UpdateAsync<T>(T val) where T : class
        {
            try {
                return await con.UpdateAsync<T>(val);
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
        
        public async Task<object> _DeleteAsync<T>(T val) where T : class
        {
            try {
                return await con.DeleteAsync<T>(val);
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


    }
