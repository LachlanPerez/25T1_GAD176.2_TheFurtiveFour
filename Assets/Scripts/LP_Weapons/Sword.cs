using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapons
{
    public class Sword : Weapons
    {
        public override void Hit()
        {
            Debug.Log("Sword Slashes");
        }
    }
}
