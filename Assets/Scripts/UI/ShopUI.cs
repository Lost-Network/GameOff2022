using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    //We provide 3 random upgrades for players to purchase, these choices are client side
    //We can also provide a way for the player to purchase a health refill as a seperate 4th option that is always there
    //Maybe a 5th option for a consumable healing item the player can pop during a wave, but have it be way more expensive and not heal as
    //much as the instant health refill option, like the 4th option, this would always be here

    public int shopOption1, shopOption2, shopOption3;
    private int dummyMoneyVariable;

    [System.Serializable]
    public class ShopItem1
    {
        public string itemName;
        public int itemID;
        public int cost;
    }
    public List<ShopItem1> itemChoices1;
    [System.Serializable]
    public class ShopItem2
    {
        public string itemName;
        public int itemID;
        public int cost;
    }
    public List<ShopItem2> itemChoices2;
    [System.Serializable]
    public class ShopItem3
    {
        public string itemName;
        public int itemID;
        public int cost;
    }
    public List<ShopItem3> itemChoices3;


    //For shopOption selection, we could have option1 always be an offensive upgrade, option2 always be defensive, and option3 be something technical like a cooldown reduction or something
    //This way we never run into a situation where we have dupe options in the shop

    public void SetShopOptions()
    {
        shopOption1 = Random.Range(0, itemChoices1.Count);
        shopOption2 = Random.Range(0, itemChoices2.Count);
        shopOption3 = Random.Range(0, itemChoices3.Count);
    }
    public void CheckIfPlayerCanAfford(int cost)
    {
        if(cost >= dummyMoneyVariable)
        {
            //We let the player buy the thing
        }
        else
        {
            //no
        }
    }
    public void ObtainOption1()
    {

    }
    public void ObtainOption2()
    {

    }
    public void ObtainOption3()
    {

    }
}
