using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryMeARiver : MonoBehaviour
{
    public GameObject[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies == null)
        {
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        foreach (GameObject Enemy in Enemies)
        {
            Enemies.Add(Enemy);
        }
    }
}
