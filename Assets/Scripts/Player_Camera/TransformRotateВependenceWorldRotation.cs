using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotateВependenceWorldRotation : MonoBehaviour
{

    private float worldRotationAngle;

    private float minAngle;

    private float t = 0.0f;

    private bool rotationAngelChanged = false;


    void Awake()
    {

        InputEvents.WORLD_ROTATE_EVENT.AddListener(OnWorldRotate);

    }
    void OnDestroy()
    {

        InputEvents.WORLD_ROTATE_EVENT.RemoveListener(OnWorldRotate);
    }

    void Start()
    {
        worldRotationAngle = Managers.WorldRotation.getRotationAngle();
        minAngle = 0;
        rotationAngelChanged = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationAngelChanged)
        {
            RotationLerpTransform(transform, minAngle, worldRotationAngle, ref t);
            if (t >= 1f)
            {
                t = 0.0f;
                minAngle = worldRotationAngle;
                rotationAngelChanged = false;
                Managers.WorldRotation.isRotating = false;
            }

        }
    }

    public void OnWorldRotate()
    {

        worldRotationAngle =(float)Managers.WorldRotation.getRotation();
        rotationAngelChanged = true;
        Managers.WorldRotation.isRotating = true;


    }


    private void RotationLerpTransform(Transform target, float targetAngle, float finishAngel, ref float time)
    {
        float angle = Mathf.LerpAngle(targetAngle, finishAngel, time);


        target.eulerAngles = new Vector3(0f, 0f, angle);

        time += 0.5f * Time.deltaTime;

    }
}

