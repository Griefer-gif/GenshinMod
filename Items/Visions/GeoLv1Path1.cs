using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GenshinMod.Items.Visions
{
    class GeoLv1Path1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geo Vision"); ;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.value = 0;
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var Modplayer = player.GetModPlayer<GenshinModPlayer>();

            Modplayer.hasVisionEquip = true;
            Modplayer.hasGeo = true;
            Modplayer.hasPathOne = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<GenshinModPlayer>().hasVisionEquip)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}
