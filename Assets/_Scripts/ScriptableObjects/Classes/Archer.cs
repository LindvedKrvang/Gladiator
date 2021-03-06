﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Characters/Archer")]
public class Archer : Character
{
    private readonly float ADJUSTED_Y = -0.1f;

    [Space] [Header("Archer Details")] public Ability NormalAbility;

    void OnEnable()
    {
        NormalAbility.InitializeAbility(ADJUSTED_Y);
    }

    public override GameObject Attack(bool isDirectionRight, Vector2 position)
    {
        return NormalAbility.UseAbility(position, isDirectionRight);
    }
}
