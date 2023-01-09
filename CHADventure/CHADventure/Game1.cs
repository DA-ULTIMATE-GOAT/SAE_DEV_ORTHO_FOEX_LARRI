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


namespace CHADventure
{
    public class Game1 : Game
    {
        private Perso _perso = new Perso();
        private Entree _entree;
        private ScreenMenu _menu;
        private SallePrincipale _sallePrincipale;
        private ParcoursDroit _parcoursDroit;
        private ParcoursGauche _parcoursGauche;
        private EnigmeDroite _enigmeDroite;
        private EnigmeGauche _enigmeGauche;
        private CouloirPrincipale _couloirPrincipale;
        private SalleBoss _salleBoss;

        private AnimatedSprite _sprite;
        private Vector2 _positionPerso;
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;
        public ushort tx;
        public ushort ty;

        private const int VITESSE_PERSO = 110;
        public const int HAUTEUR_FENETRE = 800;
        public const int LARGEUR_FENETRE = 800;

        public enum Etats { Menu, Controls, Play, Quit };
        public Etats etat;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            Etat = Etats.Menu;
        }

        protected override void Initialize()
        {
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _sprite = new AnimatedSprite(spriteSheet);
            _positionPerso = new Vector2(400, 672);
            tx = 0;
            ty = 0;
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _entree = new Entree(this); // en leur donnant une référence au Game
            _sallePrincipale = new SallePrincipale(this);
            _menu = new ScreenMenu(this);
            _parcoursDroit = new ParcoursDroit(this);
            _parcoursGauche = new ParcoursGauche(this);
            _couloirPrincipale = new CouloirPrincipale(this);
            _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice, Color.Black));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _graphics.PreferredBackBufferWidth = LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = HAUTEUR_FENETRE;
            _graphics.ApplyChanges();
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play)
                    _screenManager.LoadScreen(_entree, new FadeTransition(GraphicsDevice, Color.Black));

            }
            else if (keyboardState.IsKeyDown(Keys.E) && _entree._peutentrer)
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
                Console.WriteLine("IL ENTRE");
                _entree._peutentrer = false;
            }
            else if (keyboardState.IsKeyDown(Keys.E) && _sallePrincipale._peutsortir)
            {
                //base.UnloadContent();
                _screenManager.LoadScreen(_entree, new FadeTransition(GraphicsDevice,
                Color.Black));
                Console.WriteLine("IL SORT");
                _sallePrincipale._peutsortir = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }

    }
}