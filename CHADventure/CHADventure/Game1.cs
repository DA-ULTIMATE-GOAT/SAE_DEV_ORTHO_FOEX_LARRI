using Microsoft.Xna.Framework;
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
    public class Game1 : Game
    {
        //pour le changement de scene
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;
        private Entree _entree;
        private SallePrincipale _sallePrincipale;
        private CouloirDroit _couloirDroit;
        private CouloirGauche _couloirGauche;
        private ParcoursDroit _parcoursDroit;
        private ParcoursGauche _parcoursGauche;
        private EnigmeDroite _enigmeDroite;
        private EnigmeGauche _enigmeGauche;
        private CouloirPrincipale _couloirPrincipale;
        private SalleBoss _salleBoss;
        private Vector2 _position;
        private ClassPerso _perso;

        
        public SpriteBatch SpriteBatch { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _entree = new Entree(this); // en leur donnant une référence au Game
            _sallePrincipale = new SallePrincipale(this);
            _couloirDroit = new CouloirDroit(this);
            _couloirGauche = new CouloirGauche(this);
            _parcoursDroit = new ParcoursDroit(this);
            _parcoursGauche = new ParcoursGauche(this);
            _couloirPrincipale = new CouloirPrincipale(this);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _myGame.GraphicsDevice.Clear(Color.Black);

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _screenManager.LoadScreen(_entree, new FadeTransition(GraphicsDevice,
                Color.Black));
                //ClassPerso.InitPosition(_position);
            }
            else if (keyboardState.IsKeyDown(Keys.E))
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                _screenManager.LoadScreen(_couloirDroit, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (keyboardState.IsKeyDown(Keys.R))
            {
                _screenManager.LoadScreen(_couloirGauche, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (keyboardState.IsKeyDown(Keys.Y))
            {
                _screenManager.LoadScreen(_couloirPrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}