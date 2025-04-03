using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /// <summary>
    /// Mark
    /// </summary>
    [SerializeField] private int health;
    [SerializeField] private GameObject[] inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int Health 
    { 
        get { return health; } 
        set { health = value; }
    }
}
