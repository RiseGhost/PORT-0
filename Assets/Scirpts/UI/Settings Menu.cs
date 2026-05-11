using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("HomeMenu");
    }
}
