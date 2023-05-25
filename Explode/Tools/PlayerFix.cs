using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using iLikeExplode.Explode;
using iLikeExplode.Explode.Source;
using iLikeExplode.Explode.Projectiles;

namespace iLikeExplode.Explode.Tools {
    public class PlayerFix : ModPlayer {
        private float rot;
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo) {

            if(Main.LocalPlayer.HeldItem.ModItem is not Staff || Main.LocalPlayer.mount._active)
            return;
            // player's head rotation
            Main.LocalPlayer.headRotation = rot;
            if(
                MathHelper.ToDegrees((Main.MouseWorld - Main.LocalPlayer.Center).ToRotation()) > -90 && 
                MathHelper.ToDegrees((Main.MouseWorld - Main.LocalPlayer.Center).ToRotation()) < 90)
            {
                rot = (Main.MouseWorld - Main.LocalPlayer.Center).ToRotation();    
            }
            else {
                rot = (Main.MouseWorld - Main.LocalPlayer.Center).ToRotation() - (float)Math.PI;
            }
		}
    }
}