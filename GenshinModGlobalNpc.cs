using GenshinMod.DeBuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public override bool InstancePerEntity => true;
        public bool isAffectedElectro;
        public bool isAffectedDendro;
        public bool isAffectedHydro;

        public bool isAffectedPyro;
        private int pyroDotTimer = 0;

        public bool isAffectedCryo;
        public bool isFrozen;
        private int frozenTimer;

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModGlobalProjectile GProj = projectile.GetGlobalProjectile<ModGlobalProjectile>();
            GenshinModGlobalNpc GNpc = npc.GetGlobalNPC<GenshinModGlobalNpc>();

            //elemental reactions for when a projectile hits an enemy
            #region elemental reactions
            if (GProj.isAnemo && (isAffectedCryo || isAffectedDendro || isAffectedElectro || isAffectedHydro || isAffectedPyro))
            {
                Main.NewText("isAnemo");
                //Any anemo projectile will just boost the damage with any element with a little extra knockback
                doEStrike(damage / 3, npc, knockback, hitDirection);
            }
            else if(GProj.isElectro)
            {
                Main.NewText("isElectro");
                //electro projectile hits an enemy affected by... 
                if (isAffectedHydro)
                {
                    doEStrike(damage / 4, npc, knockback, hitDirection);
                    npc.AddBuff(ModContent.BuffType<ElectrifiedDebuff>(), 181);
                    isAffectedHydro = false;
                }
                else if(isAffectedPyro)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedPyro = false;
                }
                else if(isAffectedCryo)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedCryo = false;
                    Main.NewText("electro arrow with cryo!");
                }
                else
                {
                    isAffectedElectro = true;
                }
            }
            else if(GProj.isDendro)
            {
                Main.NewText("isDendro");
                //dendro projectile hits an enemy affected by 
                if (isAffectedPyro)
                {
                    npc.AddBuff(ModContent.BuffType<ElectrifiedDebuff>(), 181);
                    isAffectedPyro = false;
                }
            }
            else if (GProj.isHydro)
            {
                Main.NewText("isHydro");
                //Hydro projectile hits an enemy affected by...
                if (isAffectedPyro)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedPyro = false;
                }
                else if (isAffectedElectro)
                {
                    doEStrike(damage / 4, npc, knockback, hitDirection);
                    npc.AddBuff(ModContent.BuffType<ElectrifiedDebuff>(), 181);
                    isAffectedElectro = false;
                }
                else if (isAffectedCryo)
                {
                    isFrozen = true;
                }
                else
                    isAffectedHydro = true;
            }
            else if (GProj.isPyro)
            {
                Main.NewText("isPyro");
                //pyro projectile hits an enemy affected by...
                if (isAffectedHydro)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedHydro = false;
                }
                else if (isAffectedElectro)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedElectro = false;
                }
                else if (isAffectedCryo)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedCryo = false;
                }
                else
                    isAffectedPyro = true;
            }
            else if (GProj.isGeo)
            {
                Main.NewText("isGeo");
                //Geo projectiles will create a mini shield that will absorb a small amount of damage(like 10 dmg or something) on screen and they will stand still till someone touches them
            }
            else if (GProj.isCryo)
            {
                Main.NewText("isCryo");
                //Cryo projectile hits an enemy affected by...
                if (isAffectedHydro)
                {
                    isFrozen = true;
                    isAffectedHydro = false;
                    isAffectedCryo = true;
                }
                else if (isAffectedElectro)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedElectro = false;
                    Main.NewText("Cryo arrow with electro! Doing: " + ((damage / 2))  + "damage");
                }
                else if (isAffectedPyro)
                {
                    doEStrike(damage / 2, npc, knockback, hitDirection);
                    isAffectedPyro = false;
                }
                else
                    isAffectedCryo = true;
            }
            #endregion
        }

        public override bool PreAI(NPC npc)
        {
            if (isFrozen)
            {
                //bosses get unfrozen 2x faster
                if (npc.boss)
                    frozenTimer += 2;
                else
                    frozenTimer++;

                //if it has been 4 sec unfrezze
                if(frozenTimer >= 240)
                {
                    isFrozen = false;
                }

                //dont execute ai if its frozen
                return false;
            }

            return true;
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //need to figure out some way to draw a mask ice texture over the sprite
            
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }

        private void doEStrike(int dmg, NPC target, float knockback, int hitDirection)
        {
            target.StrikeNPC(dmg + target.defense, knockback, hitDirection);
        }

    }
}
