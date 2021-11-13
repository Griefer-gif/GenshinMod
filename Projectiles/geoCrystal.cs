
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Projectiles
{
    public class geoCrystal : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Geo Crystal");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			Projectile.width = 30; // The width of projectile hitbox
			Projectile.height = 30; // The height of projectile hitbox

			Projectile.penetrate = -1;
			Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
			Projectile.DamageType = DamageClass.NoScaling; // What type of damage does this projectile affect?
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the projectile
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
		}

        public override void AI()
        {
			const double amp = 20;
			const double freq = 0.07;
			Projectile.position.Y += (float)((Math.Cos(freq * Projectile.timeLeft) / 2) * amp * freq);

			
			for (int i = 0; i < Main.PlayerList.Count() - 1; i++)
			{ 
				//Main.NewText(Main.PlayerList.Count() - 1);
				Player cPlay = Main.player[i];
				GenshinModPlayer PModP = Main.player[i].GetModPlayer<GenshinModPlayer>();
				if (cPlay.Distance(Projectile.Center) < 31)
				{
					//Main.NewText("yes");
					PModP.hasGeoCrystalShield = true;
					PModP.crystalShieldTimer = 600;
					if (PModP.crystalShieldHP < PModP.crystalShieldMaxHP)
						PModP.crystalShieldHP += 50;

					Projectile.NewProjectile(cPlay.GetProjectileSource_Misc(cPlay.whoAmI), cPlay.Center, Vector2.Zero, ModContent.ProjectileType<AnimatedProjs.ShieldProj>(), 20, 0, cPlay.whoAmI);

					Main.NewText($"HP:{PModP.crystalShieldHP}, TIMER: {PModP.crystalShieldTimer}");
					Projectile.active = false;
                }
            }
			
			base.AI();
        }
    }
}
