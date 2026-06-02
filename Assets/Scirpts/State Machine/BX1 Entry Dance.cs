using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BX1EntryDance : StateMachineBehaviour
{
    [SerializeField] private GameObject[] bx1Spawns;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (stateInfo.IsName("Entry Drones BX1 DDos"))
        {
            DronesEntryAnimator[] dronesEntryAnimator = GameObject.FindObjectsByType<DronesEntryAnimator>(FindObjectsSortMode.None);
            if (dronesEntryAnimator == null || dronesEntryAnimator.Length == 0) return;
            DronesEntryAnimator bx1 = dronesEntryAnimator.Where((x) => x.gameObject.name.Contains("BX1")).ToArray().First();
            if (bx1 == null) return;
            bx1.Dance();
            if (bx1Spawns == null || bx1Spawns.Length == 0) return;
            GameObject spawn = bx1Spawns[Random.Range(0, bx1Spawns.Length)];
            spawn = MonoBehaviour.Instantiate(spawn, Vector3.zero, Quaternion.identity);
            List<GameObject> childrens = new List<GameObject>();
            foreach (Transform child in spawn.transform)
            {
                childrens.Add(child.gameObject);
            }
            Drone[] drones = childrens.Select(x => x.GetComponent<Drone>()).ToList().Where(x => x != null).ToArray();
            new WaveLaunch(drones);
        }
    }

}
