using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class NotificationAnimator
{
    private VisualElement element;
    private float speed = 350.0f;

    public NotificationAnimator(VisualElement element, NotificationServer server)
    {
        this.element = element;
        this.element.style.top = -280;
        server.StartCoroutine(AnimatorTop());
    }
    
    public NotificationAnimator(VisualElement element, NotificationServer server, NotificationsZone zone)
    {
        this.element = element;
        this.element.style.top = -280;
        switch (zone)
        {
            case NotificationsZone.Top:
                server.StartCoroutine(AnimatorTop());
                break;
            default:
                break;
        }
    }

    private IEnumerator AnimatorTop()
    {
        while (element.style.top.value.value < 10)
        {
            yield return new WaitForSeconds(0.008f);
            var y = element.style.top.value.value;
            y += speed * 0.008f;
            element.style.top = y;
        }
    }
}