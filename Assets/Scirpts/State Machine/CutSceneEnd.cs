using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneEnd : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("HomeMenu");
    }
}
