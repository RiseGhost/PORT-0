using UnityEngine;
using UnityEngine.UI;

public class UIPages : MonoBehaviour
{
    [SerializeField] protected Button Next, Previous;
    [SerializeField] protected ProgressionPageWidget progressionPageWidget;
    protected UIBook book;

    void Awake()
    {
        book = GetComponentInParent<UIBook>();
        if (book == null || Next == null) Destroy(this);
        Next.onClick.AddListener(OnNextClicked);
        if (Previous != null) Previous.onClick.AddListener(OnPreviousClicked);
    }

    void OnNextClicked()
    {
        book.nextPage();
    }

    void OnPreviousClicked()
    {
        book.backPage();
    }
}