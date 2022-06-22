using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;

    private Animator _animator;
    private float _health;

    private void Start() 
    {
        _animator = GetComponent<Animator>();
        _health = totalHealth;
    }

    private void Update() 
    {
        healthSlider.value = _health / totalHealth;    
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
