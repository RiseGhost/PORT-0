using UnityEngine;
using UnityEngine.UIElements;

/*
    Description:
        This class is responsible for controlling basic notifications.
        Basic notifications only have a background color and tow labels, one being the title and the other a short description.
        This UI is a UIDocument (XML Layout) not a Canvas UI

    Attributes:
        Label Tittle        -> This is the label that contains the title
        Label Description   -> This is the label that contains the description
*/

public class NotificationDefault : NotificationImplement
{
    private Label Tittle, Description;
    public NotificationDefault()
    {
        visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/Notification/Default/Default");
        root = visualTreeAsset.CloneTree();
        Tittle = root.Q<Label>("Tittle");
        Description = root.Q<Label>("Description");
    }

    public NotificationDefault(string tittle, string description)
    {
        visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/Notification/Default/Default");
        root = visualTreeAsset.CloneTree();
        this.tittle = tittle;
        this.description = description;
        Tittle = root.Q<Label>("Tittle");
        Description = root.Q<Label>("Description");
        Tittle.text = tittle;
        Description.text = description;
    }

    public override VisualElement GetVisualElement()
    {
        return root;
    }

    public override NotificationsZone GetZone()
    {
        return NotificationsZone.Top;
    }
}