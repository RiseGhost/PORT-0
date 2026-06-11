using UnityEngine;

public class ResetGame : MonoBehaviour
{
    void Start()
    {
        if (Debug.isDebugBuild) PlayerPrefs.DeleteAll();
    }
}
