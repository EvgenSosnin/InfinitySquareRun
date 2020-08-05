using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotation : MonoBehaviour
{


    public  bool isRotating = false;

    public int manacost = 25;

    private Rotation rotation;
    private float rotationAngle;

    void Start()
    {
        rotation = Rotation.FLOOR;
        rotationAngle = 0f;
    }

    public enum Rotation
    {
        FLOOR = 0,RIGHT_WALL = 90 ,ROOF = 180,LEFT_WALL = 270
    }


    public void  rotationRotateRight()
    {
        if (rotation == Rotation.LEFT_WALL)
        {
            rotation = Rotation.FLOOR;
            rotationAngle = 0f;
        }
        else
        {
            rotation += 90;
            rotationAngle += 90f;
        }

        Managers.ManaController.ConsumeMana(manacost);

    }

    public void rotationRotateLeft()
    {
        if (rotation == Rotation.FLOOR)
        {
            rotation = Rotation.LEFT_WALL;
            rotationAngle  = 270f;
        }
        else
        {
            rotation -= 90;
            rotationAngle -= 90f;
        }

        Managers.ManaController.ConsumeMana(manacost);

    }

    public Rotation getRotation()
    {
        return rotation;
    }

    public void ChangeRotation(Rotation newRot)
    {
        if (Managers.ManaController.CanCastSpell(manacost))
        {
            Managers.ManaController.ConsumeMana(manacost);
            rotation = newRot;
            InputEvents.WORLD_ROTATE_EVENT.Invoke();
        }
    }

    public float getRotationAngle()
    {
        return rotationAngle;
    }

}
