using System;
using System.Linq;
using UnityEngine;

public enum EmailType{
    DDosAttack,
    PowerSupply
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
            if (type == EmailType.DDosAttack){
                Subject = "Slow Connecting";
                Body = "Dear Technical Support Team," +
                "\nI am experiencing significant performance issues with my website hosted on your infrastructure. The website has become noticeably slow, and this is negatively affecting my customers' experience." +
                "\nCould you please investigate the server performance and check if there are any issues that might be causing these delays? Any assistance in identifying and resolving the problem would be greatly appreciated." +
                "\nThank you for your support." +
                "\nKind regards,\n" + client.getName();
            }
            else if (type == EmailType.PowerSupply){
                TableObjectClient citys = Resources.Load<TableObjectClient>("Task/Clients City");
                client = citys.getClients()[0];
                Subject = "Power Supply down";
                Body = "Dear Sir/Madam," +
                "\nWe have recently detected an unusually high level of energy consumption in the local grid, which appears to be significantly above historical averages." +
                "\nAt this stage, we kindly ask that energy usage be moderated where possible in order to help maintain grid stability and avoid potential future penalties or restrictions. Please note that no penalties are being applied at this time, as we are actively working with the relevant stakeholders to assess and resolve the situation." +
                "\nWe appreciate your cooperation and understanding while we address this matter. Should any further action be required, we will provide additional guidance in due course." +
                "\nKind regards,\n Long view City";
            }
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
        if (obj == null) return false;
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
        if (read) return;
        if (type == EmailType.DDosAttack){
            ComputerUI computerUI = GameObject.FindAnyObjectByType<ComputerUI>();
            if (computerUI != null) computerUI.ActivateAttack();
            read = true;
        }
        else if (type == EmailType.PowerSupply)
        {
            ComputerUI computerUI = GameObject.FindAnyObjectByType<ComputerUI>();
            if (computerUI != null) computerUI.ReturnEnergy();
            read = true;
        }
    }
}
