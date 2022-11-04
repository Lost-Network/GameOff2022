using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    bool pauseActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseActiveButton();
        }
    }

    public void pauseActiveButton()
    {
        if (pauseActive == false)
        {
            pauseActive = true;
            PauseUI.SetActive(true);
        }
        else
        {
            pauseActive = false;
            PauseUI.SetActive(false);
        }
    }
}
