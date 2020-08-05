
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class InputEvents : MonoBehaviour
{
    public static UnityEvent WORLD_ROTATE_EVENT = new UnityEvent();
    public static UnityEvent JUMP_EVENT = new UnityEvent();
    //public static UnityEvent TELEPORT_EVENT = new UnityEvent();


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JUMP_EVENT.Invoke();
        }


    }

    public void RotateRight()
    {
        if (!Managers.WorldRotation.isRotating && Managers.ManaController.CanCastSpell(Managers.WorldRotation.manacost))
        {
                Managers.WorldRotation.rotationRotateRight();
                WORLD_ROTATE_EVENT.Invoke();
        }
    }

    public void RotateLeft()
    {
        if (!Managers.WorldRotation.isRotating && Managers.ManaController.CanCastSpell(Managers.WorldRotation.manacost))
        {
            Managers.WorldRotation.rotationRotateLeft();
            WORLD_ROTATE_EVENT.Invoke();
        }
    }

}
