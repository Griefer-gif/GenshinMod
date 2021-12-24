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

namespace GenshinMod
{
    class GenshinModPlayer : ModPlayer
    {
        public bool hasVisionEquip;


        public bool hasAnemo;
        public bool hasGeo;
        public bool hasElectro;
        public bool hasDendro;
        public bool hasHydro;
        public bool hasPyro;
        public bool hasCryo;
        public bool hasChalk;

        public bool hasGeoCrystalShield;
        public int crystalShieldHP;
        public int crystalShieldMaxHP = 200;
        public float crystalShieldTimer = 0;
         
        public bool hasPathOne;
        public bool hasPathTwo;

        public override void ResetEffects()
        {
            hasVisionEquip = false;

            hasAnemo = false;

            hasGeo = false;

            hasElectro = false;
            hasDendro = false;
            hasHydro = false;
            hasPyro = false;
            hasCryo = false;
            hasChalk = false;

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
            if(GenshinMod.ElementalSkill1.JustPressed)
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
            }
        }

        private void crystalShieldDamage(int damage, bool crit)
        {
            Main.NewText($"HP:{crystalShieldHP}, TIMER: {crystalShieldTimer}");
            
        }
    }
}
