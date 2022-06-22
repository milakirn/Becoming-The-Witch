using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator _animator;
    private bool _isAttack;

    public bool IsAttack {get => _isAttack ; }
    private void Start() 
    {
        _animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _isAttack = true;
            _animator.SetTrigger("attack");
        }    
    }

    public void FinishAttack () 
    {
        _isAttack = false;    
    }

}
