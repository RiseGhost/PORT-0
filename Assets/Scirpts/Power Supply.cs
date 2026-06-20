using UnityEngine;
using UnityEngine.Audio;

public class PowerSupply
{
    private static bool Energy = true;
    private static AudioMixer audioMixer;

    public static void Break(){ 
        Energy = false;
        if (audioMixer == null) audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        if (audioMixer != null)
        {
            AudioClip powerOff = Resources.Load<AudioClip>("SFX/Power Off");
            if (powerOff != null) GameObject.FindAnyObjectByType<SoundServer>().Play(powerOff,audioMixer.FindMatchingGroups("Effects")[0]);
        }
        Lights[] lights = GameObject.FindObjectsByType<Lights>(FindObjectsSortMode.None);
        foreach (Lights l in lights) l.Deactivate();
    }
    public static void Return(){ 
        Energy = true;
        if (audioMixer == null) audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        if (audioMixer != null)
        {
            AudioClip powerOn = Resources.Load<AudioClip>("SFX/Power On");
            if (powerOn != null) GameObject.FindAnyObjectByType<SoundServer>().Play(powerOn,audioMixer.FindMatchingGroups("Effects")[0]);
        }
        Lights[] lights = GameObject.FindObjectsByType<Lights>(FindObjectsSortMode.None);
        foreach (Lights l in lights) l.Active();
        TaskServer.Lock = false;
    }    

    public static bool Exist_Energy() { return Energy; }
}
