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
        private Touches _touches;
        private SallePrincipale _sallePrincipale;
        private ParcoursDroit _parcoursDroit;
        private ParcoursGauche _parcoursGauche;
        private SalleDroite _salleDroite;
        private SalleGauche _salleGauche;
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

        public enum Etats { Menu, Controls, Play, Quit, Touch};
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _entree = new Entree(this);
            _sallePrincipale = new SallePrincipale(this);
            _menu = new ScreenMenu(this);
            _touches = new Touches(this);
            _parcoursDroit = new ParcoursDroit(this);
            _parcoursGauche = new ParcoursGauche(this);
            _couloirPrincipale = new CouloirPrincipale(this);
            _salleDroite = new SalleDroite(this);
            _salleGauche = new SalleGauche(this);
            _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice, Color.Black));
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
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play)
                    _screenManager.LoadScreen(_entree, new FadeTransition(GraphicsDevice, Color.Black));
                else if (this.Etat == Etats.Touch && _menu._peutTouche)
                {
                    _screenManager.LoadScreen(_touches, new FadeTransition(GraphicsDevice, Color.Black));
                    _menu._peutTouche = false;
                    _menu._peutMenu = true;
                }
                else if (this.Etat == Etats.Menu && _menu._peutMenu)
                {
                    _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice, Color.Black));
                    _menu._peutMenu = false;
                    _menu._peutTouche= true;
                }

            }
            else if (keyboardState.IsKeyDown(Keys.E) && _sallePrincipale._peutSortirDehors)
            {
                _screenManager.LoadScreen(_entree, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (keyboardState.IsKeyDown(Keys.E) && _entree._peutentrer)
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (/*keyboardState.IsKeyDown(Keys.E) &&*/ _sallePrincipale._peutSalleDroite)
            {
                _screenManager.LoadScreen(_salleDroite, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (/*keyboardState.IsKeyDown(Keys.E) &&*/ _sallePrincipale._peutSalleGauche)
            {
                _screenManager.LoadScreen(_salleGauche, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (/*keyboardState.IsKeyDown(Keys.E) &&*/ _salleDroite._peutSallePrincipaleD)
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            else if (/*keyboardState.IsKeyDown(Keys.E) &&*/ _salleGauche._peutSallePrincipaleG)
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
            }
            _entree._peutentrer=false;
            _sallePrincipale._peutSortirDehors = false;
            _sallePrincipale._peutSalleDroite = false;
            _sallePrincipale._peutSalleGauche = false;
            _salleGauche._peutSallePrincipaleG = false;
            _salleDroite._peutSallePrincipaleD = false;
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