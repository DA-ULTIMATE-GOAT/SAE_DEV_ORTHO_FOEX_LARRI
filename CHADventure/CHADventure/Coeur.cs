﻿using System;
using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Timers;

namespace CHADventure
{
    public class Coeur
    {

        public const int HAUTEUR_SPRITE = 33;
        public const int LARGEUR_SPRITE = 33;

        private int pv = 3;
        private Vector2 _positionCoeur;
        private AnimatedSprite _coeurSprite;
        private String _animation;


        public AnimatedSprite CoeurSprite { get => _coeurSprite; set => _coeurSprite = value; }
        public int Pv { get => pv; set => pv = value; }
        public string Animation { get => _animation; set => _animation = value; }

        public void Initialize()
        {
            _positionCoeur = new Vector2(58, 35);
        }
        public void LoadContent(Game1 game)
        {
            SpriteSheet spriteSheetCoeur = game.Content.Load<SpriteSheet>("coeur/Coeur.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            CoeurSprite = new AnimatedSprite(spriteSheetCoeur);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(this._coeurSprite, this._positionCoeur);
        }
        public string AnimationCoeur(GameTime gameTime)
        {
            if (Pv == 3)
            {
                Animation = "troisCoeurs";
            }
            else if (Pv == 2)
            {
                Animation = "deuxCoeur";
            }
            else if (Pv == 1)
            {
                Animation = "unCoeur";
            }
            else
                Animation = "zeroCoeur";
            return Animation;
        }



    }

    
}
