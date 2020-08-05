using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBlock : MonoBehaviour
{
    public int restoreMana =25;

    public float speed;

    void OnTriggerEnter(Collider other)
    {
        Managers.ManaController.RestoreMana(restoreMana);
        Destroy(gameObject);
    }

    void Start()
    {
        speed = Managers.BlockManager.blockSpeed;
    }
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.z -= speed * Time.deltaTime;
        transform.position = newPos;

        if(transform.position.z < Managers.GameManager.player.position.z)
        {
            Destroy(gameObject);
        }
    }
}
