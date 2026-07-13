using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnBackButtonPressed()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        SceneManager.LoadScene("HomeMenu");
    }
}
