using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AttackServer : MonoBehaviour
{
    private static bool AttackLaunch = false;
    private float min_task_to_attack = 3;
    private EmailBox emailBox = null;
    public static bool Lock = false;

    void Awake()
    {
        AttackLaunch = false;
        Debug.Log("Attack Server UP ⚔️");
        name = "Attack Server";
        Lock = false;
    }

    void Start(){
        StartCoroutine(run());
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
                        TaskServer.Lock = true;
                        Lock = true;
                        AttackLaunch = true;
                    } catch(Exception e){ Debug.LogError("Attack System -> " + e.Message); }
                }
                else Debug.Log("Attack System -> Don't exist email!");
            }
            else
            {
                Debug.Log("Attack System -> The number os task is bellow the minimum or are Lock");
            }
            yield return new WaitForSecondsRealtime(2.5f);
        }
    }

    public static bool AttackIsLaunch() { return AttackLaunch; }
    public static void AttackFinish()
    {
        AttackLaunch = false;
    }
}
