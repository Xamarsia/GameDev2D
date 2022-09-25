using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    public float _speed = 4f;

    private float _horizontal;

    private bool _isFacingRight = true;

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);

        Flip();
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
