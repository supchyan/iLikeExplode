using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using iLikeExplode.Explode.Tools;
using iLikeExplode.Explode.Source;

namespace iLikeExplode.Explode.Projectiles {
    public class Sparkles : ModProjectile {
        private float _preparation;
        private float _coolDown;
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
        public override bool PreDraw(ref Color lightColor) {
            return false;
        }
        public override void AI() {

            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            
            _preparation--;

            // 1

            if(_preparation == 60f) {
                for (float i = 0f; i < 360f; i++) {
                    Dust dust = Dust.NewDustPerfect(StaffDrawing._staffCenter + new Vector2(10*StaffDrawing._localDir, -30f), DustID.PortalBolt, new Vector2(
                        2f * (float)Math.Pow(Math.Cos(MathHelper.ToRadians(2f*i)), 6f/2f), 
                        2f * (float)Math.Pow(Math.Sin(MathHelper.ToRadians(2f*i)), 6f/2f)
                    ), 255, Visuals.RainbowColor, 0.8f);
                    dust.noGravity = true;
                    dust.fadeIn = 0.01f;
                }
            }

            // 2

            if(_preparation == 57f) {
                for (float i = 0f; i < 360f; i++) {
                    Dust dust = Dust.NewDustPerfect(StaffDrawing._staffCenter + new Vector2(60*StaffDrawing._localDir, -40f), DustID.PortalBolt, new Vector2(
                        4f * (float)Math.Pow(Math.Cos(MathHelper.ToRadians(2f*i)), 6f/2f), 
                        4f * (float)Math.Pow(Math.Sin(MathHelper.ToRadians(2f*i)), 6f/2f)
                    ), 255, Visuals.RainbowColor, 0.8f);
                    dust.noGravity = true;
                    dust.fadeIn = 0.01f;
                }
            }

            // 3

            if(_preparation == 54f) {
                for (float i = 0f; i < 360f; i++) {
                    Dust dust = Dust.NewDustPerfect(StaffDrawing._staffCenter + new Vector2(70*StaffDrawing._localDir, 20f), DustID.PortalBolt, new Vector2(
                        2f * (float)Math.Pow(Math.Cos(MathHelper.ToRadians(2f*i)), 6f/2f), 
                        2f * (float)Math.Pow(Math.Sin(MathHelper.ToRadians(2f*i)), 6f/2f)
                    ), 255, Visuals.RainbowColor, 0.8f);
                    dust.noGravity = true;
                    dust.fadeIn = 0.01f;
                }
            }

            // 4

            if(_preparation == 40f) {
                for (float i = 0f; i < 360f; i++) {
                    Dust dust = Dust.NewDustPerfect(StaffDrawing._staffCenter + new Vector2(90*StaffDrawing._localDir, -15f), DustID.PortalBolt, new Vector2(
                        2f * (float)Math.Pow(Math.Cos(MathHelper.ToRadians(2f*i)), 6f/2f), 
                        2f * (float)Math.Pow(Math.Sin(MathHelper.ToRadians(2f*i)), 6f/2f)
                    ), 255, Visuals.RainbowColor, 0.8f);
                    dust.noGravity = true;
                    dust.fadeIn = 0.01f;
                }
            }

            // 5

            if(_preparation == 35f) {
                for (float i = 0f; i < 360f; i++) {
                    Dust dust = Dust.NewDustPerfect(StaffDrawing._staffCenter + new Vector2(5*StaffDrawing._localDir, 30f), DustID.PortalBolt, new Vector2(
                        (float)Math.Pow(Math.Cos(MathHelper.ToRadians(2f*i)), 6f/2f), 
                        (float)Math.Pow(Math.Sin(MathHelper.ToRadians(2f*i)), 6f/2f)
                    ), 255, Visuals.RainbowColor, 0.8f);
                    dust.noGravity = true;
                    dust.fadeIn = 0.01f;
                }
            }

            // 6

            if(_preparation == 30f) {
                for (float i = 0f; i < 360f; i++) {
                    Dust dust = Dust.NewDustPerfect(StaffDrawing._staffCenter + new Vector2(50*StaffDrawing._localDir, 40f), DustID.PortalBolt, new Vector2(
                        2f * (float)Math.Pow(Math.Cos(MathHelper.ToRadians(2f*i)), 6f/2f), 
                        2f * (float)Math.Pow(Math.Sin(MathHelper.ToRadians(2f*i)), 6f/2f)
                    ), 255, Visuals.RainbowColor, 0.8f);
                    dust.noGravity = true;
                    dust.fadeIn = 0.01f;
                }
            }

            // remove

            if(_preparation == 0f)
            Projectile.timeLeft = 0;

        }
        public override void OnSpawn(IEntitySource source) {
            _preparation = 61f;
        }
    }
}