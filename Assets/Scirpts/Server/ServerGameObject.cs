using System.Collections.Generic;
using UnityEngine;

public class ServerGameObject : MonoBehaviour, StorageEntity
{
    [SerializeField] private Canvas Warring;
    public Server server;
    public SaveItem GetSaveItem() { return server; }
    private bool heighlight = false;
    private Vector3 currentPosition = Vector3.zero;
    private ServerMonitorScreen serverMonitorScreen = null;
    private Rigidbody rigidbody;

    void OnDestroy()
    {
        // 1. Verifica se foi porque o jogo fechou ou a cena mudou (evita falsos alarmes)
        if (!Application.isPlaying) return;

        // 2. Captura o StackTrace detalhado do sistema
        string fullStackTrace = System.Environment.StackTrace;
        
        Debug.LogWarning($"⚠️ [SERVER DESTROYED] O objeto '{gameObject.name}' está a ser destruído!\n" +
                         $"Subiu a partir de: {fullStackTrace}");

        // 3. Método alternativo e ultra-focado (mostra quem chamou o Destroy explicitamente)
        System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(true);
        foreach (var frame in trace.GetFrames())
        {
            var method = frame.GetMethod();
            // Ignora os métodos da própriaUnity e o próprio OnDestroy
            if (method != null && 
                !method.Name.Contains("OnDestroy") && 
                !method.DeclaringType.Namespace.StartsWith("UnityEngine"))
            {
                Debug.LogError($"🎯 CULPADO ENCONTRADO: Classe [{method.DeclaringType.Name}] no método [{method.Name}] (Linha: {frame.GetFileLineNumber()})");
                break; // Mostra apenas o primeiro culpado fora do ecossistema Unity
            }
        }
    }

    void Start()
    {
        serverMonitorScreen = GetComponent<ServerMonitorScreen>();
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
        if (serverMonitorScreen != null) serverMonitorScreen.enabled = !PowerSupply.PowerOut_Will_Exit();
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