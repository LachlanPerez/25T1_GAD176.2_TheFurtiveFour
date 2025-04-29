using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapon
{
    public class Sword : Weapon
    {
        public override void Hit()
        {
            Debug.Log("Sword Slashes");
        }
    }
}
