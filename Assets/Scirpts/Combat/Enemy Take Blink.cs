using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyTakeBlink : MonoBehaviour
{
    private MeshRenderer _meshRender;
    void Start()
    {
        _meshRender = GetComponent<MeshRenderer>();
    }

    public void Blink()
    {
        StartCoroutine(Effect());
    }

    private IEnumerator Effect()
    {
        _meshRender.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _meshRender.enabled = true;
    }
}
