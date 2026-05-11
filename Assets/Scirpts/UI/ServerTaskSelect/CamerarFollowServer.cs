using UnityEngine;

/*
 *  Description:
 *      This class are responsible to controller the Camera to focus the select server
 */

public class CamerarFollowServer : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector3 targetPos = Vector3.zero;
    private Vector3 realPos = Vector3.zero;

    void Awake()
    {
        targetPos = transform.position;
    }
    
    void Update()
    {
        if (targetPos == transform.position) return;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime *  speed);
        transform.LookAt(realPos);
    }
    
    public void setTargetPos(Vector3 pos, Vector3 realPos){
        targetPos = pos;
        this.realPos = realPos;
    }
}
