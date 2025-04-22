using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;

public class EnemyDetection : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject enemy;

    [SerializeField, Range(0,100)] public float turnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = this.gameObject;
    }

    // gets distance between this and player
    public float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    // default detect player method
    public virtual bool DetectPlayer()
    {
        Debug.Log("Enemy Detecting For Player");
        return false;
    }

    // default method when player is detected
    public virtual void DetectedPlayer()
    {
        Debug.Log("Player Detected!");
    }
}


