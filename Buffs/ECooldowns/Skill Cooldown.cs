using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Buffs.ECooldowns
{
    class ESkillCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Skill Cooldown");
            Description.SetDefault("You can not use your elemental skill");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GenshinModPlayer>().ESkillCooldown = true;
        }
    }

    class EBurstCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Burst Cooldown");
            Description.SetDefault("You can not use your elemental burst");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GenshinModPlayer>().EBurstCooldown = true;
        }
    }
}
