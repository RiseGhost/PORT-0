using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MonitorSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LabelWatts;

    void Update()
    {
        ServerGameObject[] servers = GameObject.FindObjectsByType<ServerGameObject>(FindObjectsSortMode.None);
        if (servers.Length == 0) return;
        List<ServerStatusStruct> serverStatuses = servers.ToList().Select((server) => server.server.serverStatus).ToList();
        try
        {
            float totalWatts = serverStatuses.Select((watts) => watts.getWatts()).Sum();
            if (LabelWatts != null) LabelWatts.text = totalWatts.ToString();
        } catch (System.NullReferenceException){}
    }
}
