using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Sprites")]

    private Animator _animator;

    [Header("Moving")]
    [SerializeField] private float speedX;
    [SerializeField] private float force;
    private float _horizontal = 0f;
    private const float k_speedMultiplier = 50f;

    private bool _isGround = false;
    private bool _isJump = false;

    private Rigidbody2D _rb;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && _isGround)
        {
            _isJump = true;
            _animator.SetBool("isJumping", true);

        }
#region old horizontal check
        // if (_horizontal > 0)
        // {
        //     _animator.SetFloat("Speed", _horizontal);
        //     _animator.SetBool("isRight", true);
        // }
        // else if (_horizontal < 0)
        // {
        //     _animator.SetFloat("Speed", _horizontal);
        //     _animator.SetBool("isRight", false);
        // }
        // else if (_horizontal == 0)
        // {
        //     _animator.SetFloat("Speed", _horizontal);
        //     _animator.SetBool("isRight", false);
            
        // }
#endregion
        switch (_horizontal)
        {
            case > 0:
                _animator.SetFloat("Speed", _horizontal);
                _animator.SetBool("isRight", true);
                break;
            case < 0:
                _animator.SetFloat("Speed", _horizontal);
                _animator.SetBool("isRight", false);
                break;
            default:
                _animator.SetFloat("Speed", _horizontal);
                break;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) speedX += 3;
        if (Input.GetKeyUp(KeyCode.LeftShift)) speedX -= 3;
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * k_speedMultiplier * Time.fixedDeltaTime, _rb.velocity.y);
        if (_isJump)
        {
            _animator.SetBool("isJumping", true);
            _rb.AddForce(new Vector2(0f, force));
            _isGround = false;
            _isJump = false;
            _animator.SetBool("isJumping", false);
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