using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   private void OnParticleCollision(GameObject other) 
   {
       if (other.gameObject.name == "Right Laser" || other.gameObject.name == "Left Laser")
       {
            Destroy(this.gameObject);
       }
   }
}
