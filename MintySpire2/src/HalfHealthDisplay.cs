using System;
using HarmonyLib;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace MintySpire2.MintySpire2.src;

/// <summary>
///     Display half-health breakpoint next to HP for Terror Eel.
/// </summary>
[HarmonyPatch(typeof(NHealthBar), "RefreshText")]
public static class HalfHealthDisplay
{
    // Using private field captures to avoid reflection costs
    [HarmonyPostfix]
    private static void RenderHalfwayHP(NHealthBar __instance, Creature ____creature, MegaLabel ____hpLabel)
    {
        var c = ____creature;
        if (c.Monster is TerrorEel)
        {
            // Defined in ShriekPower that is applied to TerrorEel on creation
            var halfHP = Math.Floor(c.MaxHp / 2.0M);
            if (c.CurrentHp > halfHP)
                ____hpLabel.SetTextAutoSize($"{c.CurrentHp}/{c.MaxHp} ({halfHP})");
        }
    }
}