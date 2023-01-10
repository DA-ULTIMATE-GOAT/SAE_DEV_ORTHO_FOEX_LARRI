using System;
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

        private Vector2 _positionCoeur1 = new Vector2(10, 10);
        private Vector2 _positionCoeur2 = new Vector2(10 + LARGEUR_SPRITE, 10);
        private Vector2 _positionCoeur3 =new Vector2(10+ 2* LARGEUR_SPRITE, 10);
        private AnimatedSprite _coeurSprite;
        private String _animation;


        public AnimatedSprite CoeurSprite { get => _coeurSprite; set => _coeurSprite = value; }
        public Vector2 PositionCoeur1 { get => _positionCoeur1; set => _positionCoeur1 = value; }
        public Vector2 PositionCoeur2 { get => _positionCoeur2; set => _positionCoeur2 = value; }
        public Vector2 PositionCoeur3 { get => _positionCoeur3; set => _positionCoeur3 = value; }

        public void AnimationCoeur(GameTime gameTime)
        {
           _animation = "troisCoeurs";
        }
    }

    
}
