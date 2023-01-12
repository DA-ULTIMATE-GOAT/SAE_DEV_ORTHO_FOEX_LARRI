using System;
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
        private BlueBlob blueBlob;
        private Perso _perso;


        public AnimatedSprite CoeurSprite { get => _coeurSprite; set => _coeurSprite = value; }
        public int Pv { get => pv; set => pv = value; }

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
                _animation = "troisCoeurs";
            }
            else if (Pv == 2)
            {
                _animation = "deuxCoeur";
            }
            else if (Pv == 1)
            {
                _animation = "unCoeur";
            }
            else
                _animation = "zeroCoeur";
            return _animation;
        }

        public bool Mort(GameTime gameTime)
        {
            bool mort = false;
            if (AnimationCoeur(gameTime) == "zeroCoeur")
            {
                _perso._animation = "death";
                mort = true;
            }

            return mort;
        }

    }

    
}
