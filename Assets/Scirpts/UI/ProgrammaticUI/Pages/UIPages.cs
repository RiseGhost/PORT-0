using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIPages : MonoBehaviour
{
    [SerializeField] protected Button Next, Previous;
    [SerializeField] protected ProgressionPageWidget progressionPageWidget;
    [SerializeField] private AudioClip clickSound;
    private AudioMixer mixer;
    protected UIBook book;

    void Awake()
    {
        book = GetComponentInParent<UIBook>();
        if (book == null || Next == null) Destroy(this);
        Next.onClick.AddListener(OnNextClicked);
        if (Previous != null) Previous.onClick.AddListener(OnPreviousClicked);
        mixer = Resources.Load<AudioMixer>("AudioMixer");
    }

    void OnNextClicked()
    {
        if (clickSound != null && mixer != null){
            SoundServer soundServer = GameObject.FindAnyObjectByType<SoundServer>();
            if (soundServer != null){
                soundServer.Play(clickSound,mixer.FindMatchingGroups("Effects")[0]);
            }
        }
        book.nextPage();
    }

    void OnPreviousClicked()
    {
         if (clickSound != null && mixer != null){
            SoundServer soundServer = GameObject.FindAnyObjectByType<SoundServer>();
            if (soundServer != null){
                soundServer.Play(clickSound,mixer.FindMatchingGroups("Effects")[0]);
            }
        }
        book.backPage();
    }
}