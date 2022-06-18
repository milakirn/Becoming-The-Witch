using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Sprites")]
    [SerializeField] private Sprite _leftSide;
    [SerializeField] private Sprite _rightSide;

    private SpriteRenderer _spriteRenderer;

    [Header("Moving")]
    [SerializeField] private float _speedX;
    [SerializeField] private float _force;
    private float _horizontal = 0f;
    private const float k_speedMultiplier = 50f;

    private bool _isGround = false;
    private bool _isJump = false;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && _isGround)
        {
            _isJump = true;
        }

        if (_horizontal >= 0)
        {
            _spriteRenderer.sprite = _rightSide;
        }
        else if (_horizontal <= 0)
        {
            _spriteRenderer.sprite = _leftSide;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * _speedX * k_speedMultiplier * Time.fixedDeltaTime, _rb.velocity.y);
        if (_isJump)
        {
            _rb.AddForce(new Vector2(0f, _force));
            _isGround = false;
            _isJump = false;
            // Debug.Log("Jumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }
}