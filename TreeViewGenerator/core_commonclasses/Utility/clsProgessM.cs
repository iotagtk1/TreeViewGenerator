using GLib;

public class clsProgessM
{
    public static System.Diagnostics.Process _getProgress(string appPath, string argsStr) {
        var p = new System.Diagnostics.Process();
        p.StartInfo.FileName = appPath;//@"/usr/local/bin/node";
        
        p.StartInfo.Arguments = argsStr;// "/binance.js BNBBTC";
        // コンソール・ウィンドウを開かない
        p.StartInfo.CreateNoWindow = true;
        // シェル機能を使用しない
        p.StartInfo.UseShellExecute = false;
        // 標準出力をリダイレクト
        p.StartInfo.RedirectStandardOutput = true;
        // 標準入力をリダイレクト
        p.StartInfo.RedirectStandardInput = true;
        
        p.StartInfo.RedirectStandardError = true;
  
        p.EnableRaisingEvents = true;
        //p.Exited += Proc_Exited;
        //必ず
        //p.Close();
        //p.Dispose();
        // p.Start();
        return p;
    }

}