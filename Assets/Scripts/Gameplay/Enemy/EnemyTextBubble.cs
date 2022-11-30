using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyTextBubble : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Possible text to show up in the bubble, chosen at random")]
    private string[] possiblePhrases;
    [SerializeField]
    private SpriteRenderer bubbleBackground;
    [SerializeField]
    private TextMeshPro text;


    private Vector2 padding = new(1f, 0.25f);

    private float timer = 0f;
    private float timerCap = 30f;
    public bool isTutorial = false;

    private void Start()
    {
        //So a bunch of enemies spawned at the same time don't all populate the screen with bubbles at once
        timer = Random.Range(0, timerCap);
        HideBubble();
    }
    private void FixedUpdate()
    {
        if(isTutorial)
        {
            return;
        }
        else
        {
            if (timer < timerCap)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
                ShowBubble();
            }
        }
    }
    private void ShowBubble()
    {
        text.gameObject.SetActive(true);
        text.SetText(possiblePhrases[Random.Range(0, possiblePhrases.Length - 1)]);
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues(false);
        bubbleBackground.size = textSize + padding;
        Invoke("HideBubble", 3f);
    }
    public void ShowTutorial(string tutorialText)
    {
        text.gameObject.SetActive(true);
        text.SetText(tutorialText);
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues(false);
        bubbleBackground.size = textSize + padding;
        Invoke("HideBubble", 10f);
    }
    private void HideBubble()
    {
        text.gameObject.SetActive(false);
    }

}
