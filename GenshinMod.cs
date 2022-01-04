using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace GenshinMod
{
	public class GenshinMod : Mod
	{
		public static ModKeybind ElementalBurst;
		public static ModKeybind ElementalSkill;

        public override void Load()
        {
            ElementalBurst = KeybindLoader.RegisterKeybind(this, "Elemental Burst", Microsoft.Xna.Framework.Input.Keys.Q);
            ElementalSkill = KeybindLoader.RegisterKeybind(this, "Ultimate Skill", Microsoft.Xna.Framework.Input.Keys.F);
        }

        public override void Unload()
        {
            ElementalBurst = null;
            ElementalSkill = null;
            base.Unload();
        }
    }
}