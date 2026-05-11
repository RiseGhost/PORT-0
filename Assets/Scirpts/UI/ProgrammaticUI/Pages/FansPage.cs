using UnityEngine;

public class FansPage : UIPages
{
    [SerializeField] private GROUP_FANS_Widget FANS_Widget;
    private FanStatus fanStatus;

    void Start()
    {
        if (FANS_Widget == null) Destroy(this);
    }

    void Update()
    {
        Next.interactable = (FANS_Widget.getSelect() != null);
        if (FANS_Widget.getSelect() != fanStatus)
        {
            if (FANS_Widget.getSelect() == null) progressionPageWidget.DecrementValue(100);
            else if (fanStatus == null) progressionPageWidget.IncrementValue(100);
            fanStatus = FANS_Widget.getSelect();
        }
    }

    public GROUP_FANS_Widget getGrounpFans() { return FANS_Widget; }
}