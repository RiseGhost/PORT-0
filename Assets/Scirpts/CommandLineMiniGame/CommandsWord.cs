using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public enum CommandState
{
    NoCommands,
    Incorrect,
    Correct,
    Complete
}

[System.Serializable]
public struct TeachPhrases
{
    public string Phrases;
    public string Description;
    public ushort Score;
    public ushort speed;
}

[System.Serializable]
public struct WordsSpawnZone
{
    [Range(0f,100f)]
    public float minX;
    [Range(0f,100f)]
    public float maxX;
}

public struct WordStatus
{
    public string Word;
    public ushort Score;   
}

[System.Serializable]
public class CommandWords
{
    [SerializeField] private string IDElementSpawnWords;
    [SerializeField] private List<TeachPhrases> commands = new List<TeachPhrases>();
    [SerializeField] private VisualTreeAsset UIWordsTemplate;
    [SerializeField] private VisualTreeAsset UIHistoryCardTemplate;
    [SerializeField] private WordsSpawnZone spawnZone;
    private List<WordCard> CardsMove = new List<WordCard>();
    private List<TeachPhrases> completeCommands = new List<TeachPhrases>();
    private VisualElement father_root; // Root of father UI Element
    private List<WordStatus> words = new List<WordStatus>();
    private VisualElement spawnWordsElement;
    private bool PhrasesReady = false;
    private uint score = 0;

    public void init()
    {
        words.Clear();
        var wordsMatrix = commands.Select(x => new { Phrases = x.Phrases.Split(" "), Score = x.Score });
        foreach (var wm in wordsMatrix)
        {
            foreach (string w in wm.Phrases)
            {
                WordStatus wordStatus;
                wordStatus.Word = w;
                wordStatus.Score = wm.Score;
                words.Add(wordStatus);
            }
        }
    }

    public void reset()
    {
        score = 0;
        CardsMove.Clear();
        completeCommands.Clear();
        father_root.Q<VisualElement>("History").Clear();
        init();
        PhrasesReady = true;
    }

    public void setVisualRoot(VisualElement father_root)
    {
        this.father_root = father_root;
        spawnWordsElement = father_root.Q<VisualElement>(IDElementSpawnWords);
    }

    private void completeCommand()
    {
        completeCommands.Add(commands[completeCommands.Count]);
        VisualElement history = UIHistoryCardTemplate.CloneTree();
        history.Q<Label>("Command").text = completeCommands.Last().Phrases;
        history.Q<Label>("Description").text = completeCommands.Last().Description;
        father_root.Q<VisualElement>("History").Add(history);
    }

    public CommandState CheckCurrentWord(string[] word)
    {
        if (CardsMove.Count == 0) return CommandState.NoCommands;
        if (word.Length > commands[completeCommands.Count].Phrases.Split(" ").Length) return CommandState.Incorrect;
        if (!word.Last().Equals(CardsMove.First().word)) return CommandState.Incorrect;
        CardsMove.First().Destroy((spawnWordsElement == null) ? father_root : spawnWordsElement);
        score += CardsMove.First().score;
        CardsMove.RemoveAt(0);
        if (!commands[completeCommands.Count].Phrases.Split(" ").SequenceEqual(word)) return CommandState.Correct;
        completeCommand();
        return CommandState.Complete;
    }

    private WordStatus? NextWord()
    {
        if (words.Count() == 0) return null;
        WordStatus result = words.First();
        words.RemoveAt(0);
        return result;
    }

    public void SpawnWord()
    {
        if (!PhrasesReady) return;
        if (CardsMove.Count != 0 && !CardsMove.First().isCompesed()) return;
        var next = NextWord();
        if (next == null) return;
        WordStatus word = (WordStatus) next;
        VisualElement wordUI = UIWordsTemplate.CloneTree();

        wordUI.style.position = Position.Absolute;
        wordUI.style.left = UnityEngine.Random.Range(Screen.width * (spawnZone.minX / 100f), Screen.width * (spawnZone.maxX / 100f));
        wordUI.style.top = -150f;

        if (spawnWordsElement == null) father_root.Add(wordUI);
        else spawnWordsElement.Add(wordUI);
        CardsMove.Add(new WordCard(wordUI, word.Word, word.Score, commands[completeCommands.Count].speed));
    }

    public void MoveCards(float deltaTime, string lastword)
    {
        if (CardsMove.Count() == 0) return;
        CardsMove.First().UpdateCompleteBar(lastword, deltaTime);
        if (CardsMove.First().isOver())
        {
            CardsMove.ForEach(i => i.changeSense());
        }
        CardsMove.ForEach(i => i.UpdatePosition(deltaTime));
    }

    public bool CompleteLevel() { return commands.Count == completeCommands.Count; }

    public uint getScore(){ return score; }

    public void setPhrases(List<TeachPhrases> phrases)
    {
        if (phrases == null || phrases.Count == 0) return;
        commands = phrases;
        init();
        PhrasesReady = true;
    }
}