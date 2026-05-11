using UnityEngine;

/*
    Description:
        This class represent a Technology Group that belongs to a PopUpCard class.
        The Technology Group is a component of the PopUpCard sidebar that contains cards with task technologies
*/

public class TechnologyCardGroup: MonoBehaviour
{
    [SerializeField] private TechnologyCard cardTemplate;

    public void setTechnologyAreaGroup(MiniGameTechnologyAreaGroup group)
    {
        foreach (MiniGameTechnologyArea area in group.getGroup())
        {
            TechnologyCard card = Instantiate(cardTemplate,transform).GetComponent<TechnologyCard>();
            card.setTechnologyArea(area); 
        }
    }
}