using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    public float _speed = 4f;

    private float _horizontal;

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);
    }
}
