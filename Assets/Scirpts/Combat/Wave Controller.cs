using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController
{
    private List<Drone> spawns = new List<Drone>();
    private int birthsNumber = 0;

    public WaveController(WaveDrones waveDrones)
    {
        foreach(Drone drone in waveDrones.GetDrones())
            spawns.Add(MonoBehaviour.Instantiate(drone,Vector3.zero,Quaternion.identity));
        birthsNumber = waveDrones.GetDrones().Length;
    }

    public int GetAliveDrones(){ return spawns.Where(x => x != null).Count(); }
    public float AlivePercentage(){ return birthsNumber / GetAliveDrones(); }
}

