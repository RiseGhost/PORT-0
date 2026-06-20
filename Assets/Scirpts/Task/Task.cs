using UnityEngine;

public interface Task
{
    public float getTflops();
    public string getName();
    public TaskDescription getTaskDescription();
    public MiniGame getMiniGame();
    public float getSpace();
    public MiniGameTechnologyAreaGroup getTechnologyAreaGroup();
    public TaskDifficulty getDifficulty();
    public Notification Launch(MonoBehaviour anchor);
    public Client GetClient();
}