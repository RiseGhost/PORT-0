using UnityEngine.UIElements;

public interface Notification
{
    public VisualElement GetVisualElement();
    public string GetTittle();
    public string GetDescription();
    public NotificationsZone GetZone();
    public bool isForceView(); // It goes ahead of everyone on the list and is immediately Show.
    public float GetTTL(); // TTL -> Time to live.
    public void Show();
    public void Destroy();
}
