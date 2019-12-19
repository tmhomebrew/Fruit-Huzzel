public interface IPlant
{
    LootTypes LootTypeFromIPlant { get; }
    PlantType PlantKindFromIPlant { get; set; }
    float PlantValue { get; set; }
    float LifeTime { get; }
    float GrowSpeed { get; }
}
