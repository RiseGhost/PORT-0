using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("HomeMenu");
    }
}
