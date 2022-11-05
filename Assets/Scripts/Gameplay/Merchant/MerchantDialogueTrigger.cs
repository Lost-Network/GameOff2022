using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantDialogueTrigger : MonoBehaviour
{
    public GameObject MerchantDialogueBox;

    public bool textBox = false;

    public bool triggerOnce = false;

    public float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (textBox && time > 10)
        {
            MerchantDialogueBox.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D myTrigger)
    {
        if (myTrigger.gameObject.tag == "Player" && !triggerOnce)
        {
            MerchantDialogueBox.SetActive(true);
            textBox = true;
            time = 0f;
            triggerOnce = true;
        }
    }
}
