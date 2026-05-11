using TMPro;
using UnityEngine;

public class UI_HardDisk_Widget : ToggleWidget<HardDiscStatus>
{
    [SerializeField] private TextMeshProUGUI Space, Price;

    void Start()
    {
        this.name = "HardDisk_Widget";
    }

    public void setStatus(HardDiscStatus status)
    {
        if (label != null) label.text = status.GetValue().Name;
        if (Space != null) Space.text = status.GetValue().TotalSpace.ToString();
        if (Price != null) Price.text = status.GetValue().Price.ToString() + " $";
        this.data = status;
    }

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }
}