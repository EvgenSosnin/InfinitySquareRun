using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    public Transform targetPos;

    public Vector3 offset;

    public float smooth = 0.125f;

    public float incrementFieldOfView = 50f;


    private bool isfieldOfViewIncrement = false;
    private bool isfieldOfViewDecrement = false;

    private Camera camera;

    private float fieldOfView;

    private Vector3 mainOffset;

    void Start()
    {
        camera = GetComponent<Camera>();
        fieldOfView = camera.fieldOfView;

        mainOffset = offset;
    }
    void Update()
    {
        if (isfieldOfViewIncrement && !isfieldOfViewDecrement)
        {
            camera.fieldOfView += incrementFieldOfView * Time.deltaTime;

            offset.z -= 5f * Time.deltaTime;
        }
        else if (isfieldOfViewDecrement)
        {
            camera.fieldOfView = fieldOfView;

            camera.fieldOfView -= incrementFieldOfView * Time.deltaTime;

            offset.z += 5f * Time.deltaTime;
        }
        else
        {
            camera.fieldOfView = fieldOfView;

            offset = mainOffset;
        }
    }
    void LateUpdate()
    {

        Vector3 finishPos = targetPos.position + OffsetWorldRotationDependence(offset);
        Vector3 smoothPos = Vector3.Lerp(transform.position, finishPos, smooth * Time.deltaTime);

        transform.position = smoothPos;

    }

    public void TeleportAnimation(float cameraTeleportAnimationSeconds)
    {
        StartCoroutine(CameraAnimation(cameraTeleportAnimationSeconds));
    }

    private IEnumerator CameraAnimation(float cameraTeleportAnimationSeconds)
    {

        isfieldOfViewIncrement = true;


        yield return new WaitForSeconds(cameraTeleportAnimationSeconds);


        isfieldOfViewDecrement = true;

        yield return new WaitForSeconds(0.2f);

        isfieldOfViewIncrement = false;
        isfieldOfViewDecrement = false;

    }

    private Vector3 OffsetWorldRotationDependence(Vector3 offset)
    {
        Vector3 newOffset = offset;
        WorldRotation.Rotation rot = Managers.WorldRotation.getRotation();

        switch (rot)
        {
            case WorldRotation.Rotation.FLOOR:

                newOffset = offset;
                break;

            case WorldRotation.Rotation.RIGHT_WALL:

                newOffset = new Vector3(-offset.y, offset.x, offset.z);
                break;

            case WorldRotation.Rotation.LEFT_WALL:

                newOffset = new Vector3(offset.y, -offset.x, offset.z);
                break;

            case WorldRotation.Rotation.ROOF:

                newOffset = new Vector3(offset.x, -offset.y, offset.z);
                break;

        }

        return newOffset;
    }


}
