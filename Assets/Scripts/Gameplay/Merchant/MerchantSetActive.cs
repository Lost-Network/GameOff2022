using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSetActive : MonoBehaviour
{
    public GameObject MerchantDialogueBoxTrigger;

    public void ActiveMerchantDialogueBoxTrigger()
    {
        MerchantDialogueBoxTrigger.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
