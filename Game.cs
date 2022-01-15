using System;
using System.IO;
using IDGTF2WeaponGenerator.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteFontPlus;

namespace IDGTF2WeaponGenerator
{
    public class TF2WeaponGenerator : Game
    {
        private static GraphicsDeviceManager _graphics;
        private static SpriteBatch _spriteBatch;
        public static Random random = new Random();
        public static SpriteFont font;
        public static SpriteFont TF2Font;
        public static SpriteFont TF2FontBold;
        public static Texture2D whiteTex;
        public static WeaponSystem weaponSystem;
        private static float _globalTime = 0;
        public static float GlobalTime => _globalTime;
        public static GraphicsDeviceManager Graphics => _graphics;
        public static Vector2 WindowSizeGraphics => new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        public static Point screenSize = new Point(1024, 768);
        public static string WorkingDirectory => Directory.GetCurrentDirectory();
        public static string AssetDirectory => "Assets/";
        public static SpriteBatch SpriteBatch
        {
            get
            {
                return _spriteBatch;
            }
        }
        public static TF2WeaponGenerator instance;

        public TF2WeaponGenerator()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            instance = this;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsFixedTimeStep = true;
            Content.RootDirectory = "Content";
            //adjust window size
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            Window.Title = ("TF2 Generator");
            Logging.ConsoleLog("Game Started");

            _graphics.ApplyChanges();

            weaponSystem = new WeaponSystem();
            weaponSystem.MakeNewWeapon();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            whiteTex = Content.Load<Texture2D>(AssetDirectory + "White");
            TF2Font = Content.Load<SpriteFont>(AssetDirectory + "TF2");
            TF2FontBold = Content.Load<SpriteFont>(AssetDirectory + "TF2Bold");

            font = TtfFontBaker.Bake(File.ReadAllBytes(@"C:\\Windows\\Fonts\arial.ttf"), 25, 1024, 1024, new[] { CharacterRange.BasicLatin, CharacterRange.Latin1Supplement, CharacterRange.LatinExtendedA, CharacterRange.Cyrillic }).CreateSpriteFont(GraphicsDevice); //Idk how this works but it make fonts a lot easier
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            if (whiteTex != null)
                whiteTex.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            Controls.UpdateControls();
            _globalTime = (float)(gameTime.TotalGameTime.TotalSeconds % 3600.0);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();

            Color color = Color.White;// Color.Lerp(Color.Red, Color.White, 0.50f + (float)Math.Sin(GlobalTime / 2f) / 2f);

            SpriteBatch.Draw(whiteTex, Vector2.Zero, null, color, 0, Vector2.Zero, screenSize.ToVector2() / whiteTex.Size(), SpriteEffects.None, 0);

            weaponSystem.DrawWeapon();

            SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }





    public class Logging
    {

        public static void ConsoleLog(string str)
        {
            System.Diagnostics.Debug.WriteLine(str);
        }
    }

    public class Controls
    {
        public static bool nextButton = false;
        public static Vector2 mousePos;
        public static MouseState TheMouse => Mouse.GetState();
        public static void UpdateControls()
        {
            mousePos = TheMouse.Position.ToVector2();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (!nextButton)
                {
                    TF2WeaponGenerator.weaponSystem.MakeNewWeapon();
                    nextButton = true;
                }
            }
            else
            {
                nextButton = false;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                TF2WeaponGenerator.instance.Exit();
        }

    }

    public static class HelperClass
    {
        public static Vector2 Size(this Texture2D tex)
        {
            return new Vector2(tex.Width, tex.Height) / 2f;
        }
    }
}
