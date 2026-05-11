using UnityEngine;

[System.Serializable]
public struct Fan
{
    public Texture2D icon;
    public FanNoise noise;
    public string modelo;
    public float Temperature_Decrement;
    public float Price;
    public float Watts;
    public Exhibitor exhibitor;
}