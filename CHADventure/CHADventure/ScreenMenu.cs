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
    public class ScreenMenu : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;
        private Perso _perso = new Perso();
        private ScreenMenu _menu;

        // texture du menu avec 3 boutons
        private Texture2D _textBoutons;
        private Texture2D _fond;
        private Texture2D _engrenage;
        public bool _peutTouche = true;
        public bool _peutMenu = false;
        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] lesBoutons;

        private AnimatedSprite _logo;
        public ScreenMenu(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[3];
            lesBoutons[0] = new Rectangle(50, 609, 279, 89);
            lesBoutons[1] = new Rectangle(471, 609, 279, 89);
            lesBoutons[2] = new Rectangle(686,45,72,62);
            


        }
        public override void LoadContent()
        {
            _textBoutons = Content.Load<Texture2D>("boutons");
            _fond = Content.Load<Texture2D>("Fond_Chateau");
            _engrenage = Content.Load<Texture2D>("Engrenage");
            SpriteSheet logo = Content.Load<SpriteSheet>("logo/logo.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _logo = new AnimatedSprite(logo);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _logo.Play("ecran");
            _logo.Update(gameTime);
            MouseState _mouseState = Mouse.GetState();
            _myGame.etat = Game1.Etats.Menu;
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
                            _myGame.etat = Game1.Etats.Play;
                            _perso._positionPerso = new Vector2(400, 672);
                            _perso.InitPosition(_perso._positionPerso);
                        }
                        else if (i == 1)
                            _myGame.etat = Game1.Etats.Quit;
                        else
                        {
                            _myGame.etat = Game1.Etats.Touch;
                        }
                    }

                }
            }

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_fond, new Vector2(0, 0), Color.White);
            _myGame._spriteBatch.Draw(_textBoutons, new Vector2(0, 0), Color.White);
            _myGame._spriteBatch.Draw(_logo, new Vector2(400, 350));
            _myGame._spriteBatch.Draw(_engrenage, new Vector2(0, 0), Color.White);
            _myGame._spriteBatch.End();


        }
    }
}
