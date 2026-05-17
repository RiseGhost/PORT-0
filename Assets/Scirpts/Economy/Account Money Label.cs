using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AccountMoneyLabel : MonoBehaviour
{
    [SerializeField] private string sufix = " $";
    private TextMeshProUGUI text;
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Refesh());
    }

    private IEnumerator Refesh()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            text.text = BankAccount.getCurrent(Application.persistentDataPath).amount.ToString() + sufix;
        }
    }
}
