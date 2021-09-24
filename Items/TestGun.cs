using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using GenshinMod;
using Terraria.DataStructures;
using GenshinMod.Projectiles;

namespace GenshinMod.Items
{
	class TestGunElectro : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Test gun Electro");
			Tooltip.SetDefault("Shoot shoot bang bang");
		}

		public override void SetDefaults()
		{
			Item.width = 62; // Hitbox width of the item.
			Item.height = 32; // Hitbox height of the item.
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 8; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 8; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
			Item.UseSound = SoundID.Item11; // The sound that this item plays when used.

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 20; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 16f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = AmmoID.Bullet;

		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ElectroArrow>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override void HoldItem(Player player)
		{
			//player.armorPenetration = 9999;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var quote = new TooltipLine(Mod, "", "'A true friend can penetrate any barrier.'");
			quote.overrideColor = Color.Red;
			tooltips.Add(quote);
		}
	}

	class TestGunCryo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Test gun Cryo");
			Tooltip.SetDefault("Shoot shoot bang bang");
		}

		public override void SetDefaults()
		{
			Item.width = 62; // Hitbox width of the item.
			Item.height = 32; // Hitbox height of the item.
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 8; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 8; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
			Item.UseSound = SoundID.Item11; // The sound that this item plays when used.

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 20; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 16f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = AmmoID.Bullet;

		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CryoArrow>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override void HoldItem(Player player)
		{
			//player.armorPenetration = 9999;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var quote = new TooltipLine(Mod, "", "'A true friend can penetrate any barrier.'");
			quote.overrideColor = Color.Red;
			tooltips.Add(quote);
		}


	}

	class TestGunPyro : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Test gun Pyro");
			Tooltip.SetDefault("Shoot shoot bang bang");
		}

		public override void SetDefaults()
		{
			Item.width = 62; // Hitbox width of the item.
			Item.height = 32; // Hitbox height of the item.
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 8; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 8; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
			Item.UseSound = SoundID.Item11; // The sound that this item plays when used.

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 20; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 16f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = AmmoID.Bullet;

		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PyroArrow>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override void HoldItem(Player player)
		{
			//player.armorPenetration = 9999;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var quote = new TooltipLine(Mod, "", "'A true friend can penetrate any barrier.'");
			quote.overrideColor = Color.Red;
			tooltips.Add(quote);
		}
	}


}