using GenshinMod._De_Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod
{
    class GenshinModGlobalNpc : GlobalNPC
    {
        public bool isAffectedElectro;
        public bool isAffectedDendro;
        public bool isAffectedHydro;

        public bool isAffectedPyro;
        private int pyroDotTimer = 0;

        public bool isAffectedCryo;

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModGlobalProjectile GProj = projectile.GetGlobalProjectile<ModGlobalProjectile>();
            GenshinModGlobalNpc GNpc = npc.GetGlobalNPC<GenshinModGlobalNpc>();

            if(GProj.isAnemo && isAffectedCryo || isAffectedDendro || isAffectedElectro || isAffectedHydro || isAffectedPyro)
            {
                npc.StrikeNPC(damage / 4, 0, hitDirection, false);
            }
            else if(GProj.isElectro)
            {
                if(isAffectedHydro)
                {
                    npc.AddBuff(ModContent.BuffType<ElectrifiedDebuff>(), 181);
                    isAffectedHydro = false;
                }

                if(isAffectedPyro)
                {
                    npc.StrikeNPC(damage / 2, 0, hitDirection, false);
                    isAffectedPyro = false;
                }

                if(isAffectedCryo)
                {
                    npc.StrikeNPC(damage / 2, 0, hitDirection, false);
                    isAffectedCryo = false;
                }
            }
            else if(GProj.isDendro)
            {
                if(isAffectedPyro)
                {
                    npc.AddBuff(ModContent.BuffType<ElectrifiedDebuff>(), 181);
                    isAffectedPyro = false;
                }
            }
            else if (GProj.isHydro)
            {
                if (isAffectedPyro)
                {
                    npc.StrikeNPC(damage / 2, 0, hitDirection, false);
                    isAffectedPyro = false;
                }

                if (isAffectedElectro)
                {
                    npc.AddBuff(ModContent.BuffType<ElectrifiedDebuff>(), 181);
                    isAffectedElectro = false;
                }
            }
            else if (GProj.isPyro)
            {

            }
            else if (GProj.isGeo)
            {

            }
            else if (GProj.isCryo)
            {

            }

        }

    }
}
