using UnityEngine;

/*
    Description:
        This class is responsible for putting the servers that spawn in order.
*/

public class ServersPlace : MonoBehaviour
{
    [SerializeField] private float Max, gap;
    [SerializeField] private Vector3 axi, rotation;
    [SerializeField] private float SkyDrop = 0f;
    private short count = 0;

    public void put(ServerGameObject server)
    {
        if (count == Max) return;
        server.transform.position = new Vector3(0,SkyDrop,0) + transform.position + (count + (count * gap)) * axi;
        server.transform.rotation = Quaternion.Euler(rotation);
        count++;
    }
}
