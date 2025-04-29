using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFurtiveFour.Weapons
{
    public class Fist : Weapons
    {
        public override void Hit()
        {
            Debug.Log("Fist punch!");
        }
    }
}
