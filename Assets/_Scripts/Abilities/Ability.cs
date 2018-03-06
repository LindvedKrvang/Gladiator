using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{

    public float Damage;
    public AudioClip Sound;

    public abstract void UseAbility();
}
