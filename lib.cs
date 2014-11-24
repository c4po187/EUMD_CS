#region Introduction

/* Geometry Primitives -
 * A namespace that contains some simple geometries, that XNA/Monogame
 * should have bloody well had all along. For example. having to use
 * Rectangle with a height of 1px just to construct a simple line.
 * This namepace eliminates that stupidity and gives you these classes
 * you've been craving for, and then some.
 **********************************************************************
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Rici Underwood
 * c4po187@gmail.com
 * https://github.com/c4po187
 */

#endregion

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

#region Namespaces

namespace EUMD_CS {

    namespace Graphics {

        namespace GeometryPrimitives {

            #region Enumerators

            /// <summary>
            /// Enumerator that determines how we should rotate the primitive, if at all.
            /// </summary>
            public enum RotateState {
                Off, Centre, Vertex, Origin
            }

            #endregion

            #region Classes

            #region Base Class

            /// <summary>
            /// Primitive Geometry base. This class cannot be instantiated.
            /// It merely hold all common fields and functions that are available
            /// to all its child, sub-classes.
            /// </summary>
            public abstract class Primitive : IDisposable {

                #region Fields

                // Protected fields, available to all child, sub-classes
                protected VertexPositionColor[] m_vertices;
                protected DynamicVertexBuffer m_vertexBuffer;
                protected BasicEffect m_basicEffect;
                protected GraphicsDevice m_graphicsDevice;
                protected RasterizerState m_rasterizerState;
                protected Matrix m_world, m_view, m_projection;
                protected Vector3 m_origin;
                protected RotateState m_rotateState;
                protected float m_rotateSpeed;
                protected bool m_bisDisposed;

                #endregion

                #region Properties

                /// <summary>
                /// Property that gets the number of vertices in the primitive.
                /// </summary>
                public int VertexQuantity {
                    get { return m_vertices.Length; }
                }

                /// <summary>
                /// Property that gets and sets the Rotate Speed.
                /// </summary>
                public float RotateSpeed {
                    get { return m_rotateSpeed; }
                    set { m_rotateSpeed = value; }
                }

                /// <summary>
                /// Property that gets and sets the colour of the primitive.
                /// </summary>
                public Color Colour {
                    get { return m_vertices[0].Color; }
                    set {
                        for (int i = 0; i < m_vertices.Length; ++i)
                            m_vertices[i].Color = value;
                    }
                }

                #endregion

                #region Functions

                /// <summary>
                /// Protected Initialization function, to be ran from 
                /// each sub-class constructor in order to set up the 
                /// buffers and matrices.
                /// </summary>
                /// <param name="nVertices">
                /// Parameter represents the number of vertices to be added
                /// to the Vertex Buffer.
                /// </param>
                protected void init(int nVertices) {
                    m_bisDisposed = false;

                    // Set the Rotate State to off initially, speed to zero, and isDynamic to false
                    m_rotateState = RotateState.Off;
                    m_rotateSpeed = 0.0f;

                    // Grab the centre of the viewport
                    Vector2 centre = new Vector2(
                        m_graphicsDevice.Viewport.Width * 0.5f, m_graphicsDevice.Viewport.Height * 0.5f);

                    // Set up our matrices
                    m_world = Matrix.CreateTranslation(0, 0, 0);
                    m_view = Matrix.CreateLookAt(new Vector3(centre, 0), new Vector3(centre, 1), new Vector3(0, -1, 0));
                    m_projection = Matrix.CreateOrthographic(centre.X * 2, centre.Y * 2, -0.5f, 1);

                    // Init BasicEffects
                    m_basicEffect = new BasicEffect(m_graphicsDevice);

                    // Setup rasterizer
                    m_rasterizerState = new RasterizerState();
                    m_rasterizerState.CullMode = CullMode.None;
                    m_graphicsDevice.RasterizerState = m_rasterizerState;

                    // Init VertexBuffer
                    m_vertexBuffer = new DynamicVertexBuffer(m_graphicsDevice,
                       typeof(VertexPositionColor), nVertices, BufferUsage.WriteOnly);
                    m_vertexBuffer.SetData<VertexPositionColor>(m_vertices);
                }

                /// <summary>
                /// User dispose call (frees up unmanaged resources).
                /// </summary>
                public void Dispose() {
                    dispose(true);
                    GC.SuppressFinalize(this);
                }

