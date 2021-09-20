using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.DeBuffs
{
    class PyroBurn : ModBuff
    {
        int burnTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berning!");
            Description.SetDefault("damage over time");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            burnTimer++;
            if (burnTimer % 30 == 0)
            {
                npc.StrikeNPC(10, 0, 0);
                npc.velocity /= 2;

            }
        }
    }
}
