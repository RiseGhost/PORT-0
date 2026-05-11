using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiskPage : UIPages
{
    [SerializeField] GROUP_HardDisks_Widget GROUP_HardDisks_Widget_template;
    [SerializeField] private GameObject ContentArea;
    private List<GROUP_HardDisks_Widget> disksWidget = new List<GROUP_HardDisks_Widget>();
    private short DiskNumber = 0;
    private short LastDiskNumber = 0;

    void OnEnable()
    {
        foreach (var page in book.getAllPages())
        {
            if (page is MotherBoardPage)
            {
                MotherBoardPage motherPage = (MotherBoardPage)page;
                if (motherPage.getMotherBoardWidget().getSelect() == null) return;
                UpdateDiskNumber(motherPage.getMotherBoardWidget().getSelect().GetValue().MaxDisk);
            }
        }
    }

    void Update()
    {
        var disksStatus = getDisksStatuses();
        Next.interactable = (DiskNumber > 0 && disksStatus.Count > 0);
        if (LastDiskNumber == 0 && disksStatus.Count > 0)
        {
            progressionPageWidget.IncrementValue(100);
            LastDiskNumber = 1;
        }
        if (LastDiskNumber > 0 && disksStatus.Count == 0)
        {
            progressionPageWidget.DecrementValue(100);
            LastDiskNumber = 0;
        }
    }

    public List<HardDiscStatus> getDisksStatuses()
    {
        List<HardDiscStatus> hardDiscs = new List<HardDiscStatus>();
        foreach (var disk in disksWidget)
        {
            if (disk.getSelect() != null) hardDiscs.Add(disk.getSelect());
        }
        return hardDiscs;
    }

    private void UpdateDiskNumber(short DiskNumber)
    {
        CleanChildren();
        this.DiskNumber = DiskNumber;
        for(int i = 0; i < DiskNumber; i++)
        {
            GROUP_HardDisks_Widget disk_group = Instantiate(GROUP_HardDisks_Widget_template, ContentArea.transform);
            disk_group.setSataNumber(i+1);
            disksWidget.Add(disk_group);
        }
    }

    private void CleanChildren()
    {
        while (disksWidget.Count > 0)
        {
            Destroy(disksWidget.First().gameObject);
            disksWidget.RemoveAt(0);
        }
    }
}