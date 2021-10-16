using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Projectiles
{
    public class ShieldProj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Geo Shield");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			Projectile.width = 55; // The width of projectile hitbox
			Projectile.height = 55; // The height of projectile hitbox

			Projectile.penetrate = -1;
			Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
			//Projectile.DamageType = DamageClass.NoScaling; // What type of damage does this projectile affect?
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the projectile
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 10; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.scale = 1.2f;
		}

        public override void AI()
        {
			Projectile.timeLeft = 10;
			Projectile.Center = Main.player[Projectile.owner].Center;
			if(Main.player[Projectile.owner].GetModPlayer<GenshinModPlayer>().crystalShieldHP <= 0)
            {
				Projectile.active = false;
            }
			base.AI();
        }
    }
}
