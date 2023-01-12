using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;


namespace CHADventure
{
    public class Sound
    {
        private Game1 _myGame;

        private SoundEffect _soundEffect;

        public void Initialize()
        {


        }


        public void LoadContent(Game1 game)
        {
            SoundEffect Menu = game.Content.Load<SoundEffect>("Sound/Menu/Menu2");
            _soundEffect = Menu;
        }
    }
}
