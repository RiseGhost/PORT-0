using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float fireRate = 2.0f;
    private float lastShoot = 0f;
    
    void Start()
    {
        if (_bulletPrefab == null) Destroy(this);
    }
    
    void FixedUpdate()
    {
        bool click = Mouse.current.leftButton.isPressed || Keyboard.current[Key.PageUp].isPressed || Keyboard.current[Key.PageDown].isPressed;
        if (Time.time < lastShoot + (1f/fireRate)) return;
        if (click) Instantiate(_bulletPrefab, transform.position, transform.rotation);
        if (click) lastShoot = Time.time;
    }
}
