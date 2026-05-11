using TMPro;
using UnityEngine;

public class TaskSelectServerDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Host, CPU, FreeSpace, TFLOPS, Tasks, AvailableTFLOPS;

    public void setServer(Server server)
    {
        if (server == null) return;
        ServerStatusStruct serverStatus = server.serverStatus;
        if (Host != null) Host.text = serverStatus.HostName;
        if (CPU != null) CPU.text = serverStatus.cpu.ToString();
        if (FreeSpace != null) FreeSpace.text = server.getAvailableSpace().ToString();
        if (TFLOPS != null) TFLOPS.text = serverStatus.cpu.getTFLOPS().ToString();
        if (Tasks != null) Tasks.text = server.tasks.Count.ToString();
        if (AvailableTFLOPS != null) AvailableTFLOPS.text = server.getAvailableTFLOPS().ToString();
    }
}
