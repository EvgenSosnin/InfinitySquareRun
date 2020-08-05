using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTeleport : MonoBehaviour, IDragHandler,IEndDragHandler
{
    private Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Moved )
            {
                transform.position = touch.position;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Managers.TeleportManager.TryTeleportToPoint(transform.position);
        transform.position = StartPosition;
    }

}
