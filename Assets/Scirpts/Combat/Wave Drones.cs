using UnityEngine;

[CreateAssetMenu(fileName = "Drone Wave", menuName = "ScriptTableObjects/Combat/Drones Wave")]
public class WaveDrones : ScriptableObject
{
    [SerializeField] private Drone[] drones;

    public Drone[] GetDrones(){ return drones; }
}
