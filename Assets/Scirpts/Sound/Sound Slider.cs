using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundType _type;
    private AudioMixer _audioMixer;
    [SerializeField] private AudioClip clickSound;

    void Start(){
        _audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        if (_audioMixer == null) return;
        Slider slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(SoundServer.getSoundType_SaveName(_type),0);
        _audioMixer.SetFloat(SoundServer.getSoundType_MixerName(_type),slider.value);
        slider.onValueChanged.AddListener((value) =>
        {
            if (clickSound != null) GameObject.FindFirstObjectByType<SoundServer>().Play(clickSound,_audioMixer.FindMatchingGroups("Effects")[0]);
            float sound_value = (value == slider.minValue) ? -80f : value;
            PlayerPrefs.SetFloat(SoundServer.getSoundType_SaveName(_type),sound_value);
            PlayerPrefs.Save();
            _audioMixer.SetFloat(SoundServer.getSoundType_MixerName(_type),sound_value);
        });
    }
}
