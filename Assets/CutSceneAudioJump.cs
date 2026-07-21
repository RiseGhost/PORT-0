using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CutSceneAudioJump : MonoBehaviour
{
    private AudioSource _audiosource;
    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_audiosource.isPlaying) return;
        else SceneManager.LoadScene("HomeMenu");
    }
}
