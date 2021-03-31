using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Something collided with me! {other.gameObject.name}");
        if (other.gameObject.name.Contains("Collision"))
        {
            Destroy(this);
        }
    }
}
