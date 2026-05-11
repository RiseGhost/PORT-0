using System;

[System.Serializable]
public struct UIElementAnimation
{
    public string ElementID;
    public UIElementParameters[] parameters;
}

[System.Serializable]
public struct UIElementParameters
{
    public AnimationParameter parameter;
    public float startValue;
    public float endValue;
    public float speed;
    [NonSerialized]
    public bool Finish;
}
