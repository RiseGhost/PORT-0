using UnityEngine;

[CreateAssetMenu(fileName = "TaskTable" ,menuName = "ScriptTableObjects/TaskTable")]
public class TaskTableObject : ScriptableObject
{
    [SerializeField] private TaskImplement[] tasks;

    public TaskImplement[] getTasks(){ return tasks; }
}