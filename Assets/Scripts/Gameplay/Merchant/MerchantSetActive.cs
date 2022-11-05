using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSetActive : MonoBehaviour
{
    public GameObject MerchantGreeting;

    public GameObject MerchantDeath;

    public float time = 0f;

    public bool deathCheck = false;

    public float deathTimer = 5f;

    public void ActivateMerchantGreeting()
    {
        MerchantGreeting.SetActive(true);
    }

    public void DeactivateMerchantGreeting()
    {
        MerchantGreeting.SetActive(false);
    }

    public void ActivateMerchantDeath()
    {
        MerchantDeath.SetActive(true);
        time = 0f;
        deathCheck = true;
    }

    public void DeactivateMerchantDeath()
    {
        MerchantDeath.SetActive(false);
        deathCheck = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (deathCheck && time > deathTimer)
        {
            DeactivateMerchantDeath();
        }
    }
}
