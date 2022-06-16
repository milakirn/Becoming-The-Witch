using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Net.Sockets;
using System.IO.IsolatedStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _walkDistance;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _timeToWait;
    [SerializeField] private float _minDistanceToPlayer;

    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Vector2 _leftBoundryPosition;
    private Vector2 _rightBoundryPosition;
    private Vector2 nextPoint;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private float _waitTime;
    private bool _isChasingPlayer;


    public bool IsFacingRight
    {
        get => _isFacingRight;
    }


    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
    }

    private void Start() 
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();

        _leftBoundryPosition = transform.position;
        _rightBoundryPosition = _leftBoundryPosition + (Vector2.right * _walkDistance);

        _waitTime = _timeToWait;
 }

    private void Update() {

        if (_isWait && !_isChasingPlayer)
        {
            Wait();
        }

        if (ShouldWait())
        {
            _isWait = true;
        }


    }

    private void FixedUpdate() {

        nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (Mathf.Abs(DistanceToPlayer()) < _minDistanceToPlayer)
        {
            return;
        }

        if(_isChasingPlayer)
        {
            ChasePlayer();
        }

        if (!_isWait && !_isChasingPlayer)
        {
            Patrol();
        }
    }

    private void Patrol() 
    {
        if (!_isFacingRight)
        {
            nextPoint.x *= -1;
        }

        _rb.MovePosition((Vector2)transform.position + nextPoint);
    }

    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();

        if (distance < 0)
        {
            nextPoint.x *= -1;
        }

        if (distance > 0.2f && !_isFacingRight)
        {
            Flip();
        }
        else if (distance < 0.2f && IsFacingRight)
        {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + nextPoint);

    }

    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;
    }

    private void Wait() 
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime = _timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private bool ShouldWait()
    {
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBoundryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoundryPosition.x;

        return isOutOfRightBoundary || isOutOfLeftBoundary;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundryPosition, _rightBoundryPosition);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
