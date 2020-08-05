using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class dangerBlock_Y_movement : MonoBehaviour
{
    public float speed = 0.5f;

    private float maxY = 0;
    private float minY = 0;

    private Vector3 pos;
    private Vector3 startPos;
    private float t = 0;

    private int health;

    private bool isAlive = true;

    void Start()
    {
        InitializeMaxMinY();
        health = Managers.BlockManager.Health;
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (isAlive)
        {
            MoveBlock();
        }
    }

    public void Damaged(int damage)
    {
        health -= damage;

        if (health == 0)
        {
            isAlive = false;
            BlockDeath();
        }
    }

    private void BlockDeath()
    {
        transform.localPosition = startPos;
    }

    private void MoveBlock()
    {
        pos.y = Mathf.Lerp(minY, maxY, t);

        t += speed * Time.deltaTime;

        if (t >= 1f)
        {

            float oldMax = maxY;
            maxY = minY;
            minY = oldMax;

            t = 0;
        }

        transform.localPosition = pos;
    }

    private void InitializeMaxMinY()
    {
        minY = transform.localPosition.y;

        if (transform.parent.CompareTag("Floor") || transform.parent.CompareTag("RightWall"))
        {

            maxY = transform.localPosition.y + transform.localScale.y;
        }
        else
        {
            maxY = transform.localPosition.y - transform.localScale.y;
        }

        pos = transform.localPosition;

    }
}
