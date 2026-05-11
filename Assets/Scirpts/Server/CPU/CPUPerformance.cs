using UnityEngine;

[System.Serializable]
public struct CPUPerformance
{
    public CPUFrequencies min;
    public CPUFrequencies medium;
    public CPUFrequencies max;
    [Range(0,64)]
    public ushort cores;
    [Range(0,128)]
    public ushort threads;
    public float MaxTemperature;
}