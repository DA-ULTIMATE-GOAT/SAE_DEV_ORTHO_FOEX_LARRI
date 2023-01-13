using CHADventure.map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
namespace CHADventure.screen
{
    public class ScreenGameOver : GameScreen
    {
        private Texture2D _gameover;
        private float _timer;           // Initialization des variables
        private Game1 _myGame;
        private personnage.Perso _perso;
        private SalleDroite _salleDroite;
        private SalleGauche _salleGauche;
        private personnage.Coeur _coeur;




        public bool _peutSallePrincipaleD = false;


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public ScreenGameOver(Game1 game) : base(game)
        {
            _myGame = game;
            _perso = new personnage.Perso();
            _salleGauche = new SalleGauche(game);
            _salleDroite = new SalleDroite(game);
            _coeur = new personnage.Coeur();


        }
        public override void LoadContent()
        {
            _gameover = Content.Load<Texture2D>("GameOver/GameOver");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {


            KeyboardState keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public override void Draw(GameTime gameTime) // Dessine la fenêtre game over
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_gameover, new Vector2(0, 0), Color.White);
            _myGame._spriteBatch.End();

        }
        public bool ReturnMenu(GameTime gameTime) // si le timer est a 2000 return true 
        {
            bool retour = false;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _timer += elapsed;
            if (_timer >= 2000)
            {
                retour = true;
                _timer = 0;
            }
            return retour;

        }

    }
}
