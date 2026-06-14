using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailMainContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ClientName,ClientEmail, Subject, MainContent;
    [SerializeField] private RawImage ClientIcon;
    private bool isHidden = false;

    void OnEnable()
    {
        ShowContent();
    }

    void OnDisable()
    {
        ResetAll();
    }

    void Awake()
    {
        if (ClientEmail == null || Subject == null || MainContent == null || ClientName == null){
            Destroy(this);
        }
    }

    void Start()
    {
        HiddenContent();
    }

    private void ResetAll()
    {
        ClientEmail.text = "";
        ClientName.text = "";
        Subject.text = "";
        MainContent.text = "";
        ClientIcon.texture = null;
    }

    public void HiddenContent()
    {
        ResetAll();
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }
        isHidden = true;
    }

    public void ShowContent()
    {
        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
        }
        isHidden = false;
    }

    public void setEmail(Email email){
        if (isHidden && email != null) ShowContent();
        Debug.Log("Email Main Content are setter");
        if (email == null){
            ResetAll();
            return;
        }
        ClientEmail.text = email.getClient().getEmail();
        Subject.text = email.getSubject();
        MainContent.text = email.getBody();
        ClientName.text = email.getClient().getName();
        if (ClientIcon != null) ClientIcon.texture = email.getClient().getIcon();
    }
}
