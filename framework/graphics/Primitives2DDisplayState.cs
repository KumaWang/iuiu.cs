using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace engine.framework.graphics
{
    /// <summary>
    /// </summary>
    public static class Primitives2DDisplayState 
	{
		#region Private Members

		private static readonly Dictionary<String, Vertices> circleCache = new Dictionary<string, Vertices>();
        private static readonly Dictionary<GLRenderer, Image> pixels = new Dictionary<GLRenderer, Image>();
		//private static readonly Dictionary<String, List<Vector2>> arcCache = new Dictionary<string, List<Vector2>>();

        public static Image GetPixel(GLRenderer g) 
        {
            if (g == null)
                return pixels.First().Value;

            if (!pixels.ContainsKey(g))
                pixels[g] = CreateThePixel(g);

            return pixels[g];
        }

		#endregion

		#region Private Methods

        private static Image CreateThePixel(GLRenderer spriteBatch)
		{
			var tex2D = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false);
            var bmp = new System.Drawing.Bitmap(1, 1);
            bmp.SetPixel(0, 0, System.Drawing.Color.White);

            tex2D.SetData(bmp);
            return spriteBatch.ImageFromTexture2D(tex2D);
		}


		/// <summary>
		/// Draws a list of connecting points
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// /// <param name="position">Where to position the points</param>
		/// <param name="points">The points to connect with lines</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the lines</param>
		private static void DrawPoints(GLRenderer spriteBatch, Vector2 position, List<Vector2> points, Color color, float thickness)
		{
			if (points.Count < 2)
				return;

			for (int i = 1; i < points.Count; i++)
			{
				DrawLine(spriteBatch, points[i - 1] + position, points[i] + position, color, thickness);
			}
		}

        /// <summary>
        /// Creates a list of vectors that represents a circle
        /// </summary>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <returns>A list of vectors that, if connected, will create a circle</returns>
        public static Vertices CreateCircle(float radius, int sides)
		{
            return CreateEllipse(radius, radius, sides);
		}

        public static Vertices CreateRoundedRectangle(float width, float height, float xRadius, float yRadius, int segments)
        {
            if (yRadius > height / 2 || xRadius > width / 2)
                throw new Exception("Rounding amount can't be more than half the height and width respectively.");
            if (segments < 0)
                throw new Exception("Segments must be zero or more.");

            //We need at least 8 vertices to create a rounded rectangle
            Debug.Assert(Settings.MaxPolygonVertices >= 8);

            Vertices vertices = new Vertices();
            if (segments == 0)
            {
                vertices.Add(new Vector2(width * .5f - xRadius, -height * .5f));
                vertices.Add(new Vector2(width * .5f, -height * .5f + yRadius));

                vertices.Add(new Vector2(width * .5f, height * .5f - yRadius));
                vertices.Add(new Vector2(width * .5f - xRadius, height * .5f));

                vertices.Add(new Vector2(-width * .5f + xRadius, height * .5f));
                vertices.Add(new Vector2(-width * .5f, height * .5f - yRadius));

                vertices.Add(new Vector2(-width * .5f, -height * .5f + yRadius));
                vertices.Add(new Vector2(-width * .5f + xRadius, -height * .5f));
            }
            else
            {
                int numberOfEdges = (segments * 4 + 8);

                float stepSize = MathHelper.TwoPi / (numberOfEdges - 4);
                int perPhase = numberOfEdges / 4;

                Vector2 posOffset = new Vector2(width / 2 - xRadius, height / 2 - yRadius);
                vertices.Add(posOffset + new Vector2(xRadius, -yRadius + yRadius));
                short phase = 0;
                for (int i = 1; i < numberOfEdges; i++)
                {
                    if (i - perPhase == 0 || i - perPhase * 3 == 0)
                    {
                        posOffset.X *= -1;
                        phase--;
                    }
                    else if (i - perPhase * 2 == 0)
                    {
                        posOffset.Y *= -1;
                        phase--;
                    }

                    vertices.Add(posOffset + new Vector2(xRadius * (float)Math.Cos(stepSize * -(i + phase)),
                                                         -yRadius * (float)Math.Sin(stepSize * -(i + phase))));
                }
            }

            return vertices;
        }

        public static Vertices CreateArc(float radians, int sides, float radius)
        {
            Debug.Assert(radians > 0, "The arc needs to be larger than 0");
            Debug.Assert(sides > 1, "The arc needs to have more than 1 sides");
            Debug.Assert(radius > 0, "The arc needs to have a radius larger than 0");

            Vertices vertices = new Vertices();

            float stepSize = radians / sides;
            for (int i = sides - 1; i > 0; i--)
            {
                vertices.Add(new Vector2(radius * (float)Math.Cos(stepSize * i),
                                         radius * (float)Math.Sin(stepSize * i)));
            }

            return vertices;
        }

        //Capsule contributed by Yobiv
        /// <summary>
        /// Creates an capsule with the specified height, radius and number of edges.
        /// A capsule has the same form as a pill capsule.
        /// </summary>
        /// <param name="height">Height (inner height + 2 * radius) of the capsule.</param>
        /// <param name="endRadius">Radius of the capsule ends.</param>
        /// <param name="edges">The number of edges of the capsule ends. The more edges, the more it resembles an capsule</param>
        /// <returns></returns>
        public static Vertices CreateCapsule(float height, float endRadius, int edges)
        {
            if (endRadius >= height / 2)
                throw new ArgumentException(
                    "The radius must be lower than height / 2. Higher values of radius would create a circle, and not a half circle.",
                    "endRadius");

            return CreateCapsule(height, endRadius, edges, endRadius, edges);
        }

        /// <summary>
        /// Creates an capsule with the specified  height, radius and number of edges.
        /// A capsule has the same form as a pill capsule.
        /// </summary>
        /// <param name="height">Height (inner height + radii) of the capsule.</param>
        /// <param name="topRadius">Radius of the top.</param>
        /// <param name="topEdges">The number of edges of the top. The more edges, the more it resembles an capsule</param>
        /// <param name="bottomRadius">Radius of bottom.</param>
        /// <param name="bottomEdges">The number of edges of the bottom. The more edges, the more it resembles an capsule</param>
        /// <returns></returns>
        public static Vertices CreateCapsule(float height, float topRadius, int topEdges, float bottomRadius, int bottomEdges)
        {
            if (height <= 0)
                throw new ArgumentException("Height must be longer than 0", "height");

            if (topRadius <= 0)
                throw new ArgumentException("The top radius must be more than 0", "topRadius");

            if (topEdges <= 0)
                throw new ArgumentException("Top edges must be more than 0", "topEdges");

            if (bottomRadius <= 0)
                throw new ArgumentException("The bottom radius must be more than 0", "bottomRadius");

            if (bottomEdges <= 0)
                throw new ArgumentException("Bottom edges must be more than 0", "bottomEdges");

            if (topRadius >= height / 2)
                throw new ArgumentException(
                    "The top radius must be lower than height / 2. Higher values of top radius would create a circle, and not a half circle.",
                    "topRadius");

            if (bottomRadius >= height / 2)
                throw new ArgumentException(
                    "The bottom radius must be lower than height / 2. Higher values of bottom radius would create a circle, and not a half circle.",
                    "bottomRadius");

            Vertices vertices = new Vertices();

            float newHeight = (height - topRadius - bottomRadius) * 0.5f;

            // top
            vertices.Add(new Vector2(topRadius, newHeight));

            float stepSize = MathHelper.Pi / topEdges;
            for (int i = 1; i < topEdges; i++)
            {
                vertices.Add(new Vector2(topRadius * (float)Math.Cos(stepSize * i),
                                         topRadius * (float)Math.Sin(stepSize * i) + newHeight));
            }

            vertices.Add(new Vector2(-topRadius, newHeight));

            // bottom
            vertices.Add(new Vector2(-bottomRadius, -newHeight));

            stepSize = MathHelper.Pi / bottomEdges;
            for (int i = 1; i < bottomEdges; i++)
            {
                vertices.Add(new Vector2(-bottomRadius * (float)Math.Cos(stepSize * i),
                                         -bottomRadius * (float)Math.Sin(stepSize * i) - newHeight));
            }

            vertices.Add(new Vector2(bottomRadius, -newHeight));

            return vertices;
        }

        /// <summary>
        /// Creates a gear shape with the specified radius and number of teeth.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="numberOfTeeth">The number of teeth.</param>
        /// <param name="tipPercentage">The tip percentage.</param>
        /// <param name="toothHeight">Height of the tooth.</param>
        /// <returns></returns>
        public static Vertices CreateGear(float radius, int numberOfTeeth, float tipPercentage, float toothHeight)
        {
            Vertices vertices = new Vertices();

            float stepSize = MathHelper.TwoPi / numberOfTeeth;
            tipPercentage /= 100f;
            MathHelper.Clamp(tipPercentage, 0f, 1f);
            float toothTipStepSize = (stepSize / 2f) * tipPercentage;

            float toothAngleStepSize = (stepSize - (toothTipStepSize * 2f)) / 2f;

            for (int i = numberOfTeeth - 1; i >= 0; --i)
            {
                if (toothTipStepSize > 0f)
                {
                    vertices.Add(
                        new Vector2(radius *
                                    (float)Math.Cos(stepSize * i + toothAngleStepSize * 2f + toothTipStepSize),
                                    -radius *
                                    (float)Math.Sin(stepSize * i + toothAngleStepSize * 2f + toothTipStepSize)));

                    vertices.Add(
                        new Vector2((radius + toothHeight) *
                                    (float)Math.Cos(stepSize * i + toothAngleStepSize + toothTipStepSize),
                                    -(radius + toothHeight) *
                                    (float)Math.Sin(stepSize * i + toothAngleStepSize + toothTipStepSize)));
                }

                vertices.Add(new Vector2((radius + toothHeight) *
                                         (float)Math.Cos(stepSize * i + toothAngleStepSize),
                                         -(radius + toothHeight) *
                                         (float)Math.Sin(stepSize * i + toothAngleStepSize)));

                vertices.Add(new Vector2(radius * (float)Math.Cos(stepSize * i),
                                         -radius * (float)Math.Sin(stepSize * i)));
            }

            return vertices;
        }


        /// <summary>
        /// Set this as a single edge.
        /// </summary>
        /// <param name="start">The first point.</param>
        /// <param name="end">The second point.</param>
        public static Vertices CreateLine(Vector2 start, Vector2 end)
        {
            Vertices vertices = new Vertices(2);
            vertices.Add(start);
            vertices.Add(end);

            return vertices;
        }

        /// <summary>
        /// Creates a list of vectors that represents an arc
        /// </summary>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate in the circle that this will cut out from</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The radians to draw, clockwise from the starting angle</param>
        /// <returns>A list of vectors that, if connected, will create an arc</returns>
        public static Vertices CreateArc(float radius, int sides, float startingAngle, float radians)
		{
            Vertices points = new Vertices();
			points.AddRange(CreateCircle(radius, sides));
			points.RemoveAt(points.Count - 1); // remove the last point because it's a duplicate of the first

			// The circle starts at (radius, 0)
			double curAngle = 0.0;
			double anglePerSide = MathHelper.TwoPi / sides;

			// "Rotate" to the starting point
			while ((curAngle + (anglePerSide / 2.0)) < startingAngle)
			{
				curAngle += anglePerSide;

				// move the first point to the end
				points.Add(points[0]);
				points.RemoveAt(0);
			}

			// Add the first point, just in case we make a full circle
			points.Add(points[0]);

			// Now remove the points at the end of the circle to create the arc
			int sidesInArc = (int)((radians / anglePerSide) + 0.5);
			points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);

			return points;
		}

        /// <summary>
        /// 创建椭圆形
        /// </summary>
        public static Vertices CreateEllipse(float xRadius, float yRadius, int numberOfEdges)
        {
            String circleKey = xRadius + "x" + yRadius + "x" +　numberOfEdges;
            if (circleCache.ContainsKey(circleKey))
            {
                return circleCache[circleKey];
            }

            Vertices vertices = new Vertices();

            float stepSize = MathHelper.TwoPi / numberOfEdges;

            vertices.Add(new Vector2(xRadius, 0));
            for (int i = numberOfEdges - 1; i > 0; --i)
                vertices.Add(new Vector2(xRadius * (float)Math.Cos(stepSize * i),
                                         -yRadius * (float)Math.Sin(stepSize * i)));

            circleCache.Add(circleKey, vertices);

            return vertices;
        }

        #endregion

        #region FillRectangle

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillRectangle(this GLRenderer spriteBatch, AABB rect, Color color)
		{
			// Simply use the function already there
			spriteBatch.Draw(GetPixel(spriteBatch), rect, color);
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="rect">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="angle">The angle in radians to draw the rectangle at</param>
        public static void FillRectangle(this GLRenderer spriteBatch, AABB rect, Color color, Vector2 orgin, float angle)
		{
            spriteBatch.Draw(GetPixel(spriteBatch), rect, null, color, angle, orgin, 0);
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void FillRectangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, Color color)
		{
			FillRectangle(spriteBatch, location, size, color, Vector2.Zero, 0.0f);
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="angle">The angle in radians to draw the rectangle at</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void FillRectangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, Color color, Vector2 orgin, float angle)
		{
			// stretch the pixel between the two vectors
            spriteBatch.Draw(GetPixel(spriteBatch),
			                 location,
			                 null,
			                 color,
			                 angle,
                             orgin,
			                 size,
			                 0);
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x">The X coord of the left side</param>
		/// <param name="y">The Y coord of the upper side</param>
		/// <param name="w">Width</param>
		/// <param name="h">Height</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void FillRectangle(this GLRenderer spriteBatch, float x, float y, float w, float h, Color color)
		{
			FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, Vector2.Zero, 0.0f);
		}


		/// <summary>
		/// Draws a filled rectangle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x">The X coord of the left side</param>
		/// <param name="y">The Y coord of the upper side</param>
		/// <param name="w">Width</param>
		/// <param name="h">Height</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="angle">The angle of the rectangle in radians</param>
		public static void FillRectangle(this GLRenderer spriteBatch, float x, float y, float w, float h, Color color, float angle)
		{
			FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, Vector2.Zero, angle);
		}

		#endregion

		#region DrawRectangle

		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="rect">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void DrawRectangle(this GLRenderer spriteBatch, AABB rect, Color color)
		{
			DrawRectangle(spriteBatch, rect, color, 1.0f, Vector2.Zero, 0);
		}


		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="rect">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="thickness">The thickness of the lines</param>
		public static void DrawRectangle(this GLRenderer spriteBatch, AABB rect, Color color, float thickness, Vector2 origin, float angle)
		{
            // TODO: Handle rotations
            // TODO: Figure out the pattern for the offsets required and then handle it in the line instead of here

            /*
            float yoffset = rect.Right < rect.Left ? thickness : 0;
            float xoffset = rect.Bottom < rect.Top ? -thickness : thickness;

            DrawLine(spriteBatch, new Vector2(rect.LowerBound.X, rect.LowerBound.Y + yoffset), new Vector2(rect.UpperBound.X, rect.LowerBound.Y + yoffset), color, thickness); // top
            DrawLine(spriteBatch, new Vector2(rect.LowerBound.X + xoffset, rect.LowerBound.Y), new Vector2(rect.LowerBound.X + xoffset, rect.UpperBound.Y + thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.LowerBound.X, rect.UpperBound.Y + yoffset), new Vector2(rect.UpperBound.X, rect.UpperBound.Y + yoffset), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.UpperBound.X + xoffset, rect.LowerBound.Y), new Vector2(rect.UpperBound.X + xoffset, rect.UpperBound.Y + thickness), color, thickness); // right
            */

            var leftTop = MathHelper.PointRotate(origin, rect.LeftTop, angle);
            var rightTop = MathHelper.PointRotate(origin, rect.RightTop, angle);
            var rightBottom = MathHelper.PointRotate(origin, rect.RightBottom, angle);
            var leftBottom = MathHelper.PointRotate(origin, rect.LeftBottom, angle);

            DrawLine(spriteBatch, leftTop, rightTop, color, thickness);
            DrawLine(spriteBatch, rightTop, rightBottom, color, thickness);
            DrawLine(spriteBatch, leftBottom, rightBottom, color, thickness);
            DrawLine(spriteBatch, leftTop, leftBottom, color, thickness);
		}


		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void DrawRectangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, Color color)
		{
            DrawRectangle(spriteBatch, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, 1.0f, Vector2.Zero, 0);
		}


		/// <summary>
		/// Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawRectangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, Color color, float thickness)
		{
            DrawRectangle(spriteBatch, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, thickness, Vector2.Zero, 0);
		}

		#endregion

		#region DrawLine

		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x1">The X coord of the first point</param>
		/// <param name="y1">The Y coord of the first point</param>
		/// <param name="x2">The X coord of the second point</param>
		/// <param name="y2">The Y coord of the second point</param>
		/// <param name="color">The color to use</param>
		public static void DrawLine(this GLRenderer spriteBatch, float x1, float y1, float x2, float y2, Color color)
		{
			DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
		}

		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x1">The X coord of the first point</param>
		/// <param name="y1">The Y coord of the first point</param>
		/// <param name="x2">The X coord of the second point</param>
		/// <param name="y2">The Y coord of the second point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawLine(this GLRenderer spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness)
		{
			DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
		}

		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="point1">The first point</param>
		/// <param name="point2">The second point</param>
		/// <param name="color">The color to use</param>
		public static void DrawLine(this GLRenderer spriteBatch, Vector2 point1, Vector2 point2, Color color)
		{
			DrawLine(spriteBatch, point1, point2, color, 1.0f);
		}

		/// <summary>
		/// Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="point1">The first point</param>
		/// <param name="point2">The second point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawLine(this GLRenderer spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
		{
			// calculate the distance between the two vectors
			float distance = Vector2.Distance(point1, point2);

			// calculate the angle between the two vectors
			//float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            float angle = MathHelper.GetAngleBetween2Points(point1, point2); // new Vector2(point1.X, point1.Y - distance));

            // stretch the pixel between the two vectors
            spriteBatch.Draw(GetPixel(spriteBatch),
                             point1,
                             null,
                             color,
                             angle,
                             Vector2.Zero,
                             new Vector2(thickness, -distance),
                             0);
        }

	

        #endregion

        #region PutPixel

        public static void PutPixel(this GLRenderer spriteBatch, float x, float y, Color color)
		{
			PutPixel(spriteBatch, new Vector2(x, y), color);
		}


		public static void PutPixel(this GLRenderer spriteBatch, Vector2 position, Color color)
		{
            spriteBatch.Draw(GetPixel(spriteBatch), position, color);
		}

		#endregion

		#region DrawCircle

		/// <summary>
		/// Draw a circle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="center">The center of the circle</param>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <param name="color">The color of the circle</param>
		public static void DrawCircle(this GLRenderer spriteBatch, Vector2 center, float radius, int sides, Color color)
		{
			DrawPoints(spriteBatch, center, CreateCircle(radius, sides), color, 1.0f);
		}


		/// <summary>
		/// Draw a circle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="center">The center of the circle</param>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <param name="color">The color of the circle</param>
		/// <param name="thickness">The thickness of the lines used</param>
		public static void DrawCircle(this GLRenderer spriteBatch, Vector2 center, float radius, int sides, Color color, float thickness)
		{
			DrawPoints(spriteBatch, center, CreateCircle(radius, sides), color, thickness);
		}


		/// <summary>
		/// Draw a circle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x">The center X of the circle</param>
		/// <param name="y">The center Y of the circle</param>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <param name="color">The color of the circle</param>
		public static void DrawCircle(this GLRenderer spriteBatch, float x, float y, float radius, int sides, Color color)
		{
			DrawPoints(spriteBatch, new Vector2(x, y), CreateCircle(radius, sides), color, 1.0f);
		}


		/// <summary>
		/// Draw a circle
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="x">The center X of the circle</param>
		/// <param name="y">The center Y of the circle</param>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <param name="color">The color of the circle</param>
		/// <param name="thickness">The thickness of the lines used</param>
		public static void DrawCircle(this GLRenderer spriteBatch, float x, float y, float radius, int sides, Color color, float thickness)
		{
			DrawPoints(spriteBatch, new Vector2(x, y), CreateCircle(radius, sides), color, thickness);
		}

        #endregion

        #region DrawEllipse

        public static void DrawEllipse(this GLRenderer spriteBatch, AABB rect, Color color)
        {
            DrawEllipse(spriteBatch, rect, color, 1f);
        }

        public static void DrawEllipse(this GLRenderer spriteBatch, AABB rect, Color color, float thickness)
        {
            var points = CreateEllipse(rect.Width, rect.Height, (int)(rect.Width + rect.Height));
            Vertices vertices = new Vertices();
            Vector2 offset = new Vector2(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            for (var i = 0; i < points.Count; i++)
            {
                vertices.Add(points[i] + offset);
            }

            DrawVertices(spriteBatch, vertices, color, thickness);
        }

        #endregion

        #region DrawPolygon

        public static void DrawPolygon(this GLRenderer spriteBatch, IList<Vector2> points, Color color)
        {
            DrawPolygon(spriteBatch, points, color, 1f, Vector2.Zero);
        }

        public static void DrawPolygon(this GLRenderer spriteBatch, IList<Vector2> points, Color color, float thickness, Vector2 loc)
        {
            if (points.Count < 2)
                throw new Exception("DrawPolygon(DisplayStateRender, IList<Vector2>, Color, float) length error");

            Vector2 last = points[0] + loc;
            for (var i = 1; i < points.Count; i++)
            {
                Vector2 curr = points[i] + loc;
                DrawLine(spriteBatch, last, curr, color, thickness);
                last = curr;
            }
        }

        #endregion

        #region DrawArc

        /// <summary>
        /// Draw a arc
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="center">The center of the arc</param>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The number of radians to draw, clockwise from the starting angle</param>
        /// <param name="color">The color of the arc</param>
        public static void DrawArc(this GLRenderer spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color)
		{
			DrawArc(spriteBatch, center, radius, sides, startingAngle, radians, color, 1.0f);
		}


		/// <summary>
		/// Draw a arc
		/// </summary>
		/// <param name="spriteBatch">The destination drawing surface</param>
		/// <param name="center">The center of the arc</param>
		/// <param name="radius">The radius of the arc</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
		/// <param name="radians">The number of radians to draw, clockwise from the starting angle</param>
		/// <param name="color">The color of the arc</param>
		/// <param name="thickness">The thickness of the arc</param>
		public static void DrawArc(this GLRenderer spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color, float thickness)
		{
			List<Vector2> arc = CreateArc(radius, sides, startingAngle, radians);
			//List<Vector2> arc = CreateArc2(radius, sides, startingAngle, degrees);
			DrawPoints(spriteBatch, center, arc, color, thickness);
		}

		#endregion

        #region DrawVertices

        public static void FillVertices(this GLRenderer spriteBatch, Color color, params float[] points) 
        {
            if(points.Length < 2 || points.Length % 2 != 0)
                throw new Exception("FillVertices(DisplayStateRender, Color, params float) length error");

            var vector2s = new Vector2[points.Length / 2];
            for(var i = 0; i < vector2s.Length; i++) 
            {
                vector2s[i] = new Vector2(points[i], points[i + 1]);
            }

            FillVertices(spriteBatch, color, vector2s);
        }

        public static void FillVertices(this GLRenderer spriteBatch, Color color, params Vector2[] points)
        {
            FillVertices(spriteBatch, new Vertices(points), color);
        } 

        public static void FillVertices(this GLRenderer spriteBatch, Vertices vertices, Color color) 
        {
            // 首先切割成三角形
            //var items = vertices.Triangulate();

            var items = vertices.Count <= 3 ?
                new List<Vertices>() { vertices } :
                spriteBatch.Triangulate(vertices);

            for (var i = 0; i < items.Count; i++) 
            {
                var item = items[i];

                var point1 = item[0];
                var point2 = item[1];
                var point3 = item[2];

                spriteBatch.Draw(
                    new DisplayState()
                    {
                        Image = GetPixel(spriteBatch),
                        P1 = new VertexPositionColorTexture(new Vector3(point1, 0), color, new Vector2(0, 0)),
                        P2 = new VertexPositionColorTexture(new Vector3(point2, 0), color, new Vector2(0, 1)),
                        P3 = new VertexPositionColorTexture(new Vector3(point3, 0), color, new Vector2(1, 1))
                    });
            }
        }

        public static void DrawVertices(this GLRenderer spriteBatch, Color color, float thickness, params float[] points)
        {
            if (points.Length < 2 || points.Length % 2 != 0)
                throw new Exception("FillVertices(DisplayStateRender, Color, params float) length error");

            var vector2s = new Vector2[points.Length / 2];
            for (var i = 0; i < vector2s.Length; i++)
            {
                vector2s[i] = new Vector2(points[i], points[i + 1]);
            }

            DrawVertices(spriteBatch, color, thickness, vector2s);
        }

        public static void DrawVertices(this GLRenderer spriteBatch, Color color, float thickness, params Vector2[] points)
        {
            DrawVertices(spriteBatch, new Vertices(points), color, thickness, Vector2.Zero);
        }

        public static void DrawVertices(this GLRenderer spriteBatch, Vertices vertices, Color color, float thickness)
        {
            DrawVertices(spriteBatch, vertices, color, thickness, Vector2.Zero);
        }

        public static void DrawVertices(this GLRenderer spriteBatch, Vertices vertices, Color color, float thickness, Vector2 offset) 
        {
            for (var i = 0; i < vertices.Count; i++) 
            {
                var point1 = vertices[i] + offset;
                var point2 = i == vertices.Count - 1 ? vertices[0] : vertices[i + 1];
                point2 = point2 + offset;

                DrawLine(spriteBatch, point1, point2, color, thickness);
            }
        }

        #endregion

        #region DrawDotLine

        public static void DrawDotLine(this GLRenderer spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness, float dotLength)              
        {
            var length = MathHelper.GetDistance(point1, point2);
            if (length < dotLength)
            {
                DrawLine(spriteBatch, point1, point2, color, thickness); // length, angle, color, thickness);
            }
            else
            {
                var dotCount = (int)(length / dotLength);
                for (var i = 0; i < dotCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        var currPoint = MathHelper.GetExtendPoint(point1, point2, i * dotLength);
                        var nextPoint = MathHelper.GetExtendPoint(currPoint, point2, dotLength);
                        DrawLine(spriteBatch, currPoint, nextPoint, color, thickness);
                    }
                }
            }
            
        }

        #endregion

        #region DrawDotRectangle

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void DrawDotRectangle(this GLRenderer spriteBatch, AABB rect, Color color)                                                                
        {
            DrawDotRectangle(spriteBatch, rect, color, 1.0f, 1.0f);
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        public static void DrawDotRectangle(this GLRenderer spriteBatch, AABB rect, Color color, float thickness, float dotLngth)                               
        {
            // TODO: Handle rotations
            // TODO: Figure out the pattern for the offsets required and then handle it in the line instead of here

            DrawDotLine(spriteBatch, new Vector2(rect.LowerBound.X, rect.LowerBound.Y), new Vector2(rect.UpperBound.X, rect.LowerBound.Y), color, thickness, dotLngth); // top
            DrawDotLine(spriteBatch, new Vector2(rect.LowerBound.X, rect.LowerBound.Y), new Vector2(rect.LowerBound.X, rect.UpperBound.Y), color, thickness, dotLngth); // left
            DrawDotLine(spriteBatch, new Vector2(rect.LowerBound.X, rect.UpperBound.Y), new Vector2(rect.UpperBound.X, rect.UpperBound.Y), color, thickness, dotLngth); // bottom
            DrawDotLine(spriteBatch, new Vector2(rect.UpperBound.X, rect.LowerBound.Y), new Vector2(rect.UpperBound.X, rect.UpperBound.Y), color, thickness, dotLngth); // right
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void DrawDotRectangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, Color color)                                           
        {
            DrawDotRectangle(spriteBatch, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, 1.0f, 1.0f);
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawDotRectangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, Color color, float thickness, float dotLength)         
        {
            DrawDotRectangle(spriteBatch, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, thickness, dotLength);
        }

        #endregion

        #region DrawTriangle

        public static void DrawTriangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, float angle, Color color, float thickness)
        {
            var center = new Vector2(location.X + size.X / 2, location.Y + size.Y / 2);
            var point1 = new Vector2(location.X + size.X / 2, location.Y);
            var point2 = new Vector2(location.X + size.X, location.Y + size.Y);
            var point3 = new Vector2(location.X, location.Y + size.Y);

            if (angle != 0)
            {
                point1 = MathHelper.PointRotate(center, point1, angle);
                point2 = MathHelper.PointRotate(center, point2, angle);
                point3 = MathHelper.PointRotate(center, point3, angle);
            }

            DrawVertices(spriteBatch, color, thickness, point1, point2, point3);
        }

        #endregion

        #region FillTriangle

        public static void FillTriangle(this GLRenderer spriteBatch, Vector2 location, Vector2 size, float angle, Color color)
        {
            var center = new Vector2(location.X + size.X / 2, location.Y + size.Y / 2);
            var point1 = new Vector2(location.X + size.X / 2, location.Y);
            var point2 = new Vector2(location.X + size.X, location.Y + size.Y);
            var point3 = new Vector2(location.X, location.Y + size.Y);

            if (angle != 0)
            {
                point1 = MathHelper.PointRotate(center, point1, angle);
                point2 = MathHelper.PointRotate(center, point2, angle);
                point3 = MathHelper.PointRotate(center, point3, angle);
            }

            FillVertices(spriteBatch, color, point1, point2, point3);
        }

        #endregion

        #region DrawCurve

        #endregion

        #region DrawText

        public static Vector2 MeasureString(this GLRenderer spriteBatch, ITrueTypeFont font, string text, float size) 
        {
            float width = 0, height = 0, scale = font.GetScale(size);
            foreach (var t in text)
            {
                var info = font.GetChar(t);
                var infoWidth = info.Size.X * scale;
                var infoHeight = info.Size.Y * scale;

                width += infoWidth + 1;
                if (height < infoHeight)
                    height = infoHeight;
            }

            return new Vector2(width, height);
        }

        public static void DrawString(this GLRenderer spriteBatch, ITrueTypeFont font, string text, float size, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            float xoffset = 0;
            var fontscale = font.GetScale(size) * scale;
            foreach (var t in text)
            {
                var info = font.GetChar(t);
                var infoWidth = info.Size.X * Math.Abs(fontscale.X);
                var offset = new Vector2(position.X + xoffset, position.Y);

                for (var i = 0; i < info.Triangles.Length; i++)
                {
                    var item = info.Triangles[i];

                    var point1 = item[0] * fontscale + offset;
                    var point2 = item[1] * fontscale + offset;
                    var point3 = item[2] * fontscale + offset;

                    var angle = rotation % 360;
                    if (angle != 0) 
                    {
                        var rotateCenter = position + origin;
                        point1 = MathHelper.PointRotate(rotateCenter, point1, angle);
                        point2 = MathHelper.PointRotate(rotateCenter, point2, angle);
                        point3 = MathHelper.PointRotate(rotateCenter, point3, angle);
                    }

                    spriteBatch.Draw(
                        new DisplayState()
                        {
                            Image = GetPixel(spriteBatch),
                            /*
                            LeftTop = new VertexPositionColorTexture(new Vector3(point1, 0), color, new Vector2(0, 0)),
                            LeftBottom = new VertexPositionColorTexture(new Vector3(point2, 0), color, new Vector2(0, 1)),
                            RightTop = new VertexPositionColorTexture(new Vector3(point1, 0), color, new Vector2(0, 0)),
                            RightBottom = new VertexPositionColorTexture(new Vector3(point3, 0), color, new Vector2(1, 1))
                            */
                            P1 = new VertexPositionColorTexture(new Vector3(point1, 0), color, new Vector2(0, 0)),
                            P2 = new VertexPositionColorTexture(new Vector3(point2, 0), color, new Vector2(0, 1)),
                            P3 = new VertexPositionColorTexture(new Vector3(point3, 0), color, new Vector2(1, 1))
                        });
                }

                xoffset += infoWidth + 1;
            }
        }

        #endregion

        /*
        #region DrawTarget

        public static Texture2D CreateRenderTarget(this DisplayStateRender spriteBatch, int width, int height)
        {
            var g = spriteBatch.Renderer.GraphicsDevice;
            RenderTarget2D rt = new RenderTarget2D(g, width, height, false, SurfaceFormat.Color, DepthFormat.Depth24);
            g.SetRenderTarget(rt);
            g.Clear(Color.Transparent, 0, 0);
            return rt;
        }

        public static void ResetRenderTarget(this DisplayStateRender spriteBatch) 
        {
            var g = spriteBatch.Renderer.GraphicsDevice;
            g.SetRenderTarget(null);
        }

        #endregion
        */
    }

    public static class Primitives2DDisplayStateEx 
    {
        #region FillRectangle

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetFillRectangle(GLRenderer window, Image pixel, AABB rect, Color color)
        {
            // Simply use the function already there
            return GetFillRectangle(window, pixel, rect.LowerBound, new Vector2(rect.Width, rect.Height), color);
        }

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetFillRectangle(GLRenderer window, Image pixel, Vector2 location, Vector2 size, Color color)
        {
            return GetFillRectangle(window, pixel, location, size, color, Vector2.Zero, 0.0f);
        }

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="angle">The angle in radians to draw the rectangle at</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetFillRectangle(GLRenderer window, Image pixel, Vector2 location, Vector2 size, Color color, Vector2 orgin, float angle)
        {
            // stretch the pixel between the two vectors
            return DisplayStateRenderEx.GetDisplayStates(
                            window,
                            pixel,
                            location,
                            null,
                            color,
                            angle,
                            orgin,
                            size,
                            0);
        }

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetFillRectangle(GLRenderer window, Image pixel, float x, float y, float w, float h, Color color)
        {
            return GetFillRectangle(window, pixel, new Vector2(x, y), new Vector2(w, h), color, Vector2.Zero, 0.0f);
        }

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="angle">The angle of the rectangle in radians</param>
        public static IEnumerable<DisplayState> GetFillRectangle(GLRenderer window, Image pixel, float x, float y, float w, float h, Color color, float angle)
        {
            return GetFillRectangle(window, pixel, new Vector2(x, y), new Vector2(w, h), color, Vector2.Zero, angle);
        }

        #endregion

        #region DrawRectangle

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetRectangle(GLRenderer window, Image pixel, AABB rect, Color color)
        {
            return GetRectangle(window, pixel, rect, color, 1.0f, Vector2.Zero, 0);
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        public static IEnumerable<DisplayState> GetRectangle(GLRenderer window, Image pixel, AABB rect, Color color, float thickness, Vector2 origin, float angle)
        {
            // TODO: Handle rotations
            // TODO: Figure out the pattern for the offsets required and then handle it in the line instead of here

            /*
            float yoffset = rect.Right < rect.Left ? thickness : 0;
            float xoffset = rect.Bottom < rect.Top ? -thickness : thickness;

            DrawLine(spriteBatch, new Vector2(rect.LowerBound.X, rect.LowerBound.Y + yoffset), new Vector2(rect.UpperBound.X, rect.LowerBound.Y + yoffset), color, thickness); // top
            DrawLine(spriteBatch, new Vector2(rect.LowerBound.X + xoffset, rect.LowerBound.Y), new Vector2(rect.LowerBound.X + xoffset, rect.UpperBound.Y + thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.LowerBound.X, rect.UpperBound.Y + yoffset), new Vector2(rect.UpperBound.X, rect.UpperBound.Y + yoffset), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.UpperBound.X + xoffset, rect.LowerBound.Y), new Vector2(rect.UpperBound.X + xoffset, rect.UpperBound.Y + thickness), color, thickness); // right
            */

            var leftTop = MathHelper.PointRotate(origin, rect.LeftTop, angle);
            var rightTop = MathHelper.PointRotate(origin, rect.RightTop, angle);
            var rightBottom = MathHelper.PointRotate(origin, rect.RightBottom, angle);
            var leftBottom = MathHelper.PointRotate(origin, rect.LeftBottom, angle);

            foreach (var state in GetLineDisplaysStates(window, pixel, leftTop, rightTop, color, thickness)) yield return state;
            foreach (var state in GetLineDisplaysStates(window, pixel, rightTop, rightBottom, color, thickness)) yield return state;
            foreach (var state in GetLineDisplaysStates(window, pixel, leftBottom, rightBottom, color, thickness)) yield return state;
            foreach (var state in GetLineDisplaysStates(window, pixel, leftTop, leftBottom, color, thickness)) yield return state;
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetRectangle(GLRenderer window, Image pixel, Vector2 location, Vector2 size, Color color)
        {
            return GetRectangle(window, pixel, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, 1.0f, Vector2.Zero, 0);
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the line</param>
        public static IEnumerable<DisplayState> GetRectangle(GLRenderer window, Image pixel, Vector2 location, Vector2 size, Color color, float thickness)
        {
            return GetRectangle(window, pixel, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, thickness, Vector2.Zero, 0);
        }

        #endregion

        #region DrawEllipse

        private static readonly Dictionary<String, Vertices> circleCache = new Dictionary<string, Vertices>();
        private static readonly Dictionary<string, IEnumerable<Vertices>> circleFillCache = new Dictionary<string, IEnumerable<Vertices>>();

        /// <summary>
        /// 创建椭圆形
        /// </summary>
        public static Vertices CreateEllipse(float xRadius, float yRadius, int numberOfEdges)
        {
            String circleKey = xRadius + "x" + yRadius + "x" + numberOfEdges;
            if (circleCache.ContainsKey(circleKey))
            {
                return circleCache[circleKey];
            }

            Vertices vertices = new Vertices();

            float stepSize = MathHelper.TwoPi / numberOfEdges;

            vertices.Add(new Vector2(xRadius, 0));
            for (int i = numberOfEdges - 1; i > 0; --i)
                vertices.Add(new Vector2(xRadius * (float)Math.Cos(stepSize * i),
                                         -yRadius * (float)Math.Sin(stepSize * i)));

            circleCache.Add(circleKey, vertices);

            return vertices;
        }

        public static IEnumerable<DisplayState> GetEllipse(GLRenderer window, Image pixel, AABB rect, Color color)
        {
            return GetEllipse(window, pixel, rect, color, 1f, Vector2.Zero, 0);
        }

        public static IEnumerable<DisplayState> GetEllipse(GLRenderer window, Image pixel, AABB rect, Color color, float thickness, Vector2 origin, float angle)
        {
            var points = CreateEllipse(rect.Width / 2, rect.Height / 2, (int)(rect.Width / 4 + rect.Height / 4));
            Vertices vertices = new Vertices();
            Vector2 offset = new Vector2(rect.Width / 2, rect.Height / 2);
            for (var i = 0; i < points.Count; i++)
            {
                vertices.Add(MathHelper.PointRotate(origin, points[i] + offset, angle));
            }

            return GetDrawVerticesDisplaysStates(window, pixel, vertices, color, thickness, rect.LeftTop);
        }


        public static IEnumerable<DisplayState> GetFillEllipse(GLRenderer window, Image pixel, AABB rect, Color color)
        {
            return GetFillEllipse(window, pixel, rect, color, 1f, Vector2.Zero, 0);
        }

        public static IEnumerable<DisplayState> GetFillEllipse(GLRenderer window, Image pixel, AABB rect, Color color, float thickness, Vector2 origin, float angle)
        {
            var points = CreateEllipse(rect.Width, rect.Height, (int)(rect.Width / 4 + rect.Height / 4));
            Vertices vertices = new Vertices();
            Vector2 offset = new Vector2(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            for (var i = 0; i < points.Count; i++)
            {
                var v = points[i] + offset;
                v = MathHelper.PointRotate(origin, v, angle);
                vertices.Add(v);
            }

            return GetFillVerticesDisplaysStates(window, pixel, vertices, color, Vector2.Zero);
        }

        #endregion

        public static IEnumerable<DisplayState> GetLineDisplaysStates(GLRenderer window, Image texture, float x1, float y1, float x2, float y2, Color color)
        {
            return GetLineDisplaysStates(window, texture, new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
        }

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x1">The X coord of the first point</param>
        /// <param name="y1">The Y coord of the first point</param>
        /// <param name="x2">The X coord of the second point</param>
        /// <param name="y2">The Y coord of the second point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        public static IEnumerable<DisplayState> GetLineDisplaysStates(GLRenderer window, Image texture, float x1, float y1, float x2, float y2, Color color, float thickness)
        {
            return GetLineDisplaysStates(window, texture, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="color">The color to use</param>
        public static IEnumerable<DisplayState> GetLineDisplaysStates(GLRenderer window, Image texture, Vector2 point1, Vector2 point2, Color color)
        {
            return GetLineDisplaysStates(window, texture, point1, point2, color, 1.0f);
        }

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        public static IEnumerable<DisplayState> GetLineDisplaysStates(GLRenderer window, Image texture, Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            // calculate the distance between the two vectors
            float distance = Vector2.Distance(point1, point2);

            // calculate the angle between the two vectors
            //float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            float angle = MathHelper.GetAngleBetween2Points(point1, point2) - 90; // new Vector2(point1.X, point1.Y - distance));

            return GetLineDisplaysStates(window, texture, point1, distance, angle, color, thickness);
        }

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point">The starting point</param>
        /// <param name="length">The length of the line</param>
        /// <param name="angle">The angle of this line from the starting point in radians</param>
        /// <param name="color">The color to use</param>
        public static IEnumerable<DisplayState> GetLineDisplaysStates(GLRenderer window, Image texture, Vector2 point, float length, float angle, Color color)
        {
            return GetLineDisplaysStates(window, texture, point, length, angle, color, 1.0f);
        }

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point">The starting point</param>
        /// <param name="length">The length of the line</param>
        /// <param name="angle">The angle of this line from the starting point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        public static IEnumerable<DisplayState> GetLineDisplaysStates(GLRenderer window, Image texture, Vector2 point, float length, float angle, Color color, float thickness)
        {
            return DisplayStateRenderEx.GetDisplayStates(window, texture, point, null, color, angle, new Vector2(0, thickness / 2), new Vector2(length, thickness), 0);
        }


        public static IEnumerable<DisplayState> GetFillVerticesDisplaysStates(GLRenderer window, Image texture, Color color, params float[] points)
        {
            if (points.Length < 2 || points.Length % 2 != 0)
                throw new Exception("FillVertices(DisplayStateRender, Color, params float) length error");

            var vector2s = new Vector2[points.Length / 2];
            for (var i = 0; i < vector2s.Length; i++)
            {
                vector2s[i] = new Vector2(points[i], points[i + 1]);
            }

            return GetFillVerticesDisplaysStates(window, texture, color, vector2s);
        }

        public static IEnumerable<DisplayState> GetFillVerticesDisplaysStates(GLRenderer window, Image texture, Color color, params Vector2[] points)
        {
            return GetFillVerticesDisplaysStates(window, texture, new Vertices(points), color, Vector2.Zero);
        }

        public static IEnumerable<DisplayState> GetFillVerticesDisplaysStates(GLRenderer window, Image texture, Vertices vertices, Color color, Vector2 offset)
        {
            // 首先切割成三角形
            //var items = vertices.Triangulate();
            var items = vertices.Count <= 3 ?
                new List<Vertices>() { vertices } :
                window.Triangulate(vertices);

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];

                var point1 = item[0] + offset;
                var point2 = item[1] + offset;
                var point3 = item[2] + offset;

                yield return new DisplayState()
                {
                    Image = texture,
                    P1 = new VertexPositionColorTexture(new Vector3(point1, 0), color, new Vector2(0, 0)),
                    P2 = new VertexPositionColorTexture(new Vector3(point2, 0), color, new Vector2(0, 1)),
                    P3 = new VertexPositionColorTexture(new Vector3(point3, 0), color, new Vector2(1, 1))
                };
            }
        }

        public static IEnumerable<DisplayState> GetDrawVerticesDisplaysStates(GLRenderer window, Image texture, Vertices vertices, Color color, float thickness, Vector2 offset)
        {
            for (var i = 0; i < vertices.Count; i++)
            {
                var point1 = vertices[i] + offset;
                var point2 = i == vertices.Count - 1 ? vertices[0] : vertices[i + 1];
                point2 = point2 + offset;

                foreach (var state in GetLineDisplaysStates(window, texture, point1, point2, color, thickness))
                {
                    yield return state;
                }
            }
        }

        public static IEnumerable<DisplayState> GetDrawVerticesDisplaysStates(GLRenderer window, Image texture, Color color, float thickness, params Vector2[] points)
        {
            return GetDrawVerticesDisplaysStates(window, texture, new Vertices(points), color, thickness, Vector2.Zero);
        }


        public static IEnumerable<DisplayState> GetDotLineDisplaysStates(GLRenderer window, Image texture, Vector2 point1, Vector2 point2, Color color, float thickness, float dotLength)
        {
            var length = MathHelper.GetDistance(point1, point2);
            if (length < dotLength)
            {
                foreach (var state in GetLineDisplaysStates(window, texture, point1, point2, color, thickness)) yield return state; // length, angle, color, thickness);
            }
            else
            {
                var dotCount = (int)(length / dotLength);
                for (var i = 0; i < dotCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        var currPoint = MathHelper.GetExtendPoint(point1, point2, i * dotLength);
                        var nextPoint = MathHelper.GetExtendPoint(currPoint, point2, dotLength);
                        foreach (var state in GetLineDisplaysStates(window, texture, currPoint, nextPoint, color, thickness)) yield return state;
                    }
                }
            }

        }

        public static IEnumerable<DisplayState> GetDotLineDisplaysStates(GLRenderer window, Image texture, Vector2 point, float length, float angle, Color color, float thickness, float dotLength)
        {
            if (length != 0)
            {

                if (length <= dotLength)
                {
                    foreach (var state in GetLineDisplaysStates(window, texture, point, length, angle, color, thickness)) yield return state;
                }
                else
                {
                    var endPoint = MathHelper.PointRotate(point, new Vector2(point.X, point.Y - length), angle);
                    for (var i = 0; i < length / dotLength; i++)
                    {
                        if (i % 2 == 0)
                        {
                            var currPoint = MathHelper.GetExtendPoint(point, endPoint, i * dotLength);
                            var nextPoint = MathHelper.GetExtendPoint(currPoint, endPoint, dotLength);
                            foreach (var state in GetLineDisplaysStates(window, texture, currPoint, nextPoint, color, thickness)) yield return state;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetDotRectangleDisplaysStates(GLRenderer window, Image img, AABB rect, Color color)
        {
            return GetDotRectangleDisplaysStates(window, img, rect, color, 1.0f, 1.0f);
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        public static IEnumerable<DisplayState> GetDotRectangleDisplaysStates(GLRenderer window, Image img, AABB rect, Color color, float thickness, float dotLngth)
        {
            // TODO: Handle rotations
            // TODO: Figure out the pattern for the offsets required and then handle it in the line instead of here

            foreach (var state in GetDotLineDisplaysStates(window, img, new Vector2(rect.LowerBound.X, rect.LowerBound.Y), new Vector2(rect.UpperBound.X, rect.LowerBound.Y), color, thickness, dotLngth)) yield return state;
            foreach (var state in GetDotLineDisplaysStates(window, img, new Vector2(rect.LowerBound.X, rect.LowerBound.Y), new Vector2(rect.LowerBound.X, rect.UpperBound.Y), color, thickness, dotLngth)) yield return state;
            foreach (var state in GetDotLineDisplaysStates(window, img, new Vector2(rect.LowerBound.X, rect.UpperBound.Y), new Vector2(rect.UpperBound.X, rect.UpperBound.Y), color, thickness, dotLngth)) yield return state;
            foreach (var state in GetDotLineDisplaysStates(window, img, new Vector2(rect.UpperBound.X, rect.LowerBound.Y), new Vector2(rect.UpperBound.X, rect.UpperBound.Y), color, thickness, dotLngth)) yield return state;
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static IEnumerable<DisplayState> GetDotRectangleDisplaysStates(GLRenderer window, Image img, Vector2 location, Vector2 size, Color color)
        {
            return GetDotRectangleDisplaysStates(window, img, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, 1.0f, 1.0f);
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the line</param>
        public static IEnumerable<DisplayState> GetDotRectangleDisplaysStates(GLRenderer window, Image img, Vector2 location, Vector2 size, Color color, float thickness, float dotLength)
        {
            return GetDotRectangleDisplaysStates(window, img, new AABB(new Vector2(location.X, location.Y), new Vector2(location.X + size.X, location.Y + size.Y)), color, thickness, dotLength);
        }
    }
}