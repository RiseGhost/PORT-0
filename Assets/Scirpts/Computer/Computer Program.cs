using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerProgram : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _programUI;
    private bool visible = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_programUI == null) return;
        visible = !visible;
        _programUI.gameObject.SetActive(visible);
        _programUI.transform.SetAsLastSibling();
    }
}
