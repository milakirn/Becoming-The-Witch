using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    private AttackController _attackController;
    private bool _canAttack = true;
    private float _waitTime;
    private float _fullTime = 3f;

    private void Start() 
    {
        _attackController = transform.root.GetComponent<AttackController>();    
        _waitTime = _fullTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null && _attackController.IsAttack)
        {
            enemyHealth.ReduceHealth(damage);
            Debug.Log("Take damage");
        }    
    }

    // public void StartWaitTimer() 
    // {
    //     Debug.Log($"Start timer");
    //     _waitTime -= Time.fixedDeltaTime;
        
    //     if (_waitTime <= 0f)
    //     {
    //         Debug.Log($"End timer");
    //         _canAttack = true;
    //     }
    //     _waitTime = _fullTime;

    // }

}
