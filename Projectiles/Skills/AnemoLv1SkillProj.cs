using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod.Projectiles.Skills
{
    class AnemoLv1SkillProj : ModProjectile
    {
		public bool SwirlElectro;
		public bool SwirlHydro;
		public bool SwirlPyro;
		public bool SwirlCryo;
		public bool SwirlDendro;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anemo Hurricane");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isAnemo = true;
			Projectile.width = 200; // The width of projectile hitbox
			Projectile.height = 200; // The height of projectile hitbox

			Projectile.penetrate = -1;
			Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
			Projectile.DamageType = DamageClass.NoScaling; // What type of damage does this projectile affect?
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the projectile
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 300; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			GenshinModGlobalNpc Gnpc = target.GetGlobalNPC<GenshinModGlobalNpc>();
			ModGlobalProjectile Gproj = Projectile.GetGlobalProjectile<ModGlobalProjectile>();
			//all debug stuff
			Main.NewText("-----------------------------------------------------------------------------");
			Main.NewText("isAffectedCryo: " + Gnpc.isAffectedCryo);
			Main.NewText("isAffectedPyro: " + Gnpc.isAffectedPyro);
			Main.NewText("isAffectedHydro: " + Gnpc.isAffectedHydro);
			Main.NewText("isAffectedElectro: " + Gnpc.isAffectedElectro);
			Main.NewText("-----------------------------------------------------------------------------");

			if(!SwirlCryo && !SwirlElectro && !SwirlHydro && !SwirlPyro)
            {
				if (Gnpc.isAffectedCryo)
					SwirlCryo = true;
				else if (Gnpc.isAffectedPyro)
					SwirlPyro = true;
				else if (Gnpc.isAffectedHydro)
					SwirlHydro = true;
				else if (Gnpc.isAffectedElectro)
					SwirlElectro = true;
				else if (Gnpc.isAffectedDendro)
					SwirlDendro = true;
			}
            else
            {
				if (SwirlCryo)
					Gproj.isCryo = true;
				else if (SwirlPyro)
					Gproj.isPyro = true;
				else if (SwirlHydro)
					Gproj.isHydro = true;
				else if (SwirlElectro)
					Gproj.isElectro = true;
				else if (SwirlDendro)
					Gproj.isDendro = true;
			}

			target.immune[Projectile.owner] = 30;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
		}

		public override void AI()
		{
			Projectile.velocity = Vector2.Zero;
			Vector2 nVel = Projectile.Center;
			nVel.Normalize();

			//pulls all close enemies 
			for (int i = 0; i < Main.npc.Count(); i++)
            {
				NPC npc = Main.npc[i];
				if(npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < 300f)
                {
					if(npc.boss)
                    {
						Vector2 direction = Projectile.Center - npc.Center;
						direction.Normalize();
						direction *= 0.5f;
						npc.velocity += direction;
					}
                    else
                    {
						Vector2 direction = Projectile.Center - npc.Center;
						direction.Normalize();
						direction *= 2f;
						npc.velocity += direction;
					}
                }
            }
		}
	}
}
