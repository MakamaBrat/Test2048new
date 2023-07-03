using Test2048.Scripts.GamePlay;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField]private Transform spawnPoint;
    [SerializeField]private Transform spawnParent;

    public override void InstallBindings()
    {
       BindBlockMover();
       BindInputService();
       BindLifeCircle();
       BindBlockFactory();
       FactoryInitialyze();
       BindSliderMoveView();
    }

    private void BindBlockFactory()
    {
        Container
            .Bind<IBlockFactory>().To<BlockFactory>().AsSingle();
    }
    

    
    private void FactoryInitialyze()
    {
        var blockFactory = Container.Resolve<IBlockFactory>();
        blockFactory.Load(spawnPoint,spawnParent);
    }

    private void BindInputService()
    {
        Container
            .Bind<InputService>().FromComponentInHierarchy().AsSingle();
    }

    private void BindLifeCircle()
    {
        Container
            .Bind<LifeCircle>().FromComponentInHierarchy().AsSingle();
    }

    private void BindBlockMover()
    {
        Container
            .Bind<BlockMover>().FromComponentInHierarchy().AsSingle();
    }
    
    private void BindSliderMoveView()
    {
        Container
            .Bind<SliderMoveView>().FromComponentInHierarchy().AsSingle();
    }
}