

using UnityEngine;

namespace Test2048.Scripts.GamePlay
{
    public interface IBlockFactory
    {
      
        void CreateOnSpawnPoint(int type);
        void Create(int type,Vector3 position);
        void Load(Transform spawnPoint,Transform spawnParent);

        bool CheckForMovedAtLeastOneBlock();
        void DestroyBlock(Block block);
    }
}