                /// <summary>
                /// Internal Dispose call inherited from IDisposable.
                /// </summary>
                /// <param name="disposing">
                /// Bool to initiate the disposal process.
                /// </param>
                protected virtual void dispose(bool disposing) {
                    if (m_bisDisposed)
                        return;

                    if (disposing) {
                        if (m_vertices != null) {
                            m_vertices = null;
                        }
                        if (m_vertexBuffer != null) {
                            m_vertexBuffer.Dispose();
                            m_vertexBuffer = null;
                        }
                        if (m_basicEffect != null) {
                            m_basicEffect.Dispose();
                            m_basicEffect = null;
                        }
                        if (m_rasterizerState != null) {
                            m_rasterizerState.Dispose();
                            m_rasterizerState = null;
                        }
                        if (m_graphicsDevice != null) {
                            m_graphicsDevice.Dispose();
                            m_graphicsDevice = null;
                        }
                    }

                    m_bisDisposed = true;
                }

                /// <summary>
                /// Disables the rotation of the primitive.
                /// </summary>
                public void disableRotation() {
                    m_rotateState = RotateState.Off;
                }

                /// <summary>
                /// Enables rotation of the primitive from the origin.
                /// </summary>
                public virtual void enableRotationFromCentre() {
                    m_rotateState = RotateState.Centre;
                }

                /// <summary>
                /// Enables rotation of the Primitive from one of its specified vertices.
                /// </summary>
                /// <param name="vertexIndex">
                /// The index in the vertices list to rotate around.
                /// </param>
                public virtual void enableRotationFromVertex(int vertexIndex) {
                    m_rotateState = RotateState.Vertex;
                    m_origin = m_vertices[vertexIndex].Position;
                }

                /// <summary>
                /// Enables rotation of the primitive around an origin.
                /// </summary>
                /// <param name="origin">
                /// A vector representing the point to rotate around.
                /// </param>
                public virtual void enableRotationAroundOrigin(Vector2 origin) {
                    m_rotateState = RotateState.Origin;
                    m_origin = new Vector3(origin, 0);
                }

