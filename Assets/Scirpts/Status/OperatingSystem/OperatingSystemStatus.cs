[System.Serializable]
public class OperatingSystemStatus : StatusImplement<OperatingSystem>
{
    void OnValidate()
    {
        this.type = StatusType.OperatingSystem;
    }

    public override string ToString()
    {
        return "OS { Name: " + GetValue().Name + " }";
    }
}