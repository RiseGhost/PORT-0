using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Server : SaveItem
{
    public string ID = Guid.NewGuid().ToString();
    public ServerStatusStruct serverStatus;
    public PositionStatus positionStatus = new PositionStatus();
    public string resource = "ServerBox";
    public List<TaskImplement> tasks = new List<TaskImplement>();

    public Server(){}

    public Server(ServerStatusStruct serverStatus)
    {
        this.serverStatus = serverStatus;
        GameObject gameObject = Resources.Load<GameObject>(resource);
        var server = MonoBehaviour.Instantiate(gameObject, positionStatus.pos + new Vector3(0, 5, 0), Quaternion.identity);
        server.GetComponent<ServerGameObject>().Init(this);
        Debug.Log("Server Create, status -> " + JsonUtility.ToJson(serverStatus));
        Save();
        PromisePay serverPay = new PromisePay();
        serverPay.PeriodicValue = 0f;
        serverPay.group = PayGroup.Expense;
        serverPay.payrate = 50000f;
        serverPay.type = PayType.Hardware;
        serverPay.StartValue = -1 * (serverStatus.cpu.getStatus().getPrice() 
                               + serverStatus.fanStatus.GetValue().Price 
                               + serverStatus.motherBoardStatus.GetValue().Price 
                               + serverStatus.disks.Select(x => x.GetValue().Price).Sum());
        MoneyBank.addPromisePay(serverPay);
    }

    public void Load(string json)
    {
        var data = JsonUtility.FromJson<Server>(json);
        GameObject gameObject = Resources.Load<GameObject>(resource);
        var server = MonoBehaviour.Instantiate(gameObject, positionStatus.pos + new Vector3(0, 5, 0), Quaternion.identity);
        server.GetComponent<ServerGameObject>().server = data;
        Debug.Log("\n\nLoader CPU Status is null -> " + (data.serverStatus.cpu.getStatus() == null) + "\n\n");
    }
    public string toJSON() { return JsonUtility.ToJson(this); }
    public string getID() { return ID; }

    public override bool Equals(object obj)
    {
        if (obj is Server)
        {
            return ((Server)obj).ID == this.ID;
        }
        return false;
    }

    public float getAvailableTFLOPS()
    {
        var taskFlops = tasks.Select(x => x.Tflops).Sum();
        return serverStatus.cpu.getMaxTFLOPS(serverStatus.fanStatus.GetValue()) - taskFlops;
    }

    public void Update()
    {
        StorageManager storageManager = GameObject.FindAnyObjectByType<StorageManager>();
        storageManager.UpdateData(this);
    }

    public void addTask(TaskImplement task)
    {
        TaskServer.Lock = false;
        tasks.Add(task);
        MoneyBank.addPromisePay(task.promisePay);
        Update();
    }
    
    public override string ToString()
    {
        return "";
    }

    public float getAvailableSpace()
    {
        List<float> space_disk = serverStatus.disks.Select((x) => x.GetValue().TotalSpace).ToList();
        float space = 0f;
        foreach (var item in space_disk)
        {
            space += item;
        }
        foreach (var task in tasks)
        {
            space -= task.getSpace();
        }
        return space;
    }
    
    public void Save()
    {
        StorageManager storageManager = GameObject.FindGameObjectWithTag("StorageManager").GetComponent<StorageManager>();
        if (storageManager == null) return;
        storageManager.Save(this);
    }
}