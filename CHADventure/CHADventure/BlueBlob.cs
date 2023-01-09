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
        Random rndm = new Random();
        public BlueBlob[,] _tabBlob;
        public Vector2 _positionBlob;
        public AnimatedSprite _spriteBlob;
        public String _animationBlob = "idle";
        private int _nbEnnemis = 0;
       
        public void Spawn(GameTime gametime)
        {
            if (_nbEnnemis < 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _tabBlob[i,j] = new BlueBlob();
                        _tabBlob[i,j]._positionBlob = new Vector2(rndm.Next(288, 496), rndm.Next(256, 464));
                        _tabBlob[i,j]._animationBlob = new String(_animationBlob);
                        _nbEnnemis++;
                    }
                }
            }
        }
    }
    
}
