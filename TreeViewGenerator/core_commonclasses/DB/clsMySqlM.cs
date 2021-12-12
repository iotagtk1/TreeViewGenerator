using System;
using System.CodeDom;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

    /*
     * Microsoft.Data.Sqlite.Core
     * SQLitePCLRaw.bundle_e_sqlite3
     * Dapper
     *
     *
        MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
        b.Server = "localhost";
        b.Port = 3306;
        b.Database = "";
        b.UserID = "";
        b.Password = "";

        clsMySqlM._sharedObject(b);
        clsDapper._init();
        string sql = "Select * from otherUser Where effective = '1' order by create_date asc ; ;";
        List<OtherUser> OtherUserResult = clsMySqlM.singleton._ReqAsync<OtherUser>( new OtherUser() , sql:sql);
        clsMySqlM.singleton._UpdateAsync<OtherUser>(OwnerUser1);
        clsMySqlM.singleton._DeleteAsync<OtherUser>(OwnerUser1);
        clsMySqlM.singleton._InsertAsync<OtherUser>(OwnerUser1);
     */

    public class clsMySqlM
    {
        
        public static clsMySqlM singleton;

        private List<bool> resultArray = new List<bool>();
        public static clsMySqlM _sharedObject(MySqlConnectionStringBuilder connectionBuilder) {
            if (singleton == null) {
                singleton = new clsMySqlM(connectionBuilder);
            }
            return singleton;
        }

        public static void _remove()
        {
            singleton = null;
        }

        public MySqlConnection con = null;
        public clsMySqlM(MySqlConnectionStringBuilder connectionBuilder) {

            try {

                con = new MySqlConnection(connectionBuilder.ConnectionString);
                
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                
                DapperExtensions.DapperExtensions.SqlDialect = new DapperExtensions.Sql.MySqlDialect();
                
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
                
                var r = con.Insert<T>(val);
                resultArray.Add(r);
                return r;
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        /// <summary>
        /// 使用不可
        /// </summary>
        /// <param name="val"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<object> _InsertAsync<T>(T val) where T : class
        {
            try {
                var r = await con.InsertAsync<T>(val);
                resultArray.Add(r);
                return r;
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public object _Update<T>(T val) where T : class
        {
            try {
                
                var r = con.Update<T>(val);
                resultArray.Add(r);
                return r;
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        /// <summary>
        /// 使用不可
        /// </summary>
        /// <param name="val"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<object> _UpdateAsync<T>(T val) where T : class
        {
            try
            {
                var r = await con.UpdateAsync<T>(val);
                resultArray.Add(r);
                return r;
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }

        public object _Delete<T>(T val) where T : class
        {
            try {
                
                var r = con.Delete<T>(val);
                resultArray.Add(r);
                return r; 
                
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        /// <summary>
        /// 使用不可
        /// </summary>
        /// <param name="val"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<object> _DeleteAsync<T>(T val) where T : class
        {
            try {
                
                var r = await con.DeleteAsync<T>(val);
                resultArray.Add(r);
                return r;
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public Int64 _lastId() {
            try {

                var sql ="SELECT last_insert_id() as lastId;";
                var result = con.QueryFirst(sql);
                return result.lastId;

            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }
            return -1;
        }
        
        public void _Open() {
            try {
                con.Open();
            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }
        }
        
        public void _Close() {
            try {
                con.Close();
            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }
        }  
        public MySqlTransaction _Begin() {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = con;
                MySqlTransaction  transaction = con.BeginTransaction();
                resultArray = new List<bool>();
                return transaction;

            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }

            return null;
        }  
        
        public Boolean _doRollBack(MySqlTransaction transaction ,List<bool> resultArray_t) {
            try
            {
                if (resultArray_t.Contains(false))
                {
                    transaction.Rollback();
                    return true;
                }

            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }

            return false;
        }    
        
        public void _Commit(MySqlTransaction transaction) {
            try
            {
                transaction.Commit();

            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }
        }






    }