using GenshinMod.Buffs;
using GenshinMod.Projectiles;
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

        public bool isAffectedCryo;
        public bool isFrozen;
        private int frozenTimer = 0;
        public int frozenCooldown = 0;

        //private Projectile ElectrifiedProj;

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModGlobalProjectile GProj = projectile.GetGlobalProjectile<ModGlobalProjectile>();
            

            //elemental reactions for when a projectile hits an enemy
            #region elemental reactions
            if (GProj.isAnemo && (isAffectedCryo || isAffectedDendro || isAffectedElectro || isAffectedHydro || isAffectedPyro))
            {
                Main.NewText("isAnemo");
                //Any anemo projectile will just boost the damage with any element with a little extra knockback
                doEStrike(damage / 3, npc, knockback, hitDirection);
            }
            else if (GProj.isGeo && (isAffectedCryo | isAffectedElectro | isAffectedHydro | isAffectedPyro))
            {
                Main.NewText("isGeo");
                Projectile.NewProjectile(npc.GetProjectileSpawnSource(), npc.Center, Vector2.Zero, ModContent.ProjectileType<geoCrystal>(), 20, 0, 0);
                isAffectedCryo = false;
                isAffectedElectro = false;
                isAffectedHydro = false;
                isAffectedPyro = false;

                //Geo projectiles will create a mini shield that will absorb a small amount of damage(like 10 dmg or something) on screen and they will stand still till someone touches them
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
                else if (isAffectedCryo && frozenTimer == 0)
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
            int maxFrezzeTime = 240;
            int cooldownTime = maxFrezzeTime + 300;
            //ok so this is really confusing because im dumb and wrote dumb code, any npc unfrezzes when it reaches maxfrezzetime, but the timer keeps going till cooldowntime
            //during wich the npc cannot be frozen again
            if (frozenTimer >= maxFrezzeTime && frozenTimer <= cooldownTime)
            {
                frozenTimer++;
            }
            else if(frozenTimer >= cooldownTime)
                frozenTimer = 0;

            if (isFrozen)
            {
                //if it has been 4 sec unfrezze
                if (frozenTimer >= maxFrezzeTime)
                {
                    isFrozen = false;
                    //frozenTimer = 0;
                }

                //bosses get unfrozen 2x faster
                if (npc.boss)
                {
                    int vLim = 5;
                    frozenTimer += 2;
                    //ok so this limits the boss x and y velocity, theres probably a better way to do it but idk

                    if (npc.velocity.X > vLim)
                    {
                        npc.velocity.X = vLim;
                    }
                    else if (npc.velocity.X < -vLim)
                    {
                        npc.velocity.X = -vLim;
                    }
                    
                    if (npc.velocity.Y > vLim)
                    {
                        npc.velocity.Y = vLim;
                    }
                    else if (npc.velocity.Y < -vLim)
                    {
                        npc.velocity.Y = -vLim;
                    }
                }
                else
                {
                    frozenTimer++;
                    if (npc.noGravity == true)
                        npc.velocity = Vector2.Zero;
                    else
                        npc.velocity.X = 0;

                    //dont execute ai if its frozen && its not a boss
                    return false;
                }
            }

            return true;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (isFrozen)
            {
                drawColor = new Color(134, 214, 216);
            }
            base.DrawEffects(npc, ref drawColor);
        }

        private void doEStrike(int dmg, NPC target, float knockback, int hitDirection)
        {
            target.StrikeNPC(dmg + target.defense, knockback, hitDirection);
        }

    }
}
