using UnityEngine;

[CreateAssetMenu(fileName = "CPU_Status_List" ,menuName = "ScriptTableObjects/Status/CPU_List")]
public class CPUSStatusList : ScriptableObject
{
    public ProcessorStatus[] processorStatuses;
}