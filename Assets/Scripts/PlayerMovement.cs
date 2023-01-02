using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private int _maxHp;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _hpText;

    private int _currentHp;


    [Header("Movement")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Collider2D _headCollider;
    [SerializeField] private Transform _ceilingChecker;

    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpingPower = 6f;
    [SerializeField] private float _groundCheckerRadius = 0.15f;
    [SerializeField] private float _ceilCheckerRadius = 0.15f;

    [SerializeField] private float _direction;
    [SerializeField] private bool _isFacingRight = true;

    private float _startSpeed;

    public Animator _animator;

    private bool ctrlActive;
    private bool isDead;

    private void Start()
    {
        _startSpeed = _speed;
        _currentHp = _maxHp;
        _slider.maxValue = _maxHp;
        _slider.value = _currentHp;
        _hpText.text = _currentHp.ToString();
        ctrlActive = true;
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        _slider.value = _currentHp;
        _hpText.text = _currentHp.ToString();
        Debug.Log(_currentHp);
        //if (_currentHp <= 0 )
        //{
        //    Die();
        //    Debug.LogError("Player Dead");
        //}
    }

    //private void Die()
    //{
    //    isDead = true;
    //    _animator.SetBool("Death", isDead);
    //    ctrlActive = false;
    //    _headCollider.enabled = false;
        

    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        Jump();
        Walk();
        Flip();
        Crouch();
        Animate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Potion potion = collision.collider.GetComponent<Potion>();
        if (potion != null)
        {
            Debug.Log("Pick up Potion");
            
            _speed *= potion.UpgradeTime;
            Invoke(nameof(ResetSpeed), potion.UpgradePower);
            Destroy(potion.gameObject);
        }
    }
    private void ResetSpeed()
    {
        _speed = _startSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckerRadius);
        Gizmos.color= Color.black;
        Gizmos.DrawWireSphere(_ceilingChecker.position, _ceilCheckerRadius);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _groundLayer);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpingPower);
        }
    }

    private void Walk()
    {
        _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);
    }

    private void Flip()
    {
        if (_direction < 0 && _isFacingRight || _direction > 0 && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    private void Crouch()
    {
        bool ceilAbove = Physics2D.OverlapCircle(_ceilingChecker.position, _ceilCheckerRadius, _groundLayer);

        if (Input.GetKey(KeyCode.C))
        {
            _headCollider.enabled = false;
        }
        else if (!ceilAbove)
        {
            _headCollider.enabled = true;
        }
    }

    //private void foundScroll ()
    //{
    //    
    //}

    private void Animate()
    {
        if(ctrlActive)
        {
            _animator.SetBool("Run", _direction != 0);
            _animator.SetBool("Crouch", !_headCollider.enabled);
            _animator.SetBool("Jump", !IsGrounded());
        }
    }
}
