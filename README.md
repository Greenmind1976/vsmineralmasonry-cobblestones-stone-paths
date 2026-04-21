# VSMineralMasonry - Cobblestones and Stone Paths

Standalone Vintage Story mod for the decorative pathing and cobblestone set from VSMineralMasonry.

## Included Content

- Burnished cobblestone decor
- Burnished flagstone paths
- Mossy flagstone paths
- Pitched cobblestone
- Pitching tool, which can also be used anywhere a normal chisel can be used
- Stone path placement and cycling helpers

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
