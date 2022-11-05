using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantDialogueTrigger : MonoBehaviour
{
    public GameObject MerchantDialogueBox;

    public bool textBox = false;

    public bool triggerOnce = false;

    public float time = 0f;

    void Awake()
    {
        MerchantDialogueBox = GameObject.Find("MerchantDialogueBox");
    }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !triggerOnce)
        {
            MerchantDialogueBox
                .GetComponent<MerchantSetActive>()
                .ActiveMerchantDialogueBoxTrigger();
            textBox = true;
            time = 0f;
            triggerOnce = true;
        }
    }
}
