using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudionComponent : MonoBehaviour
{
    private AudioSource audioSource; 
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<AudioSource>().Play();
    }



}
