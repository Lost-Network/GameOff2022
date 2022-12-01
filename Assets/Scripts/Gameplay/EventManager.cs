using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventManager : MonoBehaviour
{
    public ScriptableCliche[] Eventlist;
    public GameObject lobby;
    public GameObject clicheSpawn;
    public GameObject[] Options;
    public int[] optionsID;

    // Start is called before the first frame update
    void Start()
    {
        DisplayEventPage();
    }

    public void submitID(int id)
    {
        lobby.SetActive(true);
        clicheSpawn.SetActive(false);
    }

    public void DisplayEventPage()
    {


        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                optionsID[i] = Random.Range(1, Eventlist.Length);
            }
            else if (i == 1)
            {
                while (Eventlist.Length >= 3 && (optionsID[i] == 0 || optionsID[i] == optionsID[0]))
                {
                    optionsID[i] = Random.Range(1, Eventlist.Length);
                }
            }
            else if (i == 2)
            {
                while (Eventlist.Length >= 3 && (optionsID[i] == 0 || optionsID[i] == optionsID[0] || optionsID[i] == optionsID[1]))
                {
                    optionsID[i] = Random.Range(1, Eventlist.Length);
                }
            }

            Options[i].GetComponent<ListProperties>().Title.text = Eventlist[optionsID[i]].Name;
            Options[i].GetComponent<ListProperties>().Description.text = Eventlist[optionsID[i]].Description;
        }

    }
}
