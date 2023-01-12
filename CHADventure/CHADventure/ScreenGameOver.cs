using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
namespace CHADventure
{
    public class ScreenGameOver : GameScreen
    {
        private Texture2D _gameover;
        private float _timer;
        private Game1 _myGame;
        private Perso _perso;
        private SalleDroite _salleDroite;
        private SalleGauche _salleGauche;
        private Coeur _coeur;

  


        public bool _peutSallePrincipaleD = false;


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public ScreenGameOver(Game1 game) : base(game)
        {
            _myGame = game;
            _perso = new Perso();
            _salleGauche = new SalleGauche(game);
            _salleDroite = new SalleDroite(game);
            _coeur = new Coeur();


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
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_gameover, new Vector2(0, 0), Color.White);
            _myGame._spriteBatch.End();

        }
        public bool ReturnMenu(GameTime gameTime)
        {
            bool retour = false;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _timer += elapsed;
            if (_timer >= 5000)
            {
                retour = true;
                _timer = 0;
            }
            return retour;
            
        }

    }
}
