using System;
using Models;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class FuelBarView : MonoBehaviour
    {
        private PlayerModel _player;
        private Slider _slider;

        private void Start()
        {
            var services = ServiceLocator.Current;
            _player = services.Get<PlayerService>().Player.GetComponent<PlayerModel>();
            _slider = transform.GetComponent<Slider>();
        }

        private void Update()
        {
            _slider.value = _player.Fuel / _player.MaxFuel;
        }
    }
}