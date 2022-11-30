using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterTutorial : MonoBehaviour
{
    private float tutorialTimer;
    public string[] tutorialText;
    private int tutorialID = -1;
    public GameObject chatBubble;

    private void Start()
    {
        //tutorialID = 0;
        //chatBubble.GetComponent<EnemyTextBubble>().ShowTutorial(tutorialText[tutorialID]);
    }
    void Update()
    {
        if(GetComponent<Movement>().photonView.IsMine)
        {
            if (tutorialTimer < 5 && tutorialID <= 3)
            {
                tutorialTimer += Time.deltaTime;
            }
            else if(tutorialID <= 3)
            {
                tutorialTimer = 0;
                tutorialID++;
                if(tutorialID != 4)
                {
                    chatBubble.GetComponent<EnemyTextBubble>().ShowTutorial(tutorialText[tutorialID]);
                }
            }
        }
    }
}
