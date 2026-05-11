using UnityEngine;

public class GROUP_CPUS_Mark_Widget : GroupToggle<CPUMarkStatus>
{
    [SerializeField] private UI_CPU_MARK_WIDGET template;
    void Awake()
    {
        foreach (var cpu in Status)
        {
            var c = Instantiate(template, transform).GetComponent<UI_CPU_MARK_WIDGET>();
            c.setStatus(cpu);
            c.getWidget().group = getWidget();
            c.setDefaultColor(defaultColor);
            c.setSelectColor(selectColor);
            c.setSelectTextColor(selectTextColor);
            c.setDefaultTextColor(defaultTextColor);
        }
    }

    public override CPUMarkStatus getSelect()
    {
        for (short i = 0; i < transform.childCount; i++)
        {
            var cpu = transform.GetChild(i).GetComponent<UI_CPU_MARK_WIDGET>();
            if (cpu == null) continue;
            if (cpu.isSelect()) return cpu.getData();
        }
        return null;
    }

}