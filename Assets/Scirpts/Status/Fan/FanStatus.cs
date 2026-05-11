[System.Serializable]
public class FanStatus : StatusImplement<Fan>
{
    void OnValidate()
    {
        this.type = StatusType.Fan;
    }
}