using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmailBox : MonoBehaviour
{
    private List<Email> emails = new List<Email>();

    public List<Email> GetEmails(){ return emails; }
    public void AddEmail(Email email) { 
        if (email != null)
        {
            emails.Add(email); 
            NotificationServer.AddNotification(new NotificationDefault("New email", "Go to your PC to read"));
        }
    }
    public bool ExistNotReadEmail() { return emails.Select((x) => x.read).Contains(false); }
}
