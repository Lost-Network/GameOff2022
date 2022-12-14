using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    private static StatsManager playerInstance;
    public bool stoleStats = false;

    public GameObject SoundBoard;
    public GameObject SFXSlider;
    public GameObject BGMSlider;

    public float volumeSFX = 0.5f;
    public float SFXSliderInitialValue = 0.5f;
    public float SFXSliderValue = 0.5f;
    public bool SFXSliderBool = false;

    public float volumeBGM = 0.5f;
    public float BGMSliderInitialValue = 0.5f;
    public float BGMSliderValue = 0.5f;
    public bool BGMSliderBool = false;

    void Awake()
    {
      DontDestroyOnLoad(transform.gameObject);

      if (playerInstance == null)
      {
        playerInstance = this;
      }
      else
      {
        Destroy(gameObject);
      }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (GameObject.Find("SoundBoard") != null)
      {
        SoundBoard = GameObject.Find("SoundBoard");
      }
      // Keeps track of SFX slider values
      if (GameObject.Find("SFXSlider") != null)
      {
        SFXSlider = GameObject.Find("SFXSlider");
        if (!SFXSliderBool) {
          SFXSliderInitialValue = SFXSlider.GetComponent<Slider>().value;
          SFXSliderBool = true;
        }
        if (SFXSlider.GetComponent<Slider>().value == 0.5f && SFXSliderValue != 0.5f) {
          SFXSlider.GetComponent<Slider>().value = SFXSliderValue;
        }
        SFXSliderValue = SFXSlider.GetComponent<Slider>().value;
      }

      // Keeps track of BGM slider values
      if (GameObject.Find("BGMSlider") != null)
      {
        BGMSlider = GameObject.Find("BGMSlider");
        if (!BGMSliderBool) {
          BGMSliderInitialValue = BGMSlider.GetComponent<Slider>().value;
          BGMSliderBool = true;
        }
        if (BGMSlider.GetComponent<Slider>().value == 0.5f && BGMSliderValue != 0.5f) {
          BGMSlider.GetComponent<Slider>().value = BGMSliderValue;
        }
        BGMSliderValue = BGMSlider.GetComponent<Slider>().value;
      }

    }
}
