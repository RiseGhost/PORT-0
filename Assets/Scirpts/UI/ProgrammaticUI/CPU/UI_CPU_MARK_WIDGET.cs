using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CPU_MARK_WIDGET : ToggleWidget<CPUMarkStatus>
{
    [SerializeField] private TextMeshProUGUI label;

    void Start()
    {
        this.name = "CPU_Mark_WIDGET_" + getData().GetType().ToString();
    }


    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(CPUMarkStatus status)
    {
        if (label != null) label.text = status.GetValue().ToString();
        data = status;
    }
}