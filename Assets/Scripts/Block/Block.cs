using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{

    public Transform Begin;

    public Transform End;

    public float speed;
    void Start()
    {
        speed = Managers.BlockManager.blockSpeed;
    }
    void LateUpdate()
    {
        if (speed != 0)
        {
            Vector3 newPos = transform.position;
            newPos.z -= speed * Time.deltaTime;
            transform.position = newPos;
        }
    }
        

}
