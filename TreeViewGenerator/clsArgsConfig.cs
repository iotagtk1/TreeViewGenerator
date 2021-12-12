using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace TreeViewGenerator
{
    public class clsArgsConfig
    {
        private static clsArgsConfig _singleInstance = null;
        public string ProjectName = "";
        public string FileDirPath = "";
        public Boolean isDbType_Sqlite = true;
        
        public string MySql_DataBase = "";
        public string MySql_UserId = "";  
        public string MySql_Password = "";  

        public static clsArgsConfig Instance()
        {
            if (_singleInstance == null) {
                _singleInstance = new clsArgsConfig();
            }
            return _singleInstance;
        }
        
        private clsArgsConfig()
        {
            try{     
                
    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 引数の次の値が引数名でないことをチェックする
        /// </summary>
        private List<string> commndKeyArray = new List<string> {
            "-fileDir","-projectName","-dbType","-dataBase","-userId","-passWord"};
        
   
        public Boolean _validateCommandKey()
        {
            
            if (ProjectName != "")
            {
                ProjectName = _getProjectName(ProjectName);
            }

            if (FileDirPath == "" )
            {
                Console.WriteLine("projectFolderが指定されていない");
                return false;
            }

            return true;
        }
        public void _setArgs(string[] args)
        {
            
            int i = 0;
            foreach (var commandKey in args)
            {
                
                if (commandKey._indexOf("-projectName") != -1)
                {
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        ProjectName = args[i + 1];
                    }
                    i++;
                    continue;
                } 
                
                if (commandKey._indexOf("-fileDir") != -1)
                {
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        FileDirPath = args[i + 1];
                    }
                    i++;
                    continue;
                }
                
                if (commandKey._indexOf("-dbType") != -1)
                {
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        var dbType = args[i + 1];
                        if (dbType.ToLower() == "mysql")
                        {
                            isDbType_Sqlite = false;
                        }
                    }
                    i++;
                    continue;
                }
                
                if (commandKey._indexOf("-dataBase") != -1)
                {
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        MySql_DataBase = args[i + 1];
                     
                    }
                    i++;
                    continue;
                }
                
                if (commandKey._indexOf("-userId") != -1)
                {
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        MySql_UserId = args[i + 1];
                       
                    }
                    i++;
                    continue;
                }
                
                if (commandKey._indexOf("-passWord") != -1)
                {
                    if (args._safeIndexOf(i + 1) && 
                        commndKeyArray.IndexOf(args[i+1]) == -1 && 
                        args[i+1] != ""){
                        MySql_Password = args[i + 1];
                        
                    }
                    i++;
                    continue;
                }

                i++;
            }
        }
        
        /// <summary>
        /// プロジェクトパスからプロジェクト名を取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string _getProjectName(string path){
            
            path = path.TrimEnd(Path.DirectorySeparatorChar);

            var separator = Path.DirectorySeparatorChar;
            string[] pathArray = path.Split(separator);

            for (int i = pathArray.Length ; i > 0; i--)
            {
                var pathArray1 = pathArray[0..i];
                string stCsvData = string.Join("/", pathArray1);
                string csprojPath = stCsvData + "/" + pathArray[i-1] + ".csproj";
                if (File.Exists(csprojPath))
                {
                    return pathArray[i-1];
                    break;
                }
            }

            //単語
            if (pathArray.Length == 0)
            {
                return path;
            }

            return "";
        }
        
  
       
    }
}