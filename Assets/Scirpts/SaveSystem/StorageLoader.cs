using System;
using System.Collections;
using UnityEngine;

public class StorageLoader : MonoBehaviour
{
    //public StorageEntityType[] entityTypes;

    void Start()
    {
        try
        {
            StorageManager.Load<Server>();
        } catch (Exception e){}
    }
}