                /// <summary>
                /// Moves the primitive by a vector each frame. 
                /// </summary>
                /// <param name="mv_vec">
                /// Parameter represents the directional move vector.
                /// </param>
                /// <param name="gameTime">
                /// Parameter provides the current gametime.
                /// </param>
                public void move(Vector2 mv_vec, GameTime gameTime) {
                    for (int i = 0; i < m_vertices.Length; ++i) {
                        m_vertices[i].Position += new Vector3(mv_vec, 0) *
                            (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }

                /*
                 * TODO:
                 *      The following function needs fixing, pronto!
                 */

                /// <summary>
                /// Checks to see if a line and a circle intersect
                /// </summary>
                /// <param name="line">
                /// A line
                /// </param>
                /// <param name="circle">
                /// Err... a Circle?
                /// </param>
                /// <returns>
                /// True on an intersection
                /// </returns>
                public static bool isIntersectPrimitives(Line line, Circle circle) {
                    double xDiff = (line.EndPoint.X - line.StartPoint.X);
                    double yDiff = (line.EndPoint.Y - line.StartPoint.Y);
                    double dRad = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                    double det = Maths.LinearAlgebra.determinant(line.StartPoint.X, line.StartPoint.Y,
                        line.EndPoint.X, line.EndPoint.Y);

                    double incidence = (Math.Pow(circle.Radius, 2) * Math.Pow(dRad, 2) - Math.Pow(det, 2));

                    return (incidence >= 0);
                }

                /// <summary>
                /// Base Draw Call, prepares the renderer for sub-class draw routines.
                /// </summary>
                /// <param name="gameTime">
                /// Parameter represents the current game time.
                /// </param>
                public virtual void draw(GameTime gameTime) {
                    // Copy over our matrices to the BasicEffect Matrices
                    m_basicEffect.World = m_world;
                    m_basicEffect.View = m_view;
                    m_basicEffect.Projection = m_projection;
                    // Ensure that our Vertices are coloured
                    m_basicEffect.VertexColorEnabled = true;

                    // Apply Rotations
                    if (m_rotateState == RotateState.Centre || m_rotateState == RotateState.Origin) {
                        for (int i = 0; i < m_vertices.Length; ++i) {
                            m_vertices[i].Position = Vector3.Transform(
                                m_vertices[i].Position - m_origin, Matrix.CreateRotationZ(m_rotateSpeed)) + m_origin;
                        }
                    }

                    // Set the Vertex Buffer and its data
                    m_graphicsDevice.SetVertexBuffer(m_vertexBuffer);
                    m_vertexBuffer.SetData<VertexPositionColor>(m_vertices);
                }

                #endregion
            }

            #endregion

            #region Sub-Classes

            /// <summary>
            /// Class that draws a simple line
            /// </summary>
            public class Line : Primitive {

                #region Constructors

                /// <summary>
                /// Default Constructor
                /// </summary>
                public Line() { ; }

                /// <summary>
                /// Constructor: 
                /// Line constructed using two points and a colour.
                /// </summary>
                /// <param name="p0">
                /// Start point, the line will be drawn from here.
                /// </param>
                /// <param name="p1">
                /// End point, the line stops being drawn here.
                /// </param>
                /// <param name="color">
                /// The colour of the line.
                /// </param>
                /// <param name="graphicsDevice">
                /// Parameter represents the current graphics device.
                /// </param>
                public Line(Point p0, Point p1, Color color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_vertices = new VertexPositionColor[2];
                    m_vertices[0] = new VertexPositionColor(new Vector3(p0.X, p0.Y, 0), color);
                    m_vertices[1] = new VertexPositionColor(new Vector3(p1.X, p1.Y, 0), color);
                    m_origin = MidPoint;
                    if (m_graphicsDevice != null)
                        init(m_vertices.Length);
                }

                /// <summary>
                /// Constructor:
                /// Line constructed using 1 point, length and a factor for the angle of the line. 
                /// </summary>
                /// <param name="p0">
                /// The start point of the line.
                /// </param>
                /// <param name="length">
                /// The length of the line.
                /// </param>
                /// <param name="degrees">
                /// The angle of the line (0 is on the right of the origin, and rotates clockwise)
                /// </param>
                /// <param name="color">
                /// The colour of the line.
                /// </param>
                /// <param name="graphicsDevice">
                /// Parameter represents the current graphics device.
                /// </param>
                public Line(Point p0, int length, float degrees, Color color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_vertices = new VertexPositionColor[2];
                    m_vertices[0] = new VertexPositionColor(new Vector3(p0.X, p0.Y, 0), color);
                    m_vertices[1] = new VertexPositionColor(new Vector3(
                        p0.X + (float)(Math.Cos(MathHelper.ToRadians(degrees)) * length),
                        p0.Y + (float)(Math.Sin(MathHelper.ToRadians(degrees)) * length), 0), color);
                    m_origin = MidPoint;
                    if (m_graphicsDevice != null)
                        init(m_vertices.Length);
                }

                #endregion

                #region Destructor

                ~Line() {
                    dispose(false);
                }

                #endregion

                #region Fields

                private int m_nonOriginIndex;

                #endregion

                #region Properties

                /// <summary>
                /// Property that gets the distance between the start point and the end point.
                /// </summary>
                public double Distance {
                    get {
                        return Maths.LinearAlgebra.magnitude(new Vector2(
                            m_vertices[0].Position.X, m_vertices[0].Position.Y),
                            new Vector2(m_vertices[1].Position.X, m_vertices[1].Position.Y));
                    }
                }

                /// <summary>
                /// Property that gets a vector to the start of the line.
                /// </summary>
                public Vector3 StartPoint {
                    get { return m_vertices[0].Position; }
                }

                /// <summary>
                /// Property that returns the centre of the line
                /// using the Midpoint Formula.
                /// </summary>
                public Vector3 MidPoint {
                    get {
                        return new Vector3((m_vertices[0].Position.X + m_vertices[1].Position.X) / 2,
                            (m_vertices[0].Position.Y + m_vertices[1].Position.Y) / 2, 0);
                    }
                }

                /// <summary>
                /// Property that gets a vector to the end of the line.
                /// </summary>
                public Vector3 EndPoint {
                    get { return m_vertices[1].Position; }
                }

                #endregion

                #region Functions

                /// <summary>
                /// Overrides the virtual function in the base class, due to the difference
                /// in geometries.
                /// </summary>
                public override void enableRotationFromCentre() {
                    base.enableRotationFromCentre();
                    m_origin = MidPoint;
                }

                /// <summary>
                /// Override function to enable rotation of the Primitive 
                /// from one of its specified vertices.
                /// </summary>
                /// <param name="vertexIndex">
                /// The index in the vertices list to rotate around.
                /// </param>
                public override void enableRotationFromVertex(int vertexIndex) {
                    if (vertexIndex > 1 || vertexIndex < 0) {
                        throw new Exception("\nA line has 2 vertices, 0 and 1.\r" +
                            "Now please choose one of those fine integers or find the nearest bridge...\rYOU MUPPET!");
                    }
                    else {
                        base.enableRotationFromVertex(vertexIndex);
                        m_nonOriginIndex = vertexIndex ^ 1;
                    }
                }

                /// <summary>
                /// Gets the actual point of intersection between two lines.
                /// </summary>
                /// <param name="other">
                /// Parameter represents the other line to check against.
                /// </param>
                /// <returns>
                /// Returns a vector containing the intercept point.
                /// </returns>
                public Vector2 getInterceptPoint(Line other) {
                    float x1 = this.m_vertices[0].Position.X;
                    float y1 = this.m_vertices[0].Position.Y;
                    float x2 = this.m_vertices[1].Position.X;
                    float y2 = this.m_vertices[1].Position.Y;
                    float x3 = other.m_vertices[0].Position.X;
                    float y3 = other.m_vertices[0].Position.Y;
                    float x4 = other.m_vertices[1].Position.X;
                    float y4 = other.m_vertices[1].Position.Y;

                    float det1_2 = Maths.LinearAlgebra.determinant(x1, y1, x2, y2);
                    float det3_4 = Maths.LinearAlgebra.determinant(x3, y3, x4, y4);
                    float detsubs = Maths.LinearAlgebra.determinant((x1 - x2), (y1 - y2), (x3 - x4), (y3 - y4));

                    if (detsubs == 0)
                        return Vector2.Zero;
                    else {
                        float x = (Maths.LinearAlgebra.determinant(det1_2, (x1 - x2), det3_4, (x3 - x4)) / detsubs);
                        float y = (Maths.LinearAlgebra.determinant(det1_2, (y1 - y2), det3_4, (y3 - y4)) / detsubs);
                        return new Vector2(x, y);
                    }
                }

                /// <summary>
                /// Checks to see if our line intersects another line.
                /// </summary>
                /// <param name="other">
                /// The other line to check the intersection against.
                /// </param>
                /// <returns>
                /// True if the lines cross one an other (intersect).
                /// </returns>
                public bool isIntersecting(Line other) {
                    Vector2 a = new Vector2(this.m_vertices[0].Position.X, this.m_vertices[0].Position.Y);
                    Vector2 b = new Vector2(this.m_vertices[1].Position.X, this.m_vertices[1].Position.Y);
                    Vector2 c = new Vector2(other.m_vertices[0].Position.X, other.m_vertices[0].Position.Y);
                    Vector2 d = new Vector2(other.m_vertices[1].Position.X, other.m_vertices[1].Position.Y);

                    return (Maths.LinearAlgebra.scalarCrossProduct(a, c, d) *
                            Maths.LinearAlgebra.scalarCrossProduct(b, c, d) < 0) &&
                           (Maths.LinearAlgebra.scalarCrossProduct(a, b, c) *
                            Maths.LinearAlgebra.scalarCrossProduct(a, b, d) < 0);
                }

                /// <summary>
                /// Draws a line.
                /// </summary>
                /// <param name="gameTime">
                /// Parameter represents the current game time.
                /// </param>
                public override void draw(GameTime gameTime) {
                    base.draw(gameTime);

                    // Perform our rotations
                    if (m_rotateState == RotateState.Vertex) {
                        m_vertices[m_nonOriginIndex].Position = Vector3.Transform(
                            m_vertices[m_nonOriginIndex].Position - m_origin, Matrix.CreateRotationZ(m_rotateSpeed)) + m_origin;
                    }

                    // Go ahead and render all our vertices, rasterize and all that...
                    foreach (EffectPass pass in m_basicEffect.CurrentTechnique.Passes) {
                        pass.Apply();
                        m_graphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, 1);
                    }
                }

                #endregion
            }

