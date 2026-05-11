/*
    Interface Mini Game 🎮

    Methods:
        bool isCompleted        -> Return true, if Mini Game are completed and return false in Mini Game not completed
        float getScores         -> Return a number that represent score gain in this Mini Game
        string getName          -> Return the name of Mini Game, for example: "Word Rush", "Match Game", etc...
        TaskType getTaskType()  -> Return the type of task that represent this mini game
        MiniGameTechnologyAreaGroup getMiniGameTechnologyAreaGroup() -> Return the group of the technologies use to completed this Mini Game.
        void save()             -> Save the score
        void Start()            -> Play a Mini Game (Start the Mini Game Scene)
*/

public interface MiniGame
{
    public bool isCompleted();
    public void setCompleted();
    public float setScore(float score);
    public float getScore();
    public string getName();
    public TaskType getTaskType();
    public MiniGameTechnologyAreaGroup getMiniGameTechnologyAreaGroup();
    public void save();
    public void Start(Server server, Task task);
}
