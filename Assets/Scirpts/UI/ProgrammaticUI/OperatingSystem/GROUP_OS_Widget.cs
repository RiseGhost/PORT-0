using UnityEngine;

public class GROUP_OS_Widget : GroupToggle<OperatingSystemStatus>
{
    [SerializeField] private UI_OS_Widget OS_Widget_Template;

    void Start()
    {
        getWidget().allowSwitchOff = true;
        foreach(OperatingSystemStatus osStatus in Status)
        {
            var os = Instantiate(OS_Widget_Template, transform);
            os.getWidget().group = getWidget();
            os.setStatus(osStatus);
            os.setDefaultColor(defaultColor);
            os.setSelectColor(selectColor);
            os.setSelectTextColor(selectTextColor);
            os.setDefaultTextColor(defaultTextColor);
        }
    }

    public override OperatingSystemStatus getSelect()
    {
        foreach(Transform child in transform)
        {
            var os = child.gameObject.GetComponent<UI_OS_Widget>();
            if (os == null) continue;
            if (os.isSelect()) return os.getData();
        }
        return null;
    }
}