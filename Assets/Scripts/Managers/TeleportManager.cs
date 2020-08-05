
using System.Collections;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{

    public Transform player;
    public float offsetToGround = 5f;
    public float freezGameSeconds = 2f;
    public float cameraTeleportAnimationSeconds = 1f;
    public int teleportManacost = 50;

    public void TeleportPlayerToPosisiton(Transform target, string tagName)
    {
        Vector3 pos = target.position;
        float offset = target.localScale.y / 2 + player.localScale.y / 2 + 0.1f;

        int newWorldRotation;


        if (tagName == "LeftWall")
        {
            pos.x += offset + offsetToGround;
            newWorldRotation = 270;
        }
        else if(tagName == "RightWall")
        { 
            pos.x -= offset + offsetToGround;
            newWorldRotation = 90;
        }
        else if(tagName == "Roof")
        { 
            pos.y -= offset + offsetToGround;
            newWorldRotation = 180;
        }
        else
        {
            pos.y += offset + offsetToGround;
            newWorldRotation = 0;
        }

       Managers.GameManager.FreezGame(freezGameSeconds, cameraTeleportAnimationSeconds);

       StartCoroutine(Freez(pos, newWorldRotation));

       Managers.ManaController.ConsumeMana(teleportManacost);
        
    }

    private IEnumerator Freez(Vector3 pos,int newWorldRotation)
    {
        yield return new WaitForSeconds(cameraTeleportAnimationSeconds);

        Managers.WorldRotation.ChangeRotation((WorldRotation.Rotation)newWorldRotation);
        player.position = pos;
    }

    public void TryTeleportToPoint(Vector3 point)
    {
        if (CanTeleport())
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(point);


            hits = Physics.RaycastAll(ray);
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("Block"))
                {
                    TeleportPlayerToPosisiton(hit.transform, hit.transform.parent.tag);
                    return;
                }
            }
        }
    }

    public bool CanTeleport()
    {
        if (Managers.ManaController.CanCastSpell(teleportManacost))
        {
            return true;
        }

        return false;
    }


}
