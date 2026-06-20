using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class TaskServer
{
    public static bool Lock = false;
    private static bool EnergyBreak = false;
    private Notification Last_Notification = null;
    public TaskServer(TaskDifficulty difficulty,MonoBehaviour anchor)
    {
        if (!PowerSupply.Exist_Energy())
        {
            Lock = true;
            if (Last_Notification != null)
            {
                NotificationServer.RemoveNotification(Last_Notification);
                Last_Notification = null;
                return;
            }
        }
        if (Lock){
            Debug.Log("TaskServer: Lock is true, not launching");
            if (Last_Notification != null)
            {
                NotificationServer.RemoveNotification(Last_Notification);
                Last_Notification = null;
            }
            return;
        }
        try{
            GameObject[] serversGameObjects = GameObject.FindGameObjectsWithTag("ServerGameObject");
            if (serversGameObjects == null)
            {
                Debug.Log("TaskServer: Don't exist ServerGameObject in the scene");
                return;
            }
            ServerGameObject[] servers = serversGameObjects.Select(x => x.GetComponent<ServerGameObject>()).ToArray();
            int Expense_Servers = servers.Select(x => x.server.serverStatus.getWatts()).Where( x => x > ServerStatusList.get_MAX_WATTS_To_Warring()).Count();
            if (Expense_Servers >= 2 && !EnergyBreak)
            {
                Lock = true;
                EnergyBreak = true;
                NotificationServer.RemoveAll();
                anchor.StartCoroutine(PowerSupplyDown());
            }
            if (servers.Where(x => x.server.serverStatus.isOperational()).Count() <= 0)
            {
                Debug.Log("TaskServer: Don't exist Operational ServerGameObject in the scene");
                return;
            }
            Install_OS_UI[] installOS = GameObject.FindObjectsByType<Install_OS_UI>(FindObjectsSortMode.None).ToArray();
            if (installOS != null && installOS.Length > 0)
            {
                Debug.Log("TaskServer: Exist Install_OS_UI in the scene, not launching");
                return;
            }
            Task[] data     = Resources.Load<TaskTableObject>("Task/TaskTable").getTasks();
            Task[] tasks    = data.Where(x => x.getDifficulty() == difficulty).ToArray();
            if (tasks.Length == 0) Debug.Log("TaskServer: Don't exist Tasks to Launch");
            int randomIndex = UnityEngine.Random.Range(0,tasks.Length);
            Last_Notification = tasks[randomIndex].Launch(anchor);
        } catch (Exception e){}
    }

    private IEnumerator PowerSupplyDown()
    {
        yield return new WaitForSeconds(2f);
        PowerSupply.Break();
        yield return new WaitForSeconds(6f);
        EmailBox emailBox = GameObject.FindAnyObjectByType<EmailBox>();
        if (emailBox != null){
            emailBox.AddEmail(new Email(EmailType.PowerSupply));
        }
    }
}