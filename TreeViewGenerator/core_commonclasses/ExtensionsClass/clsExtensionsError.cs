using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.Diagnostics;



public static partial class ErrorExtensions {


    /// <summary>
    /// _getErrorLine
    /// </summary>
    public static string _getErrorLine(this Exception ex) {
        var st = new StackTrace(ex, true);
        var frame = st.GetFrame(0);
        string line = frame.GetFileLineNumber().ToString();

		Console.WriteLine("エラー行　" + line + "" );
        return line + "行";
    }

    /// <summary>
    /// _getErrorLineMessage
    /// </summary>
    public static string _getErrorLineMessage(this Exception ex) {
        var st = new StackTrace(ex, true);
        var frame = st.GetFrame(0);
       // string line = frame.GetFileLineNumber().ToString();

		//一つ前のスタックを取得
		StackFrame callerFrame = new StackFrame(1);

        if (callerFrame.GetMethod() != null) {
            //メソッド名
            string methodName = callerFrame.GetMethod().Name;
            //クラス名
            string className = callerFrame.GetMethod().ReflectedType.FullName;

            int line = callerFrame.GetFileLineNumber();

            Console.WriteLine("エラー クラス名 " + className + " " + " メソッド名 " + methodName + " " + line + "行" + "  " + ex.Message);
            Console.WriteLine("エラー行　" + line + "");
        }

        StackFrame callerFrame2 = new StackFrame(2);

        if (callerFrame2.GetMethod() != null) {
            //メソッド名
            string methodName2 = callerFrame2.GetMethod().Name;
            //クラス名
            string className2 = callerFrame2.GetMethod().ReflectedType.FullName;

            int line2 = callerFrame2.GetFileLineNumber();

            Console.WriteLine("エラー クラス名 " + className2 + " " + " メソッド名 " + methodName2 + " " + line2 + "行" + "  " + ex.Message);
            Console.WriteLine("エラー行　" + line2 + "");
        }

    

        return ex.Message;
    }


}


