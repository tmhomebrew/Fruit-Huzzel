using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LootBehavior))]
public class PlantComponent : PlantBehavior
{
    [Header("Plant Component - Set: Plant Type")]
    public PlantType myPType;

    private LootBehavior plantLootBehavior;
    //private IEnumerable<IPlant> plantSeeds;

    public PlantComponent(PlantType plant = PlantType.Weed)
    {
        myPType = plant;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        plantLootBehavior = GetComponent<LootBehavior>();

        base.Start();
        SetupPlantStats(myPType);
    }
}
