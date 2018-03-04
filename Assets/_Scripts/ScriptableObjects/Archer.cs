using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Characters/Archer")]
public class Archer : Character
{
    private readonly float ADJUSTED_Y = 0.1f;

    [Space]
    [Header("Archer Details")]
    public GameObject Arrow;

    public override void Attack(bool isDirectionRight, Vector2 position)
    {
        position.y -= ADJUSTED_Y;
        var arrow = Instantiate(Arrow, position, Quaternion.identity);
        var arrowController = arrow.GetComponent<ProjectileController>();
        arrowController.SetDirection(isDirectionRight);
    }
}
