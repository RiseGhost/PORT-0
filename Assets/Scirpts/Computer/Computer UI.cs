using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] bx1Spawns;
    private bool LaunchAttack = false, ActivePowerSupply;

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
        if (LaunchAttack) GameObject.FindAnyObjectByType<PlayerController>().StartCoroutine(LaunchAttacking());
        if (ActivePowerSupply) GameObject.FindAnyObjectByType<PlayerController>().StartCoroutine(ReturnPowerSupply());
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

    public void ReturnEnergy(){ ActivePowerSupply = true; }

    private IEnumerator LaunchAttacking()
    {
        yield return new WaitForSeconds(5f);
        while (!PowerSupply.Exist_Energy() && !ActivePowerSupply)
        {
            yield return new WaitForSeconds(0.5f);
        }
        EntryCombatCamera combatCamera = GameObject.FindAnyObjectByType<EntryCombatCamera>();
        combatCamera.DDosLaunch();
        LaunchAttack = false;
    }

    private IEnumerator ReturnPowerSupply()
    {
        yield return new WaitForSeconds(8f);
        PowerSupply.Return();
        ActivePowerSupply = false;
    }
}
