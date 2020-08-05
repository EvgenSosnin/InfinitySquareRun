
using System.Collections;
using UnityEngine;

public class spawnBlock : MonoBehaviour
{

    public float delay = 1f;

    private BlockManager blockManager;

    private int maxTriggerEnter = 1;
    private int triggerEnterCount = 0;

    void Start()
    {
       blockManager = Managers.BlockManager;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(blockManager != null && other.CompareTag("Player") && triggerEnterCount < maxTriggerEnter)
        {
            triggerEnterCount++;
            StartCoroutine(SwapnNewBlock());
        }
           
    }

    private IEnumerator SwapnNewBlock()
    {
        yield return new WaitForSeconds(delay);

        blockManager.DestroyBlocksBehind(transform.parent.GetComponent<Block>());

        blockManager.createBlock();
    }
}
