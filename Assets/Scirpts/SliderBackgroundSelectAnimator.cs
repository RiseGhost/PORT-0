using UnityEngine;
using UnityEngine.EventSystems;

public class SliderBackgroundSelectAnimator : MonoBehaviour
{
    [SerializeField] private GameObject FillArea;
    [SerializeField] private SliderHandleZoomAnimator animator;
    void Awake()
    {
        if (FillArea == null) Destroy(this);
    }

    void Update()
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem == null) return;
        if (eventSystem.currentSelectedGameObject == gameObject && FillArea.GetComponent<RandomGridAppearanceAnimator>() == null)
        {
            FillArea.AddComponent<RandomGridAppearanceAnimator>().autoDestroy = false;
        }
        if (eventSystem.currentSelectedGameObject != gameObject) Destroy(FillArea.GetComponent<RandomGridAppearanceAnimator>());
        if (animator != null)
            animator.changeFocus(eventSystem.currentSelectedGameObject == gameObject);
    }
}
