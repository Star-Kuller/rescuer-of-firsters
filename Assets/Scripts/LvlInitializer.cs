using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;

public class LvlInitializer : MonoBehaviour
{
    public void Awake()
    {
        var services = ServiceLocator.Current;
        
        services.TryRegister(new EventBus());
        
    }
}