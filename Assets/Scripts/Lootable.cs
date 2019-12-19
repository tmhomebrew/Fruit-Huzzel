using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lootable : MonoBehaviour
{
    private GameObject lootItemRef;
    public virtual GameObject LootItemRef { get => lootItemRef; set => lootItemRef = value; }

    [SerializeField]
    private int hitPoints;
    public virtual int HitPoints
    {
        get { return hitPoints; }
        set
        {
            hitPoints = value;
            if (hitPoints <= 0)
            {
                hitPoints = 0;
                IsAlive = false;
            }
            else
            {
                IsAlive = true;
            }
        }
    }

    [SerializeField]
    private bool isAlive;
    public virtual bool IsAlive
    {
        get { return isAlive; }
        private set
        {
            isAlive = value;
            if (isAlive == false)
            {
                DestroyLootable(this.gameObject);
            }
        }
    }

    private void DestroyLootable(GameObject GO)
    {
        //Destroy this
        Destroy(GO);
    }

    protected virtual void SetupVariables(int hitP, GameObject lootItem)
    {
        HitPoints = hitP;
        LootItemRef = lootItem;
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Ground")
        {
            HitPoints--;
            print("Im hit");
        }
    }
}
