using System;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class StatusImplement<T> : MonoBehaviour, Status<T>
{
    public string ID = Guid.NewGuid().ToString();
    public StatusType type;
    [SerializeField]
    private T value;

    public StatusType GetType() { return type; }
    public T GetValue() { return value; }

}