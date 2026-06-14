using UnityEngine;

public class DroneUILook : MonoBehaviour
{
    private Transform target;
    public float LastShownTime;
    private bool isShown = false;

    void Start()
    {
        if (target == null && GameObject.FindGameObjectWithTag("CombatCamera") != null)
            target = GameObject.FindGameObjectWithTag("CombatCamera").transform;
    }

    void Update()
    {
        if (target != null) transform.LookAt(target);
        else if (GameObject.FindGameObjectWithTag("CombatCamera") != null)
            target = GameObject.FindGameObjectWithTag("CombatCamera").transform;
        if (Time.time - LastShownTime > 0.1f)
        {
            gameObject.SetActive(false);
            isShown = false;
        }
    }

    public void Show()
    {
        if (!isShown) gameObject.SetActive(true);
        isShown = true;
        LastShownTime = Time.time;
    }
}
