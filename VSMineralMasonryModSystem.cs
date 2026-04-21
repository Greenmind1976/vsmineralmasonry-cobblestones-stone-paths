using Vintagestory.API.Common;

namespace VSMineralMasonry;

public class VSMineralMasonryModSystem : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass("BlockCobblestoneCycle", typeof(BlockCobblestoneCycle));
        api.RegisterBlockClass("BlockCobblestoneCycle5x5", typeof(BlockCobblestoneCycle5x5));
        api.RegisterBlockClass("BlockStonePathDecorCycle", typeof(BlockStonePathDecorCycle));
        api.RegisterItemClass("ItemPlaceStonePath", typeof(ItemPlaceStonePath));
        api.RegisterCollectibleBehaviorClass("CycleStonePathDecor", typeof(CollectibleBehaviorCycleStonePathDecor));
    }
}
