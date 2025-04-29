using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapon
{
    public class Weapon : MonoBehaviour
    {
        public virtual void Hit()
        {
            //using this as a hit function that will be override in the other subclasses 
        }
    }
}
