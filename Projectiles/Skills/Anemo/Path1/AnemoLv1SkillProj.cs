using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Projectiles.Skills.Anemo.Path1
{
    class AnemoLv1SkillProjPull : ModProjectile
    {
		public bool SwirlElectro;
		public bool SwirlHydro;
		public bool SwirlPyro;
		public bool SwirlCryo;
		public bool SwirlDendro;
		private int dmgTimer = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anemo Hurricane Pull");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isAnemo = true;
			Projectile.width = 50; // The width of projectile hitbox
			Projectile.height = 50; // The height of projectile hitbox

			Projectile.penetrate = -1;
			Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
			Projectile.DamageType = DamageClass.NoScaling; // What type of damage does this projectile affect?
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the projectile
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 300; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
		}

		public override void AI()
		{
			Projectile.velocity = Vector2.Zero;

			//adds to the timer, every 60 seconds spawns a proj that does damage
			dmgTimer++;

			if(dmgTimer % 60 == 0)
            {
				Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AnemoLv1SkillProjDmg>(), Projectile.damage, Projectile.knockBack, Owner: Projectile.owner, ai0: Projectile.whoAmI);
            }

			for (int i = 0; i < Main.npc.Length; i++)
            {
				// the npc instance
				NPC npc = Main.npc[i];

				//not exactly the best way to do it buuuuuuut it does what it needs to do
				//just sees if any enemy that is close enough is affected by any element, and swirls it
				
				if (npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < 200f && npc.lifeMax > 0)
                {
					GenshinModGlobalNpc Gnpc = npc.GetGlobalNPC<GenshinModGlobalNpc>();
					ModGlobalProjectile Gproj = Projectile.GetGlobalProjectile<ModGlobalProjectile>();
					// if it has not swirled any element, check if any enemy thats close is affect by an element, if yes, infuse the projectile with it
					if (!SwirlCryo && !SwirlElectro && !SwirlHydro && !SwirlPyro && !SwirlDendro)
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
				}
            }
		}
		private void DoDustEffect(Vector2 position, float distance, float minSpeed = 2f, float maxSpeed = 3f, object follow = null)
		{
			for (int i = 0; i < 100; i++)
			{
				float angle = Main.rand.NextFloat(-MathHelper.Pi, MathHelper.Pi);
				Vector2 vec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
				Vector2 vel = vec * Main.rand.NextFloat(minSpeed, maxSpeed);


				int dust = Dust.NewDust(position - vec * distance, 0, 0, DustID.GreenTorch);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].scale *= 2f;
				Main.dust[dust].velocity = vel;
				Main.dust[dust].customData = follow;
			}
		}

        public override void Kill(int timeLeft)
        {
			DoDustEffect(Projectile.Center, 10f, 0f, -10f);
			
        }
    }

	class AnemoLv1SkillProjDmg : ModProjectile
	{
		public bool SwirlElectro;
		public bool SwirlHydro;
		public bool SwirlPyro;
		public bool SwirlCryo;
		public bool SwirlDendro;
		

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anemo Hurricane Damage");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isAnemo = true;
			Projectile.width = 200; // The width of projectile hitbox
			Projectile.height = 200; // The height of projectile hitbox

			Projectile.penetrate = -1;
			Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
			Projectile.DamageType = DamageClass.Generic; // What type of damage does this projectile affect?
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.light = 1f; // How much light emit around the projectile
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 10; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
		}

		public override void AI()
        {
			Projectile sourceProj = Main.projectile[(int)Projectile.ai[0]];
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isCryo = sourceProj.GetGlobalProjectile<ModGlobalProjectile>().isCryo;
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isElectro = sourceProj.GetGlobalProjectile<ModGlobalProjectile>().isElectro;
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isHydro = sourceProj.GetGlobalProjectile<ModGlobalProjectile>().isHydro;
			Projectile.GetGlobalProjectile<ModGlobalProjectile>().isPyro = sourceProj.GetGlobalProjectile<ModGlobalProjectile>().isPyro;

			//does cool dust to show the area that the projecitle pulls
			DoDustEffect(Projectile.Center, 350f, maxSpeed: 20);
			for (int i = 0; i < Main.npc.Length; i++)
			{
				// the npc instance
				NPC npc = Main.npc[i];

				//pulls all enemies in

				if (npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < 350f)
				{
					if (npc.boss)
					{
						Vector2 direction = Projectile.Center - npc.Center;
						direction.Normalize();
						direction *= 1f;
						npc.velocity += direction;
					}
					else
					{
						Vector2 direction = Projectile.Center - npc.Center;
						direction.Normalize();
						direction *= 1f;
						npc.velocity += direction;
					}
				}
			}

			//stops the projectile from moving at all
			Projectile.velocity = Vector2.Zero;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Main.NewText("-------------------------------------------------------------------------------");
			Main.NewText(target.FullName);
			Main.NewText("isAffectedCryo: " + target.GetGlobalNPC<GenshinModGlobalNpc>().isAffectedCryo);
			Main.NewText("isAffectedPyro: " + target.GetGlobalNPC<GenshinModGlobalNpc>().isAffectedPyro);
			Main.NewText("isAffectedHydro: " + target.GetGlobalNPC<GenshinModGlobalNpc>().isAffectedHydro);
			Main.NewText("isAffectedElectro: " + target.GetGlobalNPC<GenshinModGlobalNpc>().isAffectedElectro);

			base.OnHitNPC(target, damage, knockback, crit);
        }

		private void DoDustEffect(Vector2 position, float distance, float minSpeed = 2f, float maxSpeed = 3f, object follow = null)
		{
			for (int i = 0; i < 50; i++)
			{
				float angle = Main.rand.NextFloat(-MathHelper.Pi, MathHelper.Pi);
				Vector2 vec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
				Vector2 vel = vec * Main.rand.NextFloat(minSpeed, maxSpeed);

			
				int dust = Dust.NewDust(position - vec * distance, 0, 0, DustID.GreenTorch);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].scale *= 2f;
				Main.dust[dust].velocity = vel;
				Main.dust[dust].customData = follow;
			}
		}
	}
}
