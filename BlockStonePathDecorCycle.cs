using System;
using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace VSMineralMasonry;

public class BlockStonePathDecorCycle : Block
{
    private const int Rows = 3;
    private const int Columns = 3;
    private const int RowOrigin = 1;
    private const int ColumnOrigin = 1;

    public static bool TryCycleDecor(IWorldAccessor world, DecorEditingHelper.DecorTarget target)
    {
        if (target.Block is not BlockStonePathDecorCycle cycleBlock)
        {
            return false;
        }

        string currentTile = cycleBlock.LastCodePart(0) ?? "r1c1";
        string nextTile = NextTile(currentTile);
        Block? nextBlock = world.GetBlock(cycleBlock.CodeWithParts(nextTile));
        if (nextBlock == null || nextBlock.Id == 0 || nextBlock.Id == cycleBlock.Id)
        {
            return false;
        }

        bool placed = world.BlockAccessor.SetDecor(nextBlock, target.Position, target.DecorIndex);
        if (placed)
        {
            RemoveDuplicatePathDecors(world, target.Position, target.DecorIndex, cycleBlock);
        }

        return placed;
    }

    public static void AutoAlignDecor3x3(IWorldAccessor world, DecorEditingHelper.DecorTarget target)
    {
        if (target.Block is not BlockStonePathDecorCycle cycleBlock)
        {
            return;
        }

        BlockPos origin = target.Position;
        bool mirrorColumns = cycleBlock.Attributes?["autoAlignMirrorColumns"].AsBool(false) ?? false;

        Block? originDecor = world.BlockAccessor.GetDecor(origin, target.DecorIndex);
        if (!IsSameSet(cycleBlock, originDecor) &&
            !world.BlockAccessor.SetDecor(cycleBlock, origin, target.DecorIndex))
        {
            return;
        }

        RemoveDuplicatePathDecors(world, origin, target.DecorIndex, cycleBlock);

        for (int rowOffset = -RowOrigin; rowOffset < Rows - RowOrigin; rowOffset++)
        {
            for (int colOffset = -ColumnOrigin; colOffset < Columns - ColumnOrigin; colOffset++)
            {
                BlockPos targetPos = origin.AddCopy(colOffset, 0, rowOffset);
                Block? decor = world.BlockAccessor.GetDecor(targetPos, target.DecorIndex);
                if (!IsSameSet(cycleBlock, decor))
                {
                    continue;
                }

                int column = mirrorColumns
                    ? ColumnOrigin - colOffset + 1
                    : colOffset + ColumnOrigin + 1;
                string tile = $"r{rowOffset + RowOrigin + 1}c{column}";
                Block? mapped = world.GetBlock(decor.CodeWithParts(tile));
                if (mapped != null && mapped.Id != 0 && mapped.Id != decor.Id)
                {
                    world.BlockAccessor.SetDecor(mapped, targetPos, target.DecorIndex);
                }

                RemoveDuplicatePathDecors(world, targetPos, target.DecorIndex, cycleBlock);
            }
        }
    }

    private static bool IsSameSet(BlockStonePathDecorCycle own, Block? other)
    {
        if (other is not BlockStonePathDecorCycle)
        {
            return false;
        }

        AssetLocation? ownCode = own.Code;
        AssetLocation? otherCode = other.Code;
        if (ownCode == null || otherCode == null || ownCode.Domain != otherCode.Domain)
        {
            return false;
        }

        return BasePath(ownCode.Path) == BasePath(otherCode.Path);
    }

    private static void RemoveDuplicatePathDecors(
        IWorldAccessor world,
        BlockPos position,
        int decorIndex,
        BlockStonePathDecorCycle cycleBlock)
    {
        var subDecors = world.BlockAccessor.GetSubDecors(position);
        if (subDecors == null)
        {
            return;
        }

        int faceIndex = decorIndex % 6;
        Block air = world.GetBlock(0);
        List<int> duplicateIndexes = new();

        foreach (var entry in subDecors)
        {
            if (entry.Key != decorIndex && entry.Key % 6 == faceIndex && IsSameSet(cycleBlock, entry.Value))
            {
                duplicateIndexes.Add(entry.Key);
            }
        }

        foreach (int duplicateIndex in duplicateIndexes)
        {
            world.BlockAccessor.SetDecor(air, position, duplicateIndex);
        }
    }

    private static string BasePath(string path)
    {
        int lastDash = path.LastIndexOf('-');
        return lastDash >= 0 ? path[..lastDash] : path;
    }

    private static string NextTile(string currentTile)
    {
        int currentIndex = TileIndex(currentTile);
        int nextIndex = (currentIndex + 1) % (Rows * Columns);
        int row = (nextIndex / Columns) + 1;
        int column = (nextIndex % Columns) + 1;
        return $"r{row}c{column}";
    }

    private static int TileIndex(string tile)
    {
        if (tile.Length == 4 &&
            tile[0] == 'r' &&
            tile[2] == 'c' &&
            char.IsDigit(tile[1]) &&
            char.IsDigit(tile[3]))
        {
            int row = tile[1] - '0';
            int column = tile[3] - '0';
            if (row >= 1 && row <= Rows && column >= 1 && column <= Columns)
            {
                return ((row - 1) * Columns) + (column - 1);
            }
        }

        return 0;
    }
}
