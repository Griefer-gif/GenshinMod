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
using GenshinMod.Projectiles;

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
        public const int crystalShieldMaxHP = 100;
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

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if(hasGeoCrystalShield)
            {
                //make the player immune here somehow
                Player.immuneTime = Player.immuneTime = 30;
                crystalShieldHP -= damage;
                damage = 0;
            }
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        public override void PreUpdateBuffs()
        {
            if(crystalShieldTimer > 0)
                crystalShieldTimer -= 1;

            if (crystalShieldHP < crystalShieldMaxHP)
                crystalShieldHP = 0;
            else if (crystalShieldHP > crystalShieldMaxHP)
                    crystalShieldHP = 100;

            if (crystalShieldTimer == 0)
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
                        Projectile.NewProjectile(Player.GetProjectileSource_Misc(1), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AnemoLv1SkillProj>(), 20, 0, Player.whoAmI);
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
    }
}
