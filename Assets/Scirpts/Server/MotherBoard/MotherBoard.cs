using UnityEngine;

[System.Serializable]
public struct MotherBoard
{
    public CPUSockets socket;
    public MotherBoardMark motherBoardMark;
    public string Model;
    public float Watts;
    [Range(2,32)]
    public short MaxDisk;
    public float Price;
}