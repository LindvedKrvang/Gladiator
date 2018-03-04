using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Mage")]
public class Mage : Character
{

    private readonly float ADJUSTED_Y = 0.2f;

    [Space]
    [Header("Mage Details")]
    public GameObject Frostbolt;
    
    public override void Attack(bool isDirectionRight, Vector2 position)
    {
        position.y += ADJUSTED_Y;
        var frostbolt = Instantiate(Frostbolt, position, Quaternion.identity);
        var fbController = frostbolt.GetComponent<ProjectileController>();
        fbController.SetDirection(isDirectionRight);
    }
}
