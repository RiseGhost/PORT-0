using System.Collections.Generic;
using System.Linq;

public class WaveLaunch
{
    private List<Drone> spawns = new List<Drone>();
    private int birthsNumber = 0;

    public WaveLaunch(Drone[] drones)
    {
        TaskServer.Lock = true;
        NotificationServer.RemoveAll();
        foreach(Drone drone in drones)
            spawns.Add(drone);
        birthsNumber = drones.Length;
    }

    public int GetAliveDrones(){ return spawns.Where(x => x != null).Count(); }
    public float AlivePercentage(){ return (((float) GetAliveDrones()) / ((float) birthsNumber)); }
}