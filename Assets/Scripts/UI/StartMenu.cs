using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Animator animator;


    public void OnStart()
    {
        SceneManager.LoadScene("MainScene");
        
    }

    public void OnExit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void OnMenu()
    {
        animator.SetBool("isMenu", true);
    }


    public void OffMenu()
    {
       
        animator.SetBool("isMenu", false);
    }



}
