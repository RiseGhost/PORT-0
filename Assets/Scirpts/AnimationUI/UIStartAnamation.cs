using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIStartAnimation : MonoBehaviour
{
    public UIElementAnimation[] elementAnimations;
    private VisualElement root;
    private List<List<VisualElement>> UIelements = new List<List<VisualElement>>();

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null) Destroy(this.gameObject);
        for (byte i = 0; i < elementAnimations.Length; i++)
        {
            UIelements.Add(new List<VisualElement>());
            for (byte j = 0; j < elementAnimations[i].parameters.Length; j++)
            {
                elementAnimations[i].parameters[j].Finish = false;
                VisualElement ve = root.Q<VisualElement>(elementAnimations[i].ElementID);
                UIelements.Last().Add(ve);
                applyStartEffect(i, j, elementAnimations[i].parameters[j]);
            }
        }
    }

    void Update()
    {
        bool end = true;
        for (byte i = 0; i < elementAnimations.Length; i++)
        {
            for (byte j = 0; j < elementAnimations[i].parameters.Length; j++)
            {
                var parameter = elementAnimations[i].parameters[j];
                increEffect(i, j, parameter);
                end = end && checkEndEffect(i, j, parameter);
            }
        }
        if (end) this.enabled = false;
    }

    void OnDisable()
    {
        Debug.Log("Animação acabou!!!");
    }

    private void applyStartEffect(byte i, byte j, UIElementParameters parameters) { applyEffect(i, j, parameters, parameters.startValue); }
    private void increEffect(byte i, byte j, UIElementParameters parameters) { applyEffect(i, j, parameters, parameters.endValue); }

    private void applyEffect(byte i, byte j, UIElementParameters parameters, float value)
    {
        switch (parameters.parameter)
        {
            case AnimationParameter.height:
                applyHeight(i, j, value, parameters.speed);
                break;
            case AnimationParameter.width:
                applyWidth(i, j, value, parameters.speed);
                break;
            default:
                break;
        }
    }

    private void applyHeight(byte i, byte j, float value, float speed)
    {
        UIelements[i][j].style.height = new StyleLength(
                new Length(
                    Mathf.MoveTowards(UIelements[i][j].style.height.value.value, value, Time.deltaTime * speed),
                    LengthUnit.Percent
                )
        );
        UIelements[i][j].style.minHeight = UIelements[i][j].style.height;
        UIelements[i][j].style.maxHeight = UIelements[i][j].style.height;
    }

    private void applyWidth(byte i, byte j, float value, float speed)
    {
        UIelements[i][j].style.width = new StyleLength(
                new Length(
                    Mathf.MoveTowards(UIelements[i][j].style.width.value.value, value, Time.deltaTime * speed),
                    LengthUnit.Percent
                )
        );
        UIelements[i][j].style.minWidth = UIelements[i][j].style.width;
        UIelements[i][j].style.minWidth = UIelements[i][j].style.width;
    }

    private bool checkEndEffect(byte i, byte j, UIElementParameters parameters)
    {
        switch (parameters.parameter)
        {
            case AnimationParameter.height: return checkHeightFinish(i, j, parameters);
            case AnimationParameter.width: return checkWidthFinish(i, j, parameters);
            default:
                return true;
        }
    }

    private bool checkHeightFinish(byte i, byte j, UIElementParameters parameters)
    {
        return Mathf.Abs(UIelements[i][j].style.height.value.value - parameters.endValue) < 0.5f;
    }

    private bool checkWidthFinish(byte i, byte j, UIElementParameters parameters)
    {
        return Mathf.Abs(UIelements[i][j].style.width.value.value - parameters.endValue) < 0.5f;
    }
}