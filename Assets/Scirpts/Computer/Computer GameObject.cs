using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ComputerGameObject : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    void Awake(){
        if (_canvas == null){
            Destroy(this.gameObject);
            return;
        }
        _canvas.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        if (_canvas != null) _canvas.gameObject.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        if (_canvas != null) _canvas.gameObject.SetActive(false);
    }
}
