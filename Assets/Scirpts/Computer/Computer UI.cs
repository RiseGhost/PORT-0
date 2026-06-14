using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] bx1Spawns;
    private bool LaunchAttack = false;

    void OnEnable()
    {
        PlayerController.Lock = true;
        CameraFollow.LockRotate();
        TaskServer.Lock = true;
    }

    void OnDisable()
    {
        PlayerController.Lock = false;
        CameraFollow.UnlockRotate();
        TaskServer.Lock = false;
    }

    void OnDestroy()
    {
        GameObject.FindAnyObjectByType<PlayerController>().StartCoroutine(LaunchAttacking());
        PlayerController.Lock = false;
        CameraFollow.UnlockRotate();
        TaskServer.Lock = false;
    }

    void Update()
    {
        if (Keyboard.current[Key.Tab].wasPressedThisFrame) Destroy(this.gameObject);
    }

    public void ActivateAttack(){
        LaunchAttack = true;
    }

    private IEnumerator LaunchAttacking()
    {
        yield return new WaitForSeconds(5f);
        EntryCombatCamera combatCamera = GameObject.FindAnyObjectByType<EntryCombatCamera>();
        combatCamera.DDosLaunch();
    }
}
