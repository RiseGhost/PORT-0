using UnityEngine;

[System.Serializable]
public struct HardDrive
{
    [Range(0, 8000)]
    public float TotalSpace;
    [Range(0, 8000)]
    public float UseSpace;
    public string Name;
    public float Price;
    public Texture2D icon;
}