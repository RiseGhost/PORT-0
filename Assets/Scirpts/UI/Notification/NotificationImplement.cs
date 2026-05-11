using UnityEngine;
using UnityEngine.UIElements;

/*
    Description:
        This is the base implementation of a notifications controller. It is responsible for controlling
        the data that will appear in the notification UI.

    Attributes:
        VisualTreeAsset visualTreeAsset     -> It is the object responsible for directly reading XML layout information
        VisualElement root                  -> The object that graphically represent this notification (this)
        string tittle                       -> The value that will appear in the UI Title field
        string description                  -> The value that will appear in the UI Description field
        float TTL                           -> Time to live. The time interval in which the notification will be visible.
                                                For Example TTL = 3, The notification will be visible for 3 seconds.
                                                If TTL = 0, The notification will be visible forever.
*/

public abstract class NotificationImplement : Notification
{
    protected VisualTreeAsset visualTreeAsset;
    protected VisualElement root;
    protected string tittle, description;
    protected float TTL = 3; // Time to Live

    public abstract VisualElement GetVisualElement();

    public string GetTittle() { return tittle; }
    public string GetDescription() { return description; }
    public bool isForceView() { return false; }
    public float GetTTL(){ return TTL; }

    public void Show()
    {
        NotificationServer.AddNotification(this);
    }

    public virtual void Destroy()
    {
        NotificationServer.RemoveNotification(this);
    }

    public abstract NotificationsZone GetZone();

    public override bool Equals(object other)
    {
        if (other is Notification)
        {
            var obj = (Notification)other;
            return obj.GetZone() == this.GetZone()
                && obj.GetVisualElement() == this.GetVisualElement()
                && obj.GetTittle() == this.GetTittle()
                && obj.GetDescription() == this.GetDescription();
        }
        return false;
    }
}