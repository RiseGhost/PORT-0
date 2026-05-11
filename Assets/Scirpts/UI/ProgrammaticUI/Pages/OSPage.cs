using UnityEngine;

public class OSPage : UIPages
{
    [SerializeField] private GROUP_OS_Widget os_Widget;
    private bool currentState = false;

    void Update()
    {
        var select = os_Widget.getSelect();
        Next.interactable = (select != null);
        if (currentState != Next.interactable)
        {
            if (Next.interactable == true) progressionPageWidget.IncrementValue(100);
            else progressionPageWidget.DecrementValue(100);
            currentState = Next.interactable;
        }
    }

    public GROUP_OS_Widget getGROUP_OS_Widget() { return os_Widget;}
}