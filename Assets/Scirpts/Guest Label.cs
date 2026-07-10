using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GuestLabel : MonoBehaviour
{
    [SerializeField] private string prefix = "guest-";
    private const string defaultID = "Ru_a760Bv";
    private TextMeshProUGUI label;
    FirebaseManager firebase;


    void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
        firebase = GameObject.FindFirstObjectByType<FirebaseManager>();
    }


    void Update()
    {
        if (firebase == null) label.text = prefix + defaultID;
        else label.text = (firebase.getUID().Count() == 0) ? prefix + defaultID : prefix + firebase.getUID();
    }
}
