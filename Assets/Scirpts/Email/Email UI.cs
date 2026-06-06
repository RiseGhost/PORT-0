using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailUI : MonoBehaviour
{
    [SerializeField] private Transform SideMenu;
    [SerializeField] private EmailMainContent mainContent;
    [SerializeField] private ToggleEmail toggleEmail_Template;
    private EmailBox _emailbox;
    private ToggleGroup toggleGroup;
    private float LastEmailCount = 0f;
    private Email currentEmail = null;

    void Start(){
        if (SideMenu == null || toggleEmail_Template == null)
        {
            Destroy(this.gameObject);
            return;
        }
        _emailbox = GameObject.FindAnyObjectByType<EmailBox>();
        toggleGroup = SideMenu.GetComponent<ToggleGroup>();
    }


    void Update(){
        if (_emailbox == null)
        {
            _emailbox = GameObject.FindAnyObjectByType<EmailBox>();
            return;
        }

        if (toggleGroup == null) return;
        Toggle toggle = toggleGroup.GetFirstActiveToggle();
        if (toggle == null){
            Debug.Log("Não existe nenhum toggle selecionado");
            if (mainContent != null) mainContent.HiddenContent();
        }
        else{
            ToggleEmail toggleSelect = toggleGroup.GetFirstActiveToggle().GetComponent<ToggleEmail>();
            Email email = toggleSelect.getData();
            if (currentEmail == null || !currentEmail.Equals(email)){
                currentEmail = email;
                if (mainContent != null) mainContent.setEmail(email);
                Debug.Log("Novo email selecionado");
            }
            else Debug.Log("O Email selecionado e email ao atual.");
        }

        List<Email> emails = _emailbox.GetEmails();
        if (emails.Count == LastEmailCount) return;
        LastEmailCount = emails.Count;
        foreach(Transform child in SideMenu){
            Destroy(child.gameObject);
        }

        foreach(Email e in emails)
        {
            ToggleEmail email_toggle = Instantiate(toggleEmail_Template,SideMenu);
            email_toggle.data = e;
            email_toggle.setSubject(e.getSubject());
            email_toggle.getWidget().group = SideMenu.GetComponent<ToggleGroup>();
        }
    }
}
