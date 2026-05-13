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
        float totalWatts = servers.ToList().Select((watts) => watts.server.serverStatus.getWatts()).Sum();
        if (LabelWatts != null) LabelWatts.text = totalWatts.ToString();
    }
}