            /// <summary>
            /// Class that draws a circle
            /// </summary>
            public class Circle : Primitive {

                #region Constructors

                /// <summary>
                /// Default Constructor
                /// </summary>
                public Circle() { ; }

                /// <summary>
                /// Constructor
                /// </summary>
                /// <param name="origin">
                /// The centre of the circle.
                /// </param>
                /// <param name="radius">
                /// Radius of the circle.
                /// </param>
                /// <param name="color">
                /// Color of the line.
                /// </param>
                /// <param name="graphicsDevice">
                /// Parameter provides the GraphicsDevice.
                /// </param>
                public Circle(Point origin, float radius, Color color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_origin = new Vector3((float)origin.X, (float)origin.Y, 0);
                    m_sz_centre = m_origin;
                    m_radius = radius;
                    m_vertices = new VertexPositionColor[__m_divisions];
                    createCircle(color);
                    if (m_graphicsDevice != null)
                        init(m_vertices.Length);
                }

                #endregion

                #region Destructor

                ~Circle() {
                    dispose(false);
                }

                #endregion

                #region Fields

                private const int __m_divisions = 360;
                private float m_radius;
                private int m_vertexIndex;
                private Vector3 m_sz_centre;

                #endregion

                #region Properties

                /// <summary>
                /// Property that gets the centre of the circle.
                /// </summary>
                public Vector3 Centre {
                    get {
                        List<float> x_vals = new List<float>();
                        List<float> y_vals = new List<float>();

                        for (int i = 0; i < m_vertices.Length; ++i) {
                            x_vals.Add(m_vertices[i].Position.X);
                            y_vals.Add(m_vertices[i].Position.Y);
                        }

                        return new Vector3((x_vals.Min() + m_radius), (y_vals.Min() + m_radius), 0);
                    }
                }

