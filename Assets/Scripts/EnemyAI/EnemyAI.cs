using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.EnemyAI
{
    public class EnemyAI : MonoBehaviour
    {
        public GameObject player;

        [SerializeField] private EnemyDetection enemyDetection;
        
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            enemyDetection = GetComponent<EnemyDetection>();

            if (player != null )
            {
                enemyDetection.player = player;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (enemyDetection != null && enemyDetection.DetectPlayer())
            {
                Debug.Log("PLAYER SPOTTED");
            }
        }
    }
}

