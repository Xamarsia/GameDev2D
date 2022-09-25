using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    public float _speed = 4f;

    private float _horizontal;

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        moveCharacter1();
        moveCharacter2();
    }


    private void FixedUpdate()
    {
        moveCharacter3();
        moveCharacter4();
        moveCharacter5();
    }

    void moveCharacter1()
    {
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);
    }

    void moveCharacter2()
    {
        _rigidbody.AddForce(new Vector2(_horizontal, _rigidbody.velocity.y));
    }

    void moveCharacter3()
    {
        transform.position = new Vector2(transform.position.x + _horizontal * _speed * Time.deltaTime, transform.position.y);
    }

    void moveCharacter4()
    {
        transform.Translate(new Vector2(_horizontal, 0f) * _speed * Time.deltaTime);
    }

    void moveCharacter5()
    {
        _rigidbody.MovePosition(_rigidbody.position + new Vector2(_horizontal * _speed * Time.deltaTime, 0f));
    }
}
