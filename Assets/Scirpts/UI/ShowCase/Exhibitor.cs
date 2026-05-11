using UnityEngine;

public enum ExhibitorType
{
    Processor,
    Fan,
    MotherBoard,
    Disk
}

public class Exhibitor : MonoBehaviour
{
    public ExhibitorType type;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 2);
    }

    public void setTargetPos(Vector3 targetPos) { this.targetPos = targetPos; }

    public override bool Equals(object other)
    {
        if (other is Exhibitor)
        {
            var obj = (Exhibitor) other;
            return this.type == obj.type && this.GetComponent<Mesh>() == obj.GetComponent<Mesh>();
        }
        else return false;
    }
}
