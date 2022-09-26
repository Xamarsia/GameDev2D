using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    public float _speed = 4f;
    public float _jumpingPower = 6f;
    public float _groundCheckerRadius = 0.15f;

    private float _horizontal;
    private bool _isFacingRight = true;

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpingPower);
        }
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);

        Flip();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckerRadius);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckerRadius, _groundLayer);
    }
    private void Flip()
    {
        if (_horizontal < 0 && _isFacingRight || _horizontal > 0 && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }
}
