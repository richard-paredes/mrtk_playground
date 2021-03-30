using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    public GameObject Effect;

    [SerializeField]
    public GameObject AdditionalEffect;

    [SerializeField]
    public GameObject CastEffect;

    [SerializeField]
    public GameObject AdditionalCastEffect;
    [SerializeField]
    public float ManaCost = 50.0f;
    [SerializeField]
    public float Damage = 10.0f;
}
