using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace engine.framework
{
    static class TriangleTool
    {
        public static IEnumerable<Vector2[]> CutEar(IEnumerable<Vector2> points) 
        {
            CPolygonShape shape = new CPolygonShape(points.Select(X => new CPoint2D(X.X, X.Y)).ToArray());

            shape.CutEar();

            for (var num = 0; num < shape.NumberOfPolygons; num++)
            {
                yield return shape.Polygons(num).Select(X => new Vector2((float)X.X, (float)X.Y)).ToArray();
            }
        }


        /// <summary>
        /// Summary description for NoValidReturnException.
        /// </summary>
        public class NonValidReturnException : ApplicationException
        {
            public NonValidReturnException()
                : base()
            {

            }
            public NonValidReturnException(string msg)
                : base(msg)
            {
                string errMsg = "\nThere is no valid return value available!";
                throw new NonValidReturnException(errMsg);
            }
            public NonValidReturnException(string msg,
                Exception inner)
                : base(msg, inner)
            {

            }
        }

        public class InvalidInputGeometryDataException : ApplicationException
        {
            public InvalidInputGeometryDataException()
                : base()
            {

            }
            public InvalidInputGeometryDataException(string msg)
                : base(msg)
            {

            }
            public InvalidInputGeometryDataException(string msg,
                Exception inner)
                : base(msg, inner)
            {

            }
        }

        /// <summary>
        /// Summary description for CPolygon.
        /// </summary>
        public class CPolygon
        {

            private CPoint2D[] m_aVertices;

            public CPoint2D this[int index]
            {
                set
                {
                    m_aVertices[index] = value;
                }
                get
                {
                    return m_aVertices[index];
                }
            }

            public CPolygon()
            {

            }

            public CPolygon(CPoint2D[] points)
            {
                int nNumOfPoitns = points.Length;
                try
                {
                    if (nNumOfPoitns < 3)
                    {
                        InvalidInputGeometryDataException ex =
                            new InvalidInputGeometryDataException();
                        throw ex;
                    }
                    else
                    {
                        m_aVertices = new CPoint2D[nNumOfPoitns];
                        for (int i = 0; i < nNumOfPoitns; i++)
                        {
                            m_aVertices[i] = points[i];
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(
                        e.Message + e.StackTrace);
                }
            }

            /***********************************
             From a given point, get its vertex index.
             If the given point is not a polygon vertex, 
             it will return -1 
             ***********************************/
            public int VertexIndex(CPoint2D vertex)
            {
                int nIndex = -1;

                int nNumPts = m_aVertices.Length;
                for (int i = 0; i < nNumPts; i++) //each vertex
                {
                    if (CPoint2D.SamePoints(m_aVertices[i], vertex))
                        nIndex = i;
                }
                return nIndex;
            }

            /***********************************
             From a given vertex, get its previous vertex point.
             If the given point is the first one, 
             it will return  the last vertex;
             If the given point is not a polygon vertex, 
             it will return null; 
             ***********************************/
            public CPoint2D PreviousPoint(CPoint2D vertex)
            {
                int nIndex;

                nIndex = VertexIndex(vertex);
                if (nIndex == -1)
                    return null;
                else //a valid vertex
                {
                    if (nIndex == 0) //the first vertex
                    {
                        int nPoints = m_aVertices.Length;
                        return m_aVertices[nPoints - 1];
                    }
                    else //not the first vertex
                        return m_aVertices[nIndex - 1];
                }
            }

            /***************************************
                 From a given vertex, get its next vertex point.
                 If the given point is the last one, 
                 it will return  the first vertex;
                 If the given point is not a polygon vertex, 
                 it will return null; 
            ***************************************/
            public CPoint2D NextPoint(CPoint2D vertex)
            {
                CPoint2D nextPt = new CPoint2D();

                int nIndex;
                nIndex = VertexIndex(vertex);
                if (nIndex == -1)
                    return null;
                else //a valid vertex
                {
                    int nNumOfPt = m_aVertices.Length;
                    if (nIndex == nNumOfPt - 1) //the last vertex
                    {
                        return m_aVertices[0];
                    }
                    else //not the last vertex
                        return m_aVertices[nIndex + 1];
                }
            }


            /******************************************
            To calculate the polygon's area

            Good for polygon with holes, but the vertices make the 
            hole  should be in different direction with bounding 
            polygon.
		
            Restriction: the polygon is not self intersecting
            ref: www.swin.edu.au/astronomy/pbourke/
                geometry/polyarea/
            *******************************************/
            public double PolygonArea()
            {
                double dblArea = 0;
                int nNumOfVertices = m_aVertices.Length;

                int j;
                for (int i = 0; i < nNumOfVertices; i++)
                {
                    j = (i + 1) % nNumOfVertices;
                    dblArea += m_aVertices[i].X * m_aVertices[j].Y;
                    dblArea -= (m_aVertices[i].Y * m_aVertices[j].X);
                }

                dblArea = dblArea / 2;
                return Math.Abs(dblArea);
            }

            /******************************************
            To calculate the area of polygon made by given points 

            Good for polygon with holes, but the vertices make the 
            hole  should be in different direction with bounding 
            polygon.
		
            Restriction: the polygon is not self intersecting
            ref: www.swin.edu.au/astronomy/pbourke/
                geometry/polyarea/

            As polygon in different direction, the result coulb be
            in different sign:
            If dblArea>0 : polygon in clock wise to the user 
            If dblArea<0: polygon in count clock wise to the user 		
            *******************************************/
            public static double PolygonArea(CPoint2D[] points)
            {
                double dblArea = 0;
                int nNumOfPts = points.Length;

                int j;
                for (int i = 0; i < nNumOfPts; i++)
                {
                    j = (i + 1) % nNumOfPts;
                    dblArea += points[i].X * points[j].Y;
                    dblArea -= (points[i].Y * points[j].X);
                }

                dblArea = dblArea / 2;
                return dblArea;
            }

            /***********************************************
                To check a vertex concave point or a convex point
                -----------------------------------------------------------
                The out polygon is in count clock-wise direction
            ************************************************/
            public VertexType PolygonVertexType(CPoint2D vertex)
            {
                VertexType vertexType = VertexType.ErrorPoint;

                if (PolygonVertex(vertex))
                {
                    CPoint2D pti = vertex;
                    CPoint2D ptj = PreviousPoint(vertex);
                    CPoint2D ptk = NextPoint(vertex);

                    double dArea = PolygonArea(new CPoint2D[] { ptj, pti, ptk });

                    if (dArea < 0)
                        vertexType = VertexType.ConvexPoint;
                    else if (dArea > 0)
                        vertexType = VertexType.ConcavePoint;
                }
                return vertexType;
            }


            /*********************************************
            To check the Line of vertex1, vertex2 is a Diagonal or not
  
            To be a diagonal, Line vertex1-vertex2 has no intersection 
            with polygon lines.
		
            If it is a diagonal, return true;
            If it is not a diagonal, return false;
            reference: www.swin.edu.au/astronomy/pbourke
            /geometry/lineline2d
            *********************************************/
            public bool Diagonal(CPoint2D vertex1, CPoint2D vertex2)
            {
                bool bDiagonal = false;
                int nNumOfVertices = m_aVertices.Length;
                int j = 0;
                for (int i = 0; i < nNumOfVertices; i++) //each point
                {
                    bDiagonal = true;
                    j = (i + 1) % nNumOfVertices;  //next point of i

                    //Diagonal line:
                    double x1 = vertex1.X;
                    double y1 = vertex1.Y;
                    double x2 = vertex1.X;
                    double y2 = vertex1.Y;

                    //CPolygon line:
                    double x3 = m_aVertices[i].X;
                    double y3 = m_aVertices[i].Y;
                    double x4 = m_aVertices[j].X;
                    double y4 = m_aVertices[j].Y;

                    double de = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
                    double ub = -1;

                    if (Math.Abs(de - 0) > ConstantValue.SmallValue)  //lines are not parallel
                        ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / de;

                    if ((ub > 0) && (ub < 1))
                    {
                        bDiagonal = false;
                    }
                }
                return bDiagonal;
            }


            /*************************************************
            To check FaVertices make a convex polygon or 
            concave polygon

            Restriction: the polygon is not self intersecting
            Ref: www.swin.edu.au/astronomy/pbourke
            /geometry/clockwise/index.html
            ********************************************/
            public PolygonType GetPolygonType()
            {
                int nNumOfVertices = m_aVertices.Length;
                bool bSignChanged = false;
                int nCount = 0;
                int j = 0, k = 0;

                for (int i = 0; i < nNumOfVertices; i++)
                {
                    j = (i + 1) % nNumOfVertices; //j:=i+1;
                    k = (i + 2) % nNumOfVertices; //k:=i+2;

                    double crossProduct = (m_aVertices[j].X - m_aVertices[i].X)
                        * (m_aVertices[k].Y - m_aVertices[j].Y);
                    crossProduct = crossProduct - (
                        (m_aVertices[j].Y - m_aVertices[i].Y)
                        * (m_aVertices[k].X - m_aVertices[j].X)
                        );

                    //change the value of nCount
                    if ((crossProduct > 0) && (nCount == 0))
                        nCount = 1;
                    else if ((crossProduct < 0) && (nCount == 0))
                        nCount = -1;

                    if (((nCount == 1) && (crossProduct < 0))
                        || ((nCount == -1) && (crossProduct > 0)))
                        bSignChanged = true;
                }

                if (bSignChanged)
                    return PolygonType.Concave;
                else
                    return PolygonType.Convex;
            }

            /***************************************************
            Check a Vertex is a principal vertex or not
            ref. www-cgrl.cs.mcgill.ca/~godfried/teaching/
            cg-projects/97/Ian/glossay.html
  
            PrincipalVertex: a vertex pi of polygon P is a principal vertex if the
            diagonal pi-1, pi+1 intersects the boundary of P only at pi-1 and pi+1.
            *********************************************************/
            public bool PrincipalVertex(CPoint2D vertex)
            {
                bool bPrincipal = false;
                if (PolygonVertex(vertex)) //valid vertex
                {
                    CPoint2D pt1 = PreviousPoint(vertex);
                    CPoint2D pt2 = NextPoint(vertex);

                    if (Diagonal(pt1, pt2))
                        bPrincipal = true;
                }
                return bPrincipal;
            }

            /*********************************************
            To check whether a given point is a CPolygon Vertex
            **********************************************/
            public bool PolygonVertex(CPoint2D point)
            {
                bool bVertex = false;
                int nIndex = VertexIndex(point);

                if ((nIndex >= 0) && (nIndex <= m_aVertices.Length - 1))
                    bVertex = true;

                return bVertex;
            }

            /*****************************************************
            To reverse polygon vertices to different direction:
            clock-wise <------->count-clock-wise
            ******************************************************/
            public void ReverseVerticesDirection()
            {
                int nVertices = m_aVertices.Length;
                CPoint2D[] aTempPts = new CPoint2D[nVertices];

                for (int i = 0; i < nVertices; i++)
                    aTempPts[i] = m_aVertices[i];

                for (int i = 0; i < nVertices; i++)
                    m_aVertices[i] = aTempPts[nVertices - 1 - i];
            }

            /*****************************************
            To check vertices make a clock-wise polygon or
            count clockwise polygon

            Restriction: the polygon is not self intersecting
            Ref: www.swin.edu.au/astronomy/pbourke/
            geometry/clockwise/index.html
            *****************************************/
            public PolygonDirection VerticesDirection()
            {
                int nCount = 0, j = 0, k = 0;
                int nVertices = m_aVertices.Length;

                for (int i = 0; i < nVertices; i++)
                {
                    j = (i + 1) % nVertices; //j:=i+1;
                    k = (i + 2) % nVertices; //k:=i+2;

                    double crossProduct = (m_aVertices[j].X - m_aVertices[i].X)
                        * (m_aVertices[k].Y - m_aVertices[j].Y);
                    crossProduct = crossProduct - (
                        (m_aVertices[j].Y - m_aVertices[i].Y)
                        * (m_aVertices[k].X - m_aVertices[j].X)
                        );

                    if (crossProduct > 0)
                        nCount++;
                    else
                        nCount--;
                }

                if (nCount < 0)
                    return PolygonDirection.Count_Clockwise;
                else if (nCount > 0)
                    return PolygonDirection.Clockwise;
                else
                    return PolygonDirection.Unknown;
            }


            /*****************************************
            To check given points make a clock-wise polygon or
            count clockwise polygon

            Restriction: the polygon is not self intersecting
            *****************************************/
            public static PolygonDirection PointsDirection(
                CPoint2D[] points)
            {
                int nCount = 0, j = 0, k = 0;
                int nPoints = points.Length;

                if (nPoints < 3)
                    return PolygonDirection.Unknown;

                for (int i = 0; i < nPoints; i++)
                {
                    j = (i + 1) % nPoints; //j:=i+1;
                    k = (i + 2) % nPoints; //k:=i+2;

                    double crossProduct = (points[j].X - points[i].X)
                        * (points[k].Y - points[j].Y);
                    crossProduct = crossProduct - (
                        (points[j].Y - points[i].Y)
                        * (points[k].X - points[j].X)
                        );

                    if (crossProduct > 0)
                        nCount++;
                    else
                        nCount--;
                }

                if (nCount < 0)
                    return PolygonDirection.Count_Clockwise;
                else if (nCount > 0)
                    return PolygonDirection.Clockwise;
                else
                    return PolygonDirection.Unknown;
            }

            /*****************************************************
            To reverse points to different direction (order) :
            ******************************************************/
            public static void ReversePointsDirection(
                CPoint2D[] points)
            {
                int nVertices = points.Length;
                CPoint2D[] aTempPts = new CPoint2D[nVertices];

                for (int i = 0; i < nVertices; i++)
                    aTempPts[i] = points[i];

                for (int i = 0; i < nVertices; i++)
                    points[i] = aTempPts[nVertices - 1 - i];
            }

        }		

        public struct ConstantValue
        {
            internal const double SmallValue = 0.00001;
            internal const double BigValue = 99999;
        }

        public enum VertexType
        {
            ErrorPoint,
            ConvexPoint,
            ConcavePoint
        }

        public enum PolygonType
        {
            Unknown,
            Convex,
            Concave
        }

        public enum PolygonDirection
        {
            Unknown,
            Clockwise,
            Count_Clockwise
        }

        //a Line in 2D coordinate system: ax+by+c=0
        public class CLine
        {
            //line: ax+by+c=0;
            protected double a;
            protected double b;
            protected double c;

            private void Initialize(Double angleInRad, CPoint2D point)
            {
                //angleInRad should be between 0-Pi

                try
                {
                    //if ((angleInRad<0) ||(angleInRad>Math.PI))
                    if (angleInRad > 2 * Math.PI)
                    {
                        string errMsg = string.Format(
                            "The input line angle" +
                            " {0} is wrong. It should be between 0-2*PI.", angleInRad);

                        InvalidInputGeometryDataException ex = new
                            InvalidInputGeometryDataException(errMsg);

                        throw ex;
                    }

                    if (Math.Abs(angleInRad - Math.PI / 2) <
                        ConstantValue.SmallValue) //vertical line
                    {
                        a = 1;
                        b = 0;
                        c = -point.X;
                    }
                    else //not vertical line
                    {
                        a = -Math.Tan(angleInRad);
                        b = 1;
                        c = -a * point.X - b * point.Y;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message + e.StackTrace);
                }
            }


            public CLine(Double angleInRad, CPoint2D point)
            {
                Initialize(angleInRad, point);
            }

            public CLine(CPoint2D point1, CPoint2D point2)
            {
                try
                {
                    if (CPoint2D.SamePoints(point1, point2))
                    {
                        string errMsg = "The input points are the same";
                        InvalidInputGeometryDataException ex = new
                            InvalidInputGeometryDataException(errMsg);
                        throw ex;
                    }

                    //Point1 and Point2 are different points:
                    if (Math.Abs(point1.X - point2.X)
                        < ConstantValue.SmallValue) //vertical line
                    {
                        Initialize(Math.PI / 2, point1);
                    }
                    else if (Math.Abs(point1.Y - point2.Y)
                        < ConstantValue.SmallValue) //Horizontal line
                    {
                        Initialize(0, point1);
                    }
                    else //normal line
                    {
                        double m = (point2.Y - point1.Y) / (point2.X - point1.X);
                        double alphaInRad = Math.Atan(m);
                        Initialize(alphaInRad, point1);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message + e.StackTrace);
                }
            }

            public CLine(CLine copiedLine)
            {
                this.a = copiedLine.a;
                this.b = copiedLine.b;
                this.c = copiedLine.c;
            }

            /*** calculate the distance from a given point to the line ***/
            public double GetDistance(CPoint2D point)
            {
                double x0 = point.X;
                double y0 = point.Y;

                double d = Math.Abs(a * x0 + b * y0 + c);
                d = d / (Math.Sqrt(a * a + b * b));

                return d;
            }

            /*** point(x, y) in the line, based on y, calculate x ***/
            public double GetX(double y)
            {
                //if the line is a horizontal line (a=0), it will return a NaN:
                double x;
                try
                {
                    if (Math.Abs(a) < ConstantValue.SmallValue) //a=0;
                    {
                        throw new NonValidReturnException();
                    }

                    x = -(b * y + c) / a;
                }
                catch (Exception e)  //Horizontal line a=0;
                {
                    x = System.Double.NaN;
                    System.Diagnostics.Trace.
                        WriteLine(e.Message + e.StackTrace);
                }

                return x;
            }

            /*** point(x, y) in the line, based on x, calculate y ***/
            public double GetY(double x)
            {
                //if the line is a vertical line, it will return a NaN:
                double y;
                try
                {
                    if (Math.Abs(b) < ConstantValue.SmallValue)
                    {
                        throw new NonValidReturnException();
                    }
                    y = -(a * x + c) / b;
                }
                catch (Exception e)
                {
                    y = System.Double.NaN;
                    System.Diagnostics.Trace.
                        WriteLine(e.Message + e.StackTrace);
                }
                return y;
            }

            /*** is it a vertical line:***/
            public bool VerticalLine()
            {
                if (Math.Abs(b - 0) < ConstantValue.SmallValue)
                    return true;
                else
                    return false;
            }

            /*** is it a horizontal line:***/
            public bool HorizontalLine()
            {
                if (Math.Abs(a - 0) < ConstantValue.SmallValue)
                    return true;
                else
                    return false;
            }

            /*** calculate line angle in radian: ***/
            public double GetLineAngle()
            {
                if (b == 0)
                {
                    return Math.PI / 2;
                }
                else //b!=0
                {
                    double tanA = -a / b;
                    return Math.Atan(tanA);
                }
            }

            public bool Parallel(CLine line)
            {
                bool bParallel = false;
                if (this.a / this.b == line.a / line.b)
                    bParallel = true;

                return bParallel;
            }

            /**************************************
             Calculate intersection point of two lines
             if two lines are parallel, return null
             * ************************************/
            public CPoint2D IntersecctionWith(CLine line)
            {
                CPoint2D point = new CPoint2D();
                double a1 = this.a;
                double b1 = this.b;
                double c1 = this.c;

                double a2 = line.a;
                double b2 = line.b;
                double c2 = line.c;

                if (!(this.Parallel(line))) //not parallen
                {
                    point.X = (c2 * b1 - c1 * b2) / (a1 * b2 - a2 * b1);
                    point.Y = (a1 * c2 - c1 * a2) / (a2 * b2 - a1 * b2);
                }
                return point;
            }
        }

        public class CLineSegment : CLine
        {
            //line: ax+by+c=0, with start point and end point
            //direction from start point ->end point
            private CPoint2D m_startPoint;
            private CPoint2D m_endPoint;

            public CPoint2D StartPoint
            {
                get
                {
                    return m_startPoint;
                }
            }

            public CPoint2D EndPoint
            {
                get
                {
                    return m_endPoint;
                }
            }

            public CLineSegment(CPoint2D startPoint, CPoint2D endPoint)
                : base(startPoint, endPoint)
            {
                this.m_startPoint = startPoint;
                this.m_endPoint = endPoint;
            }

            /*** chagne the line's direction ***/
            public void ChangeLineDirection()
            {
                CPoint2D tempPt;
                tempPt = this.m_startPoint;
                this.m_startPoint = this.m_endPoint;
                this.m_endPoint = tempPt;
            }

            /*** To calculate the line segment length:   ***/
            public double GetLineSegmentLength()
            {
                double d = (m_endPoint.X - m_startPoint.X) * (m_endPoint.X - m_startPoint.X);
                d += (m_endPoint.Y - m_startPoint.Y) * (m_endPoint.Y - m_startPoint.Y);
                d = Math.Sqrt(d);

                return d;
            }

            /********************************************************** 
                Get point location, using windows coordinate system: 
                y-axes points down.
                Return Value:
                -1:point at the left of the line (or above the line if the line is horizontal)
                 0: point in the line segment or in the line segment 's extension
                 1: point at right of the line (or below the line if the line is horizontal)    
             ***********************************************************/
            public int GetPointLocation(CPoint2D point)
            {
                double Ax, Ay, Bx, By, Cx, Cy;
                Bx = m_endPoint.X;
                By = m_endPoint.Y;

                Ax = m_startPoint.X;
                Ay = m_startPoint.Y;

                Cx = point.X;
                Cy = point.Y;

                if (this.HorizontalLine())
                {
                    if (Math.Abs(Ay - Cy) < ConstantValue.SmallValue) //equal
                        return 0;
                    else if (Ay > Cy)
                        return -1;   //Y Axis points down, point is above the line
                    else //Ay<Cy
                        return 1;    //Y Axis points down, point is below the line
                }
                else //Not a horizontal line
                {
                    //make the line direction bottom->up
                    if (m_endPoint.Y > m_startPoint.Y)
                        this.ChangeLineDirection();

                    double L = this.GetLineSegmentLength();
                    double s = ((Ay - Cy) * (Bx - Ax) - (Ax - Cx) * (By - Ay)) / (L * L);

                    //Note: the Y axis is pointing down:
                    if (Math.Abs(s - 0) < ConstantValue.SmallValue) //s=0
                        return 0; //point is in the line or line extension
                    else if (s > 0)
                        return -1; //point is left of line or above the horizontal line
                    else //s<0
                        return 1;
                }
            }

            /***Get the minimum x value of the points in the line***/
            public double GetXmin()
            {
                return Math.Min(m_startPoint.X, m_endPoint.X);
            }

            /***Get the maximum  x value of the points in the line***/
            public double GetXmax()
            {
                return Math.Max(m_startPoint.X, m_endPoint.X);
            }

            /***Get the minimum y value of the points in the line***/
            public double GetYmin()
            {
                return Math.Min(m_startPoint.Y, m_endPoint.Y);
            }

            /***Get the maximum y value of the points in the line***/
            public double GetYmax()
            {
                return Math.Max(m_startPoint.Y, m_endPoint.Y);
            }

            /***Check whether this line is in a longer line***/
            public bool InLine(CLineSegment longerLineSegment)
            {
                bool bInLine = false;
                if ((m_startPoint.InLine(longerLineSegment)) &&
                    (m_endPoint.InLine(longerLineSegment)))
                    bInLine = true;
                return bInLine;
            }

            /************************************************
             * Offset the line segment to generate a new line segment
             * If the offset direction is along the x-axis or y-axis, 
             * Parameter is true, other wise it is false
             * ***********************************************/
            public CLineSegment OffsetLine(double distance, bool rightOrDown)
            {
                //offset a line with a given distance, generate a new line
                //rightOrDown=true means offset to x incress direction,
                // if the line is horizontal, offset to y incress direction

                CLineSegment line;
                CPoint2D newStartPoint = new CPoint2D();
                CPoint2D newEndPoint = new CPoint2D();

                double alphaInRad = this.GetLineAngle(); // 0-PI
                if (rightOrDown)
                {
                    if (this.HorizontalLine()) //offset to y+ direction
                    {
                        newStartPoint.X = this.m_startPoint.X;
                        newStartPoint.Y = this.m_startPoint.Y + distance;

                        newEndPoint.X = this.m_endPoint.X;
                        newEndPoint.Y = this.m_endPoint.Y + distance;
                        line = new CLineSegment(newStartPoint, newEndPoint);
                    }
                    else //offset to x+ direction
                    {
                        if (Math.Sin(alphaInRad) > 0)
                        {
                            newStartPoint.X = m_startPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                            newStartPoint.Y = m_startPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));

                            newEndPoint.X = m_endPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                            newEndPoint.Y = m_endPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));

                            line = new CLineSegment(
                                           newStartPoint, newEndPoint);
                        }
                        else //sin(FalphaInRad)<0
                        {
                            newStartPoint.X = m_startPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                            newStartPoint.Y = m_startPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));
                            newEndPoint.X = m_endPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                            newEndPoint.Y = m_endPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));

                            line = new CLineSegment(
                                newStartPoint, newEndPoint);
                        }
                    }
                }//{rightOrDown}
                else //leftOrUp
                {
                    if (this.HorizontalLine()) //offset to y directin
                    {
                        newStartPoint.X = m_startPoint.X;
                        newStartPoint.Y = m_startPoint.Y - distance;

                        newEndPoint.X = m_endPoint.X;
                        newEndPoint.Y = m_endPoint.Y - distance;
                        line = new CLineSegment(
                            newStartPoint, newEndPoint);
                    }
                    else //offset to x directin
                    {
                        if (Math.Sin(alphaInRad) >= 0)
                        {
                            newStartPoint.X = m_startPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                            newStartPoint.Y = m_startPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));
                            newEndPoint.X = m_endPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                            newEndPoint.Y = m_endPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));

                            line = new CLineSegment(
                                newStartPoint, newEndPoint);
                        }
                        else //sin(FalphaInRad)<0
                        {
                            newStartPoint.X = m_startPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                            newStartPoint.Y = m_startPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));
                            newEndPoint.X = m_endPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                            newEndPoint.Y = m_endPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));

                            line = new CLineSegment(
                                newStartPoint, newEndPoint);
                        }
                    }
                }
                return line;
            }

            /********************************************************
            To check whether 2 lines segments have an intersection
            *********************************************************/
            public bool IntersectedWith(CLineSegment line)
            {
                double x1 = this.m_startPoint.X;
                double y1 = this.m_startPoint.Y;
                double x2 = this.m_endPoint.X;
                double y2 = this.m_endPoint.Y;
                double x3 = line.m_startPoint.X;
                double y3 = line.m_startPoint.Y;
                double x4 = line.m_endPoint.X;
                double y4 = line.m_endPoint.Y;

                double de = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
                //if de<>0 then //lines are not parallel
                if (Math.Abs(de - 0) < ConstantValue.SmallValue) //not parallel
                {
                    double ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / de;
                    double ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / de;

                    if ((ub > 0) && (ub < 1))
                        return true;
                    else
                        return false;
                }
                else	//lines are parallel
                    return false;
            }

        }

        /// <summary>
        /// Summary description for CPoint2D.
        /// </summary>
        //A point in Coordinate System
        public class CPoint2D
        {
            private double m_dCoordinate_X;
            private double m_dCoordinate_Y;

            public CPoint2D()
            {

            }

            public CPoint2D(double xCoordinate, double yCoordinate)
            {
                m_dCoordinate_X = xCoordinate;
                m_dCoordinate_Y = yCoordinate;
            }

            public double X
            {
                set
                {
                    m_dCoordinate_X = value;
                }
                get
                {
                    return m_dCoordinate_X;
                }
            }

            public double Y
            {
                set
                {
                    m_dCoordinate_Y = value;
                }
                get
                {
                    return m_dCoordinate_Y;
                }
            }

            public static bool SamePoints(CPoint2D Point1,
                CPoint2D Point2)
            {

                double dDeff_X =
                    Math.Abs(Point1.X - Point2.X);
                double dDeff_Y =
                    Math.Abs(Point1.Y - Point2.Y);

                if ((dDeff_X < ConstantValue.SmallValue)
                    && (dDeff_Y < ConstantValue.SmallValue))
                    return true;
                else
                    return false;
            }

            public bool EqualsPoint(CPoint2D newPoint)
            {

                double dDeff_X =
                    Math.Abs(m_dCoordinate_X - newPoint.X);
                double dDeff_Y =
                    Math.Abs(m_dCoordinate_Y - newPoint.Y);

                if ((dDeff_X < ConstantValue.SmallValue)
                    && (dDeff_Y < ConstantValue.SmallValue))
                    return true;
                else
                    return false;

            }

            /***To check whether the point is in a line segment***/
            public bool InLine(CLineSegment lineSegment)
            {
                bool bInline = false;

                double Ax, Ay, Bx, By, Cx, Cy;
                Bx = lineSegment.EndPoint.X;
                By = lineSegment.EndPoint.Y;
                Ax = lineSegment.StartPoint.X;
                Ay = lineSegment.StartPoint.Y;
                Cx = this.m_dCoordinate_X;
                Cy = this.m_dCoordinate_Y;

                double L = lineSegment.GetLineSegmentLength();
                double s = Math.Abs(((Ay - Cy) * (Bx - Ax) - (Ax - Cx) * (By - Ay)) / (L * L));

                if (Math.Abs(s - 0) < ConstantValue.SmallValue)
                {
                    if ((SamePoints(this, lineSegment.StartPoint)) ||
                        (SamePoints(this, lineSegment.EndPoint)))
                        bInline = true;
                    else if ((Cx < lineSegment.GetXmax())
                        && (Cx > lineSegment.GetXmin())
                        && (Cy < lineSegment.GetYmax())
                        && (Cy > lineSegment.GetYmin()))
                        bInline = true;
                }
                return bInline;
            }

            /*** Distance between two points***/
            public double DistanceTo(CPoint2D point)
            {
                return Math.Sqrt((point.X - this.X) * (point.X - this.X)
                    + (point.Y - this.Y) * (point.Y - this.Y));

            }

            public bool PointInsidePolygon(CPoint2D[] polygonVertices)
            {
                if (polygonVertices.Length < 3) //not a valid polygon
                    return false;

                int nCounter = 0;
                int nPoints = polygonVertices.Length;

                CPoint2D s1, p1, p2;
                s1 = this;
                p1 = polygonVertices[0];

                for (int i = 1; i < nPoints; i++)
                {
                    p2 = polygonVertices[i % nPoints];
                    if (s1.Y > Math.Min(p1.Y, p2.Y))
                    {
                        if (s1.Y <= Math.Max(p1.Y, p2.Y))
                        {
                            if (s1.X <= Math.Max(p1.X, p2.X))
                            {
                                if (p1.Y != p2.Y)
                                {
                                    double xInters = (s1.Y - p1.Y) * (p2.X - p1.X) /
                                        (p2.Y - p1.Y) + p1.X;
                                    if ((p1.X == p2.X) || (s1.X <= xInters))
                                    {
                                        nCounter++;
                                    }
                                }  //p1.y != p2.y
                            }
                        }
                    }
                    p1 = p2;
                } //for loop

                if ((nCounter % 2) == 0)
                    return false;
                else
                    return true;
            }

            /*********** Sort points from Xmin->Xmax ******/
            public static void SortPointsByX(CPoint2D[] points)
            {
                if (points.Length > 1)
                {
                    CPoint2D tempPt;
                    for (int i = 0; i < points.Length - 2; i++)
                    {
                        for (int j = i + 1; j < points.Length - 1; j++)
                        {
                            if (points[i].X > points[j].X)
                            {
                                tempPt = points[j];
                                points[j] = points[i];
                                points[i] = tempPt;
                            }
                        }
                    }
                }
            }

            /*********** Sort points from Ymin->Ymax ******/
            public static void SortPointsByY(CPoint2D[] points)
            {
                if (points.Length > 1)
                {
                    CPoint2D tempPt;
                    for (int i = 0; i < points.Length - 2; i++)
                    {
                        for (int j = i + 1; j < points.Length - 1; j++)
                        {
                            if (points[i].Y > points[j].Y)
                            {
                                tempPt = points[j];
                                points[j] = points[i];
                                points[i] = tempPt;
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Summary description for Class1.
        /// </summary>
        public class CPolygonShape
        {
            private CPoint2D[] m_aInputVertices;
            private CPoint2D[] m_aUpdatedPolygonVertices;

            private System.Collections.ArrayList m_alEars
                = new System.Collections.ArrayList();
            private CPoint2D[][] m_aPolygons;

            public int NumberOfPolygons
            {
                get
                {
                    return m_aPolygons.Length;
                }
            }

            public CPoint2D[] Polygons(int index)
            {
                if (index < m_aPolygons.Length)
                    return m_aPolygons[index];
                else
                    return null;
            }

            public CPolygonShape(CPoint2D[] vertices)
            {
                int nVertices = vertices.Length;
                if (nVertices < 3)
                {
                    System.Diagnostics.Trace.WriteLine("To make a polygon, "
                        + " at least 3 points are required!");
                    return;
                }

                //initalize the 2D points
                m_aInputVertices = new CPoint2D[nVertices];

                for (int i = 0; i < nVertices; i++)
                    m_aInputVertices[i] = vertices[i];

                //make a working copy,  m_aUpdatedPolygonVertices are
                //in count clock direction from user view
                SetUpdatedPolygonVertices();
            }

            /****************************************************
            To fill m_aUpdatedPolygonVertices array with input array.
		
            m_aUpdatedPolygonVertices is a working array that will 
            be updated when an ear is cut till m_aUpdatedPolygonVertices
            makes triangle (a convex polygon).
           ******************************************************/
            private void SetUpdatedPolygonVertices()
            {
                int nVertices = m_aInputVertices.Length;
                m_aUpdatedPolygonVertices = new CPoint2D[nVertices];

                for (int i = 0; i < nVertices; i++)
                    m_aUpdatedPolygonVertices[i] = m_aInputVertices[i];

                //m_aUpdatedPolygonVertices should be in count clock wise
                if (CPolygon.PointsDirection(m_aUpdatedPolygonVertices)
                    == PolygonDirection.Clockwise)
                    CPolygon.ReversePointsDirection(m_aUpdatedPolygonVertices);
            }

            /**********************************************************
            To check the Pt is in the Triangle or not.
            If the Pt is in the line or is a vertex, then return true.
            If the Pt is out of the Triangle, then return false.

            This method is used for triangle only.
            ***********************************************************/
            private bool TriangleContainsPoint(CPoint2D[] trianglePts, CPoint2D pt)
            {
                if (trianglePts.Length != 3)
                    return false;

                for (int i = trianglePts.GetLowerBound(0);
                    i < trianglePts.GetUpperBound(0); i++)
                {
                    if (pt.EqualsPoint(trianglePts[i]))
                        return true;
                }

                bool bIn = false;

                CLineSegment line0 = new CLineSegment(trianglePts[0], trianglePts[1]);
                CLineSegment line1 = new CLineSegment(trianglePts[1], trianglePts[2]);
                CLineSegment line2 = new CLineSegment(trianglePts[2], trianglePts[0]);

                if (pt.InLine(line0) || pt.InLine(line1)
                    || pt.InLine(line2))
                    bIn = true;
                else //point is not in the lines
                {
                    double dblArea0 = CPolygon.PolygonArea(new CPoint2D[] { trianglePts[0], trianglePts[1], pt });
                    double dblArea1 = CPolygon.PolygonArea(new CPoint2D[] { trianglePts[1], trianglePts[2], pt });
                    double dblArea2 = CPolygon.PolygonArea(new CPoint2D[] { trianglePts[2], trianglePts[0], pt });

                    if (dblArea0 > 0)
                    {
                        if ((dblArea1 > 0) && (dblArea2 > 0))
                            bIn = true;
                    }
                    else if (dblArea0 < 0)
                    {
                        if ((dblArea1 < 0) && (dblArea2 < 0))
                            bIn = true;
                    }
                }
                return bIn;
            }


            /****************************************************************
            To check whether the Vertex is an ear or not based updated Polygon vertices

            ref. www-cgrl.cs.mcgill.ca/~godfried/teaching/cg-projects/97/Ian
            /algorithm1.html

            If it is an ear, return true,
            If it is not an ear, return false;
            *****************************************************************/
            private bool IsEarOfUpdatedPolygon(CPoint2D vertex)
            {
                CPolygon polygon = new CPolygon(m_aUpdatedPolygonVertices);

                if (polygon.PolygonVertex(vertex))
                {
                    bool bEar = true;
                    if (polygon.PolygonVertexType(vertex) == VertexType.ConvexPoint)
                    {
                        CPoint2D pi = vertex;
                        CPoint2D pj = polygon.PreviousPoint(vertex); //previous vertex
                        CPoint2D pk = polygon.NextPoint(vertex);//next vertex

                        for (int i = m_aUpdatedPolygonVertices.GetLowerBound(0);
                            i < m_aUpdatedPolygonVertices.GetUpperBound(0); i++)
                        {
                            CPoint2D pt = m_aUpdatedPolygonVertices[i];
                            if (!(pt.EqualsPoint(pi) || pt.EqualsPoint(pj) || pt.EqualsPoint(pk)))
                            {
                                if (TriangleContainsPoint(new CPoint2D[] { pj, pi, pk }, pt))
                                    bEar = false;
                            }
                        }
                    } //ThePolygon.getVertexType(Vertex)=ConvexPt
                    else  //concave point
                        bEar = false; //not an ear/
                    return bEar;
                }
                else //not a polygon vertex;
                {
                    System.Diagnostics.Trace.WriteLine("IsEarOfUpdatedPolygon: " +
                        "Not a polygon vertex");
                    return false;
                }
            }

            /****************************************************
            Set up m_aPolygons:
            add ears and been cut Polygon togather
            ****************************************************/
            private void SetPolygons()
            {
                int nPolygon = m_alEars.Count + 1; //ears plus updated polygon
                m_aPolygons = new CPoint2D[nPolygon][];

                for (int i = 0; i < nPolygon - 1; i++) //add ears
                {
                    CPoint2D[] points = (CPoint2D[])m_alEars[i];

                    m_aPolygons[i] = new CPoint2D[3]; //3 vertices each ear
                    m_aPolygons[i][0] = points[0];
                    m_aPolygons[i][1] = points[1];
                    m_aPolygons[i][2] = points[2];
                }

                //add UpdatedPolygon:
                m_aPolygons[nPolygon - 1] = new
                    CPoint2D[m_aUpdatedPolygonVertices.Length];

                for (int i = 0; i < m_aUpdatedPolygonVertices.Length; i++)
                {
                    m_aPolygons[nPolygon - 1][i] = m_aUpdatedPolygonVertices[i];
                }
            }

            /********************************************************
            To update m_aUpdatedPolygonVertices:
            Take out Vertex from m_aUpdatedPolygonVertices array, add 3 points
            to the m_aEars
            **********************************************************/
            private void UpdatePolygonVertices(CPoint2D vertex)
            {
                System.Collections.ArrayList alTempPts = new System.Collections.ArrayList();

                for (int i = 0; i < m_aUpdatedPolygonVertices.Length; i++)
                {
                    if (vertex.EqualsPoint(
                        m_aUpdatedPolygonVertices[i])) //add 3 pts to FEars
                    {
                        CPolygon polygon = new CPolygon(m_aUpdatedPolygonVertices);
                        CPoint2D pti = vertex;
                        CPoint2D ptj = polygon.PreviousPoint(vertex); //previous point
                        CPoint2D ptk = polygon.NextPoint(vertex); //next point

                        CPoint2D[] aEar = new CPoint2D[3]; //3 vertices of each ear
                        aEar[0] = ptj;
                        aEar[1] = pti;
                        aEar[2] = ptk;

                        m_alEars.Add(aEar);
                    }
                    else
                    {
                        alTempPts.Add(m_aUpdatedPolygonVertices[i]);
                    } //not equal points
                }

                if (m_aUpdatedPolygonVertices.Length
                    - alTempPts.Count == 1)
                {
                    int nLength = m_aUpdatedPolygonVertices.Length;
                    m_aUpdatedPolygonVertices = new CPoint2D[nLength - 1];

                    for (int i = 0; i < alTempPts.Count; i++)
                        m_aUpdatedPolygonVertices[i] = (CPoint2D)alTempPts[i];
                }
            }


            /*******************************************************
            To cut an ear from polygon to make ears and an updated polygon:
            *******************************************************/
            public void CutEar()
            {
                CPolygon polygon = new CPolygon(m_aUpdatedPolygonVertices);
                bool bFinish = false;

                //if (polygon.GetPolygonType()==PolygonType.Convex) //don't have to cut ear
                //	bFinish=true;

                if (m_aUpdatedPolygonVertices.Length == 3) //triangle, don't have to cut ear
                    bFinish = true;

                CPoint2D pt = new CPoint2D();
                while (bFinish == false) //UpdatedPolygon
                {
                    int i = 0;
                    bool bNotFound = true;
                    while (bNotFound
                        && (i < m_aUpdatedPolygonVertices.Length)) //loop till find an ear
                    {
                        pt = m_aUpdatedPolygonVertices[i];
                        if (IsEarOfUpdatedPolygon(pt))
                            bNotFound = false; //got one, pt is an ear
                        else
                            i++;
                    } //bNotFount
                    //An ear found:}
                    if (pt != null)
                        UpdatePolygonVertices(pt);

                    polygon = new CPolygon(m_aUpdatedPolygonVertices);
                    //if ((polygon.GetPolygonType()==PolygonType.Convex)
                    //	&& (m_aUpdatedPolygonVertices.Length==3))
                    if (m_aUpdatedPolygonVertices.Length == 3)
                        bFinish = true;
                } //bFinish=false
                SetPolygons();
            }
        }
    }
}
