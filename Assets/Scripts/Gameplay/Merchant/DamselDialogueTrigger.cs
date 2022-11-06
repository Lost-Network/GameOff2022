using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamselDialogueTrigger : MonoBehaviour
{
    public GameObject DamselDialogueBox;

    public bool textBox = false;

    public bool triggerOnce = false;

    public float time = 0f;

    public float greetingTimer = 10f;

    void Awake()
    {
        DamselDialogueBox = GameObject.Find("DamselDialogueBox");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (textBox && time > greetingTimer)
        {
            DamselDialogueBox
                .GetComponent<DamselSetActive>()
                .DeactivateDamselGreeting();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !triggerOnce)
        {
            DamselDialogueBox
                .GetComponent<DamselSetActive>()
                .ActivateDamselGreeting();
            textBox = true;
            time = 0f;
            triggerOnce = true;
        }
    }
}
