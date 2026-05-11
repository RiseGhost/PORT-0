using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class WordCard
{
    public string word { get; }
    public uint score { get; }
    public float offsetDis { get; private set; } // Represent the compensation Distance
    private ushort speed = 0;
    private float top, left;
    private VisualElement root;
    private bool isDestroy = false;
    private VisualElement content;
    public WordCard(VisualElement root, string word, ushort score, ushort speed)
    {
        this.root = root;
        this.word = word.ToLower();
        this.speed = speed;
        this.root.Q<Label>("word").text = this.word;
        this.score = score;
        content = root.Q<VisualElement>("Content");
        offsetDis = 0;
    }

    public void UpdatePosition(float DeltaTime)
    {
        float y = root.style.top.value.value;
        y += DeltaTime * speed * ((offsetDis > 0) ? 1 : -1);
        root.style.top = y;
        offsetDis += DeltaTime * speed;
        top = y;
        left = root.style.left.value.value;
    }

    public void UpdateCompleteBar(string word, float deltatime)
    {
        float total = this.word.Length;
        byte points = 0;
        for (byte i = 0; i < Mathf.Min(total, word.Length); i++)
        {
            if (this.word[i] == word[i]) points++;
            else break;
        }
        ProgressBar bar = content.Q<ProgressBar>("Progress");
        bar.value = Mathf.Lerp(bar.value, points / total, 3f * deltatime);
    }

    public void Destroy(VisualElement root)
    {
        VisualElement visualElement = new VisualElement();
        visualElement.style.position = Position.Absolute;
        visualElement.style.height = 650;
        visualElement.style.width = 1250;
        visualElement.style.top = top - (650 / 2);
        visualElement.style.left = left - (1250 / 2);
        visualElement.AddToClassList("DestroyWordAnimation");

        if (!isDestroy) root.Remove(this.root);
        isDestroy = true;

        root.Add(visualElement);
    }

    public bool isOver() { return root.style.top.value.value > Screen.height - 75 - root.style.height.value.value; }

    public bool isCompesed() { return offsetDis > 0; }

    public void changeSense()
    {
        offsetDis = -200;
    }
}
