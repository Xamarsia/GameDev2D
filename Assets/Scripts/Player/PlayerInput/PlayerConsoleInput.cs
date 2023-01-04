using UnityEngine;

namespace Player.PlayerInput
{
    public class PlayerConsoleInput : MonoBehaviour
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
            if (!_jump && Input.GetButtonUp("Jump"))
                _jump = true;
            _crouch = Input.GetAxisRaw("Vertical") < 0;
        }

        private void FixedUpdate()
        {
            _playerMover.Move(_direction, _verticalDirection, _jump, _crouch);
            _jump = false;
        }
    }
}
