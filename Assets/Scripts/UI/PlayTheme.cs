using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTheme : MonoBehaviour
{

    public Transform Soundboard;

    private void Awake()
    {
        Soundboard = GameObject.Find("Soundboard/BGM").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        Soundboard.GetChild(1).GetComponent<AudioSource>().Stop();
        Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
