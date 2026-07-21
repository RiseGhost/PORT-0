using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CannonMechanic))]
public class CannonController : MonoBehaviour
{
    [SerializeField] private Key activeKey = Key.X, shotKey = Key.Space;
    [SerializeField] private InputAction action;
    [SerializeField] private CannonAim aim;
    private PlayerController player;
    private CameraSwitch cameraSwitch;
    private CannonMechanic cannonMechanic;
    private bool inCombat = false;
    private float horizontal = 0, vertical = 0;
    private float StartTimeCombat = 0f;

    void OnEnable()
    {
        action.Enable();
    }

    void Awake()
    {
        cannonMechanic = GetComponent<CannonMechanic>();
        inCombat = false;
    }

    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerController>();
        cameraSwitch = GameObject.FindFirstObjectByType<CameraSwitch>();
        if (player == null || cameraSwitch == null) {
            Destroy(this.gameObject);
            return;
        }
    }

    void Update()
    {
        if (PlayerController.Lock) return;
        EventSystem eventSystem = EventSystem.current;
        /*if (Keyboard.current[activeKey].wasPressedThisFrame && (eventSystem == null || eventSystem.currentSelectedGameObject == null))
        {
            if (!inCombat) ActiveCombatMode();
            else DeactiveCombatMode();
        }*/
        if (!inCombat) return;
        horizontal += action.ReadValue<Vector2>().x / 4;
        vertical += action.ReadValue<Vector2>().y / 4;
        vertical = Mathf.Clamp(vertical, -45f, 90f);
        cannonMechanic.setMiddlePartAngle(horizontal);
        cannonMechanic.setBumBumAngle(vertical);
        if (Keyboard.current[shotKey].isPressed)
            cannonMechanic.Shot();
    }

    public void ActiveCombatMode()
    {
        StartTimeCombat = Time.time;
        NotificationServer.RemoveAll();
        TaskServer.Lock = true;
        cameraSwitch.Switch_Combat_Camera();
        player.gameObject.SetActive(false);
        inCombat = true;
        if (aim != null) aim.gameObject.SetActive(true);
    }

    public async void DeactiveCombatMode()
    {
        AttackServer.AttackFinish();
        FirebaseManager firebase = GameObject.FindFirstObjectByType<FirebaseManager>();
        if (firebase != null)
        {
            string uid = firebase.getUID();
            PlayerDataFirebase data = await firebase.getPlayerData(uid);
            if (data != null && data.TimeToDestroyEnemys == 0)
            {
                data.TimeToDestroyEnemys = Time.time - StartTimeCombat;
                if (data.TotalTime == 0f) data.TotalTime = Time.time;
                await firebase.UpdateData(uid,data);
            }
        }
        TaskServer.Lock = false;
        cameraSwitch.Switch_main_camera();
        player.gameObject.SetActive(true);
        inCombat = false;
        if (aim != null) aim.gameObject.SetActive(false);
    }
}
