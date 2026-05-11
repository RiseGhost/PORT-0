using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    
    private VisualTreeAsset visualTree;
    private PanelSettings panelSettings;
    private bool visible = false;
    private GameObject menu;

    void Awake()
    {
        Debug.Log("Pause Menu: is ready ✅");
        this.name = "Pause Menu System";
    }

    void Start()
    {
        visualTree = Resources.Load<VisualTreeAsset>("UI/PauseMenu/Menu");
        panelSettings = Resources.Load<PanelSettings>("UI/PauseMenu/Panel");
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (menu != null) FadeEffect(Time.unscaledDeltaTime/100f,170);
        if (!Keyboard.current[Key.Escape].wasPressedThisFrame) return;
        visible = !visible;
        if (visible)
        {
            CameraFollow.LockRotate();
            Time.timeScale = 0f;
            if (menu == null) menu = new GameObject("PauseMenu");
            UIDocument uI = menu.AddComponent<UIDocument>();
            uI.sortingOrder = 1000;
            uI.visualTreeAsset = visualTree;
            uI.panelSettings = panelSettings;
            StartCoroutine(setNavigation(uI.rootVisualElement));
        }
        else DestroyMenu();
    }


    public void DestroyMenu()
    {
        visible = false;
        Time.timeScale = 1f;
        CameraFollow.UnlockRotate();
        Destroy(menu);
    }

    private System.Collections.IEnumerator setNavigation(VisualElement visualElement)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        List<Button> buttons = visualElement.Query<Button>().ToList();
        buttons.ForEach(x => x.focusable = true);
        buttons.First().clicked += () => DestroyMenu();
        buttons.Last().clicked += () =>
        {
            Application.Quit();
        };
        visualElement.Q<Button>("btn_MainMenu").clicked += () =>
        {
            DestroyMenu();
            SceneManager.LoadScene("HomeMenu");
        };
        if (buttons.Count > 0) buttons.First().Focus();
    }

    private void FadeEffect(float speed, float targetAlpha)
    {
        try
        {
           VisualElement ve = menu.GetComponent<UIDocument>().rootVisualElement;
            VisualElement bgc = ve.Q<VisualElement>("BackgroundColor");
            Color currentColor = bgc.style.backgroundColor.value;
            if (currentColor.a >= targetAlpha) return;
            currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, speed);
            bgc.style.backgroundColor = currentColor; 
        } catch (Exception e){}
    }
}
