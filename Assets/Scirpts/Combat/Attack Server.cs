using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AttackServer : MonoBehaviour
{
    private Coroutine coroutine;
    private float min_task_to_attack = 3;
    private EmailBox emailBox = null;
    public static bool Lock = false;

    void Awake()
    {
        Debug.Log("Attack Server UP ⚔️");
        name = "Attack Server";
        Lock = false;
    }

    void Start(){
        coroutine = StartCoroutine(run());
    }

    private IEnumerator run()
    {
        yield return new WaitForSeconds(10f);
        while (true)
        {
            Server[] servers = GameObject.FindObjectsByType<ServerGameObject>(FindObjectsSortMode.None).ToList().Select(x => x.server).ToArray();
            float TotalTask = servers.Select(x => x.tasks.Count).Sum();
            if (TotalTask >= min_task_to_attack && !Lock){
                if (emailBox == null) emailBox = GameObject.FindAnyObjectByType<EmailBox>();
                if (emailBox != null){
                    try{
                        emailBox.AddEmail(new Email(EmailType.DDosAttack));
                        Debug.Log("Attack System -> DDos Email send, with success");
                    } catch(Exception e){ Debug.Log("Attack System -> " + e.Message); }
                    TaskServer.Lock = true;
                    Lock = true;
                }
                else Debug.Log("Attack System -> Don't exist email!");
            }
            else
                Debug.Log("Attack System -> The number os task is bellow the minimum or are Lock");
            yield return new WaitForSecondsRealtime(2.5f);
        }
    }

}
