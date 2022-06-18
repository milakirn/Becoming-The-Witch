using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Net.Sockets;
using System.IO.IsolatedStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Patrol Distance")]
    [SerializeField] private float _walkDistance;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _chasingSpeed;
    [Tooltip("Time enemy wait before continue patrol")]
    [SerializeField] private float _timeToWait;
    [SerializeField] private float _timeToChase;
    [Tooltip("Minimal distance to start chasing")]
    [SerializeField] private float _minDistanceToPlayer;

    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Vector2 _leftBoundryPosition;
    private Vector2 _rightBoundryPosition;
    private Vector2 _nextPoint;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer;

    private float _walkSpeed;
    private float _waitTime;
    private float _chaseTime;


    public bool IsFacingRight
    {
        get => _isFacingRight;
    }



    private void Start() 
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();

        _leftBoundryPosition = transform.position;
        _rightBoundryPosition = _leftBoundryPosition + (Vector2.right * _walkDistance);

        _waitTime = _timeToWait;
        _chaseTime = _timeToChase;
        _walkSpeed = _patrolSpeed;
 }

    private void Update() 
    {
        if (_isChasingPlayer)
        {
            StartChasingTimer();
        }

        if (_isWait && !_isChasingPlayer)
        {
            StartWaitTimer();
        }

        if (ShouldWait())
        {
            _isWait = true;
        }


    }

    private void FixedUpdate() {

        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < _minDistanceToPlayer)
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

    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
        _chaseTime = _timeToChase;
        _walkSpeed = _chasingSpeed;;
    }
    private void Patrol() 
    {
        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }

        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();

        if (distance < 0)
        {
            _nextPoint.x *= -1;
        }

        if (distance > 0.2f && !_isFacingRight)
        {
            Flip();
        }
        else if (distance < 0.2f && IsFacingRight)
        {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);

    }

    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;
    }

    private void StartWaitTimer() 
    {
        _waitTime -= Time.deltaTime;
        
        if (_waitTime < 0f)
        {
            _waitTime = _timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;

        if (_chaseTime < 0f)
        {
            _isChasingPlayer = false;
            _chaseTime = _timeToChase;
            _walkSpeed = _patrolSpeed;
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
