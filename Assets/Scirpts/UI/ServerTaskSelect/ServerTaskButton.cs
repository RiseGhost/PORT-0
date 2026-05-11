using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerTaskButton : ToggleWidget<ServerGameObject>
{
    private ServerChoiceCamera camera;
    public bool block = false;
    [SerializeField] private Color TextColor, TextSelectedColor;
    [SerializeField] private GameObject[] Checks;
    
    void Start()
    {
        if (widget == null) Destroy(gameObject);
        widget.isOn = false;
        widget.onValueChanged.AddListener(NotifyFather);
        widget.group = transform.parent.GetComponent<ToggleGroup>();
    }
    
    void OnToggleSelect()
    {
        if (data != null) data.Heighlight();
        if (label != null) label.color = TextSelectedColor;
        if (block) return;
        Camera FocusCamera = GameObject.FindAnyObjectByType<CameraSwitch>().Switch_Focus_Camera();
        if (FocusCamera == null) return;
        var follow = FocusCamera.GetComponent<CamerarFollowServer>();
        if (follow == null) return;
        Transform target = data.transform;
        Vector3 pos = target.position + (target.forward * camera.distance) + new Vector3(0f,camera.height,0f);
        follow.setTargetPos(pos,data.transform.position);
    }
    
    private void OnToggleDeselect()
    {
        if (data != null) data.Unhighlight();
        if (label != null) label.color = TextColor;
    }

    void OnDestroy()
    {
        OnToggleDeselect();
    }

    private void Update()
    {
        if (data == null) return;
        if (label != null) label.text = data.server.serverStatus.HostName;
        if (Checks != null)
        {
            foreach (GameObject check in Checks)
            {
                check.SetActive(widget.isOn);
            }
        }
    }   

    public void setServer(ServerGameObject server, ServerChoiceCamera camera)
    {
        this.data = server;
        this.camera = camera;
        Debug.Log("Server Host Name = " + server.server.serverStatus.HostName);
        if (label != null) label.text = server.server.serverStatus.HostName;
    }

    public void NotifyFather(bool isOn)
    {
        if (isOn) OnToggleSelect();
        else OnToggleDeselect();
        GetComponentInParent<ServerTaskSelect>().BlockChildren(isOn);
    }

    public override void setDescription(string description)
    {
        throw new NotImplementedException();
    }
}