using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    private Animator _animator;

    private void Start() 
    {
        _animator = GetComponent<Animator>();
    }

    public void ReduceHealth (float damage) 
    {
        _health -= damage;

        if (_health <= 0f)
        {
            Die();
            Debug.Log("Hero die");
        }
    }

    private void Die () 
    {
        gameObject.SetActive(false);
    }

}
