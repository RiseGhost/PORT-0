using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleWidget<E> : ProgrammaticWidget<Toggle, E>
{
    private Color selectColor;
    private Color defaultColor;
    private Color selectTextColor;
    private Color defaultTextColor;
    [SerializeField] protected TextMeshProUGUI label;
    
    public override void setTitle(string title)
    {
        this.title = title;
        Text text = new GameObject().AddComponent<Text>();
        text.text = title;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.color = Color.black;
        text.name = "Text";
        text.alignment = TextAnchor.MiddleCenter;
        RectTransform rect = text.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        text.transform.SetParent(widget.transform);
    }

    public bool isSelect() { return widget.isOn; }

    public void setSelectColor(Color selectColor) { this.selectColor = selectColor; }
    public void setDefaultColor(Color defaultColor) { this.defaultColor = defaultColor; }
    public void setSelectTextColor(Color selectTextColor) { this.selectTextColor = selectTextColor; }
    public void setDefaultTextColor(Color defaultTextColor) { this.defaultTextColor = defaultTextColor; }

    protected void setTextColor(Color color)
    {
        label.color = color;
    }

    void Update()
    {
        setBackground((isSelect()) ? selectColor : defaultColor);
        setTextColor((isSelect()) ? selectTextColor : defaultTextColor);
    }
}