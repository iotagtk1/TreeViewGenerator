using System;
using System.CodeDom;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

    /*
     * MySql 
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


            } catch(Exception e) {
                Console.WriteLine("DBに接続できない" + e.Message);
                throw;
            }
        }

        public List<T> _Req<T>(T kata, string sql) where T : class
        {
            List<T> array = new List<T>();
            try {
                con.Open();
                var result = con.Query<T>(sql);
               
                foreach (var item in result)
                {
                    array.Add(item);
                }
                
                con.Close();
                
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
                con.Open();
                
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
                await con.OpenAsync();
                var result = await con.QueryAsync<T>(sql);
                await con.CloseAsync();
                
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
                
                con.Open();
                var result = con.Insert<T>(val);
                con.Close();

                return result;


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
                
                await con.OpenAsync();
                var result = await con.InsertAsync<T>(val);
                await con.CloseAsync();

                return result;
            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public object _Update<T>(T val) where T : class
        {
            try {
                
                 con.Open();
                 var result = con.Update<T>(val);
                 con.Close();

                 return result;
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
            try {
                
                await con.OpenAsync(); 
                var result = await con.UpdateAsync<T>(val);
                await con.CloseAsync();

                return result;

            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }

        public object _Delete<T>(T val) where T : class
        {
            try {
                 con.Open();
                 var result = con.Delete<T>(val);
                 con.Close();
                 return result;

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
                await con.OpenAsync(); 
                var result =await con.DeleteAsync<T>(val);
                await con.CloseAsync();
                return result;

            } catch(Exception en) {
                Console.WriteLine(en.Message);
                throw;
            }
            return -1;
        }
        
        public Int64 _lastId() {
            try {

                var sql ="SELECT last_insert_id() as lastId;";
                con.Open();
                var result = con.QueryFirst(sql);
                con.Close();
                
                return result.lastId;

            } catch(Exception en) {
                Console.WriteLine(en.Message);
            }
            return -1;
        }


    }
