using UnityEngine;

[System.Serializable]
public class ProcessorStatus : StatusImplement<string>
{
    void OnValidate()
    {
        this.type = StatusType.Processor;
    }

    [SerializeField] private CPUStatus<CPUArchitect> Architect;
    [SerializeField] private CPUStatus<CPUMark> Mark;
    [SerializeField] private CPUStatus<CPUPerformance> CPUPerformance;
    [SerializeField] private CPUSockets sockets;
    [SerializeField] private float Price;
    [SerializeField] private float Watts;
    [SerializeField] private Texture2D icon;
    [SerializeField] private Exhibitor exhibitor;

    public CPUStatus<CPUArchitect> getArchitect() { return Architect; }
    public CPUStatus<CPUMark> getMark() { return Mark; }
    public CPUStatus<CPUPerformance> getPerformance() { return CPUPerformance; }
    public float getPrice() { return Price; }
    public float getWatts() { return Watts; }
    public Texture2D getIcon() { return icon; }
    public CPUSockets GetCPUSockets() { return sockets; }
    public Exhibitor GetExhibitor() { return exhibitor; }
}