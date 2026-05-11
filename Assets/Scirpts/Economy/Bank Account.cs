using System;
using System.IO;
using UnityEngine;


[System.Serializable]
public class BankAccount
{
    public float amount = 0;
    
    public BankAccount()
    {
        amount = 0;
    }

    public static void Reset(string persistentDataPath)
    {
        string pathAccount = persistentDataPath + "/accounts.json";
        File.WriteAllText(pathAccount, JsonUtility.ToJson(new BankAccount()));
    }

    public void AddAmount(float amount, string persistentDataPath)
    {
        Debug.Log("Pagamento efetuado com sucesso! Valor -> " + amount);
        string pathAccount = persistentDataPath + "/accounts.json";
        this.amount += amount;
        if (amount < 0) amount = 0f;
        File.WriteAllText(pathAccount, JsonUtility.ToJson(this));
    }

    public static BankAccount getCurrent(string persistentDataPath)
    {
        try
        {
            string pathAccount = persistentDataPath + "/accounts.json";
            BankAccount save = JsonUtility.FromJson<BankAccount>(File.ReadAllText(pathAccount));
            if (save != null) return save;
            File.WriteAllText(pathAccount, JsonUtility.ToJson(new BankAccount()));
            return new BankAccount();
        }
        catch (Exception e)
        {
            return new BankAccount();
        }
    }
}
