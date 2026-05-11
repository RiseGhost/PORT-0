public class UI_CPU_Architect_WIDGET : ToggleWidget<CPUArchitectStatus>
{
    void Start()
    {
        this.name = "CPU_ARCHITECT_WIDGET_" + getData().GetType().ToString();
    }

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(CPUArchitectStatus status)
    {
        if (label != null) label.text = status.GetValue().ToString();
        data = status;
    }
}