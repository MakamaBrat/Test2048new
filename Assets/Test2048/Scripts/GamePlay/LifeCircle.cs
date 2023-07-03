using System;
using System.Collections;
using System.Collections.Generic;
using Test2048.Scripts.GamePlay;
using UnityEngine;
using Zenject;

public class LifeCircle : MonoBehaviour
{
    [SerializeField] private GameObject buttonSpawn;
    private BlockMover _blockMover;
    private InputService _inputService;
    private IBlockFactory _blockFactory;
    
    [Inject]
    public void Construct(BlockMover blockMover, IBlockFactory blockFactory, InputService inputService)
    {
        _blockMover = blockMover;
        _blockFactory = blockFactory;
        _inputService = inputService;
    }

    private void Start()
    {
        
        
    }

    public void SpawnBlockButtonPointedDown()
    {
        clearSpaceForNewSpawn();
        _blockFactory.CreateOnSpawnPoint(2);
        buttonSpawn.SetActive(false);
    }

    public void MakeCheckForAtLeastOneMovedBlock()
    {
        StartCoroutine("checkForAtLeastOneMovedBlock");
    }

    private IEnumerator checkForAtLeastOneMovedBlock()
    {
        while (_blockFactory.CheckForMovedAtLeastOneBlock())
        {
            yield return new WaitForSeconds(1f);

        }
        buttonSpawn.SetActive(true);
    }

    private void clearSpaceForNewSpawn()
    {
        Collider[] colliders = Physics.OverlapBox(Camera.main.transform.position, new Vector3(3,1,1));

        foreach (Collider collider in colliders)
        {
            Block block = collider.GetComponent<Block>();
            if (block != null)
            {
                Destroy(block.gameObject);
            }
        }
    }
    
    
   
}
