using System;

public enum PayGroup
{
    Expense,
    Profit
}

public enum PayType
{
    Services,
    Hardware,
    CryptoCoins,
    Energy,
    Water
}

[System.Serializable]
public struct PromisePay
{
    public float currentrate;
    public float payrate;
    public float StartValue;
    public float PeriodicValue;
    public PayGroup group;
    public PayType type;
    public string dataString;
}
