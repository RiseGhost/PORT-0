using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
    Description:
        This class is the popup controller that appears after the player accepts a new task.
        It is responsible for detecting witch key is pressed by player and is therefore responsible
        for accepting or refusing the task.
    
    Attributes:
        Key Cancel                  -> The key the player must press to reject the task
        Key Ok                      -> The key the player must press to accept the task
        GameObject MainContent      -> It is the panel in UI where the main content about the task will appear
        Slider SliderOK             -> It is the slider who fills in when the player chooses to refuse the task
        Slider SliderCancel         -> It is the slider who fills in when the player chooses to accept the task
        TextMeshProUGUI LabelCancel -> Is the label that shows which key to refuse
        TextMeshProUGUI LabelOK     -> Is the label that show which key to accept
*/

public class PopUpTask : MonoBehaviour
{
    [SerializeField] private Key Cancel = Key.X;
    [SerializeField] private Key OK = Key.Tab;
    [SerializeField] private GameObject MainContent;
    [SerializeField] private Slider SliderOk, SliderCancel;
    [SerializeField] private TextMeshProUGUI LabelCancel, LabelOk;
    [SerializeField] private ServerTaskSelect serverTaskSelect;
    private Task task = null;
    
    void Start()
    {
        if (SliderOk == null || SliderCancel == null || LabelCancel == null || LabelOk == null)
        {
            Destroy(this.gameObject);
        }
        LabelCancel.text = Cancel.ToString() + " to reject";
        LabelOk.text = OK.ToString() + " to accept";
        SliderOk.maxValue = 100f;
        SliderCancel.maxValue = 100f;
    }

    void LateUpdate()
    {
        if (SliderOk.value >= 100f)
        {
            if (serverTaskSelect != null)
            {
                ServerTaskSelect taskSelect = Instantiate(serverTaskSelect,Vector3.zero,Quaternion.identity);
                taskSelect.setTask(task);
            }
            Destroy(this.gameObject);
        }
        else if (SliderCancel.value >= 100f)
        {
            TaskServer.Lock = false;
            Destroy(this.gameObject);
        }
        else
        {
           foreach(var x in new List<(Slider slider,Key key)>{ (SliderOk, OK), (SliderCancel, Cancel) })
            {
                float value = x.slider.value;
                if (Keyboard.current[x.key].isPressed)
                    value += 0.5f;
                else
                    value -= 0.2f;
                if (value < 0f) value = 0f;
                x.slider.value = value;
            } 
        }
    }

    public void SetTask(Task task)
    {
        PopContentTask pop = Instantiate(BuildContentPopUp.Build(),MainContent.transform).GetComponent<PopContentTask>();
        pop.setTask(task);
        this.task = task;
    }
}