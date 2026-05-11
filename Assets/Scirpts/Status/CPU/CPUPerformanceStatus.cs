using System;

[System.Serializable]
public class CPUPerformanceStatus : CPUStatus<CPUPerformance>
{
    void OnValidate()
    {
        this.type = StatusType.CPU_Performance;
    }
}