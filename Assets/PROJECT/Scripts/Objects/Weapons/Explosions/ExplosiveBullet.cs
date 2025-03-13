using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Explosion
{
    private void OnTriggerEnter(Collider other)
    {
        ExplosionActivation(other);
    }
}
