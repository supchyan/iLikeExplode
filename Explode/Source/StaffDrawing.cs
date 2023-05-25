using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using iLikeExplode.Explode.Source;

namespace iLikeExplode.Explode.Projectiles {
    public class StaffDrawing : ModProjectile {
        private int _localDir;
        private int _toggle;
        private float _scaleLerp;
        public override string Texture => "iLikeExplode/Explode/Textures/Staff";
        private enum AIState {
            Idle,
            BattleReady
        }
        private AIState CurrentAIState {
			get => (AIState)Projectile.ai[0];
			set => Projectile.ai[0] = (float)value;
		}
		public ref float InfTimer => ref Projectile.ai[1]; // for some infinite rotations
        public ref float LerpCycle => ref Projectile.ai[2]; // for Poke
        public override void AI() {

            Player player = Main.player[Main.myPlayer];
            Projectile.netImportant = true;
            Projectile.netUpdate = true;

            Projectile.height = 96;
            Projectile.width = 96;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;

            // setting timers:

            InfTimer += 0.05f;
            
            switch(_toggle) {

                case 0 : 

                    LerpCycle += 0.01f;
                    if(LerpCycle >= 1f)
                        _toggle = 1;

                break;

                case 1 : 

                    LerpCycle -= 0.01f;
                    if(LerpCycle <= 0f)
                        _toggle = 0;

                break;
           
            }

            // setting staff's move set:

            Vector2 offset = new Vector2(0f, 5f);
            Vector2 mountBetween = Main.MouseWorld - player.MountedCenter; // fixes issue, when player's calling staff sitting on mount
            Vector2 mouseToPlayer = Main.MouseWorld - player.Center;
            Projectile.position = player.Center - new Vector2(Projectile.width/2f, Projectile.height/2f) + offset;

            if(player.mount._active)
                CurrentAIState = AIState.Idle;
                
            if(!player.mount._active)
                CurrentAIState = AIState.BattleReady;

            switch (CurrentAIState) {

                case AIState.BattleReady :

                    player.heldProj = Projectile.whoAmI;

                    Projectile.scale = 1f;
                    Projectile.spriteDirection = player.direction;
                    Projectile.velocity = Projectile.velocity.DirectionTo(mouseToPlayer)*(mouseToPlayer).Length();
                    Projectile.velocity.X /= Main.screenWidth/60f;
                    Projectile.velocity.Y /= Main.screenHeight/40f;
                    if(player.direction == 1)
                    Projectile.rotation = MathHelper.ToRadians(45f) + mouseToPlayer.ToRotation();
                    else
                    Projectile.rotation = MathHelper.ToRadians(45f+90f) + mouseToPlayer.ToRotation();

                    _scaleLerp = 0f;

                break;

                case AIState.Idle :
                    
                    _scaleLerp += 3f;
                    if(_scaleLerp > 90f)
                    _scaleLerp = 90f;
                    
                    Projectile.scale = (float)Math.Pow(Math.Sin(MathHelper.ToRadians(_scaleLerp)), 10f);
                    Projectile.velocity = new Vector2(0f, 0f).DirectionTo(mountBetween)*mountBetween.Length() +
                    new Vector2(2f * (float)Math.Cos(InfTimer), 5f * (float)Math.Sin(InfTimer));
                    Projectile.velocity.X /= -Main.screenWidth/100f;
                    Projectile.velocity.Y = 0;
                    Projectile.rotation = MathHelper.ToRadians(135f) +
                        MathHelper.Lerp(0f, MathHelper.ToRadians(10f), (float)Math.Pow(LerpCycle, 2f) /
                            (2f * ((float)Math.Pow(LerpCycle, 2f) - LerpCycle) + 1f));

                break;
                
            }

            // setting player's direction:

            if(
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) > -90f && 
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) < 90f)
            {
                player.direction = 1;
                _localDir = 1;
            }
            else {
                player.direction = -1;
                _localDir = -1;
            }

            // delete staff drawing if player doesn't holding it:

            Projectile.timeLeft = 2;
            if(player.HeldItem.ModItem is not Staff || player.dead) 
                Projectile.timeLeft = 0;
        }
    }
}