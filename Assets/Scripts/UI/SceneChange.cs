using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public float time = 0f;

    public bool play = false;
    public bool mainMenu = false;
    public bool playCheck = false;

    public GameObject Canvas;
    public GameObject Main;
    public GameObject Play;
    public GameObject PlayButton;
    public GameObject OptionsMenu;
    public GameObject OptionsButton;
    // public Transform Soundboard;
    public GameObject BlackScreen;
    // public GameObject StatsManager;
    // public GameObject MusicBotMenu;

    void Awake()
    {
        Canvas = GameObject.Find("Canvas");
        Main = GameObject.Find("MainMenu");
        Play = GameObject.Find("PlayMenu");
        PlayButton = GameObject.Find("PlayButton");
        OptionsMenu = GameObject.Find("OptionsMenu");
        OptionsButton = GameObject.Find("OptionsButton");
        // Soundboard = GameObject.Find("Soundboard/SFX").transform;
        BlackScreen = GameObject.Find("BlackScreen");
        // StatsManager = GameObject.Find("StatsManager");
        // MusicBotMenu = GameObject.Find("MusicBotMenu");
    }

    void Start()
    {
        if (OptionsMenu != null)
        {
            OptionsMenu.transform.SetParent(Canvas.transform);
            OptionsMenu.transform.localScale = new Vector3(0, 0, 0);
        }
        // if (MusicBotMenu != null)
        // {
        //     MusicBotMenu.transform.localScale = new Vector3(0, 0, 0);
        // }
        if (Play != null)
        {
            Play.transform.SetParent(Canvas.transform);
            Play.transform.localScale = new Vector3(0, 0, 0);
        }
        playCheck = false;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (play && time > 2)
        {
            playCheck = true;
            SceneManager.LoadScene("Loading");
        }

        if (mainMenu && time > 2)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // public void Play()
    // {
    //     if (playCheck == false)
    //     {
    //         // OptionsMenu.transform.SetParent(null);
    //         BlackScreen.GetComponent<GameController>().BlackoutFunction();
    //         // Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
    //         time = 0f;
    //         play = true;
    //     }
    // }

    public void Options()
    {
        // Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        OptionsMenu.transform.localScale = new Vector3(1, 1, 1);
        PlayButton.SetActive(false);
        OptionsButton.SetActive(false);
    }

    public void Back()
    {
        // Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        Play.transform.localScale = new Vector3(0, 0, 0);
        OptionsMenu.transform.localScale = new Vector3(0, 0, 0);
        PlayButton.SetActive(true);
        OptionsButton.SetActive(true);
    }

    public void MainMenu()
    {
        // StatsManager.GetComponent<StatsManager>().stoleStats = false;
        BlackScreen.GetComponent<GameController>().BlackoutFunction();
        // Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        time = 0f;
        mainMenu = true;
    }

    public void PlayMenu()
    {
        Play.transform.localScale = new Vector3(1, 1, 1);
        PlayButton.SetActive(false);
        OptionsButton.SetActive(false);
    }

    public void Multiplayer()
    {
        Play.transform.localScale = new Vector3(1, 1, 1);
        BlackScreen.GetComponent<GameController>().BlackoutFunction();
        time = 0f;
        play = true;
    }

    // public void MusicBot()
    // {
    //     Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
    //     MusicBotMenu.transform.localScale = new Vector3(1, 1, 1);
    // }

    public void Close()
    {
        // Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        // MusicBotMenu.transform.localScale = new Vector3(0, 0, 0);
    }
}