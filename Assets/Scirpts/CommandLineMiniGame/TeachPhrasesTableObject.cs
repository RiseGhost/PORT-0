using UnityEngine;

[System.Serializable]
public struct TeachPhrasesGroup
{
    public TaskType type;
    public TeachPhrases[] phrases;
}

[CreateAssetMenu(fileName = "Teach Phrases" ,menuName = "ScriptTableObjects/TeachPhrasesTableObject")]
public class TeachPhrasesTableObject : ScriptableObject
{
    [SerializeField] private TeachPhrasesGroup[] teachPhrases;

    public TeachPhrases[] SelectByType(TaskType type)
    {
        foreach (var tp in teachPhrases)
        {
            if (tp.type == type) return tp.phrases;
        }
        return null;
    }

    public TeachPhrasesGroup[] getTeachPhrasesGroups()
    {
        return teachPhrases;
    }
}