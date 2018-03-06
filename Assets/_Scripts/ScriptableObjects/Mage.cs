using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Mage")]
public class Mage : Character
{

    private readonly float ADJUSTED_Y = 0.2f;


    //public GameObject Frostbolt;

    [Space]
    [Header("Mage Details")]
    public Ability NormalAbility;

    void OnEnable()
    {
        NormalAbility.InitializeAbility(ADJUSTED_Y);
    }
    
    public override void Attack(bool isDirectionRight, Vector2 position)
    {
        NormalAbility.UseAbility(position, isDirectionRight);
    }
}