                /// <summary>
                /// Property that gets the Diameter of the circle.
                /// </summary>
                public float Diameter {
                    get { return (2 * m_radius); }
                }

                /// <summary>
                /// Property that gets the Radius of the circle.
                /// </summary>
                public float Radius {
                    get { return m_radius; }
                }

                /// <summary>
                /// Property that gets the Cicumference of the circle.
                /// </summary>
                public double Circumference {
                    get { return (2 * Math.PI) * m_radius; }
                }

                /// <summary>
                /// Property that gets the Area of the circle.
                /// </summary>
                public double Area {
                    get { return Math.PI * Math.Pow(m_radius, 2); }
                }

                #endregion

                #region Functions

                /// <summary>
                /// Creates the circle.
                /// </summary>
                /// <param name="color">
                /// The colour of the circle.
                /// </param>
                private void createCircle(Color color) {
                    float theta_i = (((float)Math.PI * 2) / m_vertices.Length);
                    for (int i = 0; i < m_vertices.Length; ++i) {
                        double theta = (theta_i * i);
                        double xi = Math.Sin(theta);
                        double yi = Math.Cos(theta);
                        m_vertices[i].Position = new Vector3((m_sz_centre.X + (float)(xi * m_radius)),
                            (m_sz_centre.Y + (float)(yi * m_radius)), 0);
                        m_vertices[i].Color = color;
                    }
                    m_vertices[m_vertices.Length - 1] = m_vertices[0];
                }

                /// <summary>
                /// Enables rotation around the centre of the circle.
                /// </summary>
                public override void enableRotationFromCentre() {
                    base.enableRotationFromCentre();
                    m_origin = Centre;
                }

                /// <summary>
                /// Enables rotation around a vertex from the circle.
                /// </summary>
                /// <param name="vertexIndex">
                /// The index of the vertex to rotate around.
                /// </param>
                public override void enableRotationFromVertex(int vertexIndex) {
                    base.enableRotationFromVertex(vertexIndex);
                    m_vertexIndex = vertexIndex;
                }

                /// <summary>
                /// Checks to see if two circles intersect.
                /// </summary>
                /// <param name="other">
                /// The other circle to check against.
                /// </param>
                /// <returns>
                /// True if there is an intersection.
                /// </returns>
                public bool isIntersecting(Circle other) {
                    /* Construct a line from the centre of our circle to the centre of the other circle.
                     * We will be needing the line for its distance.
                     * */
                    Line c = new Line(new Point((int)this.Centre.X, (int)this.Centre.Y),
                        new Point((int)other.Centre.X, (int)other.Centre.Y), Color.Transparent, null);

                    return ((Maths.LinearAlgebra.absolute(this.Radius - other.Radius) <=
                        Maths.LinearAlgebra.absolute(c.Distance)) && (Maths.LinearAlgebra.absolute(c.Distance) <=
                        Maths.LinearAlgebra.absolute(this.Radius + other.Radius)));
                }

