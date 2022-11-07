using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantStatBoost : MonoBehaviour
{

    public GameObject MerchantShop;

    public int attackBoost = 1;
    public int healthBoost = 1;
    public int speedBoost = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAttackBoost(){
      Debug.Log("Added +1 attack!");
      MerchantShop.SetActive(false);
    }

    public void AddHealthBoost(){
      Debug.Log("Added +1 health!");
      MerchantShop.SetActive(false);
    }

    public void AddSpeedBoost(){
      Debug.Log("Added +1 speed!");
      MerchantShop.SetActive(false);
    }


}
