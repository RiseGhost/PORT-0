using UnityEngine;

[System.Serializable]
public class Client
{
    [SerializeField] private Texture2D icon;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Satisfaction satisfaction = Satisfaction.Neutral;
    [SerializeField] private bool is_organization = false;
    [SerializeField] private Organization organization;
    [SerializeField] private ushort min_level_require = 0;
    [SerializeField] private ushort max_level_require = 40_000;

    public Client(string name)
    {
        this.name = name;
    }

    public string getName(){ return name; }
    public string getDescription(){ return description; }
    public bool IsOrganization(){ return is_organization; }
    public Organization getOrganization(){ return organization; }
    public Texture2D getIcon(){ return icon; }
    public ushort get_min_level_require(){ return min_level_require; }
    public ushort get_max_level_require(){ return max_level_require; }
}
