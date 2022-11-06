using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamselSetActive : MonoBehaviour
{
    public GameObject DamselGreeting;

    public GameObject DamselDeath;

    public GameObject MerchantGreeting;

    public GameObject MerchantDeath;

    public float time = 0f;

    public bool deathCheck = false;

    public float deathTimer = 5f;

    public void ActivateDamselGreeting()
    {
        DamselGreeting.SetActive(true);
        MerchantGreeting.SetActive(false);
        MerchantDeath.SetActive(false);
        DamselDeath.SetActive(false);
    }

    public void DeactivateDamselGreeting()
    {
        DamselGreeting.SetActive(false);
    }

    public void ActivateDamselDeath()
    {
        DamselDeath.SetActive(true);
        MerchantGreeting.SetActive(false);
        MerchantDeath.SetActive(false);
        DamselGreeting.SetActive(false);
        time = 0f;
        deathCheck = true;
    }

    public void DeactivateDamselDeath()
    {
        DamselDeath.SetActive(false);
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
            DeactivateDamselDeath();
        }
    }
}
