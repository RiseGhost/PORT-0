using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MotherBoard_Widget : ToggleWidget<MotherBoardStatus>
{
    [SerializeField] private TextMeshProUGUI Mark, Price, DisckNumber;
    [SerializeField] private RawImage icon;

    public override void setDescription(string description)
    {
        throw new System.NotImplementedException();
    }

    public void setStatus(MotherBoardStatus status)
    {
        MotherBoard motherBoard = status.GetValue();
        if (label != null) label.text = motherBoard.Model;
        if (Mark != null) Mark.text = motherBoard.motherBoardMark.ToString();
        if (Price != null) Price.text = motherBoard.Price.ToString() + " $";
        if (DisckNumber != null) DisckNumber.text = motherBoard.MaxDisk.ToString();
        data = status;
    }
}