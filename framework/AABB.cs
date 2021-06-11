using System;
using System.Numerics;

namespace engine.framework
{
    public struct AABB : IEquatable<AABB>
    {
        public Vector2 LowerBound;

        public Vector2 UpperBound;

        public AABB(Vector2 min, Vector2 max)
            : this(ref min, ref max)
        {
        }

        public AABB(ref Vector2 min, ref Vector2 max)
        {
            LowerBound = min;
            UpperBound = max;
        }

        public AABB(Vector2 center, float width, float height)
        {
            LowerBound = center - new Vector2(width / 2, height / 2);
            UpperBound = center + new Vector2(width / 2, height / 2);
        }

        public AABB(float x, float y, float width, float height) 
            : this(new Vector2(x, y), new Vector2(x + width, y + height))
        {
        }

        public Vector2 BottomLeft
        {
            get { return new Vector2(Left, Bottom); }
        }

        public Vector2 TopLeft
        {
            get { return new Vector2(Left, Top); }
        }

        public Vector2 BottomRight
        {
            get { return new Vector2(Right, Bottom); }
        }

        public Vector2 TopRight
        {
            get { return new Vector2(Right, Top); }
        }

        public Vector2 Location
        {
            get { return LowerBound; }
        }

        public Vector2 Size
        {
            get { return UpperBound - LowerBound; }
        }

        public Vector2 LeftTop 
        {
            get { return LowerBound; }
        }

        public Vector2 RightTop 
        {
            get { return new Vector2(Right, Top); }
        }

        public Vector2 RightBottom 
        {
            get { return UpperBound; }
        }

        public Vector2 LeftBottom 
        {
            get { return new Vector2(Left, Bottom); }
        }

        public float X 
        {
            get { return LowerBound.X; }
        }

        public float Y 
        {
            get { return LowerBound.Y; }
        }

        public float Left 
        {
            get { return LowerBound.X; }
        }

        public float Right 
        {
            get { return UpperBound.X; }
        }

        public float Top 
        {
            get { return LowerBound.Y; }
        }

        public float Bottom 
        {
            get { return UpperBound.Y; }
        }

        public float Width
        {
            get { return UpperBound.X - LowerBound.X; }
        }

        public float Height
        {
            get { return UpperBound.Y - LowerBound.Y; }
        }

        public Vector2 Center
        {
            get { return 0.5f * (LowerBound + UpperBound); }
        }

        public Vector2 Extents
        {
            get { return 0.5f * (UpperBound - LowerBound); }
        }

        public float Perimeter
        {
            get
            {
                float wx = UpperBound.X - LowerBound.X;
                float wy = UpperBound.Y - LowerBound.Y;
                return 2.0f * (wx + wy);
            }
        }

        public Vertices Vertices
        {
            get
            {
                Vertices vertices = new Vertices(4);
                vertices.Add(UpperBound);
                vertices.Add(new Vector2(UpperBound.X, LowerBound.Y));
                vertices.Add(LowerBound);
                vertices.Add(new Vector2(LowerBound.X, UpperBound.Y));
                return vertices;
            }
        }

        public AABB Q1
        {
            get { return new AABB(Center, UpperBound); }
        }

        public AABB Q2
        {
            get { return new AABB(new Vector2(LowerBound.X, Center.Y), new Vector2(Center.X, UpperBound.Y)); }
        }

        public AABB Q3
        {
            get { return new AABB(LowerBound, Center); }
        }

        public AABB Q4
        {
            get { return new AABB(new Vector2(Center.X, LowerBound.Y), new Vector2(UpperBound.X, Center.Y)); }
        }

        public bool IsValid()
        {
            Vector2 d = UpperBound - LowerBound;
            bool valid = d.X > 0.0f && d.Y > 0.0f;
            //valid = valid && LowerBound.IsValid() && UpperBound.IsValid();
            return valid;
        }

        public bool IsValid2()
        {
            Vector2 d = UpperBound - LowerBound;
            bool valid = d.X != 0.0f && d.Y != 0.0f;
            //valid = valid && LowerBound.IsValid() && UpperBound.IsValid();
            return valid;
        }

        public void Combine(ref AABB aabb)
        {
            LowerBound = Vector2.Min(LowerBound, aabb.LowerBound);
            UpperBound = Vector2.Max(UpperBound, aabb.UpperBound);
        }

        public void Combine(ref AABB aabb1, ref AABB aabb2)
        {
            var aabb = aabb1.Combine(aabb2);
            LowerBound = aabb.LowerBound;
            UpperBound = aabb.UpperBound;
        }

        public AABB Combine(AABB aabb) 
        {
            var lower1 = Vector2.Min(aabb.LowerBound, aabb.UpperBound);
            var lower2 = Vector2.Min(this.LowerBound, this.UpperBound);
            var lower = Vector2.Min(lower1, lower2);

            var upper1 = Vector2.Max(aabb.LowerBound, aabb.UpperBound);
            var upper2 = Vector2.Max(this.LowerBound, this.UpperBound);
            var upper = Vector2.Max(upper1, upper2);

            return new AABB(lower, upper);
        }


        public bool Contains(AABB aabb)
        {
            bool result = true;
            result = result && LowerBound.X <= aabb.LowerBound.X;
            result = result && LowerBound.Y <= aabb.LowerBound.Y;
            result = result && aabb.UpperBound.X <= UpperBound.X;
            result = result && aabb.UpperBound.Y <= UpperBound.Y;
            return result;
        }

        public bool Contains(Rectangle2D rect) 
        {
            bool result = true;
            result = result && LowerBound.X <= rect.Left;
            result = result && LowerBound.Y <= rect.Top;
            result = result && rect.Right <= UpperBound.X;
            result = result && rect.Bottom <= UpperBound.Y;
            return result;
        }

        public bool Contains(Vector2 point)
        {
            //using epsilon to try and gaurd against float rounding errors.
            return (point.X > (LowerBound.X + Settings.Epsilon) && point.X < (UpperBound.X - Settings.Epsilon) &&
                   (point.Y > (LowerBound.Y + Settings.Epsilon) && point.Y < (UpperBound.Y - Settings.Epsilon)));
        }

        public static bool TestOverlap(AABB a, AABB b)
        {
            Vector2 d1 = b.LowerBound - a.UpperBound;
            Vector2 d2 = a.LowerBound - b.UpperBound;

            if (d1.X > 0.0f || d1.Y > 0.0f)
                return false;

            if (d2.X > 0.0f || d2.Y > 0.0f)
                return false;

            return true;
        }

        public bool Equals(AABB other)
        {
            return LowerBound.Equals(other.LowerBound) &&
                    UpperBound.Equals(other.UpperBound);
        }

        public Rectangle2D ToRectangle() 
        {
            return new Rectangle2D(
                (int)LowerBound.X, 
                (int)LowerBound.Y, 
                (int)Width, 
                (int)Height);
        }
    }
}
