using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ServiceLocator _serviceLocator;

    public EntitySpawner _entitySpawner;

    private void Awake()
    {
        RegisterServices();
    }

    private void RegisterServices()
    {
        _serviceLocator = ServiceLocator.Instance;
        _serviceLocator.OnAllInitilized += OnAllInitialized;

        var services = GetComponentsInChildren<IService>();
        foreach(var service in services)
        {
            _serviceLocator.Add(service);
        }

        _serviceLocator.Register<MapLoaderService>(new MapLoaderService());

        _serviceLocator.InitializeAll();
    }

    private void OnAllInitialized()
    {
        _entitySpawner.Setup();
    }
}
