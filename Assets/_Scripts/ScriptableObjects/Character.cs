using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : ScriptableObject
{
    [Header("Character Details")]
    public RuntimeAnimatorController AnimatorController;
    public int Health = 100;
    public float WalkSpeed = 2f;
    public float RunSpeed = 5f;
    


    public abstract void Attack(bool isDirectionRight, Vector2 position);
}
