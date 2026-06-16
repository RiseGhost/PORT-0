using System.Linq;
using UnityEngine;

/*
    Class MiniGameTechnologyAreaGroup

    Description:
        Represent a array of MiniGameTechnologyArea.
        Used to compare whether a given MiniGameTechnologyArea[] contains all technologies
        contained in the MiniGameTechnologyArea[] represent by MiniGameTechnologyAreaGroup

    Attributes:
        MiniGameTechnologyArea[] group -> Array of all Technology of this group
*/

[System.Serializable]
public class MiniGameTechnologyAreaGroup
{
    [SerializeField] public MiniGameTechnologyArea[] group;

    public MiniGameTechnologyAreaGroup(MiniGameTechnologyArea[] group)
    {
        this.group = group;
    }

    public MiniGameTechnologyAreaGroup(){}

    public MiniGameTechnologyArea[] getGroup(){ return group; }

    public bool ContainsAllArea(MiniGameTechnologyAreaGroup other)
    {
        if (other == null || other.group == null)
            return false;

        return group.All(x => other.group.Contains(x));
    }
}