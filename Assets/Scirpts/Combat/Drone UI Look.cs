using UnityEngine;

public class DroneUILook : MonoBehaviour
{
    public Transform target;
    public float LastShownTime;
    private bool isShown = false;

    void Update()
    {
        if (target != null) transform.LookAt(target);
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
