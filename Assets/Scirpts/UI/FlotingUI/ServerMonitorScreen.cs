using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct EfficiencyUI
{
    public Slider slider;
    public TextMeshProUGUI label;
}

public class ServerMonitorScreen : FloatingUI
{
    private GameObject Panel_Install_OS;
    private Server server;
    private EfficiencyUI efficiencyUI;
    void Start()
    {
        ServerGameObject serverGameObject = GetComponent<ServerGameObject>();
        if (serverGameObject == null) return;
        server = serverGameObject.server;
    }

    protected override void InitUI()
    {
        Panel_Install_OS = UI.transform.GetChild(3).GetComponent<GameObject>();
        efficiencyUI.slider = transform.GetChild(transform.childCount - 1).GetChild(2).GetChild(0).GetComponent<Slider>();
        efficiencyUI.label = transform.GetChild(transform.childCount - 1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();

        transform.GetChild(transform.childCount - 1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = server.serverStatus.cpu.getStatus().GetValue();
        transform.GetChild(transform.childCount - 1).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = server.serverStatus.cpu.status.getArchitect().GetValue().ToString();
    }

    protected override void EntryUpdate()
    {
        try
        {
            if (Panel_Install_OS != null) Panel_Install_OS.SetActive(!server.serverStatus.OS_Install);
            if (efficiencyUI.slider != null)
                efficiencyUI.slider.value = Mathf.Lerp(efficiencyUI.slider.value, (server.serverStatus.cpu.getMaxTFLOPS(server.serverStatus.fanStatus.GetValue())/server.serverStatus.cpu.getTFLOPS()), Time.deltaTime * 0.3f);
        
            if (efficiencyUI.label != null)
                efficiencyUI.label.text = Mathf.Round((server.serverStatus.cpu.getMaxTFLOPS(server.serverStatus.fanStatus.GetValue())/server.serverStatus.cpu.getTFLOPS()) * 100).ToString() + " %";
        } catch (Exception e) {}
    }
}