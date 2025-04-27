using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrap  : MonoBehaviour
{
    [SerializeField] protected float fireInterval = 2f;
    protected float fireTimer;

    protected virtual void Start()
    {
        fireTimer = fireInterval;
    }

    protected virtual void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireInterval;
        }
    }

    
    protected abstract void Fire();
}
