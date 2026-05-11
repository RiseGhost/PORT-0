using System;
using System.Linq;
using TMPro;
using UnityEngine;

public enum EconomyType
{
    None,
    RecentWork,
    Service,
    RecentHardware,
    CryptoCoins,
    Energy,
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class BankLabel : MonoBehaviour
{
    [SerializeField] private PayGroup payGroup;
    [SerializeField] private EconomyType economyType;
    private TextMeshProUGUI text;
    
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var promises = MoneyBank.GetPromises().Where(promise => promise.group == payGroup).ToList();
        if (economyType == EconomyType.RecentWork) Debug.Log("Recent Work Total Promises: " + promises.Count);
        switch (economyType)
        {
            case EconomyType.RecentWork:
                var recentPromises = promises.Where(promises => (DateTime.Now - DateTime.Parse(promises.dataString)).TotalMinutes <= 230).ToList();
                text.text = recentPromises.Select(x => x.StartValue).Sum().ToString();
                break;
            case EconomyType.RecentHardware:
                var recentWork = promises.Where(promises => (DateTime.Now - DateTime.Parse(promises.dataString)).TotalMinutes <= 230 && promises.type == PayType.Hardware).ToList();
                text.text = recentWork.Select(x => x.StartValue).Sum().ToString();
                break;
            case EconomyType.Service:
                text.text = promises.Select(x => x.PeriodicValue).Sum().ToString();
                break;
            case EconomyType.CryptoCoins:
                var cryptos = promises.Where(promises => promises.type == PayType.CryptoCoins).ToList();
                text.text = cryptos.Select(x => x.PeriodicValue).Sum().ToString();
                break;
            case EconomyType.Energy:
                var energy = promises.Where(promises => promises.type == PayType.Energy).ToList();
                if (energy.Count == 0 || energy.Count > 1)
                {
                    text.text = "____ ";
                    break;
                }
                text.text = energy.First().PeriodicValue.ToString();
                break;
            default:
                float StartValue = promises.Select(x => x.StartValue).Sum();
                text.text = StartValue.ToString();
                break;
        }
        text.text += " $";
    }
}