using engine.framework.graphics;
using System;
using System.Numerics;

namespace engine.framework
{
    public interface IFocusable
    {
        Vector2 Position { get; }
    }

    public interface ICamera
    {
        Vector3 Position { get; set; }

        float MoveSpeed { get; set; }

        float Rotation { get; set; }

        Vector2 Origin { get; }

        float Scale { get; set; }

        Vector2 ScreenCenter { get; }

        Matrix4x4 Transform { get; }

        IFocusable Focus { get; set; }

        bool IsInView(Vector2 position, Texture2D texture);

        void Update(float elapseTime);
    }

    public class Camera : ICamera
    {
        private GLRenderer _window;
        private Vector3 _position;
        private Vector2 _origin;
        private float _rotation;
        private float _scale;
        protected float _viewportHeight;
        protected float _viewportWidth;

        public static Camera Ins;

        public Matrix4x4 SimProjection;
        public Matrix4x4 SimView;

        public Camera(GLRenderer window)
        {
            Ins = this;

            _window = window;

            _viewportWidth = _window.GraphicsDevice.Viewport.Width;
            _viewportHeight = _window.GraphicsDevice.Viewport.Height;

            ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            Scale = 1;
            MoveSpeed = 1.25f;

#if SERVER
            SimProjection = Matrix.CreateOrthographicOffCenter(0f, FarseerPhysics.ConvertUnits.ToSimUnits(_viewportWidth), FarseerPhysics.ConvertUnits.ToSimUnits(_viewportHeight), 0f, 0f, 1f);
            SimView = Matrix.Identity;
#endif
        }

        #region Properties

        public Vector3 Position
        {
            get { return _position; }
            set 
            {
                if (_position != value)
                {
                    _position = value;
                    ResetTransform();
                }
            }
        }
        public float Rotation 
        {
            get { return _rotation; }
            set 
            {
                if (_rotation != value) 
                {
                    _rotation = value;
                    ResetTransform();
                }
            }
        }

        public Vector2 Origin 
        {
            get { return _origin; }
            set 
            {
                if (_origin != value) 
                {
                    _origin = value;
                    ResetTransform();
                }
            }
        }

        public float Scale
        {
            get { return _scale; }
            set 
            {
                if (_scale != value) 
                {
                    _scale = value;
                    ResetTransform();
                }
            }
        }

        public Matrix4x4 Transform { get; private set; }
        public Vector2 ScreenCenter { get { return Vector2.Zero; } protected set { } }
        public IFocusable Focus { get; set; }
        public float MoveSpeed { get; set; }

        #endregion

        public void Update(float elapseTime)
        {
            if (Focus != null)
            {
                // Move the Camera to the position that it needs to go
                var delta = elapseTime / 1000f;
                _position.X += (Focus.Position.X - Position.X) * MoveSpeed * delta;
                _position.Y += (Focus.Position.Y - Position.Y) * MoveSpeed * delta;
                ResetTransform();
            }
        }

        /// <summary>
        /// Determines whether the target is in view given the specified position.
        /// This can be used to increase performance by not drawing objects
        /// directly in the viewport
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="texture">The texture.</param>
        /// <returns>
        ///     <c>true</c> if [is in view] [the specified position]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInView(Vector2 position, Texture2D texture)
        {
            // If the object is not within the horizontal bounds of the screen

            if ((position.X + texture.Width) < (Position.X - Origin.X) || (position.X) > (Position.X + Origin.X))
                return false;

            // If the object is not within the vertical bounds of the screen
            if ((position.Y + texture.Height) < (Position.Y - Origin.Y) || (position.Y) > (Position.Y + Origin.Y))
                return false;

            // In View
            return true;
        }

        private void ResetTransform() 
        {
            var scale = (float)Math.Max(0.01, Scale);

            Transform = Matrix4x4.Identity *
                       Matrix4x4.CreateTranslation(-Position.X, -Position.Y, 0) *
                       Matrix4x4.CreateRotationZ(Rotation) *
                       Matrix4x4.CreateTranslation(Origin.X, Origin.Y, 0) *
                       Matrix4x4.CreateScale(new Vector3(scale, scale, scale));

            Origin = ScreenCenter / scale;
        }
    }
}