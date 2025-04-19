using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheFurtiveFour.EnemyAI;

public class EnemyNoiseDetection : EnemyDetection
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void DetectedPlayer()
    {
        base.DetectedPlayer();
        Debug.Log($"Player detected by");
    }
}


