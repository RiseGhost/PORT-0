using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class CannonMechanic : MonoBehaviour
{
    [System.Serializable]
    private struct Part
    {
        public Transform gameObject;
        [Range(-180f, 180f)] public float angle;
    }

    private Animator _animation;
    private Vector3 defaultMiddlePartRotation = Vector3.zero, defaultBumBumRotation = Vector3.zero;
    [SerializeField] private float ShotInterval = 0.25f;
    [SerializeField] private Part middlePart, BumBum;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private CannonBullet bullet;
    private float lastShotTime = -Mathf.Infinity;

#if UNITY_EDITOR
    void OnValidate()
    {
        EditorApplication.delayCall -= RotateParts;
        EditorApplication.delayCall += RotateParts;
        GetDefaultRotations();
    }
    void OnDisable(){ EditorApplication.delayCall -= RotateParts; }
#endif

    void Awake()
    {
        GetDefaultRotations();
        _animation = GetComponent<Animator>();
    }

    void Update()
    {
        RotateParts();
    }

    public void Shot()
    {
        if (Time.time - lastShotTime < ShotInterval) return;
        if (_animation != null)
        {
            _animation.SetTrigger("Fire");
            Debug.Log("Shot");
            lastShotTime = Time.time;
        }
        if (shotPoint != null && bullet != null)
        {
            CannonBullet cannonBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation).GetComponent<CannonBullet>();
            cannonBullet.setCannonMouth(shotPoint);
        }
    }

    [ContextMenu("Shot")]
    private void ShotEditor()
    {
        if (_animation == null) _animation = GetComponent<Animator>();
        Shot();
    }

    [ContextMenu("Reset Rotations")]
    private void ResetRotations()
    {
        middlePart.angle = 0f;
        middlePart.gameObject.localEulerAngles = Vector3.zero;
        defaultMiddlePartRotation = Vector3.zero;
        BumBum.angle = 0f;
        BumBum.gameObject.localEulerAngles = Vector3.zero;
        defaultBumBumRotation = Vector3.zero;
    }

    public void setMiddlePartAngle(float angle)
    {
        middlePart.angle = angle;
    }

    public void setBumBumAngle(float angle)
    {
        BumBum.angle = angle;
    }

    private void GetDefaultRotations()
    {
        if (middlePart.gameObject != null && defaultMiddlePartRotation == Vector3.zero)
            defaultMiddlePartRotation = middlePart.gameObject.localEulerAngles;
        if (BumBum.gameObject != null && defaultBumBumRotation == Vector3.zero)
            defaultBumBumRotation = BumBum.gameObject.localEulerAngles;
    }

    private void RotateParts()
    {
        if (middlePart.gameObject != null)
            middlePart.gameObject.localEulerAngles = defaultMiddlePartRotation + new Vector3(0f, middlePart.angle, 0f);
        if (BumBum.gameObject != null)
            BumBum.gameObject.localEulerAngles = defaultBumBumRotation + new Vector3(Mathf.Clamp(BumBum.angle, -45f, 90f), 0f, 0f);
        BumBum.angle = Mathf.Clamp(BumBum.angle, -45f, 90f);
    }
}
