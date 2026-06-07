using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WordRushImplement: MiniGame
{
    [SerializeField] private TeachPhrasesTableObject phrases;
    [SerializeField] private TaskType type;
    [SerializeField] private MiniGameTechnologyAreaGroup technologyGroup;
    private OperatingSystem os;
    private bool completed = false;
    private static TaskImplement _task;
    private static Server _server;
    
    private static bool technologyAreaContains(MiniGameTechnologyArea technologyArea)
    {

        return false;
    }

    public WordRushImplement(TaskType type, MiniGameTechnologyAreaGroup technologyGroup)
    {
        WordRushTable data = Resources.Load<WordRushTable>("Task/MiniGame/WordRush/WordRush Table");
        List<WordRushImplement> content = data.wordRushes.Where(x => 
        x.type == type 
        && x.getMiniGameTechnologyAreaGroup().ContainsAllArea(technologyGroup)).ToList();
        if (content.Count == 0)
        {
            throw new Exception("Not exist WordRushImplement in resource with type = " + type.ToString() + " technologyGroup = " + technologyGroup.ToString());
            return;
        }
        var randomIndex = UnityEngine.Random.Range(0,content.Count);
        var selectContent = content[randomIndex];
        this.type = selectContent.getTaskType();
        this.technologyGroup = selectContent.getMiniGameTechnologyAreaGroup();
        this.phrases = selectContent.phrases;
    }

    public bool isCompleted(){ return completed; }
    public float getScore(){ return 0; }
    public float setScore(float score){ return score; }
    public void setCompleted() { completed = true; }
    public string getName(){ return "WordRush";}
    public TaskType getTaskType() { return type; }
    public MiniGameTechnologyAreaGroup getMiniGameTechnologyAreaGroup() { return technologyGroup; }
    public void save()
    {
        throw new NotImplementedException();
    }
    
    public void Start(Server server, Task task)
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("UIDocument"),Vector3.zero,Quaternion.identity);
        os = server.serverStatus.os;
        _task = (TaskImplement) task;
        _server = server;
        LoadTeachPhrases();
    }

    private static void UnloadPhrases()
    {
        //if (scene.name != "WordRush") return;
        CommandGameUI commandGameUI = GameObject.FindObjectOfType<CommandGameUI>();
        if (commandGameUI.commandWord.CompleteLevel())
        {
            _task.getMiniGame().setCompleted();
            _task.getMiniGame().setScore(commandGameUI.commandWord.getScore());
            if (_server != null) _server.addTask(_task);
        }
        MonoBehaviour.Destroy(commandGameUI.gameObject);
        //SceneManager.sceneLoaded -= LoadTeachPhrases;
    }

    private void LoadTeachPhrases()
    {
        //if (scene.name != "WordRush") return;
        CommandGameUI commandGameUI = GameObject.FindObjectOfType<CommandGameUI>();
        if (commandGameUI == null)
            return;
        commandGameUI.setOperatingSystem(os.Name);
        commandGameUI.setPhrases(phrases.SelectByType(type).ToList());
    }
    
    public static void Close()
    {
        UnloadPhrases();
    }
}