using System;
using UnityEngine;

public class ToggleEmail : ToggleWidget<Email>
{
    [SerializeField] private Color DefaultColor, SelectColor, TextColor = Color.black, DefaultTextColor = Color.black;

    void Start(){
        setDefaultColor(DefaultColor);
        setSelectColor(SelectColor);
        setTextColor(TextColor);
        setDefaultTextColor(DefaultTextColor);
        setSelectTextColor(TextColor);
    }

    public void setSubject(String Subject)
    {
        if (label != null) label.text = Subject;
    }

    public override void setDescription(string description){
        throw new System.NotImplementedException();
    }
}
