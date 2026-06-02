using UnityEngine;

public class BX01DanceFinished : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Dance"))
        {
            GameObject.FindAnyObjectByType<DronesEntryAnimator>().OnDanceFinish();
        }
    }
}
