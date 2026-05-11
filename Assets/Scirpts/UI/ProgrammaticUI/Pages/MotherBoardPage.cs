using UnityEngine;

public class MotherBoardPage : UIPages
{
    [SerializeField] private GROUP_MotherBoards_Widget MotherBoards_Widget;
    private MotherBoardStatus motherBoardStatus;

    void Start()
    {
        if (MotherBoards_Widget == null) Destroy(this);
    }

    void Update()
    {
        Next.interactable = (MotherBoards_Widget.getSelect() != null);
        if (progressionPageWidget != null) ChangeProgressionWidget();
    }

    private void ChangeProgressionWidget()
    {
        if (MotherBoards_Widget.getSelect() != motherBoardStatus)
        {
            if (MotherBoards_Widget.getSelect() == null) progressionPageWidget.DecrementValue(100);
            else if (motherBoardStatus == null) progressionPageWidget.IncrementValue(100);
            motherBoardStatus = MotherBoards_Widget.getSelect();
        }
    }

    public GROUP_MotherBoards_Widget getMotherBoardWidget() { return MotherBoards_Widget; }

    public void setCPUGroup(GROUP_CPUS_Widget cpus)
    {
        MotherBoards_Widget.setGROUP_CPU(cpus);
    }
}