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
            InsideStaff,
            AboveHead
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
            float _dScale = 1f + (float)Math.Pow(Math.Cos(MathHelper.ToRadians(Timer)), 2) / 2f *
                            3f * (float)Math.Pow(Math.Sin(MathHelper.ToRadians(10f*Timer)), 2) / 2f;
 
            Vector2 _dInsideStaffPos = player.Center + new Vector2(0f, 0f).DirectionFrom(-_mouseToPlayer) * 35f + 
                new Vector2(0f, 0f).DirectionTo(_mouseToPlayer) * _length / new Vector2(Main.screenWidth/60f, Main.screenHeight/40f) + 
                new Vector2(0f, 5f);

            Vector2 _dAboveHeadPos = player.MountedCenter + new Vector2(0f,-100f) +
                new Vector2(0f, 0f).DirectionTo(_mouseToPlayer) * _length / new Vector2(Main.screenWidth/60f, Main.screenHeight/40f) +
                new Vector2(15f * (float)Math.Cos(MathHelper.ToRadians(Timer)), 15f * (float)Math.Sin(2f * MathHelper.ToRadians(Timer))/2f);

            if(player.mount._active)
                CurrentAIState = AIState.AboveHead;
                
            if(!player.mount._active)
                CurrentAIState = AIState.InsideStaff;

            // despawn fix:

            if(_length < 0.15f)
            _length = 0.15f;

            // setting core movement:

            switch (CurrentAIState) {

                case AIState.InsideStaff :

                    Dust _dInsideStaff = Dust.NewDustPerfect(Projectile.Center, DustID.PortalBolt, Vector2.Zero, 255, Visuals.MasterColor, 1f);
                    _dInsideStaff.fadeIn = 0.01f;
                    _dInsideStaff.noGravity = true;
                    _dInsideStaff.rotation = MathHelper.ToRadians(Timer);
                    _dInsideStaff.position = _dInsideStaffPos;
                    _dInsideStaff.velocity = new Vector2((float)Math.Sin(MathHelper.ToRadians(Timer)) + Main.WindForVisuals, -2f);
                    _dInsideStaff.scale = _dScale;

                break;

                case AIState.AboveHead :

                    Dust _dAboveHead = Dust.NewDustPerfect(Projectile.Center, DustID.PortalBolt, Vector2.Zero, 255, Visuals.MasterColor, 1f);
                    _dAboveHead.fadeIn = 0.01f;
                    _dAboveHead.noGravity = true;
                    _dAboveHead.rotation = MathHelper.ToRadians(Timer);
                    _dAboveHead.position = _dAboveHeadPos;
                    _dAboveHead.velocity = new Vector2((float)Math.Sin(MathHelper.ToRadians(Timer)) + Main.WindForVisuals, -2f);
                    _dAboveHead.scale = _dScale;

                break;

            }

            // core despawn:

            Projectile.timeLeft = 2;
            if(player.ownedProjectileCounts[ModContent.ProjectileType<StaffDrawing>()] < 1)
            Projectile.timeLeft = 0;

        }
    }
}