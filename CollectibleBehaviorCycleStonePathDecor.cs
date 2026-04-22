using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace VSMineralMasonry;

public class CollectibleBehaviorCycleStonePathDecor : CollectibleBehavior
{
    public CollectibleBehaviorCycleStonePathDecor(CollectibleObject collObj) : base(collObj)
    {
    }

    public override void OnHeldInteractStart(
        ItemSlot slot,
        EntityAgent byEntity,
        BlockSelection blockSel,
        EntitySelection entitySel,
        bool firstEvent,
        ref EnumHandHandling handHandling,
        ref EnumHandling handling)
    {
        if (!firstEvent || blockSel == null)
        {
            return;
        }

        if (!DecorEditingHelper.HasEditableDecorNearSelection(byEntity.World, blockSel))
        {
            return;
        }

        if (collObj.Tool == EnumTool.Wrench)
        {
            handHandling = EnumHandHandling.Handled;
            handling = EnumHandling.PreventDefault;
            return;
        }

        if (collObj.Tool == EnumTool.Hammer)
        {
            handHandling = EnumHandHandling.Handled;
            handling = EnumHandling.PreventDefault;

            DecorEditingHelper.DecorTarget? target = DecorEditingHelper.GetSelectedDecor(byEntity.World, blockSel);
            if (byEntity.World.Side == EnumAppSide.Server && target?.Block is BlockStonePathDecorCycle)
            {
                BlockStonePathDecorCycle.AutoAlignDecor3x3(byEntity.World, target);
            }
        }
    }
}
