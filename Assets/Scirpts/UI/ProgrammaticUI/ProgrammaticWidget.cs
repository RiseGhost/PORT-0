using UnityEngine;
using UnityEngine.UI;

public abstract class ProgrammaticWidget<T, E> : MonoBehaviour where T : Component
{
    protected T widget;
    protected string title;
    protected string description;
    protected Color BackgroundColor;
    public E data;

    void Awake()
    {
        widget = GetComponent<T>();
        if (widget == null) widget = gameObject.AddComponent<T>();
    }

    public abstract void setTitle(string title);
    public abstract void setDescription(string description);
    public void setBackground(Color color)
    {
        BackgroundColor = color;
        if (GetComponent<RawImage>() == null) gameObject.AddComponent<RawImage>();
        GetComponent<RawImage>().color = color;
    }

    public T getWidget() { return widget; }
    public E getData() { return data; }
}