using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TheFurtiveFour.EnemyAI
{
    public class EnemyAI : MonoBehaviour
    {
        public GameObject player;

        [SerializeField] private TextMeshProUGUI enemyNameText;
        [SerializeField] private EnemyDetection enemyDetection;
        
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            enemyDetection = transform.GetChild(0).GetComponent<EnemyDetection>();
            enemyNameText = GetComponentInChildren<TextMeshProUGUI>();
            enemyNameText.text = name;

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
                // do move
            }
            if (enemyNameText != null)
            {
                FacePlayer();
            }
        }
        private void FacePlayer()
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0;

            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            enemyNameText.transform.rotation = Quaternion.LookRotation(-directionToPlayer);

        }
    }
}

