using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image blackoutImage;
    [SerializeField]
    private float blackoutOpacityChangeStep;
    public float blackoutTargetOpacity;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        Blackout();
    }

    private void Blackout()
    {
        float currentOpacity = blackoutImage.color.a;

        if (currentOpacity < blackoutTargetOpacity)
        {
            blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, blackoutImage.color.a + blackoutOpacityChangeStep);
            if (blackoutImage.color.a > blackoutTargetOpacity)
            {
                blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, blackoutTargetOpacity);
            }
        }
        else if (currentOpacity > blackoutTargetOpacity)
        {
            blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, blackoutImage.color.a - blackoutOpacityChangeStep);
            if (blackoutImage.color.a < blackoutTargetOpacity)
            {
                blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, blackoutTargetOpacity);
            }
        }
    }

    public void SetBlackoutOpacity(float o)
    {
        blackoutTargetOpacity = o;
    }
}