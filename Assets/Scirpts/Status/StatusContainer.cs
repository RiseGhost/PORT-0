using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusContainer<T, E> : MonoBehaviour
{
    public T status;
    public List<E> effect;
}