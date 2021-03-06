﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [Header("General Info")]
    public int Damage;
    public AudioClip Sound;

    public abstract void InitializeAbility(float adjustedY);
    public abstract GameObject UseAbility(Vector2 position, bool facingRight);
}
