using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class PowerSupply
{
    private static List<short> targetFailed = new() {2,6,8};
    private static bool FuturePowerSupplyOut = false;
    private static bool Energy = true;
    private static AudioMixer audioMixer;

    public static void Break(){ 
        Energy = false;
        SoundServer soundServer = GameObject.FindFirstObjectByType<SoundServer>();
        if (audioMixer == null) audioMixer = soundServer.GetAudioMixer();
        if (audioMixer != null)
        {
            AudioClip powerOff = Resources.Load<AudioClip>("SFX/Power Off");
            if (powerOff != null) soundServer.Play(powerOff,audioMixer.FindMatchingGroups("Effects")[0]);
        }
        if (TaskServer.Last_Notification != null) NotificationServer.RemoveNotification(TaskServer.Last_Notification);
        Lights[] lights = GameObject.FindObjectsByType<Lights>(FindObjectsSortMode.None);
        foreach (Lights l in lights) l.Deactivate();
    }
    public static void Return(){ 
        Energy = true;
        FuturePowerSupplyOut = false;
        SoundServer soundServer = GameObject.FindFirstObjectByType<SoundServer>();
        if (audioMixer == null) audioMixer = soundServer.GetAudioMixer();
        if (audioMixer != null)
        {
            AudioClip powerOn = Resources.Load<AudioClip>("SFX/Power On");
            if (powerOn != null) soundServer.Play(powerOn,audioMixer.FindMatchingGroups("Effects")[0]);
        }
        Lights[] lights = GameObject.FindObjectsByType<Lights>(FindObjectsSortMode.None);
        foreach (Lights l in lights) l.Active();
        TaskServer.Lock = false;
    }    

    public static bool Exist_Energy() { return Energy; }

    public static bool PowerOut_Will_Exit()
    {
        if (FuturePowerSupplyOut) return true;
        if (targetFailed == null || targetFailed.Count == 0) return false;
        GameObject[] serversGameObject = GameObject.FindGameObjectsWithTag("ServerGameObject");
        List<ServerStatusStruct> serverStatus = serversGameObject.Select(x => x.GetComponent<ServerGameObject>().server.serverStatus).ToList();
        var limitedWatts = serverStatus.Select(x => x.getWatts() > ServerStatusList.get_MAX_WATTS_To_Warring()).ToArray();
        if (limitedWatts.Count() >=  targetFailed.First()){
            targetFailed.RemoveAt(0);
            FuturePowerSupplyOut = true;
            return true;
        }
        else return false;
    }
}
