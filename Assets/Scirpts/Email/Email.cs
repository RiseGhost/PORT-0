using System;
using System.Linq;
using UnityEngine;

public enum EmailType{
    DDosAttack
}

public class Email
{
    private Client client;
    private String Subject, Body;
    public bool read = false;

    public Email(EmailType type)
    {
        TableObjectClient tableObjectClient = Resources.Load<TableObjectClient>(ClientServer.getPath());
        Client[] clients = tableObjectClient.getClients();
        if (clients == null || clients.Length == 0) throw new Exception("Can not read client data, ou data is null");
        client = clients[UnityEngine.Random.Range(0,clients.Length)];
        EmailTemplate emailTemplate = Resources.Load<EmailTemplate>("Email/EmailTemplate");
        if (emailTemplate == null) throw new Exception("Erro to read email template data");
        EmailDTO[] emailDTOs = emailTemplate.GetEmails().Where(x => x.type == type).ToArray();
        if (emailDTOs == null || emailDTOs.Length == 0) throw new Exception("Email templates are empty or invalid");
        EmailDTO emailDTO = emailDTOs[UnityEngine.Random.Range(0,emailDTOs.Length)];
        Subject = emailDTO.Subject;
        Body = emailDTO.Body;
    }

    public Client getClient(){ return client; }
    public string getSubject(){ return Subject; }
    public string getBody(){ return Body; }
}
