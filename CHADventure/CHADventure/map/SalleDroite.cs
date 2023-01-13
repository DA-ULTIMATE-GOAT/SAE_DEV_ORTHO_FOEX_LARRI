using System;
using CHADventure.monstre;
using CHADventure.personnage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace CHADventure.map
{
    public class SalleDroite : GameScreen
    {
        private Game1 _myGame;                  //Initialization des variables
        private BlueBlob _blueBlob;
        private Perso _perso;
        private Coeur _coeur;
        private SallePrincipale _sallePrincipale;
        private readonly ScreenManager _screenManager;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private BlueBlob[] _tabBlob;
        public AnimatedSprite _spriteBlob;
        public const int VITESSE_PERSO = 110;
        public const int TAILLE_TUILE = 16;
        private Vector2 _positionPerso;
        Random rndm = new Random();

        //changement de scene :
        public bool _peutSallePrincipaleD = false;

        public Vector2 PositionPerso { get => _positionPerso; set => _positionPerso = value; }
        public Coeur Coeur { get => _coeur; set => _coeur = value; }

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public SalleDroite(Game1 game) : base(game)
        {
            _myGame = game;
            _perso = new Perso();
            _blueBlob = new BlueBlob(_perso);

            Coeur = new Coeur();
        }
        public override void Initialize() //Initialization du tableau de blob bleu
        {
            _perso._positionPerso = PositionPerso;
            _tabBlob = new BlueBlob[5];
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i] = new BlueBlob(_perso);
            }
            base.Initialize();
        }
        public override void LoadContent()
        {
            Coeur.Initialize();
            _tiledMap = Content.Load<TiledMap>("map/Principale/CombatDroit");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacles");
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacles2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheetPerso = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._ezioSprite = new AnimatedSprite(spriteSheetPerso);
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i] = new BlueBlob(_perso);
                _tabBlob[i].Initialize();
                _tabBlob[i].LoadContent(_myGame);
            }
            Coeur.LoadContent(_myGame);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {


            KeyboardState keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                if (_perso.Degats(_blueBlob.PositionBlob, _tabBlob[i], gameTime)) // si le perso attaque un blob du tableau, lui enlève alors un pv
                {
                    _tabBlob[i].Pv -= 1;
                }
                if (_tabBlob[i].Attaque(gameTime, _perso)) // si le Perso prends un dégats, il perd un pv
                {
                    Coeur.Pv -= 1;
                }
                if (_tabBlob[i].Pv == 0) // si les pv d'un blob
                {
                    _tabBlob[i].Mort(gameTime);
                }
            }
            if (!_perso._attaque)
                _perso.DeplacementPerso(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            _perso._ezioSprite.Play(_perso._animation);
            _perso._ezioSprite.Update(deltaTime);
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i].DeplacementBlob(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            }
            Coeur.AnimationCoeur(gameTime);
            Coeur.CoeurSprite.Play(Coeur.AnimationCoeur(gameTime));
            Coeur.CoeurSprite.Update(deltaTime);
            _tiledMapRenderer.Update(gameTime);
            SallesPrincipale(_myGame.tx, _myGame.ty);
        }
        public override void Draw(GameTime gameTime) //Draw la map, les blobs, le perso et les coeurs
        {
            _tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i].Draw(_myGame._spriteBatch);
            }
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            Coeur.Draw(_myGame._spriteBatch);
            _myGame._spriteBatch.End();

        }
        public void SallesPrincipale(ushort tx, ushort ty) // permet de retourner dans la salle principale
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth - 1);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight + 1);
            _peutSallePrincipaleD = false;
            if (_mapLayer.GetTile(tx, ty).GlobalIdentifier == 35)
            {
                _peutSallePrincipaleD = true;
            }
        }
    }
}
