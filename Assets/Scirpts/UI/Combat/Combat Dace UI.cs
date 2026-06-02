using TMPro;
using UnityEngine;

public class CombatDaceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    public void setText(string text){
        if (textMeshProUGUI != null) textMeshProUGUI.text = text;
    }
}
