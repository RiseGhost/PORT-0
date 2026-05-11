using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatingUI : MonoBehaviour, FloatingUIInterface
{
    [SerializeField] protected float UPspeed = 0.8f;
    [SerializeField] protected float DownSpeed = 1.5f;
    [SerializeField] protected Vector2 DeltaXZ = Vector2.zero;
    [SerializeField] protected Canvas UITemplate;
    [SerializeField] protected Color BackgroundColor;
    [SerializeField] protected float StartY = -1f;
    [SerializeField] protected float FinalY = 2.2f;
    protected RawImage Background;
    protected Canvas UI;
    protected bool Entry = false;
    private Vector3 defaultScale = Vector3.zero;

    void Awake()
    {
        if (UITemplate != null) Background = UITemplate.GetComponent<RawImage>();
    }

    void Update()
    {
        if (Entry) EnterAnimation();
        else ExitAnimation();
        if (Entry) EntryUpdate();
    }

    protected void EnterAnimation()
    {
        if (UI == null) return;
        Vector3 targetPos = new Vector3(transform.position.x + DeltaXZ.x, FinalY, transform.position.z + DeltaXZ.y);
        UI.transform.position = Vector3.Lerp(UI.transform.position, targetPos, Time.deltaTime * UPspeed);
        UI.transform.localScale = Vector3.Lerp(UI.transform.localScale, defaultScale,Time.deltaTime);
        if (Background == null) return;
        Background.color = Vector4.Lerp(Background.color, BackgroundColor, Time.deltaTime * UPspeed);
    }

    protected void ExitAnimation()
    {
        if (UI == null) return;
        Vector3 targetPos = new Vector3(transform.position.x, StartY, transform.position.z);
        UI.transform.position = Vector3.Lerp(UI.transform.position, targetPos, Time.deltaTime * DownSpeed);
        UI.transform.localScale = Vector3.Lerp(UI.transform.localScale,UI.transform.localScale*0.1f,Time.deltaTime * DownSpeed);
        if (Vector3.Distance(targetPos, UI.transform.position) < 0.5f) UI.gameObject.SetActive(false);
        if (Background == null) return;
        Background.color = Vector4.Lerp(BackgroundColor, new Color(0,0,0,0), Time.deltaTime * DownSpeed);
    }

    public void show()
    {
        Entry = true;
        if (UI != null)
        {
            UI.gameObject.SetActive(true);
            InitUI();
            return;
        }
        if (UITemplate == null) return;
        UI = Instantiate(UITemplate, transform);
        UI.transform.Translate(0, StartY, 0);
        defaultScale = UI.transform.localScale;
        UI.transform.localScale *= 0.1f;
        if (Background == null) return;
        Background = UI.GetComponent<RawImage>();
        Background.color = new Color(0, 0, 0, 0);
        InitUI();
    }

    public void hide()
    {
        if (UI == null) return;
        Entry = false;
    }

    // This method is called as soon as the UI is installed
    protected virtual void InitUI(){}
    // This method is called while UI is entry
    protected virtual void EntryUpdate(){}

    public void destroy()
    {
        if (UI == null) return;
        hide();
        StartCoroutine(WaitUIExit());
    }

    private IEnumerator WaitUIExit()
    {
        while (UI != null || UI.gameObject.active)
        {
            yield return new WaitForFixedUpdate();
        }
        Destroy(UI.gameObject);
    }
}