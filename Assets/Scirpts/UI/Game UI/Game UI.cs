using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject EmailAlertContainer;
    [SerializeField] private GameObject BuyServerAlertContainer;
    private EmailBox _emailBox;

    void Start()
    {
        _emailBox = GameObject.FindFirstObjectByType<EmailBox>();
        if (EmailAlertContainer != null) EmailAlertContainer.SetActive(false);
        BuyServerAlertContainer.SetActive(false);
    }

    void Update()
    {
        if (EmailAlertContainer != null && _emailBox != null)
            EmailAlertContainer.SetActive(_emailBox.ExistNotReadEmail());
        
        ServerGameObject[] servers = GameObject.FindObjectsByType<ServerGameObject>(FindObjectsSortMode.None);
        if (servers == null || servers.Count() == 0) return;
        float[] spaces = servers.Select((x) => x.server.getAvailableSpace()).ToArray();
        BuyServerAlertContainer.gameObject.SetActive(spaces.Sum() == 0);
    }
}
