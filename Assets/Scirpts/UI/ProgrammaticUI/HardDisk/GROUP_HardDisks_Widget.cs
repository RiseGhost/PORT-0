using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GROUP_HardDisks_Widget : GroupToggle<HardDiscStatus>
{
    [SerializeField] private TextMeshProUGUI SataNumber;
    [SerializeField] private UI_HardDisk_Widget template;

    void Start()
    {
        if (template == null) return;
        foreach(HardDiscStatus s in Status)
        {
            UI_HardDisk_Widget HardDisk_Widget = Instantiate(template, transform);
            HardDisk_Widget.setStatus(s);
            HardDisk_Widget.getWidget().group = getWidget();
            HardDisk_Widget.setSelectColor(selectColor);
            HardDisk_Widget.setDefaultColor(defaultColor);
            HardDisk_Widget.setSelectTextColor(selectTextColor);
            HardDisk_Widget.setDefaultTextColor(defaultTextColor);
        }
    }

    public override void setDescription(string description)
    {
        base.setDescription(description);
    }

    public override HardDiscStatus getSelect()
    {
        foreach (Transform child in transform)
        {
            UI_HardDisk_Widget disk = child.gameObject.GetComponent<UI_HardDisk_Widget>();
            if (disk == null) continue;
            if (disk.isSelect()) return disk.getData();
        }
        return null;
    }

    public void setSataNumber(int number)
    {
        if (SataNumber != null) SataNumber.text = number.ToString();
    }
}