using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;

    public float GetCurrentMana() {
        return slider.value;
    }
    public void SetMaxMana(float maxMana)
    {
        slider.maxValue = maxMana;
        slider.value = maxMana;
        SetFillColor();
    }

    private void SetFillColor()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
        SetFillColor();
    }
}
