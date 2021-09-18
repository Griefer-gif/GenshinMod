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

        public override void PreUpdateBuffs()
        {
            
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if(GenshinMod.ElementalSkill1.JustPressed && hasAnemo)
            {
                Main.NewText("pogggg");
                for(int  i = 0; i < 20; i++)
                    Dust.NewDust(Player.Center - new Vector2(0, 1), 10, 10, DustID.GreenFairy, Player.velocity.X, Player.velocity.Y, Scale: 1);
                Player.position.ToScreenPosition();
            }
            

        }
    }
}
