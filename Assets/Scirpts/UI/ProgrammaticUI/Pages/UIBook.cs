using System.Collections.Generic;
using UnityEngine;

public interface UIBook
{
    public void nextPage();
    public void backPage();
    public UIPages getPreviousPage();
    public List<UIPages> getAllPages();
    //This Func execute when pages end
    public void BackOver();
    public void Close();
    public GameObject getGameObject();
}