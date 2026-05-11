using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OS_Widget : ToggleWidget<OperatingSystemStatus>
{
    [SerializeField] private RawImage icon;
    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(OperatingSystemStatus status)
    {
        if (label != null)  label.text = status.GetValue().DisplayName;
        if (icon != null)   icon.texture = status.GetValue().icon;
        data = status;
    }
}