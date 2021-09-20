using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace GenshinMod
{
    class ModGlobalProjectile : GlobalProjectile
    {
        public bool isAnemo;
        public bool isGeo;
        public bool isElectro;
        public bool isDendro;
        public bool isHydro;
        public bool isPyro;
        public bool isCryo;
        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }
    }
}
