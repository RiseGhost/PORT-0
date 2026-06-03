using System.Collections.Generic;
using UnityEngine;

public class EmailBox : MonoBehaviour
{
    private List<Email> emails = new List<Email>();

    public List<Email> GetEmails(){ return emails; }
    public void AddEmail(Email email) { if (email != null) emails.Add(email); }
}
