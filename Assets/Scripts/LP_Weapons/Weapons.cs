using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapons
{
    public class Weapons : MonoBehaviour
    {
        public virtual void Hit()
        {
            //using this as a hit function that will be override in the other subclasses 
        }

        public virtual void Shoot()
        {
            //this will be override the shoot function in pistol class 
        }

        public virtual void Reload()
        {
            //will be override in the pistol class 
        }
    }
}
