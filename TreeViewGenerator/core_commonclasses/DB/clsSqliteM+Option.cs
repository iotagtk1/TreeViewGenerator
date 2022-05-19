using System;
using System.CodeDom;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

public partial class clsSqliteM
{
    /// <summary>
    ///  最初の１行を追加し取得する
    /// </summary>
    /// <param name="tableName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> _mkFirstRow<T>(string tableName) where T : class
    {
        string sql = string.Format("select * from {0};", tableName);
        List<T> resultArray = _Req<T>(sql);
        if (resultArray.Count == 0)
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            _Insert<T>((T)obj);
        }
        resultArray = _Req<T>(sql);
  
        return resultArray;
    }
   

}