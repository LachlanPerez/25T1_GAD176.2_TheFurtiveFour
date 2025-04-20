using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;

public class EnemyDetection : MonoBehaviour
{
    public GameObject player;

    [SerializeField, Range(0,30)] public float turnSpeed;

    [SerializeField] private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = this.gameObject;
    }

    public float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    public virtual bool DetectPlayer()
    {
        Debug.Log("Enemy Detecting For Player");
        return false;
    }
    public virtual void DetectedPlayer()
    {
        Debug.Log("Player Detected!");
    }
}


