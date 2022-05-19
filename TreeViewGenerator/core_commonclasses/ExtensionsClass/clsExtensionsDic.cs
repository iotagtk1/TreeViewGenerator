using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

public static partial class DicExtensions {
    
    //並び替えはSortedDictionaryとOrderedDictionaryを使うこと
    
    /// <summary>
    /// remove
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    static public void _remove<T1,T2>(this SortedDictionary<T1,T2> dic,int index, int length)
    {
        List<T1> keyArray = new List<T1>();
        int i = 0;
        foreach (KeyValuePair<T1,T2> item in dic)
        {
            if (i >= index && i < index + length)
            {
                keyArray.Add(item.Key);
            }
            i++;
        }
        foreach (T1 key in keyArray)
        {
            dic.Remove(key);
        }
    }
    
    /// <summary>
    /// getObj
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    static public SortedDictionary<T1,T2> _getObj<T1,T2>(this SortedDictionary<T1,T2> dic,int index, int length)
    {
        SortedDictionary<T1,T2> keyValueArray = new SortedDictionary<T1,T2>();
        
        int i = 0;
        foreach (KeyValuePair<T1,T2> item in dic)
        {
            if (i >= index && i < index + length)
            {
                keyValueArray.Add(item.Key,item.Value);
            }
            i++;
        }

        return keyValueArray;
    }

    /// <summary>
    /// FirstValueを取得する
    /// </summary>
    /// <param name="dic"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    static public T2 _getFirstValue<T1,T2>(this SortedDictionary<T1,T2> dic)
    {
        if (dic.Values.Count > 0)
        {
            foreach (var VARIABLE in dic.Values)
            {
                return VARIABLE;
            }
        }
        return default(T2);
    }

    /// <summary>
    /// getKey
    /// </summary>
    /// <param name="dic"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    static public object _getFirstKey<T1,T2>(this Dictionary<T1,T2> dic)
    {
        if (dic.Keys.Count > 0)
        {
            foreach (var VARIABLE in dic.Keys)
            {
                return VARIABLE;
            }
        }
        return null;
    } 
   
    /// <summary>
    /// getValue
    /// </summary>
    /// <param name="dic"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    static public T2 _getFirstValue<T1,T2>(this Dictionary<T1,T2> dic)
    {
        if (dic.Values.Count > 0)
        {
            foreach (var VARIABLE in dic.Values)
            {
                return VARIABLE;
            }
        }
        return default(T2);
    }

}