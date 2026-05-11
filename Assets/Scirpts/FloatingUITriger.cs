using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloatingUITriger : MonoBehaviour
{
    private SphereCollider collider;
    private List<GameObject> floatings = new List<GameObject>();

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        if (collider == null) Destroy(this);
    }

    void FixedUpdate()
    {
        if (floatings.Count == 0) return;
        try
        {
            float min = Vector3.Distance(transform.position, floatings.First().transform.position);
            FloatingUIInterface floatingUI = floatings.First().GetComponent<FloatingUIInterface>();
            foreach (var f in floatings)
            {
                f.GetComponent<FloatingUIInterface>().hide();
                float distance = Vector3.Distance(transform.position, f.transform.position);
                if (distance < min)
                {
                    min = distance;
                    floatingUI = f.GetComponent<FloatingUIInterface>();
                }
            }
            floatingUI.show();
        } catch(Exception e){}
    }

    void OnTriggerEnter(Collider other)
    {
        FloatingUIInterface floating = other.gameObject.GetComponent<FloatingUIInterface>();
        if (floating == null) return;
        floatings.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        FloatingUIInterface floating = other.gameObject.GetComponent<FloatingUIInterface>();
        if (floating == null) return;
        floating.destroy();
        floatings.Remove(other.gameObject);
    }
}