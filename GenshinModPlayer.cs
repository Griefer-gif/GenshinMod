using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ID;
using GenshinMod.Items.Visions;
using GenshinMod.Projectiles.Skills;
using GenshinMod.Buffs.ECooldowns;
using GenshinMod.Buffs.SkillBuffs;
using GenshinMod.Buffs.SkillBuffs.Hydro.Path1;
using GenshinMod.Projectiles.Skills.Anemo.Path1;

namespace GenshinMod
{
    class GenshinModPlayer : ModPlayer
    {
        public bool hasVisionEquip;

        public bool ESkillCooldown;
        public bool EBurstCooldown;

        public bool hasAnemo;
        public bool hasGeo;
        public bool hasElectro;
        public bool hasDendro;
        public bool hasHydro;
        public bool hasPyro;
        public bool hasCryo;
        public bool hasChalk;

        public bool HydroPath1ESkillBuff;

        public bool hasGeoCrystalShield;
        public int crystalShieldHP;
        public int crystalShieldMaxHP = 200;
        public float crystalShieldTimer = 0;
         
        public bool hasPathOne;
        public bool hasPathTwo;

        public override void ResetEffects()
        {
            hasVisionEquip = false;

            ESkillCooldown = false;
            EBurstCooldown = false;

            hasAnemo = false;

            hasGeo = false;

            hasElectro = false;
            hasDendro = false;
            hasHydro = false;
            hasPyro = false;
            hasCryo = false;
            hasChalk = false;

            HydroPath1ESkillBuff = false;

            hasPathOne = false;
            hasPathTwo = false;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            //Main.NewText($"Shield hp: {crystalShieldHP} damage: {damage}");
            if (hasGeoCrystalShield)
            {
                Main.NewText($"Shield hp: {crystalShieldHP} damage: {damage}");
                //make the player immune here somehow
                crystalShieldHP -= damage;
                damage = 0;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void PreUpdateBuffs()
        {
            if(crystalShieldTimer > 0)
                crystalShieldTimer -= 1;

            if (crystalShieldHP < 0)
                crystalShieldHP = 0;
            else if (crystalShieldHP > crystalShieldMaxHP)
                    crystalShieldHP = 100;

            if (crystalShieldTimer <= 0 || crystalShieldHP <= 0)
            {
                hasGeoCrystalShield = false;
                crystalShieldHP = 0;
            }
        }

        //keybinds and shi
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if(GenshinMod.ElementalSkill.JustPressed && !ESkillCooldown)
            {
                if(hasAnemo)
                {
                    if(hasPathOne)
                        Projectile.NewProjectile(Player.GetProjectileSource_Misc(1), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AnemoLv1SkillProjPull>(), 20, 0, Player.whoAmI);
                    if (hasPathTwo)
                        Main.NewText("how tf");  
                }

                if (hasGeo)
                {
                    if (hasPathOne)
                        //Projectile.NewProjectile(Player.GetProjectileSource_Misc(1), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AnemoLv1SkillProj>(), 20, 0, Player.whoAmI);
                    if (hasPathTwo)
                        Main.NewText("how tf");
                }

                if (hasElectro)
                {
                    if (hasPathOne)
                        //Projectile.NewProjectile(Player.GetProjectileSource_Misc(1), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AnemoLv1SkillProj>(), 20, 0, Player.whoAmI);
                    if (hasPathTwo)
                        Main.NewText("how tf");
                }
                if (hasDendro)
                {
                    if (hasPathOne)
                        //Projectile.NewProjectile(Player.GetProjectileSource_Misc(1), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AnemoLv1SkillProjPull>(), 20, 0, Player.whoAmI);
                    if (hasPathTwo)
                        Main.NewText("how tf");
                }
                if (hasHydro)
                {
                    if (hasPathOne)
                        Player.AddBuff(ModContent.BuffType<HydroPath1ESkillBuff>(), 600);   
                    if (hasPathTwo)
                        Main.NewText("how tf");
                }

                Player.AddBuff(ModContent.BuffType<ESkillCooldown>(), 600);
            }

            if (GenshinMod.ElementalBurst.JustPressed && !EBurstCooldown)
            {
                Player.AddBuff(ModContent.BuffType<EBurstCooldown>(), 600);
            }
        }
    }
}
