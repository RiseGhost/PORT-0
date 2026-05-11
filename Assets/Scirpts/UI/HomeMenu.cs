using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class HomeMenu : MonoBehaviour
{
    [System.Serializable]
    private struct MenuOption
    {
        public string SceneName;
        public Key Key;
    }

    [SerializeField] private MenuOption[] MenuOptions;
    [SerializeField] private Key ExitKey = Key.Q;

    void Update()
    {
        if (Keyboard.current[ExitKey].wasPressedThisFrame)
            Application.Quit();
        foreach (MenuOption option in MenuOptions)
        {
            if (Keyboard.current[option.Key].wasPressedThisFrame)
                SceneManager.LoadScene(option.SceneName);
        }
    }
}
