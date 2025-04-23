using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public float takedownDistance = 2f;
    public float rotationSpeed = 5f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, enemy.position);

        if (distance <= takedownDistance)
        {
            Vector3 direction = (player.position - enemy.position).normalized;
            float angle = Vector3.Angle(enemy.forward, direction);

            if (angle > 10f)
            {
                // Rotate enemy towards player
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                enemy.rotation = Quaternion.Slerp(enemy.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Enemy is facing player, do takedown
                Debug.Log("Takedown!");
            }
        }
    }
}
