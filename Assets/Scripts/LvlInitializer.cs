using Services;
using UnityEngine;

public class LvlInitializer : MonoBehaviour
{
    public void Awake()
    {
        var services = ServiceLocator.Current;
        
        services.TryRegister(new EventBus());
        services.TryRegister(new PlayerService());
        services.TryRegister(new AnimalService());
    }
}