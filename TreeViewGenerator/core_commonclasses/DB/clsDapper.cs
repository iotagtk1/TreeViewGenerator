using System;
using System.Data;
using Dapper;
using DapperExtensions.Sql;

/*
 * DateTimeOffset、Guid、TimeSpan 変換する時に使う
 *
public class testMap : ClassMapper<test>
{
    public testMap()
    {
        Table("test");
        AutoMap();
        Map(f => f.tes1Tes2).Column("tes1_tes2");
    }
}

//mapperを指定する
 DapperExtensions.DapperExtensions.DefaultMapper = typeof (testMap);
 
  DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
 {
     typeof (testMap).Assembly,
     typeof (testMap).Assembly
 });
 * 
 */

public class clsDapper
{

    public static void _initSqlite()
    {
        //Dapperの_の入ったカラム名を有効にする。Extensionは別
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
  
        DapperExtensions.DapperExtensions.SqlDialect = new DapperExtensions.Sql.SqliteDialect();
    }
    public static void _initMySql()
    {
        //Dapperの_の入ったカラム名を有効にする。Extensionは別
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
  
        DapperExtensions.DapperExtensions.SqlDialect = new DapperExtensions.Sql.MySqlDialect();
    }

    public static void _addTypeHandler()
    {
        //SqlMapper.AddTypeHandler(new DateTimeHandler());
        //SqlMapper.AddTypeHandler(new DateTimeHandler());  
        // SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        //SqlMapper.AddTypeHandler(new GuidHandler());
        // SqlMapper.AddTypeHandler(new TimeSpanHandler());
    }

}

public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = value;
    }
    public override DateTime Parse(object value)
    {
        DateTime dTime = DateTime.Parse((string)value);
        return dTime;
    }
}


abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    // Parameters are converted by Microsoft.Data.Sqlite
    public override void SetValue(IDbDataParameter parameter, T value)
        => parameter.Value = value;
}



class DateTimeOffsetHandler : SqliteTypeHandler<DateTimeOffset>
{
    public override DateTimeOffset Parse(object value)
        => DateTimeOffset.Parse((string)value);
}

class GuidHandler : SqliteTypeHandler<Guid>
{
    public override Guid Parse(object value)
        => Guid.Parse((string)value);
}

class TimeSpanHandler : SqliteTypeHandler<TimeSpan>
{
    public override TimeSpan Parse(object value)
        => TimeSpan.Parse((string)value);
}
