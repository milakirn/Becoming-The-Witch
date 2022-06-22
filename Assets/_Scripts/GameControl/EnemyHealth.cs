using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    public void ReduceHealth (float damage) 
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
            Debug.Log("Enemy die");
        }
    }

    private void Die () 
    {
        gameObject.SetActive(false);
    }
}
