using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public float time = 0f;

    public bool playCheck = false;
    public bool playSingle = false;
    public bool playMulti = false;
    public bool mainMenu = false;
    public bool playSingleCheck = false;
    public bool playMultiCheck = false;
    public bool titleCheck = false;

    public GameObject Title;

    public GameObject Canvas;
    public GameObject Main;
    public GameObject Play;
    public GameObject PlayButton;
    public GameObject OptionsMenu;
    public GameObject OptionsButton;
    // Hide Exit button 
    public Transform Soundboard;
    public GameObject BlackScreen;
    public GameObject StatsManager;

    void Awake()
    {
      Title = GameObject.Find("TitleButton");
      Canvas = GameObject.Find("Canvas");
      Main = GameObject.Find("MainMenu");
      Play = GameObject.Find("PlayMenu");
      PlayButton = GameObject.Find("PlayButton");
      OptionsMenu = GameObject.Find("OptionsMenu");
      OptionsButton = GameObject.Find("OptionsButton");
      Soundboard = GameObject.Find("Soundboard/SFX").transform;
      BlackScreen = GameObject.Find("BlackScreen");
      StatsManager = GameObject.Find("StatsManager");
    }

    void Start()
    {
      // Hide menu and buttons on title screen
      if (Main != null)
      {
        Main.transform.SetParent(Canvas.transform);
        Main.transform.localScale = new Vector3(0, 0, 0);
      }
      if (OptionsMenu != null)
      {
        OptionsMenu.transform.SetParent(Canvas.transform);
        OptionsMenu.transform.localScale = new Vector3(0, 0, 0);
      }
      if (Play != null)
      {
        Play.transform.SetParent(Canvas.transform);
        Play.transform.localScale = new Vector3(0, 0, 0);
      }
      playSingleCheck = false;
      playMultiCheck = false;
    }

    void Update()
    {
      // Timer to load scenes after screen fades to black
      time += Time.deltaTime;
      if (playSingle && time > 2)
      {
        playSingleCheck = true;
        SceneManager.LoadScene("Gametest");
      }
      if (playMulti && time > 2)
      {
        playMultiCheck = true;
        SceneManager.LoadScene("Loading");
      }
      if (mainMenu && time > 2)
      {
        SceneManager.LoadScene("MainMenu");
      }
      if (titleCheck && time > 2)
      {
        Title.SetActive(false);
        Main.transform.localScale = new Vector3(1, 1, 1);
        BlackScreen.GetComponent<GameController>().BlackoutFunction();
      }
      if (playCheck && time > 1)
      {
        Play.transform.localScale = new Vector3(1, 1, 1);
        PlayButton.transform.localScale = new Vector3(0, 0, 0);
        OptionsButton.transform.localScale = new Vector3(0, 0, 0);
        playCheck = false;
      }
    }

    public void TitleScreen()
    {
      if(!titleCheck) {
        Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        BlackScreen.GetComponent<GameController>().BlackoutFunction();
        time = 0f;
        titleCheck = true;
      }
    }

    public void Options()
    {
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      OptionsMenu.transform.localScale = new Vector3(1, 1, 1);
      PlayButton.transform.localScale = new Vector3(0, 0, 0);
      OptionsButton.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Back()
    {
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      PlayButton.transform.localScale = new Vector3(1, 1, 1);
      OptionsButton.transform.localScale = new Vector3(1, 1, 1);
      Play.transform.localScale = new Vector3(0, 0, 0);
      OptionsMenu.transform.localScale = new Vector3(0, 0, 0);
    }

    public void MainMenu()
    {
      BlackScreen.GetComponent<GameController>().BlackoutFunction();
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      time = 0f;
      mainMenu = true;
    }

    public void PlayMenu()
    {
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      time = 0f;
      playCheck = true;
    }

    public void SinglePlayer()
    {
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      Play.transform.localScale = new Vector3(1, 1, 1);
      BlackScreen.GetComponent<GameController>().BlackoutFunction();
      time = 0f;
      playSingle = true;
    }

    public void Multiplayer()
    {
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      Play.transform.localScale = new Vector3(1, 1, 1);
      BlackScreen.GetComponent<GameController>().BlackoutFunction();
      time = 0f;
      playMulti = true;
    }


    public void Close()
    {
      Application.Quit();
    }
}