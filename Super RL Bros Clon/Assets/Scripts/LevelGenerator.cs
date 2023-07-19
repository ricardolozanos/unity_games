using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {


    public static LevelGenerator sharedInstance;

    public LevelBlock firstBlock;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPoints;

    public List<LevelBlock> currentBlocks = new List<LevelBlock>();


    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start() 
    {
        GenerateInitialBlocks();
    }

    public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        LevelBlock currentBlock;
        Vector3 spawnPosition = Vector3.zero;

        if (currentBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(firstBlock);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = levelStartPoints.position;
        }
        else
        {
            currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
            currentBlock.transform.SetParent(this.transform, false);

           

            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        Vector3 correction = new Vector3(spawnPosition.x - currentBlock.startPoint.position.x,
                                        spawnPosition.y - currentBlock.startPoint.position.y,
                                        0);
        Debug.Log(correction);
        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);


    }

    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldestBlock = (LevelBlock) currentBlocks[0];
        currentBlocks.Remove(oldestBlock);


        Destroy(oldestBlock.gameObject);

    }

    public void RemoveAllTheBlocks()
    {
        while (currentBlocks.Count > 1)
        {
            RemoveOldestLevelBlock();
        }
    }

    public void GenerateInitialBlocks()
    {
        for(int i = 0; i < 3; i++)
        {
            AddLevelBlock();
        }

    }




}
