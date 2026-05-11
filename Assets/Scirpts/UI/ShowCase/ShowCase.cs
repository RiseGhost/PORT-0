using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowCase : MonoBehaviour
{
    private List<Exhibitor> exhibitors = new List<Exhibitor>();

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(transform.up);
        UpdateAllPosition();
    }

    public void RemoveType(ExhibitorType type)
    {
        for(int i = 0; i < exhibitors.Count; i++)
        {
            if (exhibitors[i].type == type)
            {
                Destroy(exhibitors[i].gameObject);
                exhibitors.RemoveAt(i);
                break;
            }
        }
    }

    public void AddExhibitorStart(Exhibitor exhibitor)
    {
        if (exhibitor == null) return;
        if (!exhibitors.Contains(exhibitor))
        {
            RemoveType(exhibitor.type);
            exhibitors.Add(Instantiate(exhibitor, transform).GetComponent<Exhibitor>());
            exhibitors.Last().transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
        }
    }

    public void AddExhibitorBottom(Exhibitor exhibitor)
    {
        if (exhibitor == null) return;
        if (!exhibitors.Contains(exhibitor))
        {
            RemoveType(exhibitor.type);
            exhibitors.Insert(0, Instantiate(exhibitor,transform).GetComponent<Exhibitor>());
            exhibitors.First().transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
        }
    }
    
    public void UpdateAllPosition()
    {
        for (int i = 0; i < exhibitors.Count; i++)
        {
            exhibitors[i].setTargetPos(new Vector3(0, (((i+1f)/(exhibitors.Count + 1f)) * transform.localScale.y) - (0.5f * transform.localScale.y), 0));
        }
    }
}