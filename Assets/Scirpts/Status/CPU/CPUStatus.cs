[System.Serializable]
public class CPUStatus<T> : StatusImplement<T>
{
    public override string ToString()
    {
        #if UNITY_EDITOR
            return $"{{ type: {type} }}";
        #else
            return $"{{ type: {type}, value: {GetValue()} }}";
        #endif
    }
}