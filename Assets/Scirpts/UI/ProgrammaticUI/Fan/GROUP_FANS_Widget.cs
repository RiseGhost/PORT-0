using UnityEngine;

public class GROUP_FANS_Widget : GroupToggle<FanStatus>
{
    [SerializeField] private UI_FAN_Widget template;

    void Start()
    {
        foreach (FanStatus fan in Status)
        {
            var f = Instantiate(template, transform);
            f.setStatus(fan);
            f.getWidget().group = getWidget();
            f.setDefaultColor(defaultColor);
            f.setSelectColor(selectColor);
            f.setSelectTextColor(selectTextColor);
            f.setDefaultTextColor(defaultTextColor);
        }
    }

    public override FanStatus getSelect()
    {
        foreach (Transform child in transform)
        {
            UI_FAN_Widget fan = child.GetComponent<UI_FAN_Widget>();
            if (fan == null) continue;
            if (fan.isSelect()) return fan.getData();
        }
        return null;
    }
}