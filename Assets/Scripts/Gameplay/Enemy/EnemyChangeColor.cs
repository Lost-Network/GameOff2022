using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeColor : MonoBehaviour
{
    private SpriteRenderer thisRenderer;
    public Color[] possibleColors;
    private void Awake()
    {
        thisRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetEnemyColor(int colorStage)
    {
        int colorToSet = colorStage;
        if (colorToSet > possibleColors.Length - 1)
        {
            colorToSet = possibleColors.Length - 1;
        }
        thisRenderer.color = possibleColors[colorToSet];
    }
}
