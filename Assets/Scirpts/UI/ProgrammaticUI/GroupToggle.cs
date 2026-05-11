using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class GroupToggle<E> : ProgrammaticWidget<ToggleGroup, object>
{
    [SerializeField] protected Color selectColor;
    [SerializeField] protected Color defaultColor;
    [SerializeField] protected Color selectTextColor;
    [SerializeField] protected Color defaultTextColor;
    public bool AutoSelectChild = false;
    public E[] Status;

    public override void setTitle(string title)
    {
        throw new NotImplementedException();
    }

    public override void setDescription(string description)
    {
        throw new NotImplementedException();
    }

    public abstract E getSelect();
}