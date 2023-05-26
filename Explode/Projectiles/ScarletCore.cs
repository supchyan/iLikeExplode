using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using iLikeExplode.Explode.Tools;
using iLikeExplode.Explode.Source;

namespace iLikeExplode.Explode.Projectiles {
    public class ScarletCore : ModProjectile {
        public override string Texture => "iLikeExplode/Explode/Textures/ScarletCore";
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
        private enum AIState {
            Spawn,
            Despawn
        }
        private AIState CurrentAIState {
			get => (AIState)Projectile.ai[0];
			set => Projectile.ai[0] = (float)value;
		}
        public ref float Timer => ref Projectile.ai[1];
        public override void AI() {

            Timer++;

            Player player = Main.player[Main.myPlayer];
            Vector2 _mouseToPlayer = Main.MouseWorld - player.Center;
            float _length = _mouseToPlayer.Length();
            Vector2 _dustPos = player.Center + new Vector2(0,0).DirectionFrom(-_mouseToPlayer) * 35f + 
                    new Vector2(0f,0).DirectionTo(_mouseToPlayer) * _length / new Vector2(Main.screenWidth/60f, Main.screenHeight/40f) + 
                    new Vector2(0,5f);

            if(player.mount._active)
                CurrentAIState = AIState.Despawn;
                
            if(!player.mount._active)
                CurrentAIState = AIState.Spawn;

            // despawn fix:

            if(_length < 0.01f)
            _length = 0.01f;

            // setting core movement:

            switch (CurrentAIState) {

                case AIState.Spawn :

                    Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.PortalBolt, Vector2.Zero, 255, Color.Red, 1f);
                    dust.fadeIn = 0.01f;
                    dust.noGravity = true;
                    dust.rotation = MathHelper.ToRadians(Timer);
                    dust.position = _dustPos;
                    dust.velocity = new Vector2((float)Math.Sin(MathHelper.ToRadians(Timer)) + Main.WindForVisuals, -2f);

                break;

                case AIState.Despawn :

                    Projectile.timeLeft = 0;

                break;

            }

            // core despawn:

            Projectile.timeLeft = 2;
            if(player.ownedProjectileCounts[ModContent.ProjectileType<StaffDrawing>()] < 1)
            Projectile.timeLeft = 0;

        }
    }
}