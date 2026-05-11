using UnityEngine;

[CreateAssetMenu(fileName = "OSTable", menuName = "ScriptTableObjects/OperationSystem/Table")]
public class OSTable : ScriptableObject
{
    public OperatingSystemStatus[] os;

    public static OperatingSystem? getSystem(OperatingSystemName name, OSTable os)
    {
        foreach (var system in os.os)
        {
            if (system.GetValue().Name.Equals(name)) return system.GetValue();
        }
        return null;
    }
}