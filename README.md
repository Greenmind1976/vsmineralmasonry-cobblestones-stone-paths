# VSMineralMasonry - Cobblestones and Stone Paths

`VSMineralMasonry - Cobblestones and Stone Paths` is a surface-detail mod built around outdoor finish work.

It focuses on ground texture, walkways, and low-profile decorative stone treatment so you can make roads, courtyards, gardens, ruins, and settlement spaces feel more intentional than plain packed dirt or raw stone.

## What It Adds

- Burnished cobblestone decor
- Burnished flagstone paths
- Mossy flagstone paths
- Pitched cobblestone
- Pitching tool, which can also be used anywhere a normal chisel can be used
- Stone path placement and cycling helpers

## Best Use Cases

- Laying roads, plazas, courtyards, and garden paths
- Mixing clean and mossy variants for age and wear
- Adding decorative stone surfaces without bulky full blocks
- Using the pitching tool for burnished stone work or as a regular chisel when needed
- Refining settlement spaces with faster placement and cycling tools

## Build

Set `VINTAGE_STORY` to your Vintage Story app folder, then build the project:

```bash
dotnet build VSMineralMasonry.CobblestonesStonePaths.csproj -c Release -p:NuGetAudit=false
```

## Release Package

Create a distributable zip with:

```bash
./release.sh
```

## Local Install

Install the built mod into your local Vintage Story app with:

```bash
./build-install.sh
```
