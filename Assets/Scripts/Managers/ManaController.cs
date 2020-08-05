using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    public Slider slider;

    public int maxValue = 100;


    void Start()
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public bool CanCastSpell(int manacost)
    {
        if(manacost <= slider.value)
        {
            return true;
        }
        return false;
    }

    public void RestoreMana(int value)
    {
        slider.value += value;
    }

    public void ConsumeMana(int manacost)
    {
        slider.value -= manacost;
    }

    
}
