using System;
using System.Collections.Generic;

public class clsArgs
{

    static public Dictionary<string, string> _getArgs(string[] args)
    {

        Dictionary<string, string> argsDic = new Dictionary<string, string>();
        int i = 0;
        foreach (var commandKey in args)
        {
            if (commandKey.IndexOf("-") != -1)
            {
                if (args._safeIndexOf(i + 1) && args[i+1].IndexOf("-") == -1 &&  args[i+1] != ""){
                    argsDic.Add(commandKey,args[i+1]);
                }
                i++;
                continue;
            }
            i++;
        }
        return argsDic;
    }

}
