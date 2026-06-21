using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    Master,
    Music,
    Effect
}

public class SoundServer : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Sound Server: is ready .🎶");
        name = "Sound Server";
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(AudioClean());
    }

    void Start()
    {
        AudioMixer audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        if (audioMixer == null) Debug.Log("Sound Server -> AudioMixer is null");
        float MasterVolume = PlayerPrefs.GetFloat(getSoundType_SaveName(SoundType.Master),0);
        float MusicVolume = PlayerPrefs.GetFloat(getSoundType_SaveName(SoundType.Music),0);
        float EffectsVolume = PlayerPrefs.GetFloat(getSoundType_SaveName(SoundType.Effect),0);
        Debug.Log("Sound Server -> Master = " + MasterVolume + " Music = " + MusicVolume + " Effects = " + EffectsVolume);
        if (audioMixer == null) return;
        audioMixer.SetFloat(getSoundType_MixerName(SoundType.Master),(MasterVolume <= -20f) ? -80f : MasterVolume);
        audioMixer.SetFloat(getSoundType_MixerName(SoundType.Music),(MusicVolume <= -20f) ? -80f : MusicVolume);
        audioMixer.SetFloat(getSoundType_MixerName(SoundType.Effect),(EffectsVolume <= -20f) ? -80f : EffectsVolume);
    }

    public static string getSoundType_SaveName(SoundType type)
    {
        switch (type){
            case SoundType.Master:
                return "MasterVolume";
            case SoundType.Music:
                return "MusicVolume";
            case SoundType.Effect:
                return "EffectsVolume";
            default:
                return null;
        }
    }

    public static string getSoundType_MixerName(SoundType type)
    {
        switch (type)
        {
            case SoundType.Master:
                return "Master";
            case SoundType.Music:
                return "Music";
            case SoundType.Effect:
                return "Effects";
            default:
                return null;
        }
    }

    public void Play(AudioClip clip, AudioMixerGroup group)
    {
        if (clip == null) return;
        AudioSource audio = new GameObject().AddComponent<AudioSource>();
        audio.transform.SetParent(transform);
        audio.clip = clip;
        audio.outputAudioMixerGroup = group;
        audio.volume = 1.0f;
        audio.Play();
    }

    private IEnumerator AudioClean()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            foreach(Transform child in transform)
            {
                AudioSource audio = child.GetComponent<AudioSource>();
                if (!audio) continue;
                if (!audio.isPlaying) Destroy(audio.gameObject);
            }
        }
    }
}
