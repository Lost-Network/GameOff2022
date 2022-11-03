using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBlackout : MonoBehaviour
{
    public GameObject BlackScreen;

    void Awake()
    {
        BlackScreen = GameObject.Find("BlackScreen");
    }
    // Start is called before the first frame update
    void Start()
    {
        BlackScreen.GetComponent<UIManager>().blackoutTargetOpacity = 0;
        BlackScreen.GetComponent<GameController>().blackoutActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
