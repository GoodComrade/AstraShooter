using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public void Hit ()
    {
        Debug.Log("Hit!");
        Destroy(gameObject);
    }

    private void Update()
    {
        Destroy(gameObject, 4.0f);
    }
}
