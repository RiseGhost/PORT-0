using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject EmailAlertContainer;
    private EmailBox _emailBox;

    void Start()
    {
        _emailBox = GameObject.FindFirstObjectByType<EmailBox>();
        if (EmailAlertContainer != null) EmailAlertContainer.SetActive(false);
    }

    void Update()
    {
        if (EmailAlertContainer != null && _emailBox != null)
            EmailAlertContainer.SetActive(_emailBox.ExistNotReadEmail());
    }
}
