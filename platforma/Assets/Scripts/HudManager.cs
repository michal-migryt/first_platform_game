using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudManager : MonoBehaviour
{
    private enum HeartState{ FullHeart, HalfOfHeart, EmptyHeart}
    public PlayerStats playerStats;
    public Image TenthsOfCoins;
    public Image OnesOfCoins;
    public Image NumberOfLifes;
    public GameObject[] HeartContainer;
    public Image[] HeartContainerImage;
    public Sprite [] SpriteList;
    public Sprite [] NumberList; // Array index = number (0-9)
    // Start is called before the first frame update
    void Start()
    {
        SetHud();
    }

    // Update is called once per frame
    void Update()
    {
        SetHud();
    }

    private void SetHud()
    {
        for(int i=0; i < playerStats.GetMaxHearts(); i++)
        {
            HeartContainer[i].SetActive(true);
        }
        for(int i=0, temp=playerStats.GetCurrentHalfHearts(); i < playerStats.GetMaxHearts(); i++)
        {
            if(temp/2 > 0)
            HeartContainerImage[i].sprite = SpriteList[(int)HeartState.FullHeart];
            else if(temp/2 <= 0 && temp % 2 == 1 && temp > 0)
            HeartContainerImage[i].sprite = SpriteList[(int)HeartState.HalfOfHeart];
            else
            HeartContainerImage[i].sprite = SpriteList[(int)HeartState.EmptyHeart];
            temp -= 2;
        }
        TenthsOfCoins.sprite = NumberList[playerStats.GetCoins()/10];
        OnesOfCoins.sprite = NumberList[playerStats.GetCoins()%10];
        NumberOfLifes.sprite = NumberList[playerStats.GetLifes()];
    }
}
