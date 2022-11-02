using System;
using UnityEngine;

public class WorldTicker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Time until the next tick")]
    private float worldTimer;
    private float worldTimerCap = 1;

    //Subscribe to these depending on when you want an effect like poison to "tick"
    public event EventHandler OnOneSecondTick;


    #region Instance
    public static WorldTicker Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Update()
    {
        if (worldTimer >= worldTimerCap)
        {
            OnOneSecondTick?.Invoke(this, EventArgs.Empty);
            worldTimer = 0;
        }
        else
        {
            worldTimer += Time.deltaTime;
        }
    }
}