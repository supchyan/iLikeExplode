using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using iLikeExplode.Explode.Source;
using iLikeExplode.Explode.Projectiles;
using iLikeExplode.Explode.Tools;

namespace iLikeExplode.Explode.Source {
    public class StaffDrawing : ModProjectile {
        public static int _localDir;
        private int _toggle;
        private float _scaleLerp;
        public static Vector2 _staffPos;
        public static Vector2 _staffCenter;
        public static Vector2 _staffVel;
        public static float _staffRot;
        public static float _shootingFactor;
        public override string Texture => "iLikeExplode/Explode/Textures/Staff";
        private enum AIState {
            Idle,
            BattleReady
        }
        private AIState CurrentAIState {
			get => (AIState)Projectile.ai[0];
			set => Projectile.ai[0] = (float)value;
		}
		public ref float InfTimer => ref Projectile.ai[1];
        public ref float LerpCycle => ref Projectile.ai[2];
        public override void AI() {

            _staffPos = Projectile.position;
            _staffVel = Projectile.velocity;
            _staffCenter = Projectile.Center;
            _staffRot = Projectile.rotation;

            Player player = Main.player[Main.myPlayer];
            player.direction = _localDir;
            Projectile.spriteDirection = _localDir;
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

            // drawing scarlet core

            if(player.ownedProjectileCounts[ModContent.ProjectileType<ScarletCore>()] < 1) {
                Projectile.NewProjectile(Entity.GetSource_FromThis(), player.Center, Vector2.Zero,
                ModContent.ProjectileType<ScarletCore>(), 0, 0, player.whoAmI);
            }

            // setting staff's move set:

            Vector2 _offset = new Vector2(0f, 5f);
            Vector2 _mouseToPlayer = Main.MouseWorld - player.Center;
            float _length = _mouseToPlayer.Length();

            Projectile.position = player.Center - new Vector2(Projectile.width/2f, Projectile.height/2f) + _offset;

            if(player.mount._active)
                CurrentAIState = AIState.Idle;
                
            if(!player.mount._active)
                CurrentAIState = AIState.BattleReady;

            _shootingFactor+=0.1f;
            if(_shootingFactor > 1f)
            _shootingFactor = 1f;
            // if(Main.mouseLeft) { // this for future shooting code . . .
            //     _shootingFactor = 0.2f;
            // }

            switch (CurrentAIState) {

                case AIState.BattleReady :

                    player.heldProj = Projectile.whoAmI;

                    Projectile.scale = 1f;

                    // despawn fix:

                    if(_length < 0.01f)
                     _length = 0.01f;

                    // setting actual movement:

                    Projectile.velocity = Projectile.velocity.DirectionTo(_mouseToPlayer) * _length * _shootingFactor;
                    Projectile.velocity.X /= Main.screenWidth/60f;
                    Projectile.velocity.Y /= Main.screenHeight/40f;
                    if(_localDir == 1)
                    Projectile.rotation = MathHelper.ToRadians(45f) + _mouseToPlayer.ToRotation();
                    else
                    Projectile.rotation = MathHelper.ToRadians(45f+90f) + _mouseToPlayer.ToRotation();

                    _scaleLerp = 0f;                    

                break;

                case AIState.Idle :
                    
                    _scaleLerp += 3f;
                    if(_scaleLerp > 90f)
                    _scaleLerp = 90f;
                    
                    Projectile.scale = (float)Math.Pow(Math.Sin(MathHelper.ToRadians(_scaleLerp)), 5f);
                    Projectile.velocity = Projectile.velocity.DirectionTo(_mouseToPlayer) * _length +
                    new Vector2(2f * (float)Math.Cos(InfTimer), 5f * (float)Math.Sin(InfTimer));
                    Projectile.velocity.X /= -Main.screenWidth/100f;
                    Projectile.velocity.Y = 0;
                    if(_localDir == 1)
                    Projectile.rotation = MathHelper.ToRadians(135f) +
                        MathHelper.Lerp(MathHelper.ToRadians(-5f), MathHelper.ToRadians(5f), (float)Math.Pow(LerpCycle, 2f) /
                            (2f * ((float)Math.Pow(LerpCycle, 2f) - LerpCycle) + 1f));
                    else
                    Projectile.rotation = MathHelper.ToRadians(135f+90f) +
                        MathHelper.Lerp(MathHelper.ToRadians(-5f), MathHelper.ToRadians(5f), (float)Math.Pow(LerpCycle, 2f) /
                            (2f * ((float)Math.Pow(LerpCycle, 2f) - LerpCycle) + 1f));

                break;
                
            }

            // setting player's direction:

            if(
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) > -90f && 
                MathHelper.ToDegrees((Main.MouseWorld - player.Center).ToRotation()) < 90f)
            {
                _localDir = 1;
            }
            else {
                _localDir = -1;
            }

            // delete staff drawing if player doesn't holding it:

            Projectile.timeLeft = 2;
            if(player.HeldItem.ModItem is not Staff || player.dead) 
                Projectile.timeLeft = 0;
        }
    }
}