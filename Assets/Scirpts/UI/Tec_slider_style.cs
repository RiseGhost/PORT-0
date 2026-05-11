using UnityEngine;
using UnityEngine.UI;

public class Tec_slider_style : MonoBehaviour
{
    private Slider slider;
    private float step = 0f;
    [SerializeField] private float maxValue = 10f;
    [SerializeField] private GameObject BlockContainer;
    [SerializeField] private Color FillColor, EmptyColor;

    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null) Destroy(this.gameObject);
        if (BlockContainer == null) Destroy(this.gameObject);
        step = maxValue / BlockContainer.transform.childCount;
        slider.onValueChanged.AddListener(UpdateBlock);
    }

    private void UpdateBlock(float value)
    {
        for(int i = 0; i < BlockContainer.transform.childCount; i++){
            RawImage block = BlockContainer.transform.GetChild(i).GetComponent<RawImage>();
            float blockValue = step + (i * step);
            block.color = (blockValue <= slider.value) ? FillColor : EmptyColor;
        }
    }
}
