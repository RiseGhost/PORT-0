using UnityEngine;

[System.Serializable]
public class PositionStatus
{
    public Vector3 pos;
    public string fatherTag;
    public Vector3 rotation;

    public PositionStatus()
    {
        this.pos = new Vector3(0, 0, 0);
        this.fatherTag = "";
        this.rotation = new Vector3(0, 0, 0);
    }

    public PositionStatus(Transform transform)
    {
        this.pos = transform.position;
        this.fatherTag = (transform.parent != null) ? transform.parent.tag : "";
        this.rotation = transform.rotation.eulerAngles;
    }
}