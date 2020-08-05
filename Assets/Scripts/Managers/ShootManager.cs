using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShootManager : MonoBehaviour
{
    public int damage = 1;
    public int manacost = 5;

    public Image image;

    void Start()
    {
        image.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Managers.ManaController.CanCastSpell(manacost))
        {
            CheckShoot();
        }
       
    }    
    
    private void CheckShoot()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];
            dangerBlock_Y_movement dangerBlock = null;

            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (touch.phase != TouchPhase.Moved)
                {

                    RaycastHit[] hits;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    hits = Physics.RaycastAll(ray);

                    foreach (var hit in hits)
                    {
                        if (dangerBlock == null)
                        {
                            dangerBlock = hit.transform.GetComponent<dangerBlock_Y_movement>();
                        }
                    }


                    if (dangerBlock != null)
                    {
                        Managers.ManaController.ConsumeMana(manacost);

                        dangerBlock.Damaged(damage);
                        StartCoroutine(ShootVisualSoundEffects(touch.position, dangerBlock.gameObject));

                    }

                }
            }

        }
    }

    private IEnumerator ShootVisualSoundEffects(Vector3 pos,GameObject dangerBlock)
    {
        image.gameObject.SetActive(true);
        image.transform.position = pos;
        AudioSource audio = dangerBlock.GetComponentInParent<AudioSource>();
        if(audio != null)
        {
            audio.Play();
        }

        yield return new WaitForSeconds(0.25f);

        image.gameObject.SetActive(false);

    }

}
