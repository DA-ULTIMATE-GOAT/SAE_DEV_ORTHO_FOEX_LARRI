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
    public class SalleGauche : GameScreen
    {
        private Game1 _myGame;
        private Perso _perso;
        private Coeur _coeur;
        private BlueBlob _blueBlob;
        private SallePrincipale _sallePrincipale;
        private Entree _entree;

        private readonly ScreenManager _screenManager;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;


        //changement de scene :
        public bool _peutSallePrincipaleG = false;
        public const int VITESSE_PERSO = 110;
        public const int TAILLE_TUILE = 16;

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public SalleGauche(Game1 game) : base(game)
        {
            _myGame = game;
            _perso = new Perso();
            _blueBlob = new BlueBlob();
            _coeur = new Coeur();
        }
        public override void Initialize()
        {
            _perso._positionPerso = new Vector2(608, 400);
            _perso.InitPosition(_perso._positionPerso);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _coeur.Initialize();
            _tiledMap = Content.Load<TiledMap>("map/Principale/CombatGauche");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacles");
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacles2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheetPerso = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._ezioSprite = new AnimatedSprite(spriteSheetPerso);
            _coeur.LoadContent(_myGame);
            base.LoadContent();
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

            _coeur.AnimationCoeur(gameTime);
            _coeur.CoeurSprite.Play("troisCoeurs");
            _coeur.CoeurSprite.Update(deltaTime);

            SallesPrincipale(_myGame.tx, _myGame.ty);
        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            _coeur.Draw(_myGame._spriteBatch);
            _myGame._spriteBatch.End();

        }
        public void SallesPrincipale(ushort tx, ushort ty)
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth + 1);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight + 1);
            _peutSallePrincipaleG = false;
            if (_mapLayer.GetTile(tx, ty).GlobalIdentifier == 31)
            {
                _peutSallePrincipaleG = true;
            }
        }
    }
}