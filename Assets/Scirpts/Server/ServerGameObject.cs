using System.Collections.Generic;
using UnityEngine;

public class ServerGameObject : MonoBehaviour, StorageEntity
{
    [SerializeField] private Canvas Warring;
    public Server server;
    public SaveItem GetSaveItem() { return server; }
    private bool heighlight = false;
    private Vector3 currentPosition = Vector3.zero;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ServersPlace place = GameObject.FindFirstObjectByType<ServersPlace>();
        if (place == null) return;
        place.put(this);
        this.tag = "ServerGameObject";
    }

    public void Init(Server server)
    {
        this.server = server;
    }

    void Update()
    {
        if (heighlight)
        {
            transform.position = Vector3.Lerp(transform.position,currentPosition + new Vector3(0,2f,0),Time.deltaTime);
        }
        else currentPosition = transform.position;
    }
    
    void FixedUpdate()
    {
        server.positionStatus = new PositionStatus(transform);
        Warring.gameObject.SetActive(!server.serverStatus.isOperational());
    }

    public override bool Equals(object other)
    {
        if (other is ServerGameObject)
        {
            return ((ServerGameObject)other).GetSaveItem().Equals(this.GetSaveItem());
        }
        return false;
    }

    public void addTask(TaskImplement task)
    {
        if (task == null) return;
        server.tasks.Add(task);
        StorageManager storageManager = GameObject.FindAnyObjectByType<StorageManager>();
        if (storageManager == null) return;
        storageManager.UpdateData(server);
    }

    public List<TaskImplement> getTasks()
    {
        return server.tasks;
    }
    
    public void Heighlight()
    {
        if (rigidbody != null) rigidbody.useGravity = false;
        heighlight = true;
    }

    public void Unhighlight()
    {
        if (rigidbody != null) rigidbody.useGravity = true;
        heighlight = false;
    }
}