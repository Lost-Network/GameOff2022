using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamselSetActive : MonoBehaviour
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
        DeactivateDamselDeath();
      }
      if (savedCheck && time > savedTimer)
      {
        DeactivateDamselSaved();
      }
    }

    public void TurnOffLoop(){
      for (int i = 0; i < DamselDialogue.Length; i++) {
        MerchantDialogue[i].SetActive(false);
      }
      for (int i = 0; i < DamselDialogue.Length; i++) {
        DamselDialogue[i].SetActive(false);
      }
    }

    public void ActivateDamselGreeting()
    {
      TurnOffLoop();
      DamselDialogue[0].SetActive(true);
    }

    public void DeactivateDamselGreeting()
    {
      DamselDialogue[0].SetActive(false);
    }

    public void ActivateDamselDeath()
    {
      TurnOffLoop();
      DamselDialogue[1].SetActive(true);
      time = 0f;
      deathCheck = true;
    }

    public void DeactivateDamselDeath()
    {
      DamselDialogue[1].SetActive(false);
      deathCheck = false;
    }

    public void ActivateDamselSaved()
    {
      TurnOffLoop();
      DamselDialogue[2].SetActive(true);
      time = 0f;
      savedCheck = true;
    }

    public void DeactivateDamselSaved()
    {
      DamselDialogue[2].SetActive(false);
      savedCheck = false;
    }

}
