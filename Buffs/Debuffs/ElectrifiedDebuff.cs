using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Buffs
{
    class ElectrifiedDebuff : ModBuff
    {
        int electrifiedTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electrified");
            Description.SetDefault("Movement speed down and overtime damage");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            electrifiedTimer++;
            if (electrifiedTimer % 60 == 0)
            {
                npc.StrikeNPC(10, 0, 0);
                npc.velocity /= 2;
                
            }
        }
    }
}
