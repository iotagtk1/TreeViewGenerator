using System;
using System.CodeDom;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/*
 * MySql
 * Dapper
 * DapperExtension
 *
    MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
    b.Server = "localhost";
    b.Port = 3306;
    b.Database = "";
    b.UserID = "";
    b.Password = "";
       
    public IConfigurationRoot Configuration;  
       
    configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    clsMySqlM._sharedObject(b);
    
    MySqlConnectionStringBuilder conSetting = new MySqlConnectionStringBuilder();
    conSetting.Server = configuration["Server"];
    conSetting.Port = Convert.ToUInt32(configuration["Port"]);
    conSetting.Database = configuration["Database"];
    conSetting.UserID = configuration["UserID"];
    conSetting.Password = configuration["Password"];  
    clsMySqlM._sharedObject(conSetting);

    clsDapper._initMySql();
    string sql = "Select * from otherUser Where effective = '1' order by create_date asc ; ;";
    List<OtherUser> OtherUserResult = clsMySqlM.singleton._ReqAsync<OtherUser>( new OtherUser() , sql:sql);
    clsMySqlM.singleton._UpdateAsync<OtherUser>(OwnerUser1);
    clsMySqlM.singleton._DeleteAsync<OtherUser>(OwnerUser1);
    clsMySqlM.singleton._InsertAsync<OtherUser>(OwnerUser1);
 */

public class clsMySqlM
{
        
    public static clsMySqlM singleton;

    private Dictionary<long, List<bool>> resultDic = new Dictionary<long, List<bool>>();

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

            if (con.State == ConnectionState.Connecting || con.State == ConnectionState.Open)
            {
                Console.WriteLine("DBに接続しました" ); 
            }
            
            Console.WriteLine(con.State);
            
                
        } catch(Exception e) {
            Console.WriteLine("DBに接続できない" + e.Message);        
        }
    }

    public List<T> _Req<T>(string sql) where T : class
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

    public async Task<List<T>> _ReqAsync<T>(string sql) where T : class
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
        }
        return array;
    }
        
    public object _Insert<T>(T val,long timeStamp = 0) where T : class
    {
        try {
                
            var r = con.Insert<T>(val);
            if (timeStamp != 0)
            {
                List<bool> resultArray = resultDic[timeStamp];
                resultArray.Add(r);
            }
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
    public async Task<object> _InsertAsync<T>(T val,long timeStamp = 0) where T : class
    {
        try {
            var r = await con.InsertAsync<T>(val);
            if (timeStamp != 0)
            {
                List<bool> resultArray = resultDic[timeStamp];
                resultArray.Add(r);
            }
            return r;
        } catch(Exception en) {
            Console.WriteLine(en.Message);
        }
        return -1;
    }
        
    public object _Update<T>(T val,long timeStamp = 0) where T : class
    {
        try {
                
            var r = con.Update<T>(val);
            if (timeStamp != 0)
            {
                List<bool> resultArray = resultDic[timeStamp];
                resultArray.Add(r);
            }
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
    public async Task<object> _UpdateAsync<T>(T val,long timeStamp = 0) where T : class
    {
        try
        {
            var r = await con.UpdateAsync<T>(val);
            if (timeStamp != 0)
            {
                List<bool> resultArray = resultDic[timeStamp];
                resultArray.Add(r);
            }
            return r;
        } catch(Exception en) {
            Console.WriteLine(en.Message);
        }
        return -1;
    }

    public object _Delete<T>(T val,long timeStamp = 0) where T : class
    {
        try {
                
            var r = con.Delete<T>(val);
            if (timeStamp != 0)
            {
                List<bool> resultArray = resultDic[timeStamp];
                resultArray.Add(r);
            }
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
    public async Task<object> _DeleteAsync<T>(T val,long timeStamp = 0) where T : class
    {
        try {
                
            var r = await con.DeleteAsync<T>(val);
            if (timeStamp != 0)
            {
                List<bool> resultArray = resultDic[timeStamp];
                resultArray.Add(r);
            }
                
            return r;
        } catch(Exception en) {
            Console.WriteLine(en.Message);
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

    public  Dictionary<long,MySqlTransaction> _Begin() {
        try
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            MySqlTransaction  transaction = con.BeginTransaction();

            long timeStamp = new DateTime()._mkUnixTimeStamp();

            Dictionary<long, MySqlTransaction> returnDic = new Dictionary<long, MySqlTransaction>();
               
            resultDic.Add(timeStamp,new List<bool>());
                
            returnDic.Add(timeStamp,transaction);
     
            return returnDic;

        } catch(Exception en) {
            Console.WriteLine(en.Message);
        }

        return null;
    }  
        
    public Boolean _doRollBack(MySqlTransaction transaction,long timeStamp = 0) {
        try
        {
            List<bool> resultArray = resultDic[timeStamp];
                
            if (resultArray.Contains(false))
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public List<object> _Req(string sql) 
    {
        List<object> array = new List<object>();
        try {
            var result = con.Query(sql);
            foreach (var item in result)
            {
                array.Add(item);
            }
        } catch(Exception en) {
            Console.WriteLine(en.Message);
        }
        return array;
    }

    /// <summary>
    /// Count
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public int _Count(string tableName) 
    {
        try
        {
            string sql = string.Format(" select count(*) from {0};",tableName);
            var result = con.Query(sql);
            if (result != null)
            {
                foreach (var item in result)
                {
                    return item;
                }
            }
            return 0;
        } catch(Exception en) {
            Console.WriteLine(en.Message);
        }
        return 0;
    }
    
    public Boolean _DeleteAllRow(string tableName) 
    {
        try
        {
            string sql = string.Format(" delete from {0};",tableName);
            var command = new MySqlCommand();
            command.Connection = con;
            command.CommandText = sql;
            var result = command.ExecuteNonQuery();
            if (result != 1)
            {
                Console.WriteLine("データが削除できませんでした。");
                return false;
            }
            return true;
        } catch(Exception en) {
            Console.WriteLine(en.Message);
        }
        return false;
    }
    
    
    public Boolean _InsertCommand(string tableName,string sql) 
    {
        try
        {
           
            var command = new MySqlCommand();
            command.Connection = con;
            command.CommandText = sql;
            var result = command.ExecuteNonQuery();
            if (result != 1)
            {
                Console.WriteLine("データを処理しました。");
                return false;
            }
            return true;
        } catch(Exception en) {
            Console.WriteLine(en.Message);
        }
        return false;
    }
    
    

}