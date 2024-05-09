using Models;
using UnityEngine;

namespace Services
{
    public class PlayerService : IService
    {
        public GameObject Player { get; private set; }

        private PlayerModel _model;

        public PlayerService()
        {
            Player = GameObject.FindWithTag("Player");
            var services = ServiceLocator.Current;
        }

        public bool TryAddFuel(float amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning($"Нельзя уменьшить количество топлива. (Fuel:{_model.Fuel}/{_model.MaxFuel})");
                return false;
            }

            if (_model.Fuel + amount > _model.MaxFuel)
            {
                Debug.LogWarning($"Нельзя заполнить топлива больше максимума. (Fuel:{_model.Fuel}/{_model.MaxFuel})");
                return false;
            }
            _model.Fuel += amount;
            return true;
        } 
        
        public bool SetMaxFuel(float limit)
        {
            _model.MaxFuel += limit;
            return true;
        } 
    }
}