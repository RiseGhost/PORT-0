using UnityEngine;

[CreateAssetMenu(fileName = "Clients" ,menuName = "ScriptTableObjects/Clients")]
public class TableObjectClient : ScriptableObject
{
    [SerializeField] private Client[] clients;

    public Client[] getClients(){ return clients; }
}
