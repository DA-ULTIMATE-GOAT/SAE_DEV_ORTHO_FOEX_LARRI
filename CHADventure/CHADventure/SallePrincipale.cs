using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace CHADventure
{
    public class SallePrincipale : GameScreen
    {
        private Game1 _myGame;
        private Perso _perso = new Perso();
        private Entree _entree;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private AnimatedSprite _sprite;
        private Vector2 _positionPerso;
        private String _animation;

        //changement de scene :
        public bool _peutSortirDehors = false;
        public bool _peutSalleDroite = false;
        public bool _peutSalleGauche = false;

        public SallePrincipale(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            _positionPerso = new Vector2(400, 500);
            _animation = "idle";
            _perso.InitPosition(_positionPerso);
            base.Initialize();
        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map/Principale/mapcentral");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesSalle1");
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles2Salle1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheetPerso = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._ezioSprite = new AnimatedSprite(spriteSheetPerso);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _perso.Attaque(gameTime);
            if (!_perso._attaque)
                _perso.DeplacementPerso(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            _tiledMapRenderer.Update(gameTime);
            _perso._ezioSprite.Play(_perso._animation);
            _perso._ezioSprite.Update(deltaTime);
            Dehors(_myGame.tx, _myGame.ty);
            SallesDroit(_myGame.tx, _myGame.ty);
            SallesGauche(_myGame.tx, _myGame.ty);

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black); // on utilise la reference vers
            _tiledMapRenderer.Draw(); // on utilise la reference vers
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            _myGame._spriteBatch.End(); // Game1 pour changer le graphisme                                          // Game1 pour chnager le graphisme
        }

        public void Dehors(ushort tx, ushort ty)
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight + 2);
            _peutSortirDehors = false;
            if (_mapLayer.GetTile(tx, ty).GlobalIdentifier == 231)
            {
                _peutSortirDehors = true;
            }
        }
        public void SallesDroit(ushort tx, ushort ty)
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth + 1);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight + 1);
            _peutSalleDroite = false;
            if (_mapLayer2.GetTile(tx, ty).GlobalIdentifier == 31)
            {
                _peutSalleDroite = true;
            }
        }
        public void SallesGauche(ushort tx, ushort ty)
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth - 1);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight + 1);
            _peutSalleGauche = false;
            if (_mapLayer.GetTile(tx, ty).GlobalIdentifier == 31)
            {
                _peutSalleGauche = true;
            }
        }

    }
}
