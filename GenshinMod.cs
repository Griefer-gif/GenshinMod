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
		public static ModKeybind ElementalSkill1;
		public static ModKeybind ElementalSkill2;
		public static ModKeybind Ultimate;

        public override void Load()
        {
            ElementalSkill1 = KeybindLoader.RegisterKeybind(this, "Elemental Skill", Microsoft.Xna.Framework.Input.Keys.F);
        }

        public override void Unload()
        {
            ElementalSkill1 = null;
            base.Unload();
        }
    }
}