                /// <summary>
                /// Gets both intersection points of the circles.
                /// </summary>
                /// <param name="other">
                /// The other circle to analyze.
                /// </param>
                /// <returns>
                /// A pair of Vectors.
                /// </returns>
                public Pair getIntersectionPoints(Circle other) {
                    // X and Y differences of the circles
                    double xDiff = (other.Centre.X - this.Centre.X);
                    double yDiff = (other.Centre.Y - this.Centre.Y);

                    // Distance between the centres of the circles
                    double d = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

                    // Distance from our circle's centre to a line joining both points of intersection
                    double dl = (Math.Pow(d, 2) + Math.Pow(this.Radius, 2) - Math.Pow(other.Radius, 2)) / (2 * d);

                    // Left hand side intersect point
                    Vector2 i1 = new Vector2((float)(this.Centre.X + ((xDiff * dl) / d) + ((yDiff / d) *
                        Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))),
                        (float)(this.Centre.Y + ((yDiff * dl) / d) - ((xDiff / d) *
                        Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))));

                    // Right hand side intersect point
                    Vector2 i2 = new Vector2((float)(this.Centre.X + ((xDiff * dl) / d) - ((yDiff / d) *
                        Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))),
                        (float)(this.Centre.Y + ((yDiff * dl) / d) + ((xDiff / d) *
                        Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))));

                    // Return both vectors as a pair (can be accessed as Pair.First & Pair.Second)
                    return new Pair(i1, i2);
                }

                /// <summary>
                /// Sets the radius of the circle.
                /// </summary>
                /// <param name="val">
                /// The value to set the radius.
                /// </param>
                public void setRadius(float val) {
                    m_radius = val;
                    m_sz_centre = this.Centre;
                    createCircle(this.Colour);
                }

                /// <summary>
                /// Draws the circle.
                /// </summary>
                /// <param name="gameTime">
                /// Parameter represents the current game time.
                /// </param>
                public override void draw(GameTime gameTime) {
                    base.draw(gameTime);

                    if (m_rotateState == RotateState.Vertex) {
                        for (int i = 0; i < m_vertices.Length; ++i) {
                            if (i != m_vertexIndex) {
                                m_vertices[i].Position = Vector3.Transform(
                                    m_vertices[i].Position - m_origin, Matrix.CreateRotationZ(m_rotateSpeed)) + m_origin;
                            }
                        }
                    }

                    foreach (EffectPass pass in m_basicEffect.CurrentTechnique.Passes) {
                        pass.Apply();
                        m_graphicsDevice.DrawPrimitives(PrimitiveType.LineStrip, 0, m_vertices.Length - 1);
                    }
                }

