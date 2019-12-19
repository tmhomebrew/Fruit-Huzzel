using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LootBehavior : Lootable
{
    void Start()
    {
        //Lootable abstract class
        SetupVariables(2, this.gameObject);
    }

    protected override void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.name);
        base.OnCollisionEnter(col);
    }
}
