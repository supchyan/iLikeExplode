using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using iLikeExplode.Explode;
using iLikeExplode.Explode.Tools;
using iLikeExplode.Explode.Projectiles;

namespace iLikeExplode.Explode.Source {
    public class Staff : ModItem {
        public override void SetStaticDefaults() {

        }
        public override string Texture => "iLikeExplode/Explode/Textures/Staff";
        public override void SetDefaults() {

            Item.width = 48;
            Item.height = 48;

            Item.noMelee = true;
            Item.autoReuse = true;
            
            Item.damage = 1;
            Item.DamageType = DamageClass.Magic;
            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
        }
        public override bool CanUseItem(Player player) {
            return false;
        }

        public override void HoldItem(Player player) {

            // hands drawing fix

            if(player.mount._active) {
                Item.holdStyle = ItemHoldStyleID.None;
                return;
            }

            if(
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) > -135 && 
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) < -45)
            {
                Item.holdStyle = ItemHoldStyleID.HoldUp;
            }
            else if(
                (MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) > -45 && 
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) < 20) ||
                (MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) < -135 && 
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) > -179) ||
                (MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) < 179 && 
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) > 160))
            {
                Item.holdStyle = ItemHoldStyleID.HoldHeavy;
            }
            else if(
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) > 20 && 
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) < 160)
            {
                Item.holdStyle = ItemHoldStyleID.HoldRadio;
            }
            
            // drawing a staff sprite
            if(player.ownedProjectileCounts[ModContent.ProjectileType<StaffDrawing>()] < 1) {
                Projectile.NewProjectile(Entity.GetSource_FromThis(), player.Center, Vector2.Zero,
                ModContent.ProjectileType<StaffDrawing>(), 0, 0, player.whoAmI);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            return false;
        }
    }
}