using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
    Description:
        This class is responsible for controlling the data relating to a task that will appear in the UI.
        This UI Will used in the content od a PopUpTask
*/

public class PopContentTask : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LabelSubtitle;
    [SerializeField] private TextMeshProUGUI LabelDescription, LabelDifficulty, LabelServiceType;
    [SerializeField] private TechnologyCardGroup technologyCardGroup;
    [SerializeField] private TextMeshProUGUI LabelClientName, LabelClientDescription;
    [SerializeField] private GameObject ClientContent;
    [SerializeField] private GameObject OrganizationContent, PersonalContent;
    [SerializeField] private TextMeshProUGUI LabelOrganizationName, LabelOrganizationCountry, LabelOrganizationArea;
    [SerializeField] private RawImage ClientPersonalIcon, ClientOrganizationIcon;

    private Task task;

    public void setTask(Task task)
    {
        this.task = task;
        if (LabelSubtitle != null)          LabelSubtitle.text = task.getName();
        if (LabelDescription != null)       LabelDescription.text = task.getTaskDescription().description;
        if (LabelDifficulty != null)        LabelDifficulty.text = task.getDifficulty().ToString();
        if (LabelServiceType != null)       LabelServiceType.text = task.getTaskDescription().type.ToString();
        
        if (technologyCardGroup != null)    technologyCardGroup.setTechnologyAreaGroup(task.getTechnologyAreaGroup());
        
        if (task.GetClient() == null)
        {
            Destroy(ClientContent);
            return;
        }
        Client client = task.GetClient();

        if (LabelClientName != null)        LabelClientName.text = client.getName();
        if (LabelClientDescription != null) LabelClientDescription.text = client.getDescription();
        if (task.GetClient().IsOrganization())
        {
            PersonalContent.SetActive(false);
            OrganizationContent.SetActive(true);

            Organization organization = client.getOrganization();
            if (LabelOrganizationName != null)      LabelOrganizationName.text =    organization.name;
            if (LabelOrganizationCountry != null)   LabelOrganizationCountry.text = organization.country;
            if (LabelOrganizationArea != null)      LabelOrganizationArea.text =    organization.area;
            if (ClientOrganizationIcon != null)     ClientOrganizationIcon.texture = organization.icon;
        }
        else
        {
            PersonalContent.SetActive(true);
            OrganizationContent.SetActive(false);

            if (ClientPersonalIcon != null)         ClientPersonalIcon.texture = client.getIcon();
        }
    }
}