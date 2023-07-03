using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Test2048.Scripts.GamePlay
{
    public class BlockFactory:IBlockFactory
    {
        private readonly DiContainer _diContainer;
        private InputService _inputService;
        private SoundController _soundController;
        private Transform _spawnPoint;
        private Transform _spawnParent;
        Object blockPrefab;
        private List<Block> blocks;
        
        
        public void Load(Transform spawnPoint,Transform spawnParent)
        {
            blockPrefab = Resources.Load("Block");
            _spawnPoint = spawnPoint;
            _spawnParent = spawnParent;
        }

        public void DestroyBlock(Block block)
        {
            blocks.Remove(block);
        }

        public void CreateOnSpawnPoint(int type)
        {
            Block block=_diContainer
                .InstantiatePrefabForComponent<Block>(blockPrefab,_spawnPoint.position,Quaternion.identity,_spawnParent);
            _inputService.SetBlock(block);
            block.setType(type);
            blocks.Add(block);
            block.OnBlockDestroy += DestroyBlock;
            block.canCollision = false;
            block.OnCollisionSound += _soundController.PlayCollisionSound;
        }
        
        public void Create(int type, Vector3 pos)
        {
            Block block =
                _diContainer.InstantiatePrefabForComponent<Block>(blockPrefab, pos, Quaternion.identity, _spawnParent);
            block.setType(type);
            block.GetRigidbody().isKinematic = false;
            blocks.Add(block);
            block.OnBlockDestroy += DestroyBlock;
            block.OnCollisionSound += _soundController.PlayCollisionSound;
        }
        

        [Inject]
        public BlockFactory(DiContainer diContainer , InputService inputService , SoundController soundController)
        {
            _inputService = inputService;
            _diContainer = diContainer;
            _soundController = soundController;
            blocks = new List<Block>();
        }

        public bool CheckForMovedAtLeastOneBlock()
        {
            bool moved = false;
            foreach (Block block in blocks)
            {
                if (block.GetRigidbody().velocity.magnitude > 0.2f)
                {
                    moved = true;
                }
            }

            return moved;
        }
    }
}