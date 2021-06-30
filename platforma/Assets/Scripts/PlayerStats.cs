using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int Lifes = 3;
    [SerializeField]
    private int PossibleHearts = 5;
    [SerializeField]
    private int MaxHearts = 3;
    [SerializeField]
    private int CurrentHalfHearts = 6;
    [SerializeField]
    private int Coins = 0;
    private bool needRespawn = false;
   
    public int GetMaxHearts()
    {
        return MaxHearts;
    }
    public int GetCoins()
    {
        return Coins;
    }
    public int GetCurrentHalfHearts()
    {
        return CurrentHalfHearts;
    }
    public int GetPossibleHearts()
    {
        return PossibleHearts;
    }
    public int GetLifes()
    {
        return Lifes;
    }
    public bool GetRespawnNeed()
    {
        return needRespawn;
    }
    public void TakeDamage(int amountOfHalfHeartsDamage)
    {
        CurrentHalfHearts-= amountOfHalfHeartsDamage;
    }
    public void Death()
    {
        Lifes -= 1;
    }
    public void GainLife()
    {
        Lifes += 1;
    }
    public void AddCoin()
    {
        Coins++;
    }
    public void AddCoins(int amount)
    {
        Coins += amount;
    }
    public void LoseCoins(int amount)
    {
        Coins -= amount;
    }
    public void setRespawnNeed()
    {
        needRespawn = true;
    }
    public void Respawned()
    {
        needRespawn = false;
        MaxHeal();
    }
    public void Heal(int amountOfHalfHearts)
    {
        CurrentHalfHearts += amountOfHalfHearts;
    }
    public void MaxHeal()
    {
        CurrentHalfHearts = MaxHearts*2;
    }
}