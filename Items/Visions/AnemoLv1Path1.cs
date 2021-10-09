using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace GenshinMod.Items.Visions
{
    class AnemoLv1Path1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anemo Vision"); ;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var Modplayer = player.GetModPlayer<GenshinModPlayer>();

            Modplayer.hasVisionEquip = true;
            Modplayer.hasAnemo = true;
            Modplayer.hasPathOne = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<GenshinModPlayer>().hasVisionEquip)
                return false;

            return true;
        }
        
    }
}
