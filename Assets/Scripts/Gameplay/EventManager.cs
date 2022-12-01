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
    public List<int> pickedList = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < Eventlist.Length; i++)
        {
            pickedList.Add(i);
        }
        DisplayEventPage();
        if (!this.GetComponent<WaveManager>().isMasterClient())
        {
            clicheSpawn.SetActive(false);
        }
    }

    public void submitID(int id)
    {
        if (this.GetComponent<WaveManager>().wave < 3 && this.GetComponent<WaveManager>().isMasterClient()) lobby.SetActive(true);
        else
        {
            WaveManager.GM.GetComponent<WaveManager>().TownShop.SetActive(true);
        }
        clicheSpawn.SetActive(false);
        this.GetComponent<GameMaster>().AddToSpawnList(optionsID[id]);
        pickedList.Remove(optionsID[id]);
        this.GetComponent<WaveManager>().selection = true;
                //WaveManager.GM.GetComponent<WaveManager>().NextWave.SetActive(true);
    }

    public void DisplayEventPage()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                optionsID[i] = pickedList[Random.Range(1, pickedList.Count)];
            }
            else if (i == 1)
            {
                while (Eventlist.Length >= 3 && (optionsID[i] == 0 || optionsID[i] == optionsID[0]))
                {
                    optionsID[i] = pickedList[Random.Range(1, pickedList.Count)];
                }
            }
            else if (i == 2)
            {
                while (Eventlist.Length >= 3 && (optionsID[i] == 0 || optionsID[i] == optionsID[0] || optionsID[i] == optionsID[1]))
                {
                    optionsID[i] = pickedList[Random.Range(1, pickedList.Count)];
                }
            }

            Options[i].GetComponent<ListProperties>().Title.text = Eventlist[optionsID[i]].Name;
            Options[i].GetComponent<ListProperties>().Description.text = Eventlist[optionsID[i]].Description;
            Options[i].GetComponent<ListProperties>().Anim.GetComponent<Animator>().runtimeAnimatorController = Eventlist[optionsID[i]].Model.GetComponent<Animator>().runtimeAnimatorController;
        }

    }
}
