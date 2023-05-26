using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletableObstacle : MonoBehaviour
{
   public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
