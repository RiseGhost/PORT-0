using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NotificationOkCancel : NotificationKeyInteractable
{
    private Label LBOK;
    private Label LBCancel;
    protected ProgressBar Ok;
    protected ProgressBar Cancel;
    private Key OkPress, CancelPress;
    
    public NotificationOkCancel(string tittle, string description, Key Ok, Key Cancel, MonoBehaviour monoBehaviour)
    {
        visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/Notification/Interactable/2 Options");
        root = visualTreeAsset.CloneTree();
        this.tittle = tittle;
        this.description = description;
        Tittle = root.Q<Label>("Tittle");
        Description = root.Q<Label>("Description");
        LBOK = root.Q<Label>("AcceptKey");
        LBCancel = root.Q<Label>("CancelKey");
        this.Ok = root.Q<ProgressBar>("okBar");
        this.Cancel = root.Q<ProgressBar>("cancelBar");
        Tittle.text = tittle;
        Description.text = description;
        LBOK.text = Ok.ToString();
        LBCancel.text = Cancel.ToString();
        OkPress = Ok;
        CancelPress = Cancel;
        this.Ok.value = 0f;
        this.Cancel.value = 0f;
        TTL = 0;
        monoBehaviour.StartCoroutine(Update());
    }

    override public IEnumerator Update()
    {
        while (Ok.value <= 100f && Cancel.value <= 100f)
        {
            yield return null;
            foreach (var x in new List<(ProgressBar bar,Key key)>{ (Ok,OkPress), (Cancel,CancelPress) })
            {
                float value = x.bar.value;
                if (Keyboard.current[x.key].isPressed) value += 0.5f;
                else
                {
                    if (value > 0) value -= 0.2f;
                    if (value < 0) value = value = 0f;
                }
                x.bar.value = value;
            }
            
        }
        OnSelectKey();
        Destroy();
    }
}