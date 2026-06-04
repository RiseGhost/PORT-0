using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailUI : MonoBehaviour
{
    [SerializeField] private Transform SideMenu;
    [SerializeField] private ToggleEmail toggleEmail_Template;
    private EmailBox _emailbox;
    private float LastEmailCount = 0f;

    void Start()
    {
        if (SideMenu == null || toggleEmail_Template == null)
        {
            Destroy(this.gameObject);
            return;
        }
        _emailbox = GameObject.FindAnyObjectByType<EmailBox>();
    }


    void Update()
    {
        if (_emailbox == null)
        {
            _emailbox = GameObject.FindAnyObjectByType<EmailBox>();
            return;
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