                #endregion
            }

            #endregion

            #endregion
        }
    }

    namespace Maths {

        #region Classes

        /// <summary>
        /// Provides functions in the form of Linear Algebra.
        /// </summary>
        public class LinearAlgebra {

            #region Functions

            /// <summary>
            /// Factorizes the specified number.
            /// </summary>
            /// <param name="n">
            /// The integer to be factorized.
            /// </param>
            /// <returns>
            /// An integer representing the result of factorization.
            /// </returns>
            public static int factorial(int n) {
                return (n == 1) ? n : (n * factorial(n - 1));
            }

            /// <summary>
            /// Signum function, that determines the sign of
            /// a specified value.
            /// </summary>
            /// <param name="val">
            /// Represents a value to be checked.
            /// </param>
            /// <returns>
            /// An integer value representing the argument values sign,
            /// -1 (signed), 0 (zero), and 1 (unsigned).
            /// </returns>
            public static int signum(double val) {
                return (val == 0) ? 0 : (val < 0) ? -1 : 1;
            }

            /// <summary>
            /// Cross Product of two Vectors.
            /// </summary>
            /// <param name="v0">
            /// Represents the first vector.
            /// </param>
            /// <param name="v1">
            /// Represents the second vector.
            /// </param>
            /// <returns>
            /// Float reprenting the result.
            /// </returns>
            public static float crossProduct(Vector2 v0, Vector2 v1) {
                return (v0.X * v1.Y) - (v1.X * v0.Y);
            }

            /// <summary>
            /// Calculates the Scalar Cross Product between three vectors.
            /// </summary>
            /// <param name="v0">
            /// Represents the first vector.
            /// </param>
            /// <param name="v1">
            /// Represents the second vector.
            /// </param>
            /// <param name="v2">
            /// Represents the third and final vector.
            /// </param>
            /// <returns>
            /// A Float representing the product of the equation.
            /// </returns>
            public static float scalarCrossProduct(Vector2 v0, Vector2 v1, Vector2 v2) {
                return ((v0.X - v1.X) * (v2.Y - v1.Y)) - ((v0.Y - v1.Y) * (v2.X - v1.X));
            }

            /// <summary>
            /// Gets the dot product between Vector2 objects.
            /// </summary>
            /// <param name="v0">
            /// First vector.
            /// </param>
            /// <param name="v1">
            /// Second vector.
            /// </param>
            /// <returns>
            /// A float representing the dot product.
            /// </returns>
            public static float dotProductVec2(Vector2 v0, Vector2 v1) {
                return ((v0.X * v1.X) + (v0.Y * v1.Y));
            }

            /// <summary>
            /// Gets the dot product between Vector3 objects.
            /// </summary>
            /// <param name="v0">
            /// First vector.
            /// </param>
            /// <param name="v1">
            /// Second vector.
            /// </param>
            /// <returns>
            /// A float representing the dot product.
            /// </returns>
            public static float dotProductVec3(Vector3 v0, Vector3 v1) {
                return ((v0.X * v1.X) + (v0.Y * v1.Y) + (v0.Z * v1.Z));
            }

            /// <summary>
            /// Gets the dot product between Vector4 objects.
            /// </summary>
            /// <param name="v0">
            /// First vector.
            /// </param>
            /// <param name="v1">
            /// Second vector.
            /// </param>
            /// <returns>
            /// A float representing the dot product.
            /// </returns>
            public static float dotProductVec4(Vector4 v0, Vector4 v1) {
                return ((v0.X * v1.X) + (v0.Y * v1.Y) + (v0.Z * v1.Z) + (v0.W * v1.W));
            }

            /// <summary>
            /// Gets the determinant based on the specified parameters.
            /// </summary>
            /// <param name="a">
            /// Float value.
            /// </param>
            /// <param name="b">
            /// Float value.
            /// </param>
            /// <param name="c">
            /// Float value.
            /// </param>
            /// <param name="d">
            /// Float value.
            /// </param>
            /// <returns>
            /// A float representing the determinant.
            /// </returns>
            public static float determinant(float a, float b, float c, float d) {
                return (a * d) - (b * c);
            }

            /// <summary>
            /// Gets teh determinant of a matrix.
            /// </summary>
            /// <param name="m">
            /// Parameter represents a 4x4 Matrix.
            /// </param>
            /// <returns>
            /// A float representing the determinant.
            /// </returns>
            public static float determinant(Matrix m) {
                return m.Determinant();
            }

            /// <summary>
            /// Gets the magnitude of two vectors.
            /// </summary>
            /// <param name="v0">
            /// The first vector.
            /// </param>
            /// <param name="v1">
            /// The second vector.
            /// </param>
            /// <returns>
            /// Double representing the magnitude.
            /// </returns>
            public static double magnitude(Vector2 v0, Vector2 v1) {
                return Math.Sqrt(Math.Pow(v1.X - v0.X, 2) + Math.Pow(v1.Y - v0.Y, 2));
            }

            /// <summary>
            /// Gets the absolute value.
            /// </summary>
            /// <param name="val">
            /// The value to analyze.
            /// </param>
            /// <returns>
            /// A double, switching the sign if the value is negative.
            /// </returns>
            public static double absolute(double val) {
                return (val < 0) ? -val : val;
            }

            /// <summary>
            /// Gets the absolute value.
            /// </summary>
            /// <param name="val">
            /// The value to analyze.
            /// </param>
            /// <returns>
            /// An integer, switching the sign if the value is negative.
            /// </returns>
            public static int absolute(int val) {
                return (val < 0) ? -val : val;
            }

            #endregion
        }

        #endregion
    }
}

#endregion