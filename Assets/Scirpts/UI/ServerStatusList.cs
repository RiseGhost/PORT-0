using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerStatusList : MonoBehaviour
{
    [SerializeField] float increSpeed = 3f;
    [SerializeField] TextMeshProUGUI TFLOPS_Label,WATTS_Label;
    [SerializeField] Slider TFLOPS_Slider, WATTS_Slider;
    [SerializeField] ServerConfigBook serverConfigBook;
    private float currentFLOPS_Value = 0f, currentWatts_Value = 0f;
    private GROUP_CPUS_Widget group_cpus;
    private GROUP_FANS_Widget group_fans;
    private GROUP_MotherBoards_Widget group_motherboard;

    void Start()
    {
        if (serverConfigBook == null) Destroy(this);
    }

    void Update()
    {
        group_cpus = serverConfigBook.getGROUP_CPUS();
        group_fans = serverConfigBook.getFANS_Widget();
        group_motherboard = serverConfigBook.GetMotherBoards_Widget();
        CPU cpu = group_cpus.getSelectCPU();
        FanStatus fanStatus = group_fans.getSelect();
        MotherBoardStatus motherBoardStatus = group_motherboard.getSelect();
        if (cpu == null) return;

        float TFLOPS = cpu.getTFLOPS();

        float increFractorTFLOPS = (currentFLOPS_Value == TFLOPS) ? 0f : (Time.deltaTime * (1 / Mathf.Abs(TFLOPS - currentFLOPS_Value))) * increSpeed * TFLOPS_Slider.maxValue;
        currentFLOPS_Value = (float)Math.Round(Mathf.Lerp(currentFLOPS_Value, TFLOPS, increFractorTFLOPS), 2);
        
        TFLOPS_Label.text = currentFLOPS_Value.ToString();
        TFLOPS_Slider.value = Mathf.Lerp(TFLOPS_Slider.value, TFLOPS, increFractorTFLOPS);

        float Watts = 0;
        if (cpu.getStatus() != null)    Watts += cpu.getStatus().getWatts();
        if (fanStatus != null)          Watts += fanStatus.GetValue().Watts;
        if (motherBoardStatus != null)  Watts += motherBoardStatus.GetValue().Watts;

        float increFractorWatts = (currentWatts_Value == Watts) ? 0f : (Time.deltaTime * (1/Mathf.Abs(Watts - currentWatts_Value))) * increSpeed * WATTS_Slider.maxValue;
        currentWatts_Value = (float)Math.Round(Mathf.Lerp(currentWatts_Value, Watts, increFractorWatts),2);

        WATTS_Label.text = currentWatts_Value.ToString();
        WATTS_Slider.value = Mathf.Lerp(WATTS_Slider.value, Watts, increFractorWatts);
    }
}