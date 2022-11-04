using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public float time = 0f;
    public GameObject Title;
    public bool titleCheck = false;

    public GameObject BlackScreen;

    void Awake()
    {
      Title = GameObject.Find("TitleButton");
      BlackScreen = GameObject.Find("BlackScreen");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      time += Time.deltaTime;
      if (titleCheck && time > 2)
      {
        SceneManager.LoadScene("MainMenu");
      }
    }

    public void TitleFunc()
    {
      if(!titleCheck) {
        BlackScreen.GetComponent<GameController>().BlackoutFunction();
        time = 0f;
        titleCheck = true;
      }
    }
}
