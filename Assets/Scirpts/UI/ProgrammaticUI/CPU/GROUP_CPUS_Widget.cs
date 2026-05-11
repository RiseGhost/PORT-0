using UnityEngine;

public class GROUP_CPUS_Widget : GroupToggle<ProcessorStatus>
{
    [SerializeField] private UI_CPU_WIDGET template;
    [SerializeField] private CPUSStatusList CPU_Status_List;
    [SerializeField] private GROUP_CPUS_Architect_Widget GROUP_Architect_Widget;
    [SerializeField] private GROUP_CPUS_Mark_Widget GROUP_Mark_Widget;
    private CPUArchitectStatus LastArchitectStatus;
    private CPUMarkStatus LastMarkStatus;

    void Start()
    {
        if (GROUP_Architect_Widget == null) Destroy(this);
        if (GROUP_Mark_Widget == null) Destroy(this);
        if (CPU_Status_List == null) Destroy(this);
    }

    void Update()
    {
        if (GROUP_Architect_Widget.getSelect() != LastArchitectStatus)
        {
            LastArchitectStatus = GROUP_Architect_Widget.getSelect();
            UpdateCPUs();
        }
        if (GROUP_Mark_Widget.getSelect() != LastMarkStatus)
        {
            LastMarkStatus = GROUP_Mark_Widget.getSelect();
            UpdateCPUs();
        }
    }

    private void UpdateCPUs()
    {
        RemoveAllChildren();
        if (LastArchitectStatus == null || LastMarkStatus == null) return;
        foreach (ProcessorStatus processor in CPU_Status_List.processorStatuses)
        {
            if (processor.getArchitect() != LastArchitectStatus) continue;
            if (processor.getMark() != LastMarkStatus) continue;
            var cpu = Instantiate(template, transform);
            cpu.data = processor;
            cpu.getWidget().group = getWidget();
            cpu.setStatus(processor);
            cpu.setDefaultColor(defaultColor);
            cpu.setSelectColor(selectColor);
            cpu.setSelectTextColor(selectTextColor);
            cpu.setDefaultTextColor(defaultTextColor);
        }
    }

    private void RemoveAllChildren() {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public override ProcessorStatus getSelect()
    {
        for (short i = 0; i < transform.childCount; i++)
        {
            var processor = transform.GetChild(i).GetComponent<UI_CPU_WIDGET>();
            if (processor == null) continue;
            if (processor.isSelect()) return processor.getData();
        }
        return null;
    }

    public CPU getSelectCPU(){ return new CPU(getSelect()); }
}