using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EcnonomyDashSliders : MonoBehaviour
{
    [SerializeField] private Tec_slider_style profit, loss;
    [SerializeField] private float speed = 5f;
    [SerializeField] private TextMeshProUGUI MaxValue1, MaxValue2;
    [SerializeField] private TextMeshProUGUI ProfitLabel, ExpenseLabel;
    private List<PromisePay> Promises_expenses = new List<PromisePay>();
    private List<PromisePay> Promises_profits = new List<PromisePay>();
    private Slider profitSlider, lossSlider;
    private float profitValue, expenseValue, max;

    void OnDisable()
    {
        if (profit != null) profit.GetComponent<Slider>().value = 1;
        if (loss != null) loss.GetComponent<Slider>().value = 1;
    }

    void OnEnable()
    {
        Promises_expenses = MoneyBank.GetPromises().Where(promise => promise.group == PayGroup.Expense).ToList();
        Promises_profits = MoneyBank.GetPromises().Where(promise => promise.group == PayGroup.Profit).ToList();
        profitValue = Promises_profits.Sum(x => x.PeriodicValue);
        expenseValue = Promises_expenses.Sum(x => x.PeriodicValue);
        max = Mathf.Max(Mathf.Abs(profitValue), Mathf.Abs(expenseValue));
        profitSlider = profit.GetComponent<Slider>();
        lossSlider = loss.GetComponent<Slider>();
        profitSlider.value = 1;
        lossSlider.value = 1;
        profit.setMaxValue(max);
        loss.setMaxValue(max);
        if (MaxValue1 != null) MaxValue1.text = max.ToString();
        if (MaxValue2 != null) MaxValue2.text = max.ToString();
        if (ProfitLabel != null) ProfitLabel.text = profitValue.ToString();
        if (ExpenseLabel != null) ExpenseLabel.text = (Mathf.Abs(expenseValue)).ToString();
    }
    void Awake(){
        if (profit == null || loss == null)
            Destroy(this);
        profit.GetComponent<Slider>().value = 1;
        loss.GetComponent<Slider>().value = 1;
    }

    void Update(){
        if (profitSlider != null)
            profitSlider.value = Mathf.Clamp(profitSlider.value + (max * speed * Time.deltaTime),0,profitValue);
        if (lossSlider != null)
            lossSlider.value = Mathf.Clamp(lossSlider.value + (max * speed * Time.deltaTime),0,Mathf.Abs(expenseValue));
        
        if (profitSlider == null || lossSlider == null) return;

        //Debug.Log($"Profit: {profitSlider.value} / {profitValue} | Loss: {lossSlider.value} / {Mathf.Abs(expenseValue)}");
    }


}
