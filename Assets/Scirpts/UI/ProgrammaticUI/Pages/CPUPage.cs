using UnityEngine;

public class CPUPage : UIPages
{
    [SerializeField] private GROUP_CPUS_Architect_Widget Architect_Widget;
    [SerializeField] private GROUP_CPUS_Mark_Widget Mark_Widget;
    [SerializeField] private GROUP_CPUS_Widget CPU_WIDGET;
    private CPUArchitectStatus architectStatus;
    private CPUMarkStatus markStatus;
    private ProcessorStatus processorStatus;

    void Start()
    {
        if (CPU_WIDGET == null) Destroy(this);
    }

    void Update()
    {
        Next.interactable = (CPU_WIDGET.getSelect() != null);
        if (Architect_Widget.getSelect() != architectStatus)
        {
            if (Architect_Widget.getSelect() == null) progressionPageWidget.DecrementValue(33);
            else if (architectStatus == null) progressionPageWidget.IncrementValue(33);
            architectStatus = Architect_Widget.getSelect();
        }
        if (Mark_Widget.getSelect() != markStatus)
        {
            if (Mark_Widget.getSelect() == null) progressionPageWidget.DecrementValue(33);
            else if (markStatus == null) progressionPageWidget.IncrementValue(33);
            markStatus = Mark_Widget.getSelect();
        }
        if (CPU_WIDGET.getSelect() != processorStatus)
        {
            if (CPU_WIDGET.getSelect() == null) progressionPageWidget.DecrementValue(33);
            else if (processorStatus == null) progressionPageWidget.IncrementValue(33);
            processorStatus = CPU_WIDGET.getSelect();
        }
    }

    public ProcessorStatus getProcessorStatus() { return CPU_WIDGET.getSelect(); }
    public GROUP_CPUS_Widget getGroudCpuWidget(){ return CPU_WIDGET; }
}