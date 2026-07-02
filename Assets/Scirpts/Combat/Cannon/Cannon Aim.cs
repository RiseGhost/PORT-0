using System.Collections;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class CannonAim : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private float maxDistance = 10f;
    private float currentDistance = 0f;
    private Coroutine OpenAim;
#if UNITY_EDITOR
    void OnValidate()
    {
        EditorApplication.delayCall -= Aim;
        EditorApplication.delayCall += Aim;
    }

    void OnDisable()
    {
        currentDistance = 0f;
        EditorApplication.delayCall -= Aim;
    }

#endif
    void OnEnable()
    {
        if (OpenAim != null) StopCoroutine(OpenAim);
        OpenAim = StartCoroutine(OpeningAim(true));
    }

    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        if (this == null)           return;
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        float distance = currentDistance;
        RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.forward, distance);
        for(int i = 0; i < hit.Length; i++)
        {
            RaycastHit h = hit[i];
            Drone drone = h.collider.gameObject.GetComponent<Drone>();
            if (drone != null){
                drone.GetUILook().Show();
                distance = h.distance;
                break;
            }
        }
        Vector3 forward = transform.forward * distance;
        lineRenderer.SetPosition(1, transform.position + forward);
    }

    private IEnumerator OpeningAim(bool open)
    {
        yield return new WaitForSeconds(1.4f);
        while (currentDistance != maxDistance)
        {
            currentDistance = Mathf.Lerp(currentDistance, open ? maxDistance : 0f, 0.60f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        OpenAim = null;
    }
}
