using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ServerChoiceCamera
{
    public float distance;
    public float height;
    public Quaternion angle;
}

[System.Serializable]
public struct ServerLostCard
{
    public TextMeshProUGUI CurrentTFLOPS;
    public TextMeshProUGUI AfterTFLOPS;
    public TextMeshProUGUI CurrentSpace;
    public TextMeshProUGUI AfterSpace;
    public GameObject gameobject;
    public RawImage arrow;
}

public class ServerTaskSelect : MonoBehaviour
{
    private ServerGameObject[] servers;
    [SerializeField] private Button cancel_btn_template;
    [SerializeField] private Button next_btn_template;
    [SerializeField] private ServerTaskButton button_template;
    [SerializeField] private GameObject Buttons_Container;
    [SerializeField] private ToggleGroup group;
    [SerializeField] private ServerChoiceCamera camera;
    [SerializeField] private TaskSelectServerDescription serverDescriptionTemplate;
    [SerializeField] private ServerLostCard lostCard;
    private Server server = null;
    private Button next_btn = null;
    private Task task = null;
    private List<ServerTaskButton> buttons = new List<ServerTaskButton>();

    void Start()
    {
        if (lostCard.gameobject != null) lostCard.gameobject.SetActive(false);
        if (lostCard.arrow != null) lostCard.gameobject.SetActive(false);
        if (button_template == null || Buttons_Container == null)
        {
            Destroy(this.gameObject);
            return;
        }
        ListServer();
    }

    void Update()
    {
        if (next_btn == null || group == null) return;
        next_btn.gameObject.SetActive(group.AnyTogglesOn());
    }
    
    void OnDestroy()
    {
        CameraSwitch c_switch = GameObject.FindAnyObjectByType<CameraSwitch>();
        if (c_switch == null) return;
        c_switch.Switch_main_camera();
    }

    private void CleanContainer()
    {
        foreach (Transform child in Buttons_Container.transform)
            Destroy(child.gameObject);
    }
    
    public void BlockChildren(bool isOn)
    {
        foreach (var serverTaskButton in buttons)
        {
            serverTaskButton.block = isOn;
        }
    }

    private void ListServer()
    {
        ServerGameObject[] s = FindObjectsByType<ServerGameObject>(FindObjectsSortMode.None);
        servers = s.Where(x => x.server.serverStatus.isOperational()).ToArray();
        foreach (var server in servers)
        {
            Transform container = Buttons_Container.transform;
            ServerTaskButton button = Instantiate(button_template,container).GetComponent<ServerTaskButton>();
            button.setServer(server,camera);
            buttons.Add(button);
        }

        if (next_btn_template != null)
        {
            next_btn = Instantiate(next_btn_template, Buttons_Container.transform).GetComponent<Button>();
            next_btn.onClick.AddListener(() =>
            {
                if (serverDescriptionTemplate == null) return;
                ServerTaskButton toggle = group.ActiveToggles().First().GetComponent<ServerTaskButton>();
                server = toggle.getData().server;
                CleanContainer();
                if (lostCard.gameobject != null)
                {
                    lostCard.gameobject.SetActive(true);
                    lostCard.arrow.gameObject.SetActive(true);
                    lostCard.CurrentTFLOPS.text = server.getAvailableTFLOPS().ToString();
                    lostCard.CurrentSpace.text = server.getAvailableSpace().ToString();
                    lostCard.AfterTFLOPS.text = (server.getAvailableTFLOPS() - task.getTflops()).ToString();
                    lostCard.AfterSpace.text = (server.getAvailableSpace() - task.getSpace()).ToString();
                }
                TaskSelectServerDescription serverDescription = Instantiate(this.serverDescriptionTemplate,Buttons_Container.transform).GetComponent<TaskSelectServerDescription>();
                serverDescription.setServer(toggle.getData().server);
                Button confirm_btn = Instantiate(next_btn_template, Buttons_Container.transform).GetComponent<Button>();
                confirm_btn.GetComponentInChildren<TextMeshProUGUI>().text = "Confirm";
                confirm_btn.onClick.AddListener(() =>
                {
                    if (task == null) return;
                    task.getMiniGame().Start(server, task);
                    Destroy(gameObject);
                    return;
                });
                Button back_btn = Instantiate(cancel_btn_template, Buttons_Container.transform).GetComponent<Button>();
                back_btn.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
                back_btn.onClick.AddListener(() =>
                {
                    if (lostCard.gameobject != null) lostCard.gameobject.SetActive(false);
                    if (lostCard.arrow != null) lostCard.arrow.gameObject.SetActive(false);
                    CleanContainer();
                    ListServer();
                });
                GetComponentInChildren<FixedSelect>().setDefaultIndex(1);
            });
        }
            
        if (cancel_btn_template != null)
        {
            Button cancel_btn = Instantiate(cancel_btn_template,Buttons_Container.transform).GetComponent<Button>();
            cancel_btn.onClick.AddListener(() => { Destroy(this.gameObject); TaskServer.Lock = false; });
        }
    }

    public void setTask(Task task)
    {
        this.task = task;
    }

    public Task getTask()
    {
        return task;
    }
}
