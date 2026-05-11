using UnityEngine;

[System.Serializable]
public class CPUMarkStatus : CPUStatus<CPUMark>
{
    void OnValidate()
    {
        this.type = StatusType.Mark_CPU;
    }
}