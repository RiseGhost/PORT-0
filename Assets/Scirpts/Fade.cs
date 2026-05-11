using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 *  Description:
 *      This class is responsible for controlling the alpha channel of a RawImage,
 *      in order to create a Fade effect.
 */

public enum FadeType
{
    FadeIn,
    FadeOut
}

[RequireComponent(typeof(RawImage))]
public class Fade : MonoBehaviour
{
    [SerializeField] private FadeType fadeType;
    [SerializeField] private float speed = 1f;
    private RawImage image;
    void Awake()
    {
        image = GetComponent<RawImage>();
    }

    void OnEnable()
    {
        StopAllCoroutines();
        Color color = image.color;
        color.a = 0f;
        image.color = color;
        StartCoroutine(Animate());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Animate()
    {
        if (fadeType == FadeType.FadeIn)
        {
            while (image.color.a < 1f)
            {
                if (!image.enabled) break;
                Color color = image.color;
                color.a += Time.deltaTime * speed;
                image.color = color;
                yield return null;
            }
        }
        else
        {
            while (image.color.a > 0f)
            {
                if (!image.enabled) break;
                Color color = image.color;
                color.a -= Time.deltaTime * speed;
                image.color = color;
                yield return null;
            }
        }
    }
}
