using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpList : MonoBehaviour
{
    public Transform[] List;
    // Start is called before the first frame update
    void Start()
    {
        List = this.transform.GetComponentsInChildren<Transform>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
