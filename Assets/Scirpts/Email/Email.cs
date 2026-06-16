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
    private EmailType type;
    public bool read = false;

    public Email(EmailType type)
    {
        this.type = type;
        TableObjectClient tableObjectClient = Resources.Load<TableObjectClient>(ClientServer.getPath());
        Client[] clients = tableObjectClient.getClients();
        if (clients == null || clients.Length == 0) throw new Exception("Can not read client data, ou data is null");
        client = clients[UnityEngine.Random.Range(0,clients.Length)];
        EmailTemplate emailTemplate = Resources.Load<EmailTemplate>("Email/Email Template");
        if (emailTemplate == null){
            Subject = "Slow Connecting";
            Body = "Dear Technical Support Team," +
            "\nI am experiencing significant performance issues with my website hosted on your infrastructure. The website has become noticeably slow, and this is negatively affecting my customers' experience." +
            "\nCould you please investigate the server performance and check if there are any issues that might be causing these delays? Any assistance in identifying and resolving the problem would be greatly appreciated." +
            "\nThank you for your support." +
            "\nKind regards,\n" + client.getName();
            return;
        }
        EmailDTO[] emailDTOs = emailTemplate.GetEmails().Where(x => x.type == type).ToArray();
        if (emailDTOs == null || emailDTOs.Length == 0) throw new Exception("Email templates are empty or invalid");
        EmailDTO emailDTO = emailDTOs[UnityEngine.Random.Range(0,emailDTOs.Length)];
        Subject = emailDTO.Subject;
        Body = emailDTO.Body;
    }

    public override bool Equals(object obj)
    {
        if (obj is Email){
            Email e = (Email) obj;
            return e.getClient().Equals(client) && e.getSubject().Equals(Subject) && e.getBody().Equals(Body);
        }
        else return false;
    }

    public Client getClient(){ return client; }
    public string getSubject(){ return Subject; }
    public string getBody(){ return Body; }
    public void Reading(){
        read = true;
        if (type == EmailType.DDosAttack){
            ComputerUI computerUI = GameObject.FindAnyObjectByType<ComputerUI>();
            if (computerUI != null) computerUI.ActivateAttack();
        }
    }
}
