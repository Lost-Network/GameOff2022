using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSetActive : MonoBehaviour
{
    public GameObject[] MerchantDialogue;
    public GameObject[] DamselDialogue;

    public float time = 0f;
    public float deathTimer = 5f;
    public float savedTimer = 10f;

    public bool deathCheck = false;
    public bool savedCheck = false;

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
      if (savedCheck && time > savedTimer)
      {
        DeactivateMerchantSaved();
      }
    }

    public void TurnOffLoop(){
      for (int i = 0; i < MerchantDialogue.Length; i++) {
        MerchantDialogue[i].SetActive(false);
      }
      for (int i = 0; i < DamselDialogue.Length; i++) {
        DamselDialogue[i].SetActive(false);
      }
    }

    public void ActivateMerchantGreeting()
    {
      TurnOffLoop();
      MerchantDialogue[0].SetActive(true);
    }

    public void DeactivateMerchantGreeting()
    {
      MerchantDialogue[0].SetActive(false);
    }

    public void ActivateMerchantDeath()
    {
      TurnOffLoop();
      MerchantDialogue[1].SetActive(true);
      time = 0f;
      deathCheck = true;
    }

    public void DeactivateMerchantDeath()
    {
      MerchantDialogue[1].SetActive(false);
      deathCheck = false;
    }

    public void ActivateMerchantSaved()
    {
      TurnOffLoop();
      MerchantDialogue[2].SetActive(true);
      time = 0f;
      savedCheck = true;
    }

    public void DeactivateMerchantSaved()
    {
      MerchantDialogue[2].SetActive(false);
      savedCheck = false;
    }

}
