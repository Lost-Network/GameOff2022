using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private UIManager uiManager;
    public bool blackoutActivated;
    private bool pause;
    private static GameController playerInstance;

    void Awake()
    {
        // DontDestroyOnLoad(transform.gameObject);

        // if (playerInstance == null)
        // {
        //     playerInstance = this;
        // }
        // else
        // {
        //     DestroyObject(gameObject);
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlackoutFunction()
    {
        blackoutActivated = !blackoutActivated;
        if (blackoutActivated)
        {
            uiManager.SetBlackoutOpacity(1);
        }
        else
        {
            uiManager.SetBlackoutOpacity(0);
        }
    }
}
