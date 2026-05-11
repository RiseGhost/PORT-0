using System.Collections;
using UnityEngine;

public class TesteNoti : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Teste());
    }
    
    private IEnumerator Teste()
    {
        yield return null;
        while (true)
        {
            new NotificationDefault("Test Notification","Description").Show();
            yield return new WaitForFixedUpdate();
        }
    }
}