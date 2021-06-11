// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.Serialization;

namespace engine.framework
{
    /// <summary>
    /// Describes a 2D-point.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct Point2D : IEquatable<Point2D>
    {
        #region Private Fields

        private static readonly Point2D zeroPoint = new Point2D();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="Point2D"/>.
        /// </summary>
        [DataMember]
        public int X;

        /// <summary>
        /// The y coordinate of this <see cref="Point2D"/>.
        /// </summary>
        [DataMember]
        public int Y;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="Point2D"/> with coordinates 0, 0.
        /// </summary>
        public static Point2D Zero
        {
            get { return zeroPoint; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString()
                );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a point with X and Y from two values.
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space.</param>
        /// <param name="y">The y coordinate in 2d-space.</param>
        public Point2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a point with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates in 2d-space.</param>
        public Point2D(int value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="Point2D"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="Point2D"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static Point2D operator +(Point2D value1, Point2D value2)
        {
            return new Point2D(value1.X + value2.X, value1.Y + value2.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> from a <see cref="Point2D"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Point2D"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="Point2D"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static Point2D operator -(Point2D value1, Point2D value2)
        {
            return new Point2D(value1.X - value2.X, value1.Y - value2.Y);
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Point2D"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Point2D"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static Point2D operator *(Point2D value1, Point2D value2)
        {
            return new Point2D(value1.X * value2.X, value1.Y * value2.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="Point2D"/> by the components of another <see cref="Point2D"/>.
        /// </summary>
        /// <param name="source">Source <see cref="Point2D"/> on the left of the div sign.</param>
        /// <param name="divisor">Divisor <see cref="Point2D"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static Point2D operator /(Point2D source, Point2D divisor)
        {
            return new Point2D(source.X / divisor.X, source.Y / divisor.Y);
        }

        /// <summary>
        /// Compares whether two <see cref="Point2D"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="Point2D"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="Point2D"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Point2D a, Point2D b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares whether two <see cref="Point2D"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="Point2D"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="Point2D"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(Point2D a, Point2D b)
        {
            return !a.Equals(b);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Point2D) && Equals((Point2D)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Point2D"/>.
        /// </summary>
        /// <param name="other">The <see cref="Point2D"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(Point2D other)
        {
            return ((X == other.X) && (Y == other.Y));
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Point2D"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Point2D"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }

        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Point2D"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="Point2D"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }

        /// <summary>
        /// Gets a <see cref="Vector2"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Vector2"/> representation for this object.</returns>
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        #endregion
    }
}


