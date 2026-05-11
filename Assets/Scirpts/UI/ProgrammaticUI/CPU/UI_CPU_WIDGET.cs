using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CPU_WIDGET : ToggleWidget<ProcessorStatus>
{
    [SerializeField] private TextMeshProUGUI CPUName, Architect, Mark, Price, Cores, Threads;
    [SerializeField] private RawImage icon;
    private ProcessorStatus status;

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(ProcessorStatus status)
    {
        this.status = status;
        if (CPUName != null)    CPUName.text = status.GetValue();
        if (Architect != null)  Architect.text = status.getArchitect().GetValue().ToString();
        if (Mark != null)       Mark.text = status.getMark().GetValue().ToString();
        if (Price != null)      Price.text = status.getPrice().ToString() + " $";
        if (Cores != null)      Cores.text = status.getPerformance().GetValue().cores.ToString();
        if (Threads != null)    Threads.text = status.getPerformance().GetValue().threads.ToString();
        this.data = status;
    }
}