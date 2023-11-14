using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;
    public string unitLevel;

    public int damage;
    public int Magicdamage;

    public int MagicPoints;
    public int MaxMagicPoints;
    public int MagicPointCost;


    public int maxHP;
    public int currentHP;


    public bool ReducePoints(int cost)
    {
        if (MagicPoints >= cost)
        {
            MagicPoints -= cost;
            return true; // There are enough magic points
        }
        else
        {
            return false; // Not enough magic points
        }
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }



}
