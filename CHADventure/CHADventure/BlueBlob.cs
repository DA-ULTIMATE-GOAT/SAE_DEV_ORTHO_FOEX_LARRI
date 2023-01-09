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
    public class BlueBlob
    {
        private Game1 _myGame;
        public const int TAILLE_FENETRE = 800;
        public const int LARGEUR_BLOB = 25;
        public const int HAUTEUR_BLOB = 19;
        public const int VITESSE_BLOB = 90;
        Random spawn = new Random();

        private BlueBlob[] blobs;
        private Vector2 _positionBlob;
        private AnimatedSprite _blueBlob;
        private String _animationBlob = "idle";
        private int _nbEnnemis = 0;
       
        public void Spawn(GameTime gametime)
        {
            if (_nbEnnemis < 3)
            {
                for (int i = 0; i < 1; i++)
                {

                }
            }
        }
    }
    
}
