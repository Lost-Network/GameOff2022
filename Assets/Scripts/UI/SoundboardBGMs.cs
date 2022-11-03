using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundboardBGMs : MonoBehaviour
{
    public GameObject BGM;
    public GameObject[] Sounds;

    public GameObject StatsManager;

    private static SoundboardBGMs playerInstance;

    Slider slider;

    public float BGMSliderValue;

    public GameObject Theme;

    void Awake()
    {
      BGM = GameObject.Find("Soundboard/BGM");
      StatsManager = GameObject.Find("StatsManager");
      Theme = GameObject.Find("Theme");

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
      for (int i = 0; i < BGM.transform.childCount; i++)
      {
        Sounds[i] = BGM.transform.GetChild(i).gameObject;
      }

      slider = GetComponent<Slider>();
      BGMSliderValue = StatsManager.GetComponent<StatsManager>().BGMSliderValue;
      if( slider != null && Theme != null )
        slider.onValueChanged.AddListener( value => Theme.GetComponent<VolumeValueChange>().SetVolume(BGMSliderValue)) ;
      else
        Debug.LogError("Either the slider is not attached to the GO or no OptionsSwitch in the scene" ) ;
    }

    // Update is called once per frame
    void Update()
    {

    }
}