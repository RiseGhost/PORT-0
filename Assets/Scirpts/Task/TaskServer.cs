using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class TaskServer
{
    private static bool _lock = false;
    private static short TotalTask = 0;
    public static bool Lock
    {
        get => _lock;
        set
        {
            // Só dispara o log se o valor realmente mudar de sinal
            if (_lock != value)
            {
                _lock = value;

                // Captura quem chamou esta propriedade
                StackTrace stackTrace = new StackTrace();
                // O frame 1 é quem chamou o "set" do Lock (o frame 0 é o próprio set)
                var callingFrame = stackTrace.GetFrame(1);
                var callingMethod = callingFrame.GetMethod();
                string className = callingMethod.DeclaringType.Name;
                string methodName = callingMethod.Name;

                // Log formatado para o Unity Console
                UnityEngine.Debug.LogWarning($"[LOCK ALTERADO] Novo estado: <b>{_lock}</b> | Alterado por: <b>{className}.{methodName}()</b>");
            }
        }
    }
    private static bool EnergyBreak = false;
    public static Notification Last_Notification = null;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Reset(){ TotalTask = 0; }

    public TaskServer(TaskDifficulty difficulty,MonoBehaviour anchor)
    {
        if (!PowerSupply.Exist_Energy() || PowerSupply.PowerOut_Will_Exit())
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
            UnityEngine.Debug.LogWarning("TaskServer [LOCK ALTERADO]: Lock is true, not launching");
            return;
        }
        try{
            GameObject[] serversGameObjects = GameObject.FindGameObjectsWithTag("ServerGameObject");
            if (serversGameObjects == null)
            {
                UnityEngine.Debug.Log("TaskServer: Don't exist ServerGameObject in the scene");
                return;
            }
            ServerGameObject[] servers = serversGameObjects.Select(x => x.GetComponent<ServerGameObject>()).ToArray();
            int Expense_Servers = servers.Select(x => x.server.serverStatus.getWatts()).Where( x => x > ServerStatusList.get_MAX_WATTS_To_Warring()).Count();
            if (PowerSupply.PowerOut_Will_Exit() && !EnergyBreak)
            {
                Lock = true;
                EnergyBreak = true;
                NotificationServer.RemoveAll();
                anchor.StartCoroutine(PowerSupplyDown());
            }
            if (servers.Where(x => x.server.serverStatus.isOperational()).Count() <= 0)
            {
                UnityEngine.Debug.Log("TaskServer: Don't exist Operational ServerGameObject in the scene");
                return;
            }
            Install_OS_UI[] installOS = GameObject.FindObjectsByType<Install_OS_UI>(FindObjectsSortMode.None).ToArray();
            if (installOS != null && installOS.Length > 0)
            {
                UnityEngine.Debug.Log("TaskServer: Exist Install_OS_UI in the scene, not launching");
                return;
            }
            Task[] data     = Resources.Load<TaskTableObject>("Task/TaskTable").getTasks();
            Task[] tasks    = data.Where(x => x.getDifficulty() == difficulty).Select(x => x.clone()).ToArray();
            if (tasks.Length == 0) UnityEngine.Debug.Log("TaskServer: Don't exist Tasks to Launch");
            int randomIndex = UnityEngine.Random.Range(0,tasks.Length);
            Task selectTask = tasks[randomIndex];
            if (TotalTask == 1) selectTask.IncrementSpace(GameObject.FindFirstObjectByType<ServerGameObject>().server.getAvailableSpace() - selectTask.getSpace());
            Last_Notification = selectTask.Launch(anchor);
            TotalTask++;
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