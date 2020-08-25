using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace KnightRunRerun
{
    public class Game : IDisposable
    {
        private GameTime gameTime;

        public Color4 ClearColor { get; set; }
        public GameWindow Window { get; private set; }
        public bool UpdateWhileNotFocused { get; set; }

        public Game(int width, int height, string title)
        {
            // Create our game window with given dimensions (width, height) and title
            var graphicsMode = new GraphicsMode(32, 24, 8, 4);
            Window = new GameWindow(width, height, graphicsMode, title);

            // Register events for when the window processes each frame (update and render)
            Window.UpdateFrame += (sender, e) => updateFrame(e);
            Window.RenderFrame += (sender, e) => renderFrame(e);
            Window.Resize += (sender, e) => Resize(); // Event for when the user resizes the window

            // Default values for clear color and update
            ClearColor = Color4.CornflowerBlue;
            UpdateWhileNotFocused = false;

            // Enable alpha blending for transparent PNGs
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // Used for tracking time between each frames
            gameTime = new GameTime();
        }

        public void Run()
        {
            Initialize();
            LoadContent();

            Window.Run();
        }
        public void Resize()
        {
            // Update GL viewport to the new window size
            GL.ClearColor(ClearColor);
            GL.Viewport(0, 0, Window.Width, Window.Height);
        }

        public virtual void Initialize() { }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }

        public void Dispose()
        {
            UnloadContent();
            Window.Dispose();
        }

        protected virtual void updateFrame(FrameEventArgs e)
        {
            // Only update the game while the game window is focused or if we want to update in the background.
            if (Window.Focused || UpdateWhileNotFocused)
            {
                gameTime.ElapsedTime = TimeSpan.FromSeconds(e.Time);
                gameTime.TotalTime.Add(gameTime.ElapsedTime);

                Update(gameTime);
            }
        }
        protected virtual void renderFrame(FrameEventArgs e)
        {
            // Clear the screen and then call our draw function
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Draw(gameTime);

            // Dual buffered rendering.
            Window.SwapBuffers();

            // We have two "buffers" that we render to.  
            // One buffer is what is currently on the screen and the other is offscreen that we actively render to.
            // After we render everything to the offscreen buffer, we swap those.  This helps prevent artifacting.
        }
    }
}
