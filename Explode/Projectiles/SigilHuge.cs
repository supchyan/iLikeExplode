using System;
using Terraria;
using Terraria.ID;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using iLikeExplode.Explode.Tools;

namespace iLikeExplode.Explode.Projectiles {
    public class SigilHuge : ModProjectile {
        private int _switch;
        private Vector2 _scale;
        private float _rad;

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
		public ref float _lerp => ref Projectile.ai[1];
        public ref float _deg => ref Projectile.ai[2];
        public override void AI() {
            
            if((_deg >= 0 && _deg < 90) || (_deg >= 180 && _deg < 270))
            _lerp++;
            else
            _lerp--;

            _deg++;
            if(_deg>=360)
            _deg = 0;

            _rad = MathHelper.ToRadians(_deg);

            Player player = Main.player[Main.myPlayer];
            
            Projectile.netImportant = true;
            Projectile.netUpdate = true;

            Projectile.timeLeft = 2;

            Projectile.position = player.Center - new Vector2(Projectile.width, Projectile.height) / 2f;

            Projectile.rotation = _rad;
        
        }
        public override bool PreDraw(ref Color lightColor) {

            Player player = Main.player[Main.myPlayer];

			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Rectangle _rect = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 _drawPosition = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + 
                Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Vector2 _drawOrigin = new Vector2(texture.Width, texture.Height) / 2f;
            
            _scale = new Vector2(1f, 0.5f);
            Main.EntitySpriteDraw(texture, _drawPosition + new Vector2(0, -100f), null, Color.White, Projectile.rotation, _drawOrigin, _scale, SpriteEffects.None, 0);

            return false;

        }
    }
}