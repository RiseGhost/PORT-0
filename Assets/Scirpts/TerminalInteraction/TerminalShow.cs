using System.Diagnostics;
using System.IO;
using UnityEngine;

public class TerminalShow
{
    public TerminalShow(string filename)
    {
        TextAsset exeFile = Resources.Load<TextAsset>(filename);
        if (exeFile != null)
        {
            string exePath = Path.Combine(Application.persistentDataPath, filename + ".exe");

            if (!File.Exists(exePath))
            {
                File.WriteAllBytes(exePath, exeFile.bytes);
            }

            Process proc = new Process();
            proc.StartInfo.FileName = exePath;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
        }
        else UnityEngine.Debug.Log("Executavel null");
    }
}