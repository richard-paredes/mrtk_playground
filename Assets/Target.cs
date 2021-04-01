using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Collision"))
        {
            gameObject.SetActive(false);
        }
    }
}
