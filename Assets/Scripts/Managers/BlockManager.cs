
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockManager : MonoBehaviour
{

    public List<Block> blocks;

    public Block blockPref;

    public float dangerBlockChance = 0.1f;

    public float restoreManaBlockChance = 0.1f;

    public GameManager gameManager;

    public int maxBlocksInArray = 8;

    public int blockSpeed = 15;
    public int gameDificult = 1;
    public float Increment_dangerBlockChance = 0.003f;

    public int Health { get; private set; }

    [SerializeField] private GameObject restoreManaBlockPref;

    void Awake()
    {
        blocks = new List<Block>();
        Health = 1;
    }

    void Start()
    {
        
        Block firstBlock = Instantiate<Block>(blockPref, Vector3.zero, Quaternion.identity);
        blocks.Add(firstBlock);

    }


    public Block getLast()
    {
        return blocks[blocks.Count - 1];
    }

    public void FreezBlockVelocity(float freezTime)
    {
        StartCoroutine(FreezBlockVelocityCor(freezTime));
    }

    private IEnumerator FreezBlockVelocityCor(float freezTime)
    {
        int speed = blockSpeed;

        blockSpeed = 0;

        foreach (Block block in blocks)
        {
            block.speed = blockSpeed;
        }

        yield return new WaitForSeconds(freezTime);


        blockSpeed = speed;

        foreach (Block block in blocks)
        {
            block.speed = blockSpeed;
        }

        
    }


    void Update()
    {
        while (blocks.Count < maxBlocksInArray)
        {
            createBlock();
        }
    }

    public void createBlock()
    {

        Block lastBlock = getLast();

        Vector3 pos = lastBlock.End.position - lastBlock.Begin.localPosition ;

        Block newBlock = Instantiate<Block>(blockPref, pos, Quaternion.identity);

        blocks.Add(newBlock);

        InitializeNewBlock(newBlock);

        

        Managers.GameManager.GameDifficult += 1;
        dangerBlockChance += Increment_dangerBlockChance;
    }

    public void DestroyBlocksBehind(Block currentBlock)
    {
       while(blocks[0] != currentBlock)
        {
            Destroy(blocks[0].gameObject);
            blocks.RemoveAt(0);
        }
    }

    private void InitializeNewBlock(Block block)
    {
        int holeCount = 0;
        float difficult =(float) gameManager.GameDifficult;
       

        for (int i = 0; i < block.transform.childCount; i++)
        {

            Transform child = block.transform.GetChild(i);

            if (child.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {

                for (int j = 0; j < child.childCount; j++)
                {
                    float randomValue = Random.Range(0f, 1f);

                    if (difficult > holeCount && randomValue < dangerBlockChance)
                    {
                        holeCount++;
                        // Destroy(child.GetChild(j).gameObject);
                        child.GetChild(j).gameObject.AddComponent<dangerBlock_Y_movement>();
                    }
                    else if( Random.Range(0f, 1f) <= restoreManaBlockChance)
                    {
                        Vector3 manaPos = child.GetChild(j).transform.position + child.GetChild(j).transform.localScale/2 ;

                        if (child.CompareTag("Floor") || child.CompareTag("RightWall"))
                        {

                            manaPos.y += restoreManaBlockPref.transform.localScale.y *2f;
                        }
                        else
                        {
                            manaPos.y -= restoreManaBlockPref.transform.localScale.y *2f;
                        }

                        Instantiate(restoreManaBlockPref, manaPos, Quaternion.identity);
                    }
                }

            }
        }
    }
}
