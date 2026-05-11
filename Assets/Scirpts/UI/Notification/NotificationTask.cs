using UnityEngine;
using UnityEngine.InputSystem;

public class NotificationTask : NotificationOkCancel
{
    private Task task;

    public NotificationTask(string tittle, string description, Key Ok, Key Cancel, MonoBehaviour monoBehaviour)
    : base (tittle,description,Ok,Cancel,monoBehaviour){}

    public void setTask(Task task)
    {
        this.task = task;
    }

    protected override void OnSelectKey()
    {
        if (Ok.value <= 99f) return;
        PopUpTask popup = Resources.Load<PopUpTask>("UI/PopUp/PopUpTask");
        var pop = MonoBehaviour.Instantiate(popup,Vector3.zero,Quaternion.identity);
        pop.SetTask(task);
    }

    public override void Destroy()
    {
        if (this.Cancel.value >= 95f) TaskServer.Lock = false;
        base.Destroy();
    }
}