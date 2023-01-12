using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;


namespace CHADventure
{
    public class Game1 : Game
    {
        public const int HAUTEUR_FENETRE = 800; // Constantes pour la taille de la fenêtre
        public const int LARGEUR_FENETRE = 800;

                                      
        private Entree _entree;          // Variables de game1
        private ScreenMenu _menu;
        private Touches _touches;
        private SallePrincipale _sallePrincipale;
        private SalleDroite _salleDroite;
        private SalleGauche _salleGauche;
        private SalleBoss _salleBoss;
        private ScreenGameOver _screenGameOver;
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;
        public ushort tx;
        public ushort ty;


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
            base.Initialize();
        }

        protected override void LoadContent() 
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _entree = new Entree(this);
            _sallePrincipale = new SallePrincipale(this);
            _menu = new ScreenMenu(this);
            _touches = new Touches(this);
 
            _salleDroite = new SalleDroite(this);
            _salleGauche = new SalleGauche(this);
            _salleBoss = new SalleBoss(this);
            _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice, Color.Black));
            _screenGameOver = new ScreenGameOver(this);
        }

        protected override void Update(GameTime gameTime) //Update sert a la gestion des scène
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _graphics.PreferredBackBufferWidth = LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = HAUTEUR_FENETRE;
            _graphics.ApplyChanges();
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)  // Le menu
            {
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play)
                {
                    _screenManager.LoadScreen(_entree, new FadeTransition(GraphicsDevice, Color.Black));
                    _entree.PositionPerso = new Vector2(400, 672);
                }
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
            else if (keyboardState.IsKeyDown(Keys.E) && _sallePrincipale._peutSortirDehors) // Si la touche E est utilisé au bonne cordonnée, le perso retourne dans entree au vecteur de position défini
            {
                _screenManager.LoadScreen(_entree, new FadeTransition(GraphicsDevice,
                Color.Black));
                _entree.PositionPerso = new Vector2(400, 66);
            }
            else if (keyboardState.IsKeyDown(Keys.E) && _entree.Peutentrer) // Si la touche E est utilisé au bonne cordonnée, le perso va dans la salle principale au vecteur de position défini
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
                _sallePrincipale.PositionPerso = new Vector2(400, 600);
            } 
            else if (_sallePrincipale._peutSalleDroite) // si le perso va dans le couloir droite, charche la salle droite au vecteur de position défini
            {
                _screenManager.LoadScreen(_salleDroite, new FadeTransition(GraphicsDevice, 
                Color.Black));
                _salleDroite.PositionPerso = new Vector2(174, 377);
            }
            else if ( _sallePrincipale._peutSalleGauche) // si le perso va dans le couloir gauche, charche la salle gauche au vecteur de position défini
            {
                _screenManager.LoadScreen(_salleGauche, new FadeTransition(GraphicsDevice,
                Color.Black));
                _salleGauche.PositionPerso = new Vector2(624, 400);
            }
            else if (keyboardState.IsKeyDown(Keys.R) && _screenGameOver.ReturnMenu(gameTime)) // si le menu game over est lancé rapuyer sur R après 2 sec pour aller dans le menu et remet les coeur de la salle gauche et droite a 3 
            {
                _screenManager.LoadScreen(_menu, new FadeTransition(GraphicsDevice, Color.Black));
                _salleGauche.Coeur.Pv = 3;
                _salleDroite.Coeur.Pv = 3;
            }
            else if (_salleDroite._peutSallePrincipaleD) // le perso peut reprendre le couloir de droite pour retourner dans la salle principale
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
                _sallePrincipale.PositionPerso = new Vector2(751, 202);
            }
            else if (_salleGauche._peutSallePrincipaleG) // le perso peut reprendre le couloir de gauche pour retourner dans la salle principale
            {
                _screenManager.LoadScreen(_sallePrincipale, new FadeTransition(GraphicsDevice,
                Color.Black));
                _sallePrincipale.PositionPerso = new Vector2(38, 202);
            }
            else if (_salleGauche.Coeur.Pv == 0 || _salleDroite.Coeur.Pv == 0)  // si les pv du perso dans la salle droite ou dans la salle gauche sont à 0, alors lance l'ecran game over
                _screenManager.LoadScreen(_screenGameOver, new FadeTransition(GraphicsDevice,
                Color.Black));

            _entree.Peutentrer=false;
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