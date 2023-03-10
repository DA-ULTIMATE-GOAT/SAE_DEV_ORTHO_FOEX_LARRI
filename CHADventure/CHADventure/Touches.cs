using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHADventure
{
    public class Touches : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;

        // texture du menu avec 3 boutons
        public Texture2D _engrenage;
        private Texture2D _fondTouches;

        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] lesBoutons;

        public Touches(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[1];
            lesBoutons[0] = new Rectangle(686, 45, 72, 62);
        }
        public override void LoadContent()
        {
            _engrenage = Content.Load<Texture2D>("Engrenage");
            _fondTouches = Content.Load<Texture2D>("Touches Fond");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _myGame.etat = Game1.Etats.Touch;
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lesBoutons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 0)
                        {
                            _myGame.etat = Game1.Etats.Menu;
                        }
                    }

                }
            }

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_fondTouches, new Vector2(0, 0), Color.White);
            _myGame._spriteBatch.Draw(_engrenage, new Vector2(0, 0), Color.White);
            _myGame._spriteBatch.End();


        }
    }
}
