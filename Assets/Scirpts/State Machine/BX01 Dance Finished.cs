using UnityEngine;

public class BX01DanceFinished : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Dance"))
        {
            GameObject.FindAnyObjectByType<CombatDaceUI>().setText("");
            GameObject.FindAnyObjectByType<DronesEntryAnimator>().OnDanceFinish();
        }
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Dance"))
        {
            Debug.Log("Dance Start");
            GameObject.FindAnyObjectByType<CombatDaceUI>().setText("DDoS Attack");
        }
    }
}
