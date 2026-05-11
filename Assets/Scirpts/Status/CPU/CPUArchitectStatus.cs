using UnityEngine;

[System.Serializable]
public class CPUArchitectStatus : CPUStatus<CPUArchitect>
{
    void OnValidate()
    {
        this.type = StatusType.Architect_CPU;
    }
}