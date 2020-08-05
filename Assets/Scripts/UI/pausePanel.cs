using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausePanel : MonoBehaviour
{


    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        ActivatePausePanel();
    }

    public void Resume()
    {
        DeactivatePausePanel();
    }

    public void MainMenu()
    {
        DeactivatePausePanel();
        Managers.GameManager.RestartScene();
    }


    private void ActivatePausePanel()
    {
        Managers.GameManager.GamePause();
        gameObject.SetActive(true);
    }

    private void DeactivatePausePanel()
    {
        Managers.GameManager.GameResume();
        gameObject.SetActive(false);
    }
}
