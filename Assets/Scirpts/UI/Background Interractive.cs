using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundInterractive : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("OnSelect");
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("OnDeselect");
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
