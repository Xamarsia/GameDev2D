using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Player.PlayerAnimation;
using StatSystem;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private PlayerStatsController _playerStats;
        [SerializeField] private Rigidbody2D _rigidbody;
        //[Header("Stats")]
        //[SerializeField] private int _maxHp;
        //[SerializeField] private Slider _slider;
        //[SerializeField] private TMP_Text _hpText;

        //private int _currentHp;
        [Header("Jump")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private float _groundCheckerRadius = 0.15f;
        

        [Header("Crouch")]
        [SerializeField] private Collider2D _headCollider;
        [SerializeField] private Transform _ceilingChecker;
        [SerializeField] private float _ceilCheckerRadius = 0.15f;

        //[SerializeField] private float _speed = 4f;
        //[SerializeField] private float _jumpingPower = 6f;

        

        private bool _isFacingRight = true;

        //private float _startSpeed;


        //private void Start()
        //{
        //    _startSpeed = _speed;
        //    _currentHp = _maxHp;
        //    _slider.maxValue = _maxHp;
        //    _slider.value = _currentHp;
        //    _hpText.text = _currentHp.ToString();
        //}

        //public void TakeDamage(int damage)
        //{
        //    _currentHp -= damage;
        //    _slider.value = _currentHp;
        //    _hpText.text = _currentHp.ToString();

        //    if (_currentHp <= 0)
        //    {
        //        Die();
        //        Debug.LogError("Player Dead");
        //    }
        //}

        //private void Die()
        //{
        //    _rigidbody.bodyType = RigidbodyType2D.Static;
        //    _animator.SetTrigger("Death");
        //}

        //private void RestartLevel1()
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //    Start();
        //}

        //void Update()
        //{
        //    //_direction = Input.GetAxisRaw("Horizontal");

        //    //Jump();
        //    //Walk();
        //    //Flip();
        //    //Crouch();
        //    //Animate();
        //}

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Potion potion = collision.collider.GetComponent<Potion>();
        //    if (potion != null)
        //    {
        //        Debug.Log("Pick up Potion");

        //        _speed *= potion.UpgradeTime;
        //        Invoke(nameof(ResetSpeed), potion.UpgradePower);
        //        Destroy(potion.gameObject);
        //    }
        //}
        //private void ResetSpeed()
        //{
        //    _speed = _startSpeed;
        //}

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckerRadius);
        //    Gizmos.color= Color.black;
        //    Gizmos.DrawWireSphere(_ceilingChecker.position, _ceilCheckerRadius);
        //}

        //private bool IsGrounded()
        //{
        //    return Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _groundLayer);
        //}

        //private void Jump()
        //{
        //    if (Input.GetButtonDown("Jump") && IsGrounded())
        //    {
        //        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpingPower);
        //    }
        //}

        //private void Walk()
        //{
        //    _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);
        //}


        private void SetDirection(float direction)
        {
            if (direction < 0 && _isFacingRight || direction > 0 && !_isFacingRight)
            {
                _isFacingRight = !_isFacingRight;
                transform.Rotate(0, 180, 0);
            }
        }
        //private void Flip()
        //{
        //    if (_direction < 0 && _isFacingRight || _direction > 0 && !_isFacingRight)
        //    {
        //        _isFacingRight = !_isFacingRight;
        //        transform.Rotate(0, 180, 0);
        //    }
        //}

        public void Move(float direction, float verticalDirection, bool jump, bool crouch)
        {
            var grounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _groundLayer);
            bool ceilAbove = Physics2D.OverlapCircle(_ceilingChecker.position, _ceilCheckerRadius, _groundLayer);
            Vector2 velocity = _rigidbody.velocity;
            float speedX = direction * _playerStats.GetStatValue(StatType.MovementSpeed);

            SetDirection(direction);

            if (crouch)
            {
                _headCollider.enabled = false;
                speedX *= 0.5f;
            }
            else if (!ceilAbove)
            {
                _headCollider.enabled = true;
            }


            if (grounded && jump)
            {
                velocity = new Vector2(velocity.x, _playerStats.GetStatValue(StatType.JumpForce));
            }

            if (!grounded)
            {
                speedX = direction == 0 ? velocity.x : _playerStats.GetStatValue(StatType.MovementSpeed) * 0.8f * direction;
            }


            _rigidbody.velocity = new Vector2(speedX, velocity.y);

            _playerAnimator.PlayAnimation(AnimationType.CROUCH, !_headCollider.enabled);
            _playerAnimator.PlayAnimation(AnimationType.FLY_UP, !grounded && verticalDirection > 0);
            _playerAnimator.PlayAnimation(AnimationType.FLY_DOWN, !grounded && verticalDirection < 0);
            _playerAnimator.PlayAnimation(AnimationType.RUN, direction != 0 && grounded && _headCollider.enabled);
        }

        //private void Crouch()
        //{
        //    bool ceilAbove = Physics2D.OverlapCircle(_ceilingChecker.position, _ceilCheckerRadius, _groundLayer);

        //    if (Input.GetKey(KeyCode.C))
        //    {
        //        _headCollider.enabled = false;
        //    }
        //    else if (!ceilAbove)
        //    {
        //        _headCollider.enabled = true;
        //    }
        //}

        //private void foundScroll ()
        //{
        //    
        //}

        //private void Animate()
        //{
        //    _animator.SetBool("Run", _direction != 0);
        //    _animator.SetBool("Crouch", !_headCollider.enabled);
        //    _animator.SetBool("Jump", !IsGrounded());
        //}
    }
}

