using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

[System.Serializable]
public class PromisePayList
{
    public List<PromisePay> promises = new List<PromisePay>();
}

public class MoneyBank
{
    private static PromisePayList promisePayList = new PromisePayList();
    private static string pathPay;
    private static bool running = true;
    private static string path = Application.persistentDataPath;

    public MoneyBank()
    {
        pathPay = Application.persistentDataPath + "/pay.json";
    }

    public void Start()
    {
        try
        {
            PromisePayList save = JsonUtility.FromJson<PromisePayList>(File.ReadAllText(pathPay));
            if (save != null)
                promisePayList = save;
            else promisePayList.promises.Add(getDefaultEnergy());
        }
        catch (Exception e) {}
        
        Thread t = new Thread(Save);
        t.IsBackground = true;
        t.Start();
    }

    private static PromisePay getDefaultEnergy()
    {
        PromisePay energy = new PromisePay();
        energy.payrate = 3600f;
        energy.PeriodicValue = -35f;
        energy.type = PayType.Energy;
        energy.dataString = DateTime.Now.ToString();
        return energy;
    }
    
    public static void Exit()
    {
        running = false;
    }
    
    private void Save()
    {
        while (running)
        {
            lock (promisePayList)
            {
                for (int index = 0; index < promisePayList.promises.Count; index++)
                {
                    PromisePay pay = promisePayList.promises[index];
                    pay.currentrate += 0.2f;
                    if (pay.currentrate > pay.payrate)
                    {
                        BankAccount.getCurrent(path).AddAmount(pay.PeriodicValue,path);
                        pay.currentrate = 0;
                    }
                    promisePayList.promises[index] = pay;
                }
                File.WriteAllText(pathPay, JsonUtility.ToJson(promisePayList));
            }
            Thread.Sleep(200);
        }
    }

    private void PrintPromise(PromisePay pay)
    {
        Debug.Log("CurrentRate -> " + pay.currentrate + " PayRate -> " + pay.payrate + " StartValue -> " + pay.StartValue);
    }

    public static void addPromisePay(PromisePay pay)
    {
        PromisePay newPay = pay;
        newPay.dataString = DateTime.Now.ToString();
        BankAccount.getCurrent(path).AddAmount(pay.StartValue,path);
        promisePayList.promises.Add(newPay);
    }

    public static void removePromisePay(PromisePay pay)
    {
        promisePayList.promises.Remove(pay);
    }

    public void UpdatePromisePay(PayGroup payGroup, PayType type, PromisePay newPay)
    {
        var exist = promisePayList.promises.Select(x => x.group == payGroup && x.type == type);
        if (exist.Count() == 0) return;
        for (int i = 0; i < promisePayList.promises.Count; i++)
        {
            PromisePay pay = promisePayList.promises[i];
            if (pay.group == payGroup && pay.type == type)
                promisePayList.promises[i] = newPay;
        }
    }

    public static void Reset()
    {
        promisePayList.promises.Clear();
        promisePayList.promises.Add(getDefaultEnergy());
    }
    
    public static List<PromisePay> GetPromises()
    {
        return promisePayList.promises;
    }
}