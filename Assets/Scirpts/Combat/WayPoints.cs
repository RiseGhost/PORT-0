using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    private List<Transform> points = new List<Transform>();
    void Start()
    {
        foreach(Transform child in transform)
        {
            points.Add(child);
        }
    }

    public List<Transform> GetPoints(int size)
    {
        List<Transform> result = new List<Transform>();
        List<Transform> shuffle = points.OrderBy(_ => Guid.NewGuid()).ToList();
        result.Add(shuffle[0]);
        for (int i = 1; i < shuffle.Count; i++)
        {
            if (result.Count >= size) break;
            if (Physics.Raycast(result.Last().position, shuffle[i].position - result.Last().position, out RaycastHit hit, 500f))
                result.Add(shuffle[i]);
        }
        return result;
    }
}
