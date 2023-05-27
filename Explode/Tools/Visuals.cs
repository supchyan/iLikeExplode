using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace iLikeExplode.Explode.Tools {
    public class Visuals : ModSystem {

        // rainbow color

        private int rbT;
        private int rbR;
        private int rbG;
        private int rbB;


        // master color

        private int scT;
        private int scR;
        private int scG;
        private int scB;

        // rain intensity

        private float intensity;
        private float transition;

        public static Color RainbowColor;
        public static Color MasterColor;

        public override void PreUpdateTime() {
            
            switch(rbT) { // RAINBOW EFFECT
                case 0:
                rbR=255;
                rbB+=15;
                if(rbB == 255) {
                    rbT = 1;
                }
                break;
                case 1:
                rbR-=15;
                if(rbR == 0) {
                    rbT = 2;
                }
                break;
                case 2:
                rbG+=15;
                if(rbG == 255) {
                    rbT = 3;
                }
                break;
                case 3:
                rbB-=15;
                if(rbB == 0) {
                    rbT = 4;
                }
                break;
                case 4:
                rbR+=15;
                if(rbR == 255) {
                    rbT = 5;
                }
                break;
                case 5:
                rbG-=15;
                if(rbG == 0) {
                    rbT = 0;
                }
                break;
            }
        
            RainbowColor = new Color(rbR, rbG, rbB); // SETTING COLOR

            switch(scT) { // MASTER COLOR EFFECT
                case 0:
                scR=255;
                scG+=10;
                if(scG == 200) {
                    scT = 1;
                }
                break;
                case 1:
                scG-=5;
                if(scG == 0) {
                    scT = 0;
                }
                break;

            }

            MasterColor = new Color(scR, scG, scB); // SETTING COLOR

            // RAIN FILTER

            if (!Filters.Scene["RainFilter"].IsActive()) 
            Filters.Scene.Activate("RainFilter"); 

            if(Main.raining) {
                transition+=0.01f;
                if(transition > 1f)
                transition = 1f;
                if(Main.rainTime <= 1000) {
                    Main.maxRain = (int)MathHelper.Lerp(0, Main.maxRain, (float)Main.rainTime/1000);
                    Main.maxRaining = MathHelper.Lerp(0, Main.maxRaining, (float)Main.rainTime/1000);
                }
            }
            else {
                transition-=0.01f;
                if(transition < 0f)
                transition = 0f;
                
            }

            intensity = (Main.maxRain * Main.maxRaining / (20.0f * 645.0f));

            Filters.Scene["RainFilter"].GetShader().UseOpacity(intensity);

        }

    }
}