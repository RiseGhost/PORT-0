using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NotificationKeyInteractable : NotificationImplement
{
    protected VisualElement root;
    protected Label Tittle, Description;
    private Label Key;
    private ProgressBar progress;
    private Key KeyPress;
    private float value = 0f;

    public NotificationKeyInteractable(){}

    public NotificationKeyInteractable(string tittle, string description, Key Key, MonoBehaviour monoBehaviour)
    {
        visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/Notification/Interactable/Default");
        root = visualTreeAsset.CloneTree();
        this.tittle = tittle;
        this.description = description;
        Tittle = root.Q<Label>("Tittle");
        Description = root.Q<Label>("Description");
        this.Key = root.Q<Label>("PressKey");
        progress = root.Q<ProgressBar>("mybar");
        Tittle.text = tittle;
        Description.text = description;
        this.Key.text = Key.ToString();
        KeyPress = Key;
        TTL = 0;
        monoBehaviour.StartCoroutine(Update());
    }

    public virtual IEnumerator Update()
    {
        while (progress.value <= 100f)
        {
            yield return null;
            if (Keyboard.current[KeyPress].isPressed) value += 0.5f;
            else
            {
                if (value > 0) value -= 0.2f;
                if (value < 0) value = value = 0f;
            }
            progress.value = value;
        }
        OnSelectKey();
        Destroy();
    }

    virtual protected void OnSelectKey(){}

    public override VisualElement GetVisualElement()
    {
        return root;
    }

    public override NotificationsZone GetZone()
    {
        return NotificationsZone.Top;
    }
}