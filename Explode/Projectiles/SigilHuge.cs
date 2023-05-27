using System;
using Terraria;
using Terraria.ID;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using iLikeExplode.Explode.Tools;

namespace iLikeExplode.Explode.Projectiles {
    public class SigilHuge : ModProjectile {
        private Vector2 _scale;
        public override string Texture => "iLikeExplode/Explode/Textures/MainSigil";
        public override void SetStaticDefaults() {

        }
        public override void SetDefaults() {
            
            Projectile.width = 128;
            Projectile.height = 128;

            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        private enum AIState {
            ToPlayerCenter,
            ToBattlePos
        }
        private AIState CurrentAIState {
			get => (AIState)Projectile.ai[0];
			set => Projectile.ai[0] = (float)value;
		}
		public ref float _time => ref Projectile.ai[1];
        public ref float _deg => ref Projectile.ai[2];
        public override void AI() {

            _time++;

            Player player = Main.player[Main.myPlayer];
            
            Projectile.netImportant = true;
            Projectile.netUpdate = true;

            Projectile.position = player.Center - new Vector2(Projectile.width, Projectile.height) /2f + new Vector2(0, -150f);
            Projectile.rotation = MathHelper.ToRadians(_time);

            Projectile.timeLeft = 2;
        
        }
    }
}