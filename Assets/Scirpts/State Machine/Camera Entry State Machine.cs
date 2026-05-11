using System;
using UnityEngine;

public class CameraEntryStateMachine : StateMachineBehaviour
{
    [SerializeField] private Canvas ShopTutorialCanvas;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Camera Entry"))
        {
            PlayerController.Lock = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Camera Entry"))
        {
            PlayerController.Lock = false;
            GameObject.FindGameObjectWithTag("CameraEntry").SetActive(false);
            ServerGameObject[] servers = GameObject.FindObjectsByType<ServerGameObject>(FindObjectsSortMode.None);
            if ((servers == null || servers.Length == 0) && ShopTutorialCanvas != null) Instantiate(ShopTutorialCanvas,Vector3.zero,Quaternion.identity);
        }
    }
}
