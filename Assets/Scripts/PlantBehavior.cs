using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantBehavior : MonoBehaviour, IPlant
{
    #region Fields
    private LootTypes lootType;
    private PlantType plantKind;
    [SerializeField]
    private float curLifeTime = 0;

    [Header("Plant Value")]
    [SerializeField]
    private float pValue;
    [SerializeField]
    private float lifeTime;
    private float growSpeed;

    #endregion
    #region Properties
    public LootTypes LootTypeFromIPlant { get => lootType; private set => lootType = value; }
    public PlantType PlantKindFromIPlant
    {
        get => plantKind; 
        set
        {
            plantKind = value;
        }
    }
    public float PlantValue
    {
        get => pValue; 
        set
        {
            pValue = value;
        }
    }
    public float LifeTime { get => lifeTime; private set => lifeTime = value; }
    public float GrowSpeed { get => growSpeed; private set => growSpeed = value; }

    #endregion

    private void PlantValueCalc(PlantType pt)
    {
        switch (pt)
        {
            case PlantType.Weed:
                PlantValue = 0;
                LifeTime = 1;
                GrowSpeed = 0.001f;
                break;
            case PlantType.Grass:
                PlantValue = 1; 
                LifeTime = 3;
                GrowSpeed = 0.006f;
                break;
            case PlantType.Corn:
                PlantValue = 5;
                LifeTime = 6;
                GrowSpeed = 0.00095f;
                break;
            case PlantType.Wheat:
                PlantValue = 6;
                LifeTime = 8;
                GrowSpeed = 0.0005f;
                break;
            case PlantType.Hay:
                PlantValue = 4;
                LifeTime = 5f;
                GrowSpeed = 0.0008f;
                break;
            default:
                break;
        }
    }

    protected virtual void SetupPlantStats(PlantType typeOfPlant)
    {
        LootTypeFromIPlant = LootTypes.Plant;
        PlantKindFromIPlant = typeOfPlant;
        PlantValueCalc(PlantKindFromIPlant);   
    }

    protected virtual void Start()
    {
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 5 * Time.deltaTime, 0f), Space.Self);
        PlantGrowTime();
    }

    protected virtual void PlantGrowTime()
    {
        if (curLifeTime < 1)
        {
            curLifeTime += GrowSpeed;
            transform.localScale = new Vector3(
                transform.localScale.x,
                transform.localScale.y + (GrowSpeed),
                transform.localScale.z
            );
        }
    }   
}
