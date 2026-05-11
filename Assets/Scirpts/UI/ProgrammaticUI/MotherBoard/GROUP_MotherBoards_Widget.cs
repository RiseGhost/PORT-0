using UnityEngine;

public class GROUP_MotherBoards_Widget : GroupToggle<MotherBoardStatus>
{
    [SerializeField] private UI_MotherBoard_Widget template;
    [SerializeField] private GROUP_CPUS_Widget cpus;
    private ProcessorStatus SelectCPU = null;

    private void UpdateContent(ProcessorStatus SelectCPU)
    {
        CleanContent();
        foreach (MotherBoardStatus status in Status)
        {
            if (status.GetValue().socket != SelectCPU.GetCPUSockets()) continue;
            else
            {
                UI_MotherBoard_Widget motherboard = Instantiate(template, transform);
                motherboard.getWidget().group = getWidget();
                motherboard.setStatus(status);
                motherboard.setSelectColor(selectColor);
                motherboard.setDefaultColor(defaultColor);
                motherboard.setSelectTextColor(selectTextColor);
                motherboard.setDefaultTextColor(defaultTextColor);
            }
        }
    }

    private void CleanContent()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        if (cpus == null)
        {
            return;
        }
        ProcessorStatus status = cpus.getSelect();
        if (status == null)
        {
            CleanContent();
            SelectCPU = null;
            return;
        }
        if (SelectCPU == null || SelectCPU.GetCPUSockets() != status.GetCPUSockets())
        {
            UpdateContent(status);
            SelectCPU = status;
        }
    }

    public override MotherBoardStatus getSelect()
    {
        foreach (Transform child in transform)
        {
            UI_MotherBoard_Widget motherboard = child.GetComponent<UI_MotherBoard_Widget>();
            if (motherboard == null) continue;
            if (motherboard.isSelect()) return motherboard.getData();
        }
        return null;
    }

    public void setGROUP_CPU(GROUP_CPUS_Widget cpus)
    {
        this.cpus = cpus;
    }
}