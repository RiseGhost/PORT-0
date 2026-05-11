[System.Serializable]
public class HardDiscStatus : StatusImplement<HardDrive>
{
    void OnValidate()
    {
        this.type = StatusType.HarDrive;
    }

    public override string ToString()
    {
        return "Disk{ Nome: " + GetValue().Name + " Total space: " + GetValue().TotalSpace + " Price: " + GetValue().Price + " }";
    }
}