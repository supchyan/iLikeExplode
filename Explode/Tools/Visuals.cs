using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace iLikeExplode.Explode.Tools {
    public class Visuals : ModSystem {
        private int i;
        private int r;
        private int g;
        private int b;
        public static Color RainbowColor;
        public override void PreUpdateTime() {
            
            switch(i) { // CODE FOR RAINBOW EFFECT
                case 0:
                r=255;
                b+=15;
                if(b == 255) {
                    i = 1;
                }
                break;
                case 1:
                r-=15;
                if(r == 0) {
                    i = 2;
                }
                break;
                case 2:
                g+=15;
                if(g == 255) {
                    i = 3;
                }
                break;
                case 3:
                b-=15;
                if(b == 0) {
                    i = 4;
                }
                break;
                case 4:
                r+=15;
                if(r == 255) {
                    i = 5;
                }
                break;
                case 5:
                g-=15;
                if(g == 0) {
                    i = 0;
                }
                break;
            }
        
            RainbowColor = new Color(r, g, b); // SETTING COLORS
        }
    }
}