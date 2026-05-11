using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class NotificationQue
{
    public List<Notification> top = new List<Notification>();
    public List<Notification> bottom = new List<Notification>();
    public List<Notification> right = new List<Notification>();
}

public class NotificationServer : MonoBehaviour
{
    [SerializeField] private string TopZoneID = "top", BottomZoneID = "bottom", RightZoneID = "right";
                            //  { Top have Notifications, Bottom Have Notifications, Right Have Notifications }
    private bool[] actives =    { false, false, false };
    private VisualElement root;
    private static NotificationQue notificationQue = new NotificationQue();
    private static event Action NewNotification;
    private static event Action<Notification> RemoveNotificationEvent;
    private static bool Starting = true;

    void Awake()
    {
        this.name = "Notification Server";
        Debug.Log("Notification Server: is ready ✅");
        VisualTreeAsset treeAsset = Resources.Load<VisualTreeAsset>("UI/Notification/NotificationUI");
        UIDocument document = gameObject.AddComponent<UIDocument>();
        document.visualTreeAsset = treeAsset;
        document.panelSettings = Resources.Load<PanelSettings>("UI/PauseMenu/Panel");
    }

    void Start()
    {
        StartCoroutine(Initialize());
        DontDestroyOnLoad(this);
    }

    private IEnumerator Initialize()
    {
        yield return null;
        root = GetComponent<UIDocument>().rootVisualElement;
        NewNotification += ProcessNewNotification;
        RemoveNotificationEvent += DestroyNotification;
        if (root == null)
        {
            Debug.Log("Notification Server: Don't have a VisualTreeAsset set. It's destroy.");
            Destroy(this);
        }
        Starting = false;
    }

    public static void AddNotification(Notification notification)
    {
        if (Starting)   return;
        switch (notification.GetZone())
        {
            case NotificationsZone.Top:
                notificationQue.top.Add(notification);
                break;
            case NotificationsZone.Bottom:
                notificationQue.bottom.Add(notification);
                break;
            case NotificationsZone.Right:
                notificationQue.right.Add(notification);
                break;
            default:
                break;
        }
        NewNotification.Invoke();
    }

    public static void RemoveNotification(Notification notification)
    {
        if (Starting) return;
        RemoveNotificationEvent.Invoke(notification);
    }

    private VisualElement GetVisualElementZone(NotificationsZone zone)
    {
        switch (zone)
        {
            case NotificationsZone.Top:
                return root.Q<VisualElement>(TopZoneID);
            case NotificationsZone.Bottom:
                return root.Q<VisualElement>(BottomZoneID);
            case NotificationsZone.Right:
                return root.Q<VisualElement>(RightZoneID);
            default:
                return null;
        }
    }
    
    private Notification GetNotificationByZone(NotificationsZone zone)
    {
        switch (zone)
        {
            case NotificationsZone.Top:
                return notificationQue.top.First();
            case NotificationsZone.Bottom:
                return notificationQue.bottom.First();
            case NotificationsZone.Right:
                return notificationQue.right.First();
            default:
                return null;
        }
    }

    private void AddNotificationUI(NotificationsZone zone)
    {
        Notification notification = GetNotificationByZone(zone);
        var VisualElement = GetVisualElementZone(zone);
        VisualElement.Add(notification.GetVisualElement());
        new NotificationAnimator(VisualElement,this,zone);
        StartCoroutine(ProcessDestroyNotification(notification));
    }

    private void ProcessNewNotification()
    {
        if (!actives[0] && notificationQue.top.Count > 0)
        {
            actives[0] = true;
            AddNotificationUI(NotificationsZone.Top);
        }
        else if (!actives[1] && notificationQue.bottom.Count > 0)
        {
            actives[1] = true;
            AddNotificationUI(NotificationsZone.Bottom);
        }
        else if (!actives[2] && notificationQue.right.Count > 0)
        {
            actives[2] = true;
            AddNotificationUI(NotificationsZone.Right);
        }
        else { }
    }
    
    private void DestroyNotification(Notification notification)
    {
        GetVisualElementZone(notification.GetZone()).RemoveAt(0);
        switch (notification.GetZone())
        {
            case NotificationsZone.Top:
                actives[0] = false;
                notificationQue.top.RemoveAt(0);
                break;
            case NotificationsZone.Bottom:
                actives[1] = false;
                notificationQue.bottom.RemoveAt(0);
                break;
            case NotificationsZone.Right:
                actives[2] = false;
                notificationQue.right.RemoveAt(0);
                break;
            default:
                break;
        }
    }

    private IEnumerator ProcessDestroyNotification(Notification notification)
    {
        if (notification.GetTTL() == 0){
            yield return new WaitForEndOfFrame();
        }
        else
        {
            yield return new WaitForSecondsRealtime(notification.GetTTL());
            RemoveNotificationEvent.Invoke(notification);
        }
    }
}