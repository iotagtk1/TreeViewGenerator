using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json.Linq;

public static class clsExtensionsJson {


    /// <summary>
    /// パース
    /// </summary>
    static public JObject _toJson(this string jsonStr) {
        JObject json = JObject.Parse(jsonStr);
        return json;
    }

    /// <summary>
    /// パース
    /// </summary>
    static public JObject _getJsonObj(this string jsonStr) {
        JObject json = JObject.Parse(jsonStr);
        return json;
    }

    /// <summary>
    ///  jObjectの中に指定したキーの値を取得する
    /// </summary>
    public static dynamic _getTargetKeyValue(this JObject jObj, string key) {
        
        foreach (JProperty prop in jObj.Properties()) {
            if (prop.Name == key) {
                return prop.Value;
            }
        }
        return null;
    }

    /// <summary>
    ///  jObjectの中にKeyを取得する
    /// </summary>
    public static ArrayList _getKeys(this JObject jObj) {
        ArrayList newArray = new ArrayList();
        foreach (JProperty prop in jObj.Properties()) {
            newArray.Add(prop.Name.ToString());
        }
        return newArray;
    }

    /// <summary>
    ///  jObjectの中にValueをを取得する
    /// </summary>
    public static ArrayList _getValues(this JObject jObj) {
        ArrayList newArray = new ArrayList();
        foreach (JProperty prop in jObj.Properties()) {
            newArray.Add(prop.Value.ToString());
        }
        return newArray;
    }


    /// <summary>
    /// Hashtableを取得する
    /// </summary>
    public static Hashtable _getHashtable(this JObject jObj) {
        Hashtable newArray = new Hashtable();
        foreach (JProperty prop in jObj.Properties()) {
            newArray.Add(prop.Name, prop.Value);
        }
        return newArray;
    }



    /// <summary>
    ///  jObjectの中にKeyが含まれているかどうか
    /// </summary>
    public static int _indexKey(this JObject jObj, string key) {
        int i = -1;
        foreach (JProperty prop in jObj.Properties()) {
            i++;
            if (prop.Name == key) {
                return i;
            }
        }
        return i;
    }


    /// <summary>
    ///  jObjectの中にKeyが含まれているかどうか
    /// </summary>
    public static Boolean _containsKey(this JObject jObj, string key) {
        foreach (JProperty prop in jObj.Properties()) {
            if (prop.Name == key) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    ///  jObjectの中にValueが含まれているかどうか
    /// </summary>
    public static Boolean _containsValue(this JObject jObj, string value) {
        foreach (JProperty prop in jObj.Properties()) {
            if (prop.Value.ToString() == value) {
                return true;
            }
        }
        return false;
    }



    /// <summary>
    ///  Jarrayを結合する
    /// </summary>
    public static String _join(this JArray jarrayTwo,string joinKey) {

        ArrayList array = new ArrayList();
		for(int i=0; i<jarrayTwo.Count; i++){
            array.Add(jarrayTwo[i].ToString());
		}

        string joinStr = array._join(joinKey);

        return joinStr;	
	
	}





 
}


