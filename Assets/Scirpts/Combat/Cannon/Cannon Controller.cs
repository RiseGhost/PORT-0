using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CannonMechanic))]
public class CannonController : MonoBehaviour
{
    [SerializeField] private Key activeKey = Key.X, shotKey = Key.Space;
    [SerializeField] private InputAction action;
    private PlayerController player;
    private CameraSwitch cameraSwitch;
    private CannonMechanic cannonMechanic;
    private bool inCombat = false;
    private float horizontal = 0, vertical = 0;

    void OnEnable()
    {
        action.Enable();
    }

    void Awake()
    {
        cannonMechanic = GetComponent<CannonMechanic>();
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
        if (Keyboard.current[activeKey].wasPressedThisFrame)
        {
            if (!inCombat) ActiveCombatMode();
            else DeactiveCombatMode();
        }
        if (!inCombat) return;
        horizontal += action.ReadValue<Vector2>().x / 2;
        vertical += action.ReadValue<Vector2>().y / 2;
        vertical = Mathf.Clamp(vertical, -45f, 90f);
        cannonMechanic.setMiddlePartAngle(horizontal);
        cannonMechanic.setBumBumAngle(vertical);
        if (Keyboard.current[shotKey].isPressed)
            cannonMechanic.Shot();
    }

    public void ActiveCombatMode()
    {
        cameraSwitch.Switch_Combat_Camera();
        player.gameObject.SetActive(false);
        inCombat = true;
    }

    public void DeactiveCombatMode()
    {
        cameraSwitch.Switch_main_camera();
        player.gameObject.SetActive(true);
        inCombat = false;
    }
}
