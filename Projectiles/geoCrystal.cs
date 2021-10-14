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
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isAnemo = true;
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

			
			for (int i = 0; i < Main.player.Length; i++)
            {
				Main.NewText(Main.player.Length);
				Player cPlay = Main.player[i];
				GenshinModPlayer PModP = Main.player[i].GetModPlayer<GenshinModPlayer>();
				if(cPlay.Distance(Projectile.Center) < 31)
                {
					if(!PModP.hasGeoCrystalShield)
                    {

						PModP.hasGeoCrystalShield = true;
						PModP.crystalShieldTimer = 300;
						if(PModP.crystalShieldHP < 100)
							PModP.crystalShieldHP += 25;
					}
					Projectile.active = false;
                }
            }
			
			base.AI();
        }
    }
}
