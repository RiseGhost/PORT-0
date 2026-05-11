using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OSInstallPart0 : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OS Install Part 0 finished");
        Install_OS_UI install = GameObject.FindAnyObjectByType<Install_OS_UI>();
        if (install == null)
        {
            Debug.Log("Install_OS_UI not found in the scene.");
            return;
        }
        if (install.name.Contains("Install OS Parte 0")) install.StartCoroutine(ColorLerp(install));
        else Debug.Log("The current Install_OS_UI is not the correct one.");
    }

    private IEnumerator ColorLerp(Install_OS_UI install)
    {
        RawImageColorLerp colorLerp = install.AddComponent<RawImageColorLerp>();
        colorLerp.setSpeed(0.5f);
        Server server = install.GetServer();
        OperatingSystem os = server.serverStatus.os;
        colorLerp.setColors(Color.black, os.PrimaryColor);
        yield return new WaitForSecondsRealtime(2.5f);
        install.NextButtomFunc();
    } 
}
