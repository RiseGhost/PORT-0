using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_FAN_Widget : ToggleWidget<FanStatus>
{
    [SerializeField] private TextMeshProUGUI Modelo, Price, TemperatureDecrement;
    [SerializeField] private RawImage icon;

    void Start()
    {
        this.name = "FAN_Widget";
    }

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(FanStatus status)
    {
        Modelo.text = status.GetValue().modelo;
        Price.text = status.GetValue().Price.ToString() + " $";
        TemperatureDecrement.text = "-" + status.GetValue().Temperature_Decrement.ToString() + "º C";
        if (icon != null) icon.texture = status.GetValue().icon;
        this.data = status;
    }
}