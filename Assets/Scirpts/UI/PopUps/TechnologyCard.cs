using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
    Description:
        This class represent a technology card within that belongs to a TechnologyCardGroup class.
*/

public class TechnologyCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LabelName;
    [SerializeField] private RawImage icon;
    private MiniGameTechnologyArea area;
    
    public void setTechnologyArea(MiniGameTechnologyArea area)
    {
        this.area = area;
        if (LabelName == null)  LabelName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (icon == null)       icon = transform.GetChild(0).GetComponent<RawImage>();
        LabelName.text = area.technology.ToString();
        icon.texture = area.icon;
    }
}