using System;
using UnityEngine;

[Serializable]
public struct EmailDTO
{
    public string Subject,Body;
    public EmailType type;
}

[CreateAssetMenu(fileName = "Email Template" ,menuName = "ScriptTableObjects/Email Template")]
public class EmailTemplate : ScriptableObject
{
    [SerializeField] private EmailDTO[] emailDTOs;

    public EmailDTO[] GetEmails(){ return emailDTOs; }
}
