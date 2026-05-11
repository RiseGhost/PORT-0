using System.Linq;
using UnityEngine;

/*
    Description:
        This class is use to select a random client.
        It may be possible to select completely randomly or radom client given the player's level.
*/

public static class ClientServer
{
    private static string path = "Task/Clients";
    public static Client random()
    {
        Client[] clients = Resources.Load<TableObjectClient>(path).getClients();
        if (clients.Length == 0) return null;
        int randomIndex = Random.Range(0,clients.Length);
        return clients[randomIndex];
    }

    public static Client random(ushort min_level_require, ushort max_level_require)
    {
        Client[] data = Resources.Load<TableObjectClient>(path).getClients();
        Client[] clients = data.Where(x => 
            x.get_min_level_require() >= min_level_require &&
            x.get_max_level_require() <= max_level_require).ToArray<Client>();
        if (clients.Length == 0) return null;
        int randomIndex = Random.Range(0,clients.Length);
        return clients[randomIndex];
    }
}