using System;
using UnityEngine;

public class WorldTicker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Time until the next tick")]
    private float worldTimer;
    private float worldTimerCap = 1;

    [SerializeField]
    [Tooltip("The current tick we are on")]
    private int tick = 0;


    //Subscribe to these depending on when you want an effect like poison to "tick"
    public event EventHandler OnOneSecondTick, OnTwoSecondTick, OnThreeSecondTick;

    private void Update()
    {
        if (worldTimer >= worldTimerCap)
        {
            tick++;
            worldTimer = 0;
            if (tick == 1)
            {
                OnOneSecondTick?.Invoke(this, EventArgs.Empty);
            }
            else if (tick == 2)
            {
                OnTwoSecondTick?.Invoke(this, EventArgs.Empty);
            }
            else if (tick == 3)
            {
                OnThreeSecondTick?.Invoke(this, EventArgs.Empty);
                tick = 0;
            }
        }
        else
        {
            worldTimer += Time.deltaTime;
        }
    }
}