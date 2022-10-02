using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Collider2D _headCollider;
    [SerializeField] private Transform _ceilingChecker;

    public float _speed = 4f;
    public float _jumpingPower = 6f;
    public float _groundCheckerRadius = 0.15f;
    public float _ceilCheckerRadius = 0.15f;

    private float _direction;
    private bool _isFacingRight = true;

    public Animator _animator;

    void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        Jump();
        Walk();
        Flip();
        Crouch();
        Animate();
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

    private void Animate()
    {
        _animator.SetBool("Run", _direction != 0);
        _animator.SetBool("Crouch", !_headCollider.enabled);
        _animator.SetBool("Jump", !IsGrounded());
    }
}
