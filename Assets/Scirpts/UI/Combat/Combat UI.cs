using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private Slider WaveLength_1, WaveLength_2;
    private WaveLaunch wave;
    private bool Lock = false;

    void Update(){
        if (Lock) return;
        if (WaveLength_1 != null && wave != null) WaveLength_1.value = wave.AlivePercentage();
        if (WaveLength_2 != null && WaveLength_1 != null && wave != null) WaveLength_2.value = Mathf.MoveTowards(WaveLength_2.value,WaveLength_1.value,Time.deltaTime);
        if (wave != null && wave.GetAliveDrones() == 0) StartCoroutine(ReturnNormalGame());
    }

    public void setWave(WaveLaunch wave){ 
        Lock = false;
        this.wave = wave;
    }

    public IEnumerator ReturnNormalGame(){
        Lock = true;
        yield return new WaitForSeconds(2);
        GameObject.FindFirstObjectByType<CannonController>().DeactiveCombatMode();
    }
}
