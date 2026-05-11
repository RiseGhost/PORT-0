using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryAnimation : MonoBehaviour
{
    [SerializeField] private GameObject Options;
    private RawImage rawImage;
    private Texture2D[] frames;
    private AudioSource audioSource;
    private short sense = 0;
    private short FramesIndex = 0;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        if (rawImage == null) Destroy(this);
        frames = Resources.LoadAll<Texture2D>("UI/Frames/Bits").OrderBy(t => ExtractNumber(t.name)).ToArray();
        rawImage.texture = frames[FramesIndex];
        StartCoroutine(Animation());
        audioSource = GetComponent<AudioSource>();
        audioSource.time = 4f;
        audioSource.loop = false;
    }

    private int ExtractNumber(string name)
    {
        return Int16.Parse(name.Split("-")[1]);
    }

    void Update()
    {
        if (Keyboard.current[Key.Space].isPressed)
        {
            sense = 1;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.time = 4f;
                if (Options != null) Options.SetActive(false);
            }
        }
        else
        {
            sense = -1;
            if (audioSource.isPlaying)
                audioSource.Stop();
            if (Options != null) Options.SetActive(true);
        }
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.016f);
            if (sense == -1 && FramesIndex > 0) FramesIndex--;
            if (sense == 1 && FramesIndex < frames.Length - 1) FramesIndex++;
            if (FramesIndex == frames.Length - 1) SceneManager.LoadScene("Teste FBX");
            rawImage.texture = frames[FramesIndex];
        }
    }
}