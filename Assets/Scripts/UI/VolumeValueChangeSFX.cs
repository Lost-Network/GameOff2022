using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValueChangeSFX : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume;

    public GameObject StatsManager;
    void Awake()
    {
        StatsManager = GameObject.Find("StatsManager");
        musicVolume = StatsManager.GetComponent<StatsManager>().BGMSliderValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = StatsManager.GetComponent<StatsManager>().SFXSliderValue;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
