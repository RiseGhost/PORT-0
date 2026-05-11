using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WordRushImplement: MiniGame
{
    [SerializeField] private TeachPhrasesTableObject phrases;
    [SerializeField] private TaskType type;
    [SerializeField] private MiniGameTechnologyAreaGroup technologyGroup;
    private OperatingSystem os;
    private bool completed = false;
    private TaskImplement _task;
    private Server _server;
    
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
        os = server.serverStatus.os;
        _task = (TaskImplement) task;
        _server = server;
        SceneManager.sceneLoaded += LoadTeachPhrases;
        SceneManager.sceneUnloaded += UnloadPhrases;
        SceneManager.LoadSceneAsync("WordRush", LoadSceneMode.Additive);
    }

    private void UnloadPhrases(Scene scene)
    {
        if (scene.name != "WordRush") return;
        CommandGameUI commandGameUI = GameObject.FindObjectOfType<CommandGameUI>();
        Debug.Log("CommandGameUI is null -> " + (commandGameUI == null));
        Debug.Log("CommandGameUI & commandWord is null -> " + (commandGameUI.commandWord == null));
        if (commandGameUI.commandWord.CompleteLevel())
        {
            _task.getMiniGame().setCompleted();
            _task.getMiniGame().setScore(commandGameUI.commandWord.getScore());
            if (_server != null) _server.addTask(_task);
        }
        MonoBehaviour.Destroy(commandGameUI.gameObject);
        SceneManager.sceneLoaded -= LoadTeachPhrases;
    }

    private void LoadTeachPhrases(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "WordRush") return;
        CommandGameUI commandGameUI = GameObject.FindObjectOfType<CommandGameUI>();
        if (commandGameUI == null)
            return;
        commandGameUI.setOperatingSystem(os.Name);
        commandGameUI.setPhrases(phrases.SelectByType(type).ToList());
        SceneManager.sceneLoaded -= LoadTeachPhrases;
    }
    
    public static void Close()
    {
        SceneManager.UnloadSceneAsync("WordRush");
    }
}