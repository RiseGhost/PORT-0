using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerStatusStruct
{
    public CPU cpu;
    public FanStatus fanStatus = new FanStatus();
    public MotherBoardStatus motherBoardStatus = new MotherBoardStatus();
    public OperatingSystem os = new OperatingSystem();
    public List<HardDiscStatus> disks = new List<HardDiscStatus>();
    public bool OS_Install = false;
    public string HostName = "";
    public string Password = "";

    public void setCPU(ProcessorStatus status) { cpu = new CPU(status); }
    public void Install_OS() { OS_Install = true; }
    public void setHostName(string HostName){ this.HostName = HostName; }
    public bool isOperational(){ return cpu != null && fanStatus != null && motherBoardStatus != null && disks != null && OS_Install; }
}