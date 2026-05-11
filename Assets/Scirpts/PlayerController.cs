using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    private struct MoveStatus
    {
        public float MoveForce;
        public float JumpVelocity;
        public float FinalVelocity;
    }

    [SerializeField] MoveStatus moveStatus;
    [SerializeField] ServerConfigBook CreateServerBook;
    public static bool Lock = false;
    private ServerConfigBook currentServerBook = null;
    public InputAction action;
    private bool canJump = false;
    private Rigidbody rigidbody;

    void OnEnable()
    {
        action.Enable();
    }

    void OnDisable()
    {
        action.Disable();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Application.targetFrameRate = 0;
        QualitySettings.vSyncCount = 0;
        StartCoroutine(teste());
    }

    private IEnumerator teste()
    {
        yield return new WaitForSeconds(20);
        new TaskServer(TaskDifficulty.Very_Easy,this);
        while (true)
        {
            yield return new WaitForSeconds(15);
            new TaskServer(TaskDifficulty.Very_Easy,this);
        }
    }

    void Update()
    {
        if (Lock) return;
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            return;
        if (Keyboard.current[Key.H].wasPressedThisFrame)
        {
            StorageManager.DeletedStorage();
            MoneyBank.Reset();
            BankAccount.Reset(Application.persistentDataPath);
            new NotificationDefault("Game Save System","All storage entities are deleted.").Show();
        }
        if (CreateServerBook != null && Keyboard.current[Key.F].wasPressedThisFrame && currentServerBook == null)
        {
            currentServerBook = Instantiate(CreateServerBook.getGameObject(),Vector3.zero,Quaternion.identity).GetComponent<ServerConfigBook>();
        }
    }

    void FixedUpdate()
    {
        if (Lock) return;
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            return;
        if (Camera.main == null) return;
        Vector3 move = Camera.main.transform.rotation * new Vector3(action.ReadValue<Vector2>().x, 0, action.ReadValue<Vector2>().y);
        move.y = 0f;
        if (Keyboard.current[Key.Space].isPressed && canJump)
        {
            Vector3 velocity = rigidbody.linearVelocity;
            velocity.y = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * moveStatus.JumpVelocity);
            rigidbody.linearVelocity = velocity;
        }
        if (Mathf.Abs(rigidbody.linearVelocity.x) < moveStatus.FinalVelocity) rigidbody.AddForce(new Vector3(move.x * moveStatus.MoveForce, 0, 0));
        if (Mathf.Abs(rigidbody.linearVelocity.z) < moveStatus.FinalVelocity) rigidbody.AddForce(new Vector3(0, 0, move.z * moveStatus.MoveForce));

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 7f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor")) canJump = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor")) canJump = false;
    }
}
