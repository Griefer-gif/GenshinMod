using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Buffs.SkillBuffs.Hydro.Path1
{
    class HydroPath1ESkillBuff : ModBuff
    { 

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydro elemental skill buff");
            Description.SetDefault("Your projectiles now heal allies");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GenshinModPlayer>().HydroPath1ESkillBuff = true;
        }
    }
}
