using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cliche", menuName = "Cliche")]
public class ScriptableCliche : ScriptableObject
{
    public string Name;
    [TextArea(1, 3)]
    public string Description;
    public int id;
    public GameObject Model;
    //public Sprite cardSprite;
    //public Sprite Jewel;

    //public CardColor cardColor;
    //public int cardMana;
    //public int cardCooldown;
    //public int cardAttack;
    //public int cardHealth;
    //public int cardCost;
    ////public int color;
    ////public GameObject skill;
    //public int cardID;
    //public bool needsTarget;
    //public bool combo;
    //public bool cast;
}
