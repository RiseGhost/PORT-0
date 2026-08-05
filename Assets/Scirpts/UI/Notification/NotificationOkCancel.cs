using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private VisualElement InteractZone;
    private VisualElement DisableAction;
    private float speed = 45f;
    
    public NotificationOkCancel(string tittle, string description, Key Ok, Key Cancel, MonoBehaviour monoBehaviour)
    {
        visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/Notification/Interactable/2 Options");
        root = visualTreeAsset.CloneTree();
        this.tittle = tittle;
        this.description = description;
        DisableAction = root.Q<VisualElement>("DisableAction");
        InteractZone = root.Q<VisualElement>("InteractZone");
        Tittle = root.Q<Label>("Tittle");
        Description = root.Q<Label>("Description");
        LBOK = root.Q<Label>("AcceptKey");
        LBCancel = root.Q<Label>("CancelKey");
        this.Ok = root.Q<ProgressBar>("okBar");
        this.Cancel = root.Q<ProgressBar>("cancelBar");
        Tittle.text = tittle;
        Description.text = description;
        LBOK.text = Ok.ToString() + " - Accept";
        LBCancel.text = Cancel.ToString() + " - Reject";
        OkPress = Ok;
        CancelPress = Cancel;
        this.Ok.value = 0f;
        this.Cancel.value = 0f;
        DisableAction.style.visibility = Visibility.Hidden;
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
                bool lockTab = false;
                bool lockInteract = false;
                List<BtnInstallOS> btns = GameObject.FindObjectsByType<BtnInstallOS>(FindObjectsSortMode.None).ToList();
                if (btns.Select((x) => x.enabled).Contains(true)) lockTab = true;
                List<ComputerTabButton> computer = GameObject.FindObjectsByType<ComputerTabButton>(FindObjectsSortMode.None).ToList();
                if (computer.Select((x) => x.enabled).Contains(true)) lockTab = true;
                List<ComputerUI> computerUIs = GameObject.FindObjectsByType<ComputerUI>(FindObjectsSortMode.None).ToList();
                if (computerUIs.Select((x) => x.enabled).Contains(true)) { lockTab = true; lockInteract = true; }
                List<Install_OS_UI> installUI = GameObject.FindObjectsByType<Install_OS_UI>(FindObjectsSortMode.None).ToList();
                if (installUI.Select((x) => x.enabled).Contains(true)) { lockTab = true; lockInteract = true; }
                List<ServerConfigBook> shop = GameObject.FindObjectsByType<ServerConfigBook>(FindObjectsSortMode.None).ToList();
                if (shop.Select((x) => x.enabled).Contains(true)) { lockTab = true; lockInteract = true; }
                float value = x.bar.value;
                DisableAction.style.visibility = (lockInteract) ? Visibility.Visible : Visibility.Hidden;
                InteractZone.style.visibility = (lockInteract) ? Visibility.Hidden : Visibility.Visible;
                if (Keyboard.current[x.key].isPressed && !lockTab) value += Time.deltaTime * speed;
                else
                {
                    if (value > 0) value -= Time.deltaTime * speed * 2f;
                    if (value < 0) value = value = 0f;
                }
                x.bar.value = value;
            }
            
        }
        OnSelectKey();
        Destroy();
    }
}