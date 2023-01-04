using System;
using UnityEngine;

namespace Player.PlayerInput
{
    public class PlayerPcInput : MonoBehaviour
    {
        [SerializeField] private PlayerMover _playerMover;

        private float _direction;
        private float _verticalDirection;
        private bool _jump;
        private bool _crouch;

        private void Update()
        {
            
            _direction = Input.GetAxisRaw("Horizontal");
            _verticalDirection = Input.GetAxisRaw("Vertical");

            if (!_jump && Input.GetKeyUp(KeyCode.Space))
                _jump = true;
            _crouch = Input.GetKey(KeyCode.C);
        }

        private void FixedUpdate()
        {
            _playerMover.Move(_direction, _verticalDirection, _jump, _crouch);
            _jump = false;
        }
    }
}


