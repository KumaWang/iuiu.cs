// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace engine.framework
{
    /// <summary>
    /// Contains commonly used precalculated values and mathematical operations.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Represents the mathematical constant e(2.71828175).
        /// </summary>
        public const float E = (float)Math.E;

        /// <summary>
        /// Represents the log base ten of e(0.4342945).
        /// </summary>
        public const float Log10E = 0.4342945f;

        /// <summary>
        /// Represents the log base two of e(1.442695).
        /// </summary>
        public const float Log2E = 1.442695f;

        /// <summary>
        /// Represents the value of pi(3.14159274).
        /// </summary>
        public const float Pi = (float)Math.PI;

        /// <summary>
        /// Represents the value of pi divided by two(1.57079637).
        /// </summary>
        public const float PiOver2 = (float)(Math.PI / 2.0);

        /// <summary>
        /// Represents the value of pi divided by four(0.7853982).
        /// </summary>
        public const float PiOver4 = (float)(Math.PI / 4.0);

        /// <summary>
        /// Represents the value of pi times two(6.28318548).
        /// </summary>
        public const float TwoPi = (float)(Math.PI * 2.0);

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="value1">The first position in the interpolation.</param>
        /// <param name="value2">The second position in the interpolation.</param>
        /// <param name="value3">The third position in the interpolation.</param>
        /// <param name="value4">The fourth position in the interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            double amountSquared = amount * amount;
            double amountCubed = amountSquared * amount;
            return (float)(0.5 * (2.0 * value2 +
                (value3 - value1) * amount +
                (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>   
        public static float Clamp(float value, float min, float max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            value = (value < min) ? min : value;

            // There's no check to see if min > max.
            return value;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            value = (value > max) ? max : value;
            value = (value < min) ? min : value;
            return value;
        }

        /// <summary>
        /// Calculates the absolute value of the difference of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>Distance between the two values.</returns>
        public static float Distance(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">Source position.</param>
        /// <param name="tangent1">Source tangent.</param>
        /// <param name="value2">Source position.</param>
        /// <param name="tangent2">Source tangent.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            double sCubed = s * s * s;
            double sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return (float)result;
        }


        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns> 
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>value1 + (value2 - value1) * amount</code>.
        /// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// See <see cref="MathHelper.LerpPrecise"/> for a less efficient version with more precision around edge cases.
        /// </remarks>
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }


        /// <summary>
        /// Linearly interpolates between two values.
        /// This method is a less efficient, more precise version of <see cref="MathHelper.Lerp"/>.
        /// See remarks for more info.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Destination value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns>
        /// <remarks>This method performs the linear interpolation based on the following formula:
        /// <code>((1 - amount) * value1) + (value2 * amount)</code>.
        /// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// This method does not have the floating point precision issue that <see cref="MathHelper.Lerp"/> has.
        /// i.e. If there is a big gap between value1 and value2 in magnitude (e.g. value1=10000000000000000, value2=1),
        /// right at the edge of the interpolation range (amount=1), <see cref="MathHelper.Lerp"/> will return 0 (whereas it should return 1).
        /// This also holds for value1=10^17, value2=10; value1=10^18,value2=10^2... so on.
        /// For an in depth explanation of the issue, see below references:
        /// Relevant Wikipedia Article: https://en.wikipedia.org/wiki/Linear_interpolation#Programming_language_support
        /// Relevant StackOverflow Answer: http://stackoverflow.com/questions/4353525/floating-point-linear-interpolation#answer-23716956
        /// </remarks>
        public static float LerpPrecise(float value1, float value2, float amount)
        {
            return ((1 - amount) * value1) + (value2 * amount);
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static float Max(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static int Max(int value1, int value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static float Min(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static int Min(int value1, int value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Interpolated value.</returns>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            float result = MathHelper.Clamp(amount, 0f, 1f);
            result = MathHelper.Hermite(value1, 0f, value2, 0f, result);

            return result;
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = 180 / pi
        /// </remarks>
        public static float ToDegrees(float radians)
        {
            return (float)(radians * 57.295779513082320876798154814105);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = pi / 180
        /// </remarks>
        public static float ToRadians(float degrees)
        {
            return (float)(degrees * 0.017453292519943295769236907684886);
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        public static float WrapAngle(float angle)
        {
            if ((angle > -Pi) && (angle <= Pi))
                return angle;
            angle %= TwoPi;
            if (angle <= -Pi)
                return angle + TwoPi;
            if (angle > Pi)
                return angle - TwoPi;
            return angle;
        }

        /// <summary>
        /// Determines if value is powered by two.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns><c>true</c> if <c>value</c> is powered by two; otherwise <c>false</c>.</returns>
        public static bool IsPowerOfTwo(int value)
        {
            return (value > 0) && ((value & (value - 1)) == 0);
        }


        private static Random mRandom = new Random();

        public static int Random(int max)
        {
            return Random(0, max);
        }

        public static int Random(int min, int max)
        {
            return mRandom.Next(min, max);
        }

        public static float Random(float max)
        {
            return Random(0f, max);
        }

        /// Rotate a vector
        public static Vector2 Mul(Rot q, Vector2 v)
        {
            return new Vector2(q.c * v.X - q.s * v.Y, q.s * v.X + q.c * v.Y);
        }

        /// Inverse rotate a vector
        public static Vector2 MulT(Rot q, Vector2 v)
        {
            return new Vector2(q.c * v.X + q.s * v.Y, -q.s * v.X + q.c * v.Y);
        }

        public static float Random(float min, float max)
        {
            return (float)mRandom.NextDouble() * (max - min) + min;
        }

        // Determine whether two vectors v1 and v2 point to the same direction
        // v1 = Cross(AB, AC)
        // v2 = Cross(AB, AP)
        private static bool SameSide(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
        {
            Vector3 AB = B - A;
            Vector3 AC = C - A;
            Vector3 AP = P - A;

            Vector3 v1 = Vector3.Cross(AB, AC);
            Vector3 v2 = Vector3.Cross(AB, AP);

            // v1 and v2 should point to the same direction
            return Vector3.Dot(v1, v2) >= 0;
        }

        public static bool PointinTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
        {
            return PointinTriangle(
                new Vector3(A, 0),
                new Vector3(B, 0),
                new Vector3(C, 0),
                new Vector3(P, 0));
        }

        public static bool PointinTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
        {
            return SameSide(A, B, C, P) &&
                    SameSide(B, C, A, P) &&
                    SameSide(C, A, B, P);
        }

        public static bool PointInLine(Vector2 pf, Vector2 p1, Vector2 p2, double range)
        {

            //range 判断的的误差，不需要误差则赋值0
            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
            if (cross <= 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2) return false;

            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;
            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) < range;
        }

        public static bool PointInLine(Point pf, Point p1, Point p2, double range)
        {
            return PointInLine(new Vector2(pf.X, pf.Y), new Vector2(p1.X, p1.Y), new Vector2(p2.X, p2.Y), range);
        }

        public static bool PointInLine(PointF pf, PointF p1, PointF p2, double range)
        {
            return PointInLine(new Vector2(pf.X, pf.Y), new Vector2(p1.X, p1.Y), new Vector2(p2.X, p2.Y), range);
        }

        public static Vector2 Barycentric(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
        {
            Vector3 v0 = C - A;
            Vector3 v1 = B - A;
            Vector3 v2 = P - A;


            var dot00 = Vector3.Dot(v0, v0);
            var dot01 = Vector3.Dot(v0, v1);
            var dot02 = Vector3.Dot(v0, v2);
            var dot11 = Vector3.Dot(v1, v1);
            var dot12 = Vector3.Dot(v1, v2);

            float inverDeno = 1 / (dot00 * dot11 - dot01 * dot01);

            float u = (dot11 * dot02 - dot01 * dot12) * inverDeno;
            /*
            if (u < 0 || u > 1) // if u out of range, return directly
            {
                return false;
            }
            */
            float v = (dot00 * dot12 - dot01 * dot02) * inverDeno;
            /*
            if (v < 0 || v > 1) // if v out of range, return directly
            {
                return false;
            }
            */
            return new Vector2(u, v);
        }

        public static Vector2? GetIntersection(Vector2 lineAStart, Vector2 lineAEnd, Vector2 lineBStart, Vector2 lineBEnd)
        {
            var result = GetIntersection(
                new PointF(lineAStart.X, lineAStart.Y),
                new PointF(lineAEnd.X, lineAEnd.Y),
                new PointF(lineBStart.X, lineBStart.Y),
                new PointF(lineBEnd.X, lineBEnd.Y));

            if (result != null)
                return new Vector2(result.Value.X, result.Value.Y);

            return null;
        }

        public static PointF? GetIntersection(PointF lineAStart, PointF lineAEnd, PointF lineBStart, PointF lineBEnd)
        {
            bool lines_intersect;
            bool segments_intersect;
            PointF intersection;
            PointF close_p1;
            PointF close_p2;

            FindIntersection(
                lineAStart,
                lineAEnd,
                lineBStart,
                lineBEnd,
                out lines_intersect, out segments_intersect, out intersection, out close_p1, out close_p2);

            if (!lines_intersect || !segments_intersect || close_p1 == lineAStart)
                return null;

            return intersection;
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        private static void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointF intersection,
            out PointF close_p1, out PointF close_p2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointF(float.NaN, float.NaN);
                close_p1 = new PointF(float.NaN, float.NaN);
                close_p2 = new PointF(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        /// <summary>
        /// 以中心点旋转Angle角度
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="p1">待旋转的点</param>
        /// <param name="angle">旋转角度（弧度）</param>
        public static Point PointRotate(Point center, Point p1, double angle)
        {
            Point tmp = new Point();
            double angleHude = -angle * Math.PI / 180;/*角度变成弧度*/
            double x1 = (p1.X - center.X) * Math.Cos(angleHude) + (p1.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(p1.X - center.X) * Math.Sin(angleHude) + (p1.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            tmp.X = (int)x1;
            tmp.Y = (int)y1;
            return tmp;
        }

        /// <summary>
        /// 以中心点旋转Angle角度
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="p1">待旋转的点</param>
        /// <param name="angle">旋转角度（弧度）</param>
        public static PointF PointRotate(PointF center, PointF p1, double angle)
        {
            PointF tmp = new PointF();
            double angleHude = -angle * Math.PI / 180;/*角度变成弧度*/
            double x1 = (p1.X - center.X) * Math.Cos(angleHude) + (p1.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(p1.X - center.X) * Math.Sin(angleHude) + (p1.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            tmp.X = (float)x1;
            tmp.Y = (float)y1;
            return tmp;
        }

        /// <summary>
        /// 以中心点旋转Angle角度
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="p1">待旋转的点</param>
        /// <param name="angle">旋转角度（弧度）</param>
        public static Vector2 PointRotate(Vector2 center, Vector2 p1, double angle)
        {
            Vector2 tmp = new Vector2();
            double angleHude = -angle * Math.PI / 180;/*角度变成弧度*/
            double x1 = (p1.X - center.X) * Math.Cos(angleHude) + (p1.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(p1.X - center.X) * Math.Sin(angleHude) + (p1.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            tmp.X = (float)x1;
            tmp.Y = (float)y1;
            return tmp;
        }

        /// <summary>
        /// 以中心点旋转Angle角度
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="p1">待旋转的点</param>
        /// <param name="angle">旋转角度（弧度）</param>
        public static Vector3 PointRotate(Vector3 center, Vector3 p1, double angle)
        {
            Vector3 tmp = new Vector3();
            double angleHude = -angle * Math.PI / 180;/*角度变成弧度*/
            double x1 = (p1.X - center.X) * Math.Cos(angleHude) + (p1.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(p1.X - center.X) * Math.Sin(angleHude) + (p1.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            tmp.X = (float)x1;
            tmp.Y = (float)y1;
            tmp.Z = center.Z;
            return tmp;
        }

        public static Vector3 RotateX(Vector3 point3D, double degrees)
        {
            //Here we use Euler's matrix formula for rotating a 3D point x degrees around the x-axis

            //[ a  b  c ] [ x ]   [ x*a + y*b + z*c ]
            //[ d  e  f ] [ y ] = [ x*d + y*e + z*f ]
            //[ g  h  i ] [ z ]   [ x*g + y*h + z*i ]

            //[ 1    0        0   ]
            //[ 0   cos(x)  sin(x)]
            //[ 0   -sin(x) cos(x)]

            float cDegrees = (float)((Math.PI * degrees) / 180.0f); //Convert degrees to radian for .Net Cos/Sin functions
            float cosDegrees = (float)Math.Cos(cDegrees);
            float sinDegrees = (float)Math.Sin(cDegrees);

            float y = (point3D.Y * cosDegrees) + (point3D.Z * sinDegrees);
            float z = (point3D.Y * -sinDegrees) + (point3D.Z * cosDegrees);

            return new Vector3(point3D.X, y, z);
        }

        public static Vector3 RotateY(Vector3 point3D, double degrees)
        {
            //Y-axis

            //[ cos(x)   0    sin(x)]
            //[   0      1      0   ]
            //[-sin(x)   0    cos(x)]

            float cDegrees = (float)((Math.PI * degrees) / 180.0); //Radians
            float cosDegrees = (float)Math.Cos(cDegrees);
            float sinDegrees = (float)Math.Sin(cDegrees);

            float x = (point3D.X * cosDegrees) + (point3D.Z * sinDegrees);
            float z = (point3D.X * -sinDegrees) + (point3D.Z * cosDegrees);

            return new Vector3(x, point3D.Y, z);
        }

        public static Vector3 RotateZ(Vector3 point3D, double degrees)
        {
            //Z-axis

            //[ cos(x)  sin(x) 0]
            //[ -sin(x) cos(x) 0]
            //[    0     0     1]

            float cDegrees = (float)((Math.PI * degrees) / 180.0); //Radians
            float cosDegrees = (float)Math.Cos(cDegrees);
            float sinDegrees = (float)Math.Sin(cDegrees);

            float x = (point3D.X * cosDegrees) + (point3D.Y * sinDegrees);
            float y = (point3D.X * -sinDegrees) + (point3D.Y * cosDegrees);

            return new Vector3(x, y, point3D.Z);
        }

        public static Vector3 Translate(Vector3 points3D, Vector3 oldOrigin, Vector3 newOrigin)
        {
            //Moves a 3D point based on a moved reference point
            Vector3 difference = new Vector3(newOrigin.X - oldOrigin.X, newOrigin.Y - oldOrigin.Y, newOrigin.Z - oldOrigin.Z);
            points3D.X += difference.X;
            points3D.Y += difference.Y;
            points3D.Z += difference.Z;
            return points3D;
        }

        public static Vector3[] RotateX(Vector3[] points3D, double degrees)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = RotateX(points3D[i], degrees);
            }
            return points3D;
        }

        public static Vector3[] RotateY(Vector3[] points3D, double degrees)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = RotateY(points3D[i], degrees);
            }
            return points3D;
        }

        public static Vector3[] RotateZ(Vector3[] points3D, double degrees)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = RotateZ(points3D[i], degrees);
            }
            return points3D;
        }

        public static Vector3[] Translate(Vector3[] points3D, Vector3 oldOrigin, Vector3 newOrigin)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                points3D[i] = Translate(points3D[i], oldOrigin, newOrigin);
            }
            return points3D;
        }

        /// <summary>
        /// 从俩点间获得旋转角度
        /// </summary>
        public static float GetAngleBetween2Points(Point p1, Point p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            if (xDiff == 0 && yDiff == 0) return 0;

            double angle = Math.Atan2(xDiff, yDiff) * 180.0 / Math.PI;
            return (float)(180 - angle) % 360;
        }

        /// <summary>
        /// 从俩点间获得旋转角度
        /// </summary>
        public static float GetAngleBetween2Points(PointF p1, PointF p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            if (xDiff == 0 && yDiff == 0) return 0;

            double angle = Math.Atan2(xDiff, yDiff) * 180.0 / Math.PI;
            return (float)(180 - angle) % 360;
        }

        /// <summary>
        /// 从俩点间获得旋转角度
        /// </summary>
        public static float GetAngleBetween2Points(Vector2 p1, Vector2 p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            if (xDiff == 0 && yDiff == 0) return 0;

            double angle = Math.Atan2(xDiff, yDiff) * 180.0 / Math.PI;
            return (float)((180 - angle) % 360);
        }

        /// <summary>
        /// 从俩点间获得旋转角度
        /// </summary>
        public static float GetAngle(Vector2 p1, Vector2 p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            if (xDiff == 0 && yDiff == 0) return 0;

            return (float)(Math.Atan2(xDiff, yDiff) * 180.0 / Math.PI);
        }

        /// <summary>
        /// 从俩点间获得旋转角度
        /// </summary>
        public static float GetAngleBetween2Points(Vector3 p1, Vector3 p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;

            if (xDiff == 0 && yDiff == 0) return 0;

            double angle = Math.Atan2(xDiff, yDiff) * 180.0 / Math.PI;
            return (float)((180 - angle) % 360);
        }

        /// <summary>
        /// 根据余弦定理求两个线段夹角
        /// </summary>
        /// <param name="o">端点</param>
        /// <param name="s">start点</param>
        /// <param name="e">end点</param>
        /// <returns></returns>
        public static double GetIncludedAngle(Vector2 o, Vector2 s, Vector2 e)
        {
            double cosfi = 0, fi = 0, norm = 0;
            double dsx = s.X - o.X;
            double dsy = s.Y - o.Y;
            double dex = e.X - o.X;
            double dey = e.Y - o.Y;

            cosfi = dsx * dex + dsy * dey;
            norm = (dsx * dsx + dsy * dsy) * (dex * dex + dey * dey);
            cosfi /= Math.Sqrt(norm);

            if (cosfi >= 1.0) return 0;
            if (cosfi <= -1.0) return Math.PI;
            fi = Math.Acos(cosfi);

            if (180 * fi / Math.PI < 180)
            {
                return 180 * fi / Math.PI;
            }
            else
            {
                return 360 - 180 * fi / Math.PI;
            }
        }

        public static double GetIncludedAngle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            //
            // calculate the angle between the line from p1 to p2
            // and the line from p3 to p4
            //
            double x1 = p1.X - p2.X;
            double y1 = p1.Y - p2.Y;
            double x2 = p3.X - p4.X;
            double y2 = p3.Y - p4.Y;
            //
            double angle1, angle2, angle;
            //
            if (x1 != 0.0f)
                angle1 = Math.Atan(y1 / x1);
            else
                angle1 = Math.PI / 2.0; // 90 degrees
                                        //
            if (x2 != 0.0f)
                angle2 = Math.Atan(y2 / x2);
            else
                angle2 = Math.PI / 2.0; // 90 degrees
                                        //
            angle = Math.Abs(angle2 - angle1);
            angle = angle * 180.0 / Math.PI;    // convert to degrees ???
                                                //
            return angle;
        }

        /// <summary>
        /// 根据角度计算出弧度
        /// </summary>
        /// <param name="angle">角度值</param>
        /// <returns>弧度</returns>
        public static double GetRadian(double angle)
        {
            return angle * Math.PI / 180;
        }

        /// <summary>
        /// 将窗口坐标系中的坐标换算成游戏坐标系中的坐标
        /// </summary>
        public static Vector2 GetGameCoordinate(double angle, Vector2 p, double gridSize)
        {
            if (angle == 0)
            {
                return new Vector2((int)(p.X / gridSize), (int)(p.Y / gridSize));
            }
            else
            {
                double radian = GetRadian(angle);
                return new Vector2(
                    (int)((p.Y / (2 * Math.Cos(radian)) + p.X / (2 * Math.Sin(radian))) / gridSize),
                    (int)((p.Y / (2 * Math.Cos(radian)) - p.X / (2 * Math.Sin(radian))) / gridSize)
                );
            }
        }

        /// <summary>
        /// 同GetGameCoordinate,得到最精确值
        /// </summary>
        public static Vector2 GetAccurateGameCoordinate(double angle, Vector2 p, double gridSize)
        {
            if (angle == 0)
            {
                return new Vector2((float)(p.X / gridSize), (float)(p.Y / gridSize));
            }
            else
            {
                double radian = GetRadian(angle);
                return new Vector2(
                    (float)((p.Y / (2 * Math.Cos(radian)) + p.X / (2 * Math.Sin(radian))) / gridSize),
                    (float)((p.Y / (2 * Math.Cos(radian)) - p.X / (2 * Math.Sin(radian))) / gridSize)
                );
            }
        }

        /// <summary>
        /// 将游戏坐标系中的坐标换算成窗口坐标系中的坐标
        /// </summary>
        public static Vector2 GetWindowCoordinate(double angle, Vector2 p, double gridSize)
        {
            if (angle == 0)
            {
                return new Vector2((float)(p.X * gridSize), (float)(p.Y * gridSize));
            }
            else
            {
                double radian = GetRadian(angle);
                return new Vector2(
                    (float)((p.X - p.Y) * Math.Sin(radian) * gridSize),
                    (float)((p.X + p.Y) * Math.Cos(radian) * gridSize)
                );
            }
        }


        /// <summary>
        /// 获得两点间距离
        /// </summary>
        public static float GetDistance(Point p1, Point p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return (float)distance;
        }

        /// <summary>
        /// 获得两点间距离
        /// </summary>
        public static float GetDistance(PointF p1, PointF p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return (float)distance;
        }

        /// <summary>
        /// 获得两点间距离
        /// </summary>
        public static float GetDistance(Vector2 p1, Vector2 p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return (float)distance;
        }

        /// <summary>
        /// 获得两点间距离
        /// </summary>
        public static double GetDistance64(Vector2 p1, Vector2 p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            return Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// 获得两点间距离
        /// </summary>
        public static float GetDistance(Vector3 p1, Vector3 p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return (float)distance;
        }

        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <returns></returns>
        public static bool GetPointIsInLine(Vector2 pf, Vector2 p1, Vector2 p2, double range)
        {
            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
            if (cross <= 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2) return false;

            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;

            //判断距离是否小于误差
            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) <= range;
        }

        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <returns></returns>
        public static bool GetPointIsInLine(PointF pf, PointF p1, PointF p2, double range)
        {
            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
            if (cross <= 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2) return false;

            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;

            //判断距离是否小于误差
            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) <= range;
        }

        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <returns></returns>
        public static bool GetPointIsInLine(Point pf, Point p1, Point p2, double range)
        {
            double cross = (p2.X - p1.X) * (pf.X - p1.X) + (p2.Y - p1.Y) * (pf.Y - p1.Y);
            if (cross < 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross > d2) return false;

            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;

            //判断距离是否小于误差
            return Math.Sqrt((pf.X - px) * (pf.X - px) + (py - pf.Y) * (py - pf.Y)) <= range;
        }

        /// <summary>
        /// 获得p1点对于p2点角度的延伸点
        /// </summary>
        public static Point GetExtendPoint(Point p1, Point p2, double length)
        {
            var rotation = GetAngleBetween2Points(p1, p2);
            var target = PointRotate(p1, new Point(p1.X, (int)(p1.Y - length)), rotation);
            return target;
        }

        /// <summary>
        /// 获得p1点对于p2点角度的延伸点
        /// </summary>
        public static PointF GetExtendPoint(PointF p1, PointF p2, float length)
        {
            var rotation = GetAngleBetween2Points(p1, p2);
            var target = PointRotate(p1, new PointF(p1.X, p1.Y - length), rotation);
            return target;
        }

        /// <summary>
        /// 获得p1点对于p2点角度的延伸点
        /// </summary>
        public static Vector2 GetExtendPoint(Vector2 p1, Vector2 p2, double length)
        {
            var rotation = GetAngleBetween2Points(p1, p2);
            var target = PointRotate(p1, new Vector2(p1.X, (float)(p1.Y - length)), rotation);
            return target;
        }

        public static double DistanceForPointToABLine(Vector2 p, Vector2 start, Vector2 end)//所在点到AB线段的垂线长度
        {
            var x = p.X;
            var y = p.Y;
            var x1 = start.X;
            var y1 = start.Y;
            var x2 = end.X;
            var y2 = end.Y;

            float reVal = 0f;
            bool retData = false;

            float cross = (x2 - x1) * (x - x1) + (y2 - y1) * (y - y1);
            if (cross <= 0)
            {
                reVal = (float)Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                retData = true;
            }

            float d2 = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            if (cross >= d2)
            {
                reVal = (float)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
                retData = true;
            }

            if (!retData)
            {
                float r = cross / d2;
                float px = x1 + (x2 - x1) * r;
                float py = y1 + (y2 - y1) * r;
                reVal = (float)Math.Sqrt((x - px) * (x - px) + (py - y) * (py - y));
            }

            return reVal;

        }


        /// <summary>
        ///  点到线段最短距离的那条直线与线段的交点，{x=...,y=...}
        /// </summary>
        /// <param name="x">线段外的点的x坐标</param>
        /// <param name="y">线段外的点的y坐标</param>
        /// <param name="x1">线段顶点1的x坐标</param>
        /// <param name="y1">线段顶点1的y坐标</param>
        /// <param name="x2">线段顶点2的x坐标</param>
        /// <param name="y2">线段顶点2的y坐标</param>
        /// <returns></returns>
        public static Vector2 PointForPointToABLine(Vector2 p, Vector2 start, Vector2 end)
        {
            var x = p.X;
            var y = p.Y;
            var x1 = start.X;
            var y1 = start.Y;
            var x2 = end.X;
            var y2 = end.Y;

            Vector2 reVal = new Vector2();
            // 直线方程的两点式转换成一般式
            // A = Y2 - Y1
            // B = X1 - X2
            // C = X2*Y1 - X1*Y2
            float a1 = y2 - y1;
            float b1 = x1 - x2;
            float c1 = x2 * y1 - x1 * y2;
            float x3, y3;
            if (a1 == 0)
            {
                // 线段与x轴平行
                reVal = new Vector2(x, y1);
                x3 = x;
                y3 = y1;
            }
            else if (b1 == 0)
            {
                // 线段与y轴平行
                reVal = new Vector2(x1, y);
                x3 = x1;
                y3 = y;
            }
            else
            {
                // 普通线段
                float k1 = -a1 / b1;
                float k2 = -1 / k1;
                float a2 = k2;
                float b2 = -1;
                float c2 = y - k2 * x;
                // 直线一般式和二元一次方程的一般式转换
                // 直线的一般式为 Ax+By+C=0
                // 二元一次方程的一般式为 Ax+By=C
                c1 = -c1;
                c2 = -c2;

                // 二元一次方程求解(Ax+By=C)
                // a=a1,b=b1,c=c1,d=a2,e=b2,f=c2;
                // X=(ce-bf)/(ae-bd)
                // Y=(af-cd)/(ae-bd)
                x3 = (c1 * b2 - b1 * c2) / (a1 * b2 - b1 * a2);
                y3 = (a1 * c2 - c1 * a2) / (a1 * b2 - b1 * a2);
            }
            // 点(x3,y3)作为点(x,y)到(x1,y1)和(x2,y2)组成的直线距离最近的点,那(x3,y3)是否在(x1,y1)和(x2,y2)的线段之内(包含(x1,y1)和(x2,y2))
            if (((x3 > x1) != (x3 > x2) || x3 == x1 || x3 == x2) && ((y3 > y1) != (y3 > y2) || y3 == y1 || y3 == y2))
            {
                // (x3,y3)在线段上
                reVal = new Vector2(x3, y3);
            }
            else
            {
                // (x3,y3)在线段外
                float d1_quadratic = (x - x1) * (x - x1) + (y - y1) * (y - y1);
                float d2_quadratic = (x - x2) * (x - x2) + (y - y2) * (y - y2);
                if (d1_quadratic <= d2_quadratic)
                {
                    reVal = new Vector2(x1, y1);
                }
                else
                {
                    reVal = new Vector2(x2, y2);
                }
            }
            return reVal;
        }

        /// <summary>
        /// 获得p1点对于p2点角度的延伸点
        /// </summary>
        public static Vector3 GetExtendPoint(Vector3 p1, Vector3 p2, double length)
        {
            var rotation = GetAngleBetween2Points(p1, p2);
            var target = PointRotate(p1, new Vector3(p1.X, (float)(p1.Y - length), p2.Z), rotation);
            return target;
        }

        /// <summary>
        /// 获得2点间中心点
        /// </summary>
        public static Vector2 GetCenterPoint(Vector2 p1, Vector2 p2)
        {
            return GetExtendPoint(p1, p2, GetDistance(p1, p2) / 2);
        }

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <returns>Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.</returns>
        public static float Area(Vector2 a, Vector2 b, Vector2 c)
        {
            return Area(ref a, ref b, ref c);
        }

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <returns>Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.</returns>
        public static float Area(ref Vector2 a, ref Vector2 b, ref Vector2 c)
        {
            return a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y);
        }

        /// <summary>
        /// 计算三角形面积
        /// </summary>
        public static double TriangleArea(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            //为三个边长赋初值
            var a = GetDistance64(p1, p2);
            var b = GetDistance64(p2, p3);
            var c = GetDistance64(p3, p1);
            //计算半周长
            var p = (a + b + c) / 2;
            //计算面积
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static float Cross(ref Vector2 a, ref Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static void Cross(ref Vector2 a, ref Vector2 b, out float c)
        {
            c = a.X * b.Y - a.Y * b.X;
        }

        /// <summary>
        /// Determines if three vertices are collinear (ie. on a straight line)
        /// </summary>
        /// <param name="a">First vertex</param>
        /// <param name="b">Second vertex</param>
        /// <param name="c">Third vertex</param>
        /// <param name="tolerance">The tolerance</param>
        /// <returns></returns>
        public static bool IsCollinear(ref Vector2 a, ref Vector2 b, ref Vector2 c, float tolerance = 0)
        {
            return FloatInRange(Area(ref a, ref b, ref c), -tolerance, tolerance);
        }

        public static bool IsHollow(List<Vector3> curveloopPoints)
        {
            //使用角度和判断凹凸性：凸多边形的内角和为（n-2）*180° 
            var num = curveloopPoints.Count;
            float angleSum = 0.0f;
            for (int i = 0; i < num; i++)
            {
                Vector3 e1;
                if (i == 0)
                {
                    e1 = curveloopPoints[num - 1] - curveloopPoints[i];
                }
                else
                {
                    e1 = curveloopPoints[i - 1] - curveloopPoints[i];
                }
                Vector3 e2;
                if (i == num - 1)
                {
                    e2 = curveloopPoints[0] - curveloopPoints[i];
                }
                else
                {
                    e2 = curveloopPoints[i + 1] - curveloopPoints[i];
                }
                //标准化
                //e1.Normalize(); e2.Normalize();
                //计算点乘
                float mdot = Vector3.Dot(e1, e2);
                //计算夹角弧度
                float theta = (float)Math.Acos(mdot);
                //注意计算内角
                angleSum += theta;
            }
            //计算内角和 
            float convexAngleSum = (float)((num - 2)) * (float)Math.PI;
            //判断凹凸性 
            if (angleSum < (convexAngleSum - (num * 0.00001)))
            {
                return true;//是凹 
            }
            return false;//否则是凸 
        }

        public static bool IsRectangle(Point[] points)
        {
            var max = GetMaxBox(points);
            var min = GetMinBox(points);
            return min == max;
        }

        // function to find if given point 
        // lies inside a given rectangle or not. 
        public static bool FindPoint(Rectangle rect, Point p)
        {
            var x1 = rect.X;
            var y1 = rect.Y;
            var x2 = rect.X + rect.Width;
            var y2 = rect.Y + rect.Height;

            var x = p.X;
            var y = p.Y;

            if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                return true;

            return false;
        }

        public static Rectangle GetMinBox(params Point[] points)
        {
            // Find the MinMax quadrilateral.
            Point ul = new Point(0, 0), ur = ul, ll = ul, lr = ul;
            GetMinMaxCorners(points, ref ul, ref ur, ref ll, ref lr);

            // Get the coordinates of a box that lies inside this quadrilateral.
            int xmin, xmax, ymin, ymax;
            xmin = ul.X;
            ymin = ul.Y;

            xmax = ur.X;
            if (ymin < ur.Y) ymin = ur.Y;

            if (xmax > lr.X) xmax = lr.X;
            ymax = lr.Y;

            if (xmin < ll.X) xmin = ll.X;
            if (ymax > ll.Y) ymax = ll.Y;

            var result = new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
            return result;
        }

        public static Rectangle GetMaxBox(params Point[] points)
        {
            int left = int.MaxValue, right = int.MinValue, top = int.MaxValue, bottom = int.MinValue;
            for (var i = 0; i < points.Length; i++)
            {
                var x1 = (int)points[i].X;
                var y1 = (int)points[i].Y;

                if (x1 < left) left = x1;
                if (x1 > right) right = x1;
                if (y1 < top) top = y1;
                if (y1 > bottom) bottom = y1;
            }
            return new Rectangle(left, top, right - left, bottom - top);
        }

        public static AABB GetMaxBox(params Vector2[] points)
        {
            float left = float.MaxValue, right = float.MinValue, top = float.MaxValue, bottom = float.MinValue;
            for (var i = 0; i < points.Length; i++)
            {
                var x1 = points[i].X;
                var y1 = points[i].Y;

                if (x1 < left) left = x1;
                if (x1 > right) right = x1;
                if (y1 < top) top = y1;
                if (y1 > bottom) bottom = y1;
            }
            return new AABB(left, top, right - left, bottom - top);
        }

        private static void GetMinMaxCorners(Point[] points, ref Point ul, ref Point ur, ref Point ll, ref Point lr)
        {
            // Start with the first point as the solution.
            ul = points[0];
            ur = ul;
            ll = ul;
            lr = ul;

            // Search the other points.
            foreach (Point pt in points)
            {
                if (-pt.X - pt.Y > -ul.X - ul.Y) ul = pt;
                if (pt.X - pt.Y > ur.X - ur.Y) ur = pt;
                if (-pt.X + pt.Y > -ll.X + ll.Y) ll = pt;
                if (pt.X + pt.Y > lr.X + lr.Y) lr = pt;
            }
        }

        /// <summary>
        /// Checks if a floating point Value is within a specified
        /// range of values (inclusive).
        /// </summary>
        /// <param name="value">The Value to check.</param>
        /// <param name="min">The minimum Value.</param>
        /// <param name="max">The maximum Value.</param>
        /// <returns>True if the Value is within the range specified,
        /// false otherwise.</returns>
        public static bool FloatInRange(float value, float min, float max)
        {
            return (value >= min && value <= max);
        }

        public static bool FloatEquals(float value1, float value2)
        {
            return Math.Abs(value1 - value2) <= Settings.Epsilon;
        }

        /// <summary>
        /// Checks if a floating point Value is equal to another,
        /// within a certain tolerance.
        /// </summary>
        /// <param name="value1">The first floating point Value.</param>
        /// <param name="value2">The second floating point Value.</param>
        /// <param name="delta">The floating point tolerance.</param>
        /// <returns>True if the values are "equal", false otherwise.</returns>
        public static bool FloatEquals(float value1, float value2, float delta)
        {
            return FloatInRange(value1, value2 - delta, value2 + delta);
        }


        /// <summary>
        /// 获得小数位数
        /// </summary>
        public static int DecimalCount(double value)
        {
            var numStr = value.ToString();
            var decimalIndex = numStr.IndexOf('.');
            if (decimalIndex == -1)
                return 0;
            else
                return numStr.Length - decimalIndex - 1;
        }

        /// <summary>
        /// 获得最大公约数
        /// </summary>
        public static int CommonDivisor(params int[] nums)
        {
            return nums.Aggregate(CommonDivisor);
        }

        /// <summary>
        /// 获得最大公约数
        /// </summary>
        public static int CommonDivisor(int num1, int num2)
        {
            int tmp;
            if (num1 < num2)
            {
                tmp = num1; num1 = num2; num2 = tmp;
            }
            int a = num1; int b = num2;
            while (b != 0)
            {
                tmp = a % b;
                a = b;
                b = tmp;
            }

            return a;
        }

        /// <summary>
        /// 获得最小公倍数
        /// </summary>
        public static int CommonMultiple(int num1, int num2)
        {
            int tmp;
            if (num1 < num2)
            {
                tmp = num1; num1 = num2; num2 = tmp;
            }
            int a = num1; int b = num2;
            while (b != 0)
            {
                tmp = a % b;
                a = b;
                b = tmp;
            }

            return num1 * num2 / a;
        }

        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static Vector3 ToVector3(this Vector2 v)
        {
            return new Vector3(v.X, v.Y, 0);
        }

        public static bool IsNumeric(string str)
        {
            double value = 0;
            return Double.TryParse(str, out value);

            var rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            var result = -1;
            if (rex.IsMatch(str))
            {
                result = int.Parse(str);
                return true;
            }
            else
                return false;
        }

        private class PolygonHierachy
        {
            public Vertices Current;
            public List<PolygonHierachy> Childs;
            public PolygonHierachy Next;

            public PolygonHierachy(Vertices polygon)
            {
                Current = polygon;
                Childs = new List<PolygonHierachy>();
            }
        }

        public struct PolygonCollisionResult
        {
            public bool WillIntersect; // Are the polygons going to intersect forward in time?
            public bool Intersect; // Are the polygons currently intersecting
            public Vector2 MinimumTranslationVector; // The translation to apply to polygon A to push the polygons appart.
        }

        // Check if polygon A is going to collide with polygon B for the given velocity
        public static PolygonCollisionResult PolygonCollision(Vertices polygonA, Vertices polygonB, Vector2 velocity)
        {
            PolygonCollisionResult result = new PolygonCollisionResult();
            result.Intersect = true;
            result.WillIntersect = true;

            int edgeCountA = polygonA.Count;
            int edgeCountB = polygonB.Count;
            float minIntervalDistance = float.PositiveInfinity;
            Vector2 translationAxis = new Vector2();
            Vector2 edge;

            // Loop through all the edges of both polygons
            for (int edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB; edgeIndex++)
            {
                if (edgeIndex < edgeCountA)
                {
                    edge = polygonA.GetEdge(edgeIndex);
                }
                else
                {
                    edge = polygonB.GetEdge(edgeIndex - edgeCountA);
                }

                // ===== 1. Find if the polygons are currently intersecting =====

                // Find the axis perpendicular to the current edge
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                //axis.Normalize();

                // Find the projection of the polygon on the current axis
                float minA = 0; float minB = 0; float maxA = 0; float maxB = 0;
                ProjectPolygon(axis, polygonA, ref minA, ref maxA);
                ProjectPolygon(axis, polygonB, ref minB, ref maxB);

                // Check if the polygon projections are currentlty intersecting
                if (IntervalDistance(minA, maxA, minB, maxB) > 0) result.Intersect = false;

                // ===== 2. Now find if the polygons *will* intersect =====

                // Project the velocity on the current axis
                float velocityProjection = Vector2.Dot(axis, velocity); // axis.DotProduct(velocity);

                // Get the projection of polygon A during the movement
                if (velocityProjection < 0)
                {
                    minA += velocityProjection;
                }
                else
                {
                    maxA += velocityProjection;
                }

                // Do the same test as above for the new projection
                float intervalDistance = IntervalDistance(minA, maxA, minB, maxB);
                if (intervalDistance > 0) result.WillIntersect = false;

                // If the polygons are not intersecting and won't intersect, exit the loop
                if (!result.Intersect && !result.WillIntersect) break;

                // Check if the current interval distance is the minimum one. If so store
                // the interval distance and the current distance.
                // This will be used to calculate the minimum translation vector
                intervalDistance = Math.Abs(intervalDistance);
                if (intervalDistance < minIntervalDistance)
                {
                    minIntervalDistance = intervalDistance;
                    translationAxis = axis;

                    Vector2 d = polygonA.GetCentroid() - polygonB.GetCentroid();
                    if (Vector2.Dot(d, translationAxis) < 0) translationAxis = -translationAxis;
                }
            }

            // The minimum translation vector can be used to push the polygons appart.
            // First moves the polygons by their velocity
            // then move polygonA by MinimumTranslationVector.
            if (result.WillIntersect) result.MinimumTranslationVector = translationAxis * minIntervalDistance;

            return result;
        }

        // Calculate the distance between [minA, maxA] and [minB, maxB]
        // The distance will be negative if the intervals overlap
        public static float IntervalDistance(float minA, float maxA, float minB, float maxB)
        {
            if (minA < minB)
            {
                return minB - maxA;
            }
            else
            {
                return minA - maxB;
            }
        }

        // Calculate the projection of a polygon on an axis and returns it as a [min, max] interval
        public static void ProjectPolygon(Vector2 axis, Vertices polygon, ref float min, ref float max)
        {
            // To project a point on an axis use the dot product
            float d = Vector2.Dot(axis, polygon[0]);
            min = d;
            max = d;
            for (int i = 0; i < polygon.Count; i++)
            {
                d = Vector2.Dot(polygon[i], axis);
                if (d < min)
                {
                    min = d;
                }
                else
                {
                    if (d > max)
                    {
                        max = d;
                    }
                }
            }
        }

        public static Vertices CombinePolygon(Vertices v1, Vertices v2)
        {
            var v3 = GetClipPolygonSegment(v1, v2);
            var v4 = GetClipPolygonSegment(v2, v1);
            var result = new Vertices();

            //result.AddRange(v3.SelectMany(X => X.Vertices));

            if (v4.Count > 0)
            {
                for (var i = 0; i < v4.Count - 1; i++)
                {
                    result.AddRange(v4[i].Vertices);
                }

                result.InsertRange(0, v4.Last().Vertices);
                result = new Vertices(result.Distinct());

                foreach (var seg in v3)
                {
                    var startIndex = result.IndexOf(seg.Start);
                    var endIndex = result.LastIndexOf(seg.End);

                    if ((startIndex < endIndex || endIndex == -1) && startIndex != -1)
                    {

                        result.InsertRange(startIndex, seg.Vertices);
                        //result = new Vertices(result.Distinct());
                    }
                    else if (endIndex != -1)
                    {

                        result.InsertRange(0, seg.Vertices);
                    }
                }
            }

            return new Vertices(result.Distinct());
        }

        public static Vertices GetClipPolygon(Vertices polygon, Vertices clip)
        {
            Vertices v = new Vertices();
            var intersectPoints = IntersectionPolygons(polygon, clip).ToArray();

            if (intersectPoints.Length % 2 == 0 && intersectPoints.Length > 1)
            {
                for (var c = 0; c < clip.Count; c++)
                {

                    for (var k = 0; k < intersectPoints.Length; k = k + 2)
                    {
                        var start = intersectPoints[k];
                        var end = intersectPoints[k + 1];


                        if (!start.Reverse)
                        {
                            if (c == start.Index)
                            {
                                v.Add(end.Point);
                            }

                            if (c > start.Index && c < end.Index + 1)
                            {
                                v.Add(clip[c]);
                            }

                            if (c == start.Index)
                            {
                                v.Add(start.Point);
                            }
                        }
                        else
                        {
                            if (c > start.Index && c < end.Index + 1)
                            {
                            }
                            else
                            {
                                v.Add(clip[c]);
                            }

                            if (c == start.Index)
                            {
                                v.Add(start.Point);
                                v.Add(end.Point);
                            }
                        }
                    }
                }
            }

            return v;
        }

        public static List<IndexSegment> GetClipPolygonSegment(Vertices polygon, Vertices clip)
        {
            var result = new List<IndexSegment>();

            var intersectPoints = IntersectionPolygons(polygon, clip).ToList();
            if (intersectPoints.Count == 0 || intersectPoints.Count % 2 != 0)
                return result;

            Queue<IndexPoint> queue = new Queue<IndexPoint>(intersectPoints);
            var frist = queue.Peek();
            if (frist.Reverse)
            {
                queue.Enqueue(queue.Dequeue());
            }

            while (queue.Count > 0)
            {
                var start = queue.Dequeue();
                var end = queue.Dequeue();

                if (!clip.Contains(start.Point) && !clip.Contains(end.Point))
                {

                    IndexSegment seg = new IndexSegment(start.Point, end.Point);

                    if (end == frist)
                    {
                        seg.Vertices.Add(start.Point);
                        for (var i = start.Index; i < clip.Count; i++)
                        {
                            var index = i == clip.Count - 1 ? 0 : i + 1;

                            seg.Vertices.Add(clip[index]);
                        }

                        for (var i = 0; i < end.Index; i++)
                        {
                            seg.Vertices.Add(clip[i + 1]);
                        }
                        seg.Vertices.Add(end.Point);
                    }
                    else
                    {
                        seg.Vertices.Add(start.Point);

                        for (var i = start.Index + 1; i < end.Index + 1; i++)
                        {
                            seg.Vertices.Add(clip[i]);
                        }
                        seg.Vertices.Add(end.Point);
                    }

                    result.Add(seg);
                }
                else
                {
                    IndexSegment seg = new IndexSegment(start.Point, end.Point);
                    //seg.Vertices.Add(start.Point);

                    result.Add(seg);
                }
            }

            return result;
        }

        public static Vertices GetMaskPolygon(Vertices polygon, Vertices mask)
        {
            Vertices v = new Vertices();
            var intersectPoints = IntersectionPolygons(polygon, mask).ToArray();

            if (intersectPoints.Length == 2 && intersectPoints.Length > 1)
            {
                for (var c = 0; c < mask.Count; c++)
                {

                    for (var k = intersectPoints.Length - 2; k >= 0; k = k - 2)
                    {
                        var start = intersectPoints[k];
                        var end = intersectPoints[k + 1];


                        if (start.Reverse)
                        {
                            if (c == start.Index)
                            {
                                v.Add(end.Point);
                            }

                            if (c > start.Index && c < end.Index + 1)
                            {
                                v.Add(mask[c]);
                            }

                            if (c == start.Index)
                            {
                                v.Add(start.Point);
                            }
                        }
                        else
                        {
                            if (c > start.Index && c < end.Index + 1)
                            {
                            }
                            else
                            {
                                v.Add(mask[c]);
                            }

                            if (c == start.Index)
                            {
                                v.Add(start.Point);
                                v.Add(end.Point);
                            }
                        }
                    }
                }
            }

            v.AddRange(mask);

            return v;
        }

        class IndexPointComparer : IEqualityComparer<IndexPoint>
        {
            public bool Equals(IndexPoint x, IndexPoint y)
            {
                return x.Point == y.Point;
            }

            public int GetHashCode(IndexPoint obj)
            {
                return obj.Point.GetHashCode();
            }
        }

        public class IndexPoint
        {
            public int Index { get; set; }
            public Vector2 Point { get; set; }
            public bool Reverse { get; set; }
        }

        public class IndexSegment
        {
            public Vertices Vertices { get; set; }
            public Vector2 Start { get; set; }
            public Vector2 End { get; set; }

            public IndexSegment(Vector2 start, Vector2 end)
            {
                Start = start;
                End = end;
                Vertices = new Vertices();
            }
        }

        public static IEnumerable<IndexPoint> IntersectionPolygons(Vertices v1, Vertices v2)
        {
            List<IndexPoint> result = new List<IndexPoint>();
            var temp = false;

            for (var i = 0; i < v2.Count; i++)
            {
                var curr = v2[i];
                var next = v2[i == v2.Count - 1 ? 0 : i + 1];

                foreach (var v in ClipLineWithPolygon(out temp, curr, next, v1))
                {
                    result.Add(new IndexPoint() { Index = i, Point = v, Reverse = temp });

                }
            }

            return result;
        }

        // Return points where the segment enters and leaves the polygon.
        public static Vector2[] ClipLineWithPolygon(out bool starts_outside_polygon, Vector2 point1, Vector2 point2, List<Vector2> polygon_points)
        {
            // Make lists to hold points of
            // intersection and their t values.
            List<Vector2> intersections = new List<Vector2>();
            List<float> t_values = new List<float>();

            // Add the segment's starting point.
            //intersections.Add(point1);
            //t_values.Add(0f);
            starts_outside_polygon =
                !PointIsInPolygon(point1.X, point1.Y,
                    polygon_points.ToArray());

            // Examine the polygon's edges.
            for (int i1 = 0; i1 < polygon_points.Count; i1++)
            {
                // Get the end points for this edge.
                int i2 = (i1 + 1) % polygon_points.Count;

                // See where the edge intersects the segment.
                bool lines_intersect, segments_intersect;
                Vector2 intersection, close_p1, close_p2;
                float t1, t2;
                FindIntersection(point1, point2,
                    polygon_points[i1], polygon_points[i2],
                    out lines_intersect, out segments_intersect,
                    out intersection, out close_p1, out close_p2,
                    out t1, out t2);

                // See if the segment intersects the edge.
                if (segments_intersect)
                {
                    // See if we need to record this intersection.

                    // Record this intersection.
                    intersections.Add(intersection);
                    t_values.Add(t1);
                }
            }

            // Add the segment's ending point.
            //intersections.Add(point2);
            //t_values.Add(1f);

            // Sort the points of intersection by t value.
            Vector2[] intersections_array = intersections.ToArray();
            float[] t_array = t_values.ToArray();
            Array.Sort(t_array, intersections_array);

            // Return the intersections.
            return intersections_array;
        }

        private static void FindIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4,
              out bool lines_intersect, out bool segments_intersect,
              out Vector2 intersection, out Vector2 close_p1, out Vector2 close_p2,
              out float t1, out float t2)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);
            t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new Vector2(float.NaN, float.NaN);
                close_p1 = new Vector2(float.NaN, float.NaN);
                close_p2 = new Vector2(float.NaN, float.NaN);
                t2 = float.PositiveInfinity;
                return;
            }
            lines_intersect = true;

            t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

            // Find the point of intersection.
            intersection = new Vector2(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0) t1 = 0;
            else if (t1 > 1) t1 = 1;

            if (t2 < 0) t2 = 0;
            else if (t2 > 1) t2 = 1;

            close_p1 = new Vector2(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new Vector2(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        // Return true if the point is in the polygon.
        public static bool PointIsInPolygon(float X, float Y, Vector2[] polygon_points)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon_points.Length - 1;
            float total_angle = GetAngle(
                polygon_points[max_point].X, polygon_points[max_point].Y,
                X, Y,
                polygon_points[0].X, polygon_points[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    polygon_points[i].X, polygon_points[i].Y,
                    X, Y,
                    polygon_points[i + 1].X, polygon_points[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 0.00001);
        }

        // Return the angle ABC.
        // Return a value between PI and -PI.
        // Note that the value is the opposite of what you might
        // expect because Y coordinates increase downward.
        public static float GetAngle(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            // Get the dot product.
            float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return (float)Math.Atan2(cross_product, dot_product);
        }

        // Return the dot product AB · BC.
        // Note that AB · BC = |AB| * |BC| * Cos(theta).
        private static float DotProduct(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        // Return the cross product AB x BC.
        // The cross product is a vector perpendicular to AB
        // and BC having length |AB| * |BC| * Sin(theta) and
        // with direction given by the right-hand rule.
        // For two vectors in the X-Y plane, the result is a
        // vector with X and Y components 0 so the Z component
        // gives the vector's length and direction.
        public static float CrossProductLength(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }
    }
}
