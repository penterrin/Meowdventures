using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class BattleHud : MonoBehaviour
{

    public TMP_Text nameText;
    public Slider hpSlider;
    public Slider mpSlider;


    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;


        mpSlider.maxValue = unit.MaxMagicPoints;
        mpSlider.value = unit.MagicPoints;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetMP(int mp)
    {
        mpSlider.value = mp;
    }


}
