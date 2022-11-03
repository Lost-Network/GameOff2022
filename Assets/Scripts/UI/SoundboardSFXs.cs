using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundboardSFXs : MonoBehaviour
{
    public GameObject SFX;
    public GameObject[] Sounds;

    public GameObject StatsManager;

    private static SoundboardSFXs playerInstance;

    Slider slider;

    public float SFXSliderValue;

    public GameObject Theme;

    void Awake()
    {
      SFX = GameObject.Find("Soundboard/SFX");
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
      for (int i = 0; i < SFX.transform.childCount; i++)
      {
        Sounds[i] = SFX.transform.GetChild(i).gameObject;
      }

      slider = GetComponent<Slider>();
      SFXSliderValue = StatsManager.GetComponent<StatsManager>().SFXSliderValue;
      if( slider != null && Theme != null )
        slider.onValueChanged.AddListener( value => Theme.GetComponent<VolumeValueChange>().SetVolume(SFXSliderValue)) ;
      else
        Debug.LogError("Either the slider is not attached to the GO or no OptionsSwitch in the scene" ) ;
    }

    // Update is called once per frame
    void Update()
    {

    }
}