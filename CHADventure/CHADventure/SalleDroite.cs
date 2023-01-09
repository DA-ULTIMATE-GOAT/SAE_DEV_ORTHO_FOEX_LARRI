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
    public class SalleDroite : GameScreen
    {
        private Game1 _myGame;
        private BlueBlob _blueBlob = new BlueBlob();
        private Perso _perso = new Perso();
        private readonly ScreenManager _screenManager;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private Vector2 _positionPerso;

        public const int VITESSE_PERSO = 110;
        public const int TAILLE_TUILE = 16;


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public SalleDroite(Game1 game) : base(game)
        {
            _myGame = game;


        }
        public override void Initialize()
        {

        }
        public override void LoadContent()
        {


        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _perso.Attaque(gameTime);
            if (!_perso._attaque)
                _perso.DeplacementPerso(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            _tiledMapRenderer.Update(gameTime);
            _perso._ezioSprite.Play(_perso._animation);
            _perso._ezioSprite.Update(deltaTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            //_myGame._spriteBatch.Draw();
            _myGame._spriteBatch.End();

        }
    }
}
