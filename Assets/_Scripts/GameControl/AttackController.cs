using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool _isAttack;

    public bool isAttack {get => _isAttack;}

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
            animator.SetTrigger("attack");
            Debug.Log("Attack");
        }    
    }

}
