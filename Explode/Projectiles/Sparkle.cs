using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using iLikeExplode.Explode.Tools;

namespace iLikeExplode.Explode.Projectiles {
    public class Sparkle : ModProjectile {
        public override string Texture => "iLikeExplode/Explode/Textures/Dot";
        public override void SetStaticDefaults() {

        }
        public override void SetDefaults() {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override void AI() {

        }
        public override void OnSpawn(IEntitySource source) {
            for (float i = 0; i < 360; i+=1) {
                Dust dust = Dust.NewDustPerfect(Main.MouseWorld, DustID.PortalBolt, new Vector2(
                    4*(float)Math.Pow(Math.Cos(MathHelper.ToRadians(2*i)), 6f/2f), 
                    4*(float)Math.Pow(Math.Sin(MathHelper.ToRadians(2*i)), 6f/2f)
                ), 255, Visuals.RainbowColor, 0.8f);
                dust.noGravity = true;
                dust.fadeIn = 0.01f;
            }
        }
    }
}