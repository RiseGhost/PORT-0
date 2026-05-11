using System;
using System.Linq;
using UnityEngine;

public class TaskServer
{
    public static bool Lock = false;
    public TaskServer(TaskDifficulty difficulty,MonoBehaviour anchor)
    {
        if (Lock)
        {
            Debug.Log("TaskServer: Lock is true, not launching");
            return;
        }
        try
        {
            GameObject[] serversGameObjects = GameObject.FindGameObjectsWithTag("ServerGameObject");
            if (serversGameObjects == null)
            {
                Debug.Log("TaskServer: Don't exist ServerGameObject in the scene");
                return;
            }
            ServerGameObject[] servers = serversGameObjects.Select(x => x.GetComponent<ServerGameObject>()).ToArray();
            if (servers.Where(x => x.server.serverStatus.isOperational()).Count() <= 0)
            {
                Debug.Log("TaskServer: Don't exist Operational ServerGameObject in the scene");
                return;
            }
            Task[] data     = Resources.Load<TaskTableObject>("Task/TaskTable").getTasks();
            Task[] tasks    = data.Where(x => x.getDifficulty() == difficulty).ToArray();
            if (tasks.Length == 0) Debug.Log("TaskServer: Don't exist Tasks to Launch");
            int randomIndex = UnityEngine.Random.Range(0,tasks.Length);
            tasks[randomIndex].Launch(anchor);
        } catch (Exception e){}
    }
}