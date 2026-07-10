using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundType _type;
    private AudioMixer _audioMixer;
    [SerializeField] private AudioClip clickSound;
    private SoundServer soundServer;

    void Start(){
        soundServer = GameObject.FindFirstObjectByType<SoundServer>();
        _audioMixer = soundServer.GetAudioMixer();
        if (_audioMixer == null) return;
        Slider slider = GetComponent<Slider>();
        float realValue = PlayerPrefs.GetFloat(SoundServer.getSoundType_SaveName(_type),0);
        slider.value = realValue;
        _audioMixer.SetFloat(SoundServer.getSoundType_MixerName(_type),realValue);
        slider.onValueChanged.AddListener((value) =>
        {
            if (clickSound != null) soundServer.Play(clickSound,_audioMixer.FindMatchingGroups("Effects")[0]);
            float sound_value = value;
            if (sound_value <= slider.minValue + 0.2f) sound_value = -80f;
            PlayerPrefs.SetFloat(SoundServer.getSoundType_SaveName(_type),sound_value);
            PlayerPrefs.Save();
            _audioMixer.SetFloat(SoundServer.getSoundType_MixerName(_type),sound_value);
        });
    }
}
