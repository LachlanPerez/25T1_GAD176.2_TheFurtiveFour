using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapon
{
    public class Fist : Weapon
    {
        public override void Hit()
        {
            Debug.Log("Fist punch!");
        }
    }
}
