#region Introduction

/* Eh Up Me Duck C Sharp Framework -
 * A framework that provides developers with a hierarchy of classes, 
 * functions and fields to help aid in game development alongside the
 * XNA/Monogame framework. 
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

        #region Structures

        /// <summary>
        /// Simple struct that represents a screen resolution.
        /// </summary>
        public struct Resolution {

            #region Members

            public int width, height;

            #endregion

            #region Overloads

            /// <summary>
            /// Creates the possibility of implicitly converting a
            /// Vector2 to this; Resolution.
            /// </summary>
            /// <param name="vec">
            /// Represents the Vector to be cast.
            /// </param>
            /// <returns>
            /// An instance of Resolution.
            /// </returns>
            public static implicit operator Resolution(Vector2 vec) {
                return new Resolution { width = (int)vec.X, height = (int)vec.Y };
            }

            /// <summary>
            /// Creates the possibility of implicitly converting an
            /// instance of this, Resolution to Vector2.
            /// </summary>
            /// <param name="resolution">
            /// Represents the instance of Resolution to be cast.
            /// </param>
            /// <returns>
            /// An instance of Vector2.
            /// </returns>
            public static implicit operator Vector2(Resolution resolution) {
                return new Vector2((float)resolution.width, (float)resolution.height);
            }

            /// <summary>
            /// Creates the possibility of implicitly converting a
            /// Point to this; Resolution. 
            /// </summary>
            /// <param name="point">
            /// Represents the Point to be cast.
            /// </param>
            /// <returns>
            /// An instance of Resolution.
            /// </returns>
            public static implicit operator Resolution(Point point) {
                return new Resolution { width = point.X, height = point.Y };
            }

            /// <summary>
            /// Creates the possibility of implicitly converting an
            /// instance of this, Resolution to a Point. 
            /// </summary>
            /// <param name="resolution">
            /// Represents the instance of Resolution to be cast.
            /// </param>
            /// <returns>
            /// An instance of Point.
            /// </returns>
            public static implicit operator Point(Resolution resolution) {
                return new Point(resolution.width, resolution.height);
            }

            #endregion
        }

        #endregion

        #region Macros

        /// <summary>
        /// Class that holds pre-defined, screen resolution macros. 
        /// </summary>
        public static class CommonResolutions {
            /// <summary>
            /// HVGA (480 x 320)
            /// </summary>
            public static Resolution HVGA = new Resolution { width = 480, height = 320 };

            /// <summary>
            /// VGA (640 x 480)
            /// </summary>
            public static Resolution VGA = new Resolution { width = 640, height = 480 };

            /// <summary>
            /// WGA (800 x 480)
            /// </summary>
            public static Resolution WGA = new Resolution { width = 800, height = 480 };

            /// <summary>
            /// SVGA (800 x 600) 
            /// </summary>
            public static Resolution SVGA = new Resolution { width = 800, height = 600 };

            /// <summary>
            /// WSVGA (1024 x 600)
            /// </summary>
            public static Resolution WSVGA = new Resolution { width = 1024, height = 600 };

            /// <summary>
            /// WXGA_H (1280 x 720)
            /// </summary>
            public static Resolution WXGA_H = new Resolution { width = 1280, height = 720 };

            /// <summary>
            /// WXGA (1280 x 768)
            /// </summary>
            public static Resolution WXGA = new Resolution { width = 1280, height = 768 };

            /// <summary>
            /// WXGA_MAX (1280 x 800)
            /// </summary>
            public static Resolution WXGA_MAX = new Resolution { width = 1280, height = 800 };

            /// <summary>
            /// NETDEV (1024 x 1024)
            /// </summary>
            public static Resolution NETDEV = new Resolution { width = 1024, height = 1024 };

            /// <summary>
            /// HD_R (1366 x 768)
            /// </summary>
            public static Resolution HD_R = new Resolution { width = 1366, height = 768 };

            /// <summary>
            /// WSXGA (1440 x 900)
            /// </summary>
            public static Resolution WSXGA = new Resolution { width = 1440, height = 900 };

            /// <summary>
            /// SXGA (1280 x 1024)
            /// </summary>
            public static Resolution SXGA = new Resolution { width = 1280, height = 1024 };

            /// <summary>
            /// HD_PLUS (1600 x 900)
            /// </summary>
            public static Resolution HD_PLUS = new Resolution { width = 1600, height = 900 };

            /// <summary>
            /// A4 (1440 x 1024)
            /// </summary>
            public static Resolution A4 = new Resolution { width = 1440, height = 1024 };

            /// <summary>
            /// HDV (1440 x 1080)
            /// </summary>
            public static Resolution HDV = new Resolution { width = 1440, height = 1080 };

            /// <summary>
            /// UXGA (1600 x 1200)
            /// </summary>
            public static Resolution UXGA = new Resolution { width = 1600, height = 1200 };

            /// <summary>
            /// FULL_HD (1920 x 1080)
            /// </summary>
            public static Resolution FULL_HD = new Resolution { width = 1920, height = 1080 };
        }

        #endregion

		namespace Imaging {
			
			#region Public Enumerators

			/// <summary>
			/// Determines the sequence of fade transitions.
			/// </summary>
			public enum FadeSequence {
				In, Out, In_Out, Out_In
			}

			/// <summary>
			/// Tracks the current state of the transition.
			/// </summary>
			public enum FadeState {
				Delay, InFade, OnImage, OutFade, EndBlackTime, Completed
			}

			#endregion
			
			#region Objects

			/// <summary>
			/// Provides a simple to use mechanism, allowing texture fading to a predefined sequence.
			/// Requires the graphics device to be cleared to black (or any other colour of choosing),
			/// as this class modifies the opacity value used to display a texture.
			/// </summary>
			public class Fader {

				#region Constructors

				/// <summary>
				/// Common Constructor.
				/// </summary>
				/// <param name="imageTexture">
				/// Texture containing the image to be faded.
				/// </param>
				/// <param name="imageTopLeft">
				/// A point in the Euclidean 2D Plane representing the top left corner of the image.
				/// </param>
				/// <param name="width">
				/// The desired width of the image.
				/// </param>
				/// <param name="height">
				/// The desired height of the image.
				/// </param>
				/// <param name="fadeSequence">
				/// The desired sequence the animation will follow.
				/// </param>
				/// <param name="delay">
				/// Delay (seconds) before the fading begins.
				/// </param>
				/// <param name="inDuration">
				/// 'Fade In' duration (seconds).
				/// </param>
				/// <param name="imageFocusDuration">
				/// Focus duration (seconds).
				/// </param>
				/// <param name="outDuration">
				/// 'Fade Out' duration (seconds).
				/// </param>
				public Fader(Texture2D imageTexture, Point imageTopLeft, int width, int height, FadeSequence fadeSequence,
					float? delay, float? inDuration, float? imageFocusDuration, float? outDuration, float? blackTime) {
					// Set members
					m_imgTexture = imageTexture;
					m_imgTopLeft = imageTopLeft;
					m_imgWidth = width;
					m_imgHeight = height;
					m_fadeSeq = fadeSequence;

					// Set timing members if needed
					if (inDuration.HasValue) m_inDuration = inDuration.Value;
					if (imageFocusDuration.HasValue) m_OnImgDuration = imageFocusDuration.Value;
					if (outDuration.HasValue) m_outDuration = outDuration.Value;
					if (blackTime.HasValue) m_blackTime = blackTime.Value;

					// Set rectangle
					m_imgRect = new Rectangle(m_imgTopLeft.X, m_imgTopLeft.Y, m_imgWidth, m_imgHeight);

					// Set fade state
					if (delay.HasValue) {
						m_delay = delay.Value;
						m_fadeState = FadeState.Delay;
					}
					else {
						if (fadeSequence == FadeSequence.In || fadeSequence == FadeSequence.In_Out)
							m_fadeState = FadeState.InFade;
						else {
							if (imageFocusDuration.HasValue)
								m_fadeState = FadeState.OnImage;
							else
								m_fadeState = FadeState.OutFade;
						}
					}

					// Set Opacity
					switch (m_fadeState) { 
						case FadeState.Completed:
							break;
						case FadeState.Delay:
							if (fadeSequence == FadeSequence.In || fadeSequence == FadeSequence.In_Out)
								m_opacity = 0f;
							else
								m_opacity = 1f;
							break;
						case FadeState.InFade:
							m_opacity = 0f;
							break;
						case FadeState.OutFade:
							m_opacity = 1f;
							break;
						case FadeState.OnImage:
							m_opacity = 1f;
							break;
						case FadeState.EndBlackTime:
							m_opacity = 0f;
							break;
					}

					// Init timer
					m_timeElapsed = 0f;
				}

				#endregion

				#region Declarations

				private Texture2D       m_imgTexture;
				private Point           m_imgTopLeft;
				private Rectangle       m_imgRect;
				private int             m_imgWidth,     m_imgHeight;
				private float           m_delay,        m_inDuration,       m_OnImgDuration,        m_outDuration, 
										m_blackTime,    m_timeElapsed,  m_opacity;
				private FadeSequence    m_fadeSeq;
				private FadeState       m_fadeState;

				#endregion

				#region Properties

				/// <summary>
				/// Gets the image texture.
				/// </summary>
				public Texture2D ImageTexture { 
					get { return m_imgTexture; }
				}

				/// <summary>
				/// Gets and sets the image's top left corner.
				/// </summary>
				public Point ImageTopLeft {
					get { return m_imgTopLeft; }
					set {
						m_imgTopLeft = value;
						m_imgRect.X = m_imgTopLeft.X;
						m_imgRect.Y = m_imgTopLeft.Y;
					}
				}

				/// <summary>
				/// Gets and sets the image width.
				/// </summary>
				public int Width {
					get { return m_imgWidth; }
					set {
						m_imgWidth = value;
						m_imgRect.Width = m_imgWidth;
					}
				}

				/// <summary>
				/// Gets and sets the image height.
				/// </summary>
				public int Height {
					get { return m_imgHeight; }
					set {
						m_imgHeight = value;
						m_imgRect.Height = m_imgHeight;
					}
				}

				/// <summary>
				/// Gets and sets the image opacity,
				/// </summary>
				public float Opacity {
					get { return m_opacity; }
					set { m_opacity = value; }
				}

				/// <summary>
				/// Gets and sets the current state of the transition.
				/// </summary>
				public FadeState CurrentState {
					get { return m_fadeState; }
					set { m_fadeState = value; }
				}

				/// <summary>
				/// Gets the predefined sequence.
				/// </summary>
				public FadeSequence Sequence {
					get { return m_fadeSeq; }
				}

				/// <summary>
				/// Gets and sets the transition delay.
				/// </summary>
				public float Delay {
					get { return m_delay; }
					set { m_delay = value; }
				}

				/// <summary>
				/// Gets and sets the 'Fade In' duration.
				/// </summary>
				public float FadeIn_Duration {
					get { return m_inDuration; }
					set { m_inDuration = value; }
				}

				/// <summary>
				/// Gets and sets the 'Fade Out' duration.
				/// </summary>
				public float FadeOut_Duration {
					get { return m_outDuration; }
					set { m_outDuration = value; }
				}

				/// <summary>
				/// Gets and sets the focus duration.
				/// </summary>
				public float FocusDuration {
					get { return m_OnImgDuration; }
					set { m_OnImgDuration = value; }
				}

				/// <summary>
				/// Gets and sets the end black time duration.
				/// </summary>
				public float BlackTime {
					get { return m_blackTime; }
					set { m_blackTime = value; }
				}

				/// <summary>
				/// Gets and sets the Timer.
				/// </summary>
				public float TimeElapsed {
					get { return m_timeElapsed; }
					set { m_timeElapsed = value; }
				}

				#endregion

				#region Functions

				/// <summary>
				/// The purpose of this function is to modify the opacity value, in reflection
				/// to the current state of the fade transition.
				/// </summary>
				/// <param name="gameTime">
				/// Provides a snapshot of the games timing values.
				/// </param>
				public void update(GameTime gameTime) {
					if (m_fadeState != FadeState.Completed)
						m_timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

					switch (m_fadeState) {
						case FadeState.Completed:
							break;
						case FadeState.Delay:
							if (m_timeElapsed > m_delay) {
								// Switch up state
								if (m_fadeSeq == FadeSequence.In || m_fadeSeq == FadeSequence.In_Out)
									m_fadeState = FadeState.InFade;
								else
									m_fadeState = FadeState.OutFade;
								// Reset timer
								m_timeElapsed = 0f;
							}
							break;
						case FadeState.InFade:
							m_opacity = (1f / m_inDuration) * m_timeElapsed;
							if (m_timeElapsed > m_inDuration) {
								// Set image to fully opaque
								m_opacity = 1f;
								// Switch up state
								if (m_fadeSeq == FadeSequence.In || m_fadeSeq == FadeSequence.Out_In)
									m_fadeState = FadeState.EndBlackTime;
								else
									m_fadeState = FadeState.OnImage;
								// Reset timer
								m_timeElapsed = 0f;
							}
							break;
						case FadeState.OnImage:
							if (m_timeElapsed > m_OnImgDuration) { 
								// Switch up state
								if (m_fadeSeq == FadeSequence.In_Out)
									m_fadeState = FadeState.OutFade;
								else if (m_fadeSeq == FadeSequence.Out_In)
									m_fadeState = FadeState.InFade;
								else
									m_fadeState = FadeState.EndBlackTime;
								// Reset timer
								m_timeElapsed = 0f;
							}
							break;
						case FadeState.OutFade:
							m_opacity = (1f - ((1f / m_outDuration) * m_timeElapsed));
							if (m_timeElapsed > m_outDuration) { 
								// Set image to fully transparent
								m_opacity = 0f;
								// Switch up state
								if (m_fadeSeq == FadeSequence.Out_In)
									m_fadeState = FadeState.OnImage;
								else
									m_fadeState = FadeState.EndBlackTime;
								// Reset timer
								m_timeElapsed = 0f;
							}
							break;
						case FadeState.EndBlackTime:
							m_opacity = 0f;
							if (m_timeElapsed > m_blackTime)
								m_fadeState = FadeState.Completed;
							break;
					}
				}

				/// <summary>
				/// Renders the fading texture to the screen.
				/// </summary>
				/// <param name="spriteBatch"></param>
				public void draw(SpriteBatch spriteBatch) {
					spriteBatch.Draw(m_imgTexture, m_imgRect, (Color.White * m_opacity)); 
				}

				#endregion
			}

            #endregion
		}

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
            /// 
            /// NOTE:
            ///     Before moving, or resizing any of the primitives, rotation
            ///     must be disabled first if it is enabled.
            /// </summary>
            public abstract class Primitive : IDisposable {

                #region Fields

                // Protected fields, available to all child, sub-classes
                protected VertexPositionColor[] m_vertices;
                protected DynamicVertexBuffer m_vertexBuffer;
                protected VertexDeclaration m_vertexDeclaration;
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
                    m_rasterizerState.FillMode = FillMode.WireFrame;
                    m_graphicsDevice.RasterizerState = m_rasterizerState;

                    // Setup Vertex Declaration
                    m_vertexDeclaration = new VertexDeclaration(new VertexElement[] {
                        new VertexElement(0, VertexElementFormat.Vector3,
                            VertexElementUsage.Position, 0),
                        new VertexElement(sizeof(float) * 3,
                            VertexElementFormat.Color, VertexElementUsage.Color, 0)
                    });

                    // Init VertexBuffer
                    //m_vertexBuffer = new DynamicVertexBuffer(m_graphicsDevice,
                    //   typeof(VertexPositionColor), nVertices, BufferUsage.WriteOnly);
                    m_vertexBuffer = new DynamicVertexBuffer(m_graphicsDevice, m_vertexDeclaration,
                        m_vertices.Length, BufferUsage.None);
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

                /// <summary>
                /// Function provides a mechanism for checking whether a line and circle intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="line">
                /// A Line.
                /// </param>
                /// <param name="circle">
                /// An Circle.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a pair
                /// containing the points of intersection between the two primitives.
                /// </returns>
                public static Tuple<bool, Pair> getPointsOnIntersect(Line line, Circle circle) {
                    // First check if there is in fact an intersection at all
                    Vector3 sc = circle.Centre - line.StartPoint;
                    Vector3 ec = line.EndPoint - line.StartPoint;
                    float ecd = Maths.LinearAlgebra.dotProductVec3(ec, ec);
                    float secd = Maths.LinearAlgebra.dotProductVec3(sc, ec);
                    float _t = secd / ecd;

                    if (_t < 0) _t = 0; else if (_t > 1) _t = 1;

                    Vector3 h = ((ec * _t) + line.StartPoint) - circle.Centre;
                    float hd = Maths.LinearAlgebra.dotProductVec3(h, h);

                    bool bIntersects = (hd <= Math.Pow(circle.Radius, 2));

                    // Setup empty vectors for no intersection (we have to return something)
                    Vector2 i1, i2;
                    i1 = i2 = new Vector2(float.NaN, float.NaN);

                    // Now calculate the intersection points
                    double dx = (line.EndPoint.X - line.StartPoint.X);
                    double dy = (line.EndPoint.Y - line.StartPoint.Y);

                    double a = (Math.Pow(dx, 2) + Math.Pow(dy, 2));
                    double b = 2 * (dx * (line.StartPoint.X - circle.Centre.X) +
                        dy * (line.StartPoint.Y - circle.Centre.Y));
                    double c = (Math.Pow((line.StartPoint.X - circle.Centre.X), 2) +
                        Math.Pow((line.StartPoint.Y - circle.Centre.Y), 2) -
                        Math.Pow(circle.Radius, 2));
                    double d = Math.Pow(b, 2) - 4 * a * c;
                    double t;

                    if ((a >= Maths.LinearAlgebra.EPSILON) || (d >= 0)) {
                        if (d == 0) {
                            // One solution
                            t = -b / (2 * a);
                            i1 = new Vector2((float)(line.StartPoint.X + t * dx),
                                (float)(line.StartPoint.Y + t * dy));
                        }
                        else {
                            // Two solutions
                            t = ((-b + Math.Sqrt(d)) / (2 * a));
                            i1 = new Vector2((float)(line.StartPoint.X + t * dx),
                                (float)(line.StartPoint.Y + t * dy));
                            t = ((-b - Math.Sqrt(d)) / (2 * a));
                            i2 = new Vector2((float)(line.StartPoint.X + t * dx),
                                (float)(line.StartPoint.Y + t * dy));
                        }
                    }

                    return Tuple.Create(bIntersects, new Pair(i1, i2));
                }

                /// <summary>
                /// Function provides a mechanism for checking whether a line and triangle intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="line">
                /// A Line.
                /// </param>
                /// <param name="triangle">
                /// A Triangle.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a pair 
                /// containing the points of intersection between the two primitives.
                /// </returns>
                public static Tuple<bool, Pair> getPointsOnIntersect(Line line, Triangle triangle) {
                    bool bIntersects = false;
                    Vector2 nullvec = new Vector2(float.NaN, float.NaN);
                    List<Vector2> isecs = new List<Vector2>();

                    Line[] tris = new Line[3];
                    tris[0] = new Line(new Point((int)triangle.Vertex_A.X, (int)triangle.Vertex_A.Y),
                        new Point((int)triangle.Vertex_B.X, (int)triangle.Vertex_B.Y), Color.Transparent, null);
                    tris[1] = new Line(new Point((int)triangle.Vertex_B.X, (int)triangle.Vertex_B.Y),
                        new Point((int)triangle.Vertex_C.X, (int)triangle.Vertex_C.Y), Color.Transparent, null);
                    tris[2] = new Line(new Point((int)triangle.Vertex_C.X, (int)triangle.Vertex_C.Y),
                        new Point((int)triangle.Vertex_A.X, (int)triangle.Vertex_A.Y), Color.Transparent, null);

                    foreach (Line t in tris) {
                        if (t.getPointsOnIntersect(line).Item1) {
                            bIntersects = true;
                            isecs.Add(t.getPointsOnIntersect(line).Item2);
                        }
                    }

                    Pair points = new Pair();
                    if (isecs.Count > 0) {
                        points.First = (Vector2)isecs[0];
                        if (isecs.Count > 1)
                            points.Second = (Vector2)isecs[1];
                        else
                            points.Second = (Vector2)nullvec;
                    }

                    return Tuple.Create(bIntersects, points);
                }

                /// <summary>
                /// Function provides a mechanism for checking whether a line and oblong intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="line">
                /// A Line.
                /// </param>
                /// <param name="oblong">
                /// An Oblong.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two primitives.
                /// </returns>
                public static Tuple<bool, List<Vector2>> getPointsOnIntersect(Line line, Oblong oblong) {
                    bool bIntersects = false;
                    List<Vector2> isecs = new List<Vector2>();

                    Line[] oLines = new Line[4];
                    oLines[0] = new Line(new Point((int)oblong.m_vertices[0].Position.X, (int)oblong.m_vertices[0].Position.Y),
                        new Point((int)oblong.m_vertices[1].Position.X, (int)oblong.m_vertices[1].Position.Y),
                        Color.Transparent, null);
                    oLines[1] = new Line(new Point((int)oblong.m_vertices[1].Position.X, (int)oblong.m_vertices[1].Position.Y),
                        new Point((int)oblong.m_vertices[2].Position.X, (int)oblong.m_vertices[2].Position.Y),
                        Color.Transparent, null);
                    oLines[2] = new Line(new Point((int)oblong.m_vertices[2].Position.X, (int)oblong.m_vertices[2].Position.Y),
                        new Point((int)oblong.m_vertices[3].Position.X, (int)oblong.m_vertices[3].Position.Y),
                        Color.Transparent, null);
                    oLines[3] = new Line(new Point((int)oblong.m_vertices[3].Position.X, (int)oblong.m_vertices[3].Position.Y),
                        new Point((int)oblong.m_vertices[0].Position.X, (int)oblong.m_vertices[0].Position.Y),
                        Color.Transparent, null);

                    foreach (Line o in oLines) {
                        if (o.getPointsOnIntersect(line).Item1) {
                            bIntersects = true;
                            isecs.Add(o.getPointsOnIntersect(line).Item2);
                        }
                    }

                    return Tuple.Create(bIntersects, isecs);
                }

                /// <summary>
                /// Function provides a mechanism for checking whether a circle and triangle intersect,
                /// and also retrieves the points of intersection. 
                /// </summary>
                /// <param name="circle">
                /// A Circle.
                /// </param>
                /// <param name="triangle">
                /// A Triangle.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two primitives. 
                /// </returns>
                public static Tuple<bool, List<Vector2>> getPointsOnIntersect(Circle circle, Triangle triangle) {
                    bool bIntersects = false;
                    List<Vector2> isecs = new List<Vector2>();

                    // Create 3 Lines from the triangle
                    Line[] tris = new Line[3];
                    tris[0] = new Line(new Point((int)triangle.Vertex_A.X, (int)triangle.Vertex_A.Y),
                        new Point((int)triangle.Vertex_B.X, (int)triangle.Vertex_B.Y), Color.Transparent, null);
                    tris[1] = new Line(new Point((int)triangle.Vertex_B.X, (int)triangle.Vertex_B.Y),
                        new Point((int)triangle.Vertex_C.X, (int)triangle.Vertex_C.Y), Color.Transparent, null);
                    tris[2] = new Line(new Point((int)triangle.Vertex_C.X, (int)triangle.Vertex_C.Y),
                        new Point((int)triangle.Vertex_A.X, (int)triangle.Vertex_A.Y), Color.Transparent, null);

                    foreach (Line t in tris) {
                        if (getPointsOnIntersect(t, circle).Item1) {
                            bIntersects = true;
                            Pair cPoints = getPointsOnIntersect(t, circle).Item2;
                            isecs.Add((Vector2)cPoints.First);
                            isecs.Add((Vector2)cPoints.Second);
                        }
                    }

                    return Tuple.Create(bIntersects, isecs);
                }

                /// <summary>
                /// Function provides a mechanism for checking whether a circle and an oblong intersect,
                /// and also retrieves the points of intersection. 
                /// </summary>
                /// <param name="circle">
                /// A Circle.
                /// </param>
                /// <param name="oblong">
                /// An Oblong.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two primitives. 
                /// </returns>
                public static Tuple<bool, List<Vector2>> getPointsOnIntersect(Circle circle, Oblong oblong) {
                    bool bIntersects = false;
                    List<Vector2> isecs = new List<Vector2>();

                    // Create an array of lines from the Oblong
                    Line[] oLines = new Line[4];
                    oLines[0] = new Line(new Point((int)oblong.m_vertices[0].Position.X, (int)oblong.m_vertices[0].Position.Y),
                        new Point((int)oblong.m_vertices[1].Position.X, (int)oblong.m_vertices[1].Position.Y),
                        Color.Transparent, null);
                    oLines[1] = new Line(new Point((int)oblong.m_vertices[1].Position.X, (int)oblong.m_vertices[1].Position.Y),
                        new Point((int)oblong.m_vertices[2].Position.X, (int)oblong.m_vertices[2].Position.Y),
                        Color.Transparent, null);
                    oLines[2] = new Line(new Point((int)oblong.m_vertices[2].Position.X, (int)oblong.m_vertices[2].Position.Y),
                        new Point((int)oblong.m_vertices[3].Position.X, (int)oblong.m_vertices[3].Position.Y),
                        Color.Transparent, null);
                    oLines[3] = new Line(new Point((int)oblong.m_vertices[3].Position.X, (int)oblong.m_vertices[3].Position.Y),
                        new Point((int)oblong.m_vertices[0].Position.X, (int)oblong.m_vertices[0].Position.Y),
                        Color.Transparent, null);

                    foreach (Line o in oLines) {
                        if (getPointsOnIntersect(o, circle).Item1) {
                            bIntersects = true;
                            Pair points = getPointsOnIntersect(o, circle).Item2;
                            isecs.Add((Vector2)points.First);
                            isecs.Add((Vector2)points.Second);
                        }
                    }

                    return Tuple.Create(bIntersects, isecs);
                }

                /// <summary>
                /// Function provides a mechanism for checking whether a triangle and an oblong intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="triangle">
                /// A Triangle.
                /// </param>
                /// <param name="oblong">
                /// An Oblong.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two primitives. 
                /// </returns>
                public static Tuple<bool, List<Vector2>> getPointsOnIntersect(Triangle triangle, Oblong oblong) {
                    bool bIntersects = false;
                    List<Vector2> isecs = new List<Vector2>();

                    // Create array of Lines from Triangle
                    Line[] tris = new Line[3];
                    tris[0] = new Line(new Point((int)triangle.Vertex_A.X, (int)triangle.Vertex_A.Y),
                        new Point((int)triangle.Vertex_B.X, (int)triangle.Vertex_B.Y), Color.Transparent, null);
                    tris[1] = new Line(new Point((int)triangle.Vertex_B.X, (int)triangle.Vertex_B.Y),
                        new Point((int)triangle.Vertex_C.X, (int)triangle.Vertex_C.Y), Color.Transparent, null);
                    tris[2] = new Line(new Point((int)triangle.Vertex_C.X, (int)triangle.Vertex_C.Y),
                        new Point((int)triangle.Vertex_A.X, (int)triangle.Vertex_A.Y), Color.Transparent, null);

                    // Create array of lines from the Oblong
                    Line[] oLines = new Line[4];
                    oLines[0] = new Line(new Point((int)oblong.m_vertices[0].Position.X, (int)oblong.m_vertices[0].Position.Y),
                        new Point((int)oblong.m_vertices[1].Position.X, (int)oblong.m_vertices[1].Position.Y),
                        Color.Transparent, null);
                    oLines[1] = new Line(new Point((int)oblong.m_vertices[1].Position.X, (int)oblong.m_vertices[1].Position.Y),
                        new Point((int)oblong.m_vertices[2].Position.X, (int)oblong.m_vertices[2].Position.Y),
                        Color.Transparent, null);
                    oLines[2] = new Line(new Point((int)oblong.m_vertices[2].Position.X, (int)oblong.m_vertices[2].Position.Y),
                        new Point((int)oblong.m_vertices[3].Position.X, (int)oblong.m_vertices[3].Position.Y),
                        Color.Transparent, null);
                    oLines[3] = new Line(new Point((int)oblong.m_vertices[3].Position.X, (int)oblong.m_vertices[3].Position.Y),
                        new Point((int)oblong.m_vertices[0].Position.X, (int)oblong.m_vertices[0].Position.Y),
                        Color.Transparent, null);

                    foreach (Line t in tris) {
                        foreach (Line o in oLines) {
                            if (t.getPointsOnIntersect(o).Item1) {
                                bIntersects = true;
                                isecs.Add(t.getPointsOnIntersect(o).Item2);
                            }
                        }
                    }

                    return Tuple.Create(bIntersects, isecs);
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
                public Line(Point p0, Point p1, Color? color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_vertices = new VertexPositionColor[2];
                    m_vertices[0] = new VertexPositionColor(new Vector3(p0.X, p0.Y, 0), color.Value);
                    m_vertices[1] = new VertexPositionColor(new Vector3(p1.X, p1.Y, 0), color.Value);
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
                public Line(Point p0, int length, float degrees, Color? color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_vertices = new VertexPositionColor[2];
                    m_vertices[0] = new VertexPositionColor(new Vector3(p0.X, p0.Y, 0), color.Value);
                    m_vertices[1] = new VertexPositionColor(new Vector3(
                        p0.X + (float)(Math.Cos(MathHelper.ToRadians(degrees)) * length),
                        p0.Y + (float)(Math.Sin(MathHelper.ToRadians(degrees)) * length), 0), color.Value);
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
                /// Function provides a mechanism for checking whether two lines intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="other">
                /// The other line to check against.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two lines.
                /// </returns>
                public Tuple<bool, Vector2> getPointsOnIntersect(Line other) {
                    // Check whether there is an intersection initially
                    bool bIntersects = false;

                    Vector2 a = new Vector2(this.m_vertices[0].Position.X, this.m_vertices[0].Position.Y);
                    Vector2 b = new Vector2(this.m_vertices[1].Position.X, this.m_vertices[1].Position.Y);
                    Vector2 c = new Vector2(other.m_vertices[0].Position.X, other.m_vertices[0].Position.Y);
                    Vector2 d = new Vector2(other.m_vertices[1].Position.X, other.m_vertices[1].Position.Y);

                    if ((Maths.LinearAlgebra.scalarCrossProduct(a, c, d) *
                            Maths.LinearAlgebra.scalarCrossProduct(b, c, d) < 0) &&
                           (Maths.LinearAlgebra.scalarCrossProduct(a, b, c) *
                            Maths.LinearAlgebra.scalarCrossProduct(a, b, d) < 0)) bIntersects = true;

                    // Now get the intersect point
                    Vector2 isec = new Vector2(float.NaN, float.NaN);

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

                    float x, y;

                    if (detsubs != 0) {
                        x = (Maths.LinearAlgebra.determinant(det1_2, (x1 - x2), det3_4, (x3 - x4)) / detsubs);
                        y = (Maths.LinearAlgebra.determinant(det1_2, (y1 - y2), det3_4, (y3 - y4)) / detsubs);
                        isec = new Vector2(x, y);
                    }

                    return Tuple.Create(bIntersects, isec);
                }

                /// <summary>
                /// Adjusts the length of the line.
                /// </summary>
                /// <param name="val">
                /// The value to add to or subtract from the line. 
                /// </param>
                public void adjustLength(float val) {
                    if (m_vertices[1].Position.X > m_vertices[0].Position.X)
                        m_vertices[1].Position.X += val;
                    else if (m_vertices[1].Position.X < m_vertices[0].Position.X)
                        m_vertices[1].Position.X -= val;
                    else {
                        if (m_vertices[1].Position.Y >= m_vertices[0].Position.Y)
                            m_vertices[1].Position.Y += val;
                        else
                            m_vertices[1].Position.Y -= val;
                    }
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
                public Circle(Point origin, float radius, Color? color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_origin = new Vector3((float)origin.X, (float)origin.Y, 0);
                    m_sz_centre = m_origin;
                    m_radius = radius;
                    m_vertices = new VertexPositionColor[__m_divisions];
                    createCircle(color.Value);
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
                /// Function provides a mechanism for checking whether two circles intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="other">
                /// The other Circle to check against.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two circles.
                /// </returns>
                public Tuple<bool, Pair> getPointsOnIntersect(Circle other) {
                    // Check for an intersection initially
                    Line c = new Line(new Point((int)this.Centre.X, (int)this.Centre.Y),
                        new Point((int)other.Centre.X, (int)other.Centre.Y), Color.Transparent, null);
                    bool bIntersects = ((Maths.LinearAlgebra.absolute(this.Radius - other.Radius) <=
                        Maths.LinearAlgebra.absolute(c.Distance)) && (Maths.LinearAlgebra.absolute(c.Distance) <=
                        Maths.LinearAlgebra.absolute(this.Radius + other.Radius)));

                    // Setup two empty vectors
                    Vector2 i1, i2;
                    i1 = i2 = new Vector2(float.NaN, float.NaN);

                    // Now get intersection points (if any)
                    double xDiff = (other.Centre.X - this.Centre.X);
                    double yDiff = (other.Centre.Y - this.Centre.Y);
                    double d = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                    double dl = (Math.Pow(d, 2) + Math.Pow(this.Radius, 2) - Math.Pow(other.Radius, 2)) / (2 * d);

                    if (bIntersects) {
                        // Left hand side intersect point
                        i1 = new Vector2((float)(this.Centre.X + ((xDiff * dl) / d) + ((yDiff / d) *
                            Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))),
                            (float)(this.Centre.Y + ((yDiff * dl) / d) - ((xDiff / d) *
                            Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))));

                        // Right hand side intersect point
                        i2 = new Vector2((float)(this.Centre.X + ((xDiff * dl) / d) - ((yDiff / d) *
                            Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))),
                            (float)(this.Centre.Y + ((yDiff * dl) / d) + ((xDiff / d) *
                            Math.Sqrt(Math.Pow(this.Radius, 2) - Math.Pow(dl, 2)))));
                    }

                    return Tuple.Create(bIntersects, new Pair(i1, i2));
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

            /// <summary>
            /// Class that represents a triangle.
            /// NOTE: Circles are drawn anti-clockwise, starting from the top (A, B, C). 
            /// </summary>
            public class Triangle : Primitive {

                #region Constructors

                /// <summary>
                /// Default Constructor
                /// </summary>
                public Triangle() { ; }

                /// <summary>
                /// Creates a triangle from 3 specified points.
                /// </summary>
                /// <param name="p0">
                /// Top point.
                /// </param>
                /// <param name="p1">
                /// Left-Bottom point.
                /// </param>
                /// <param name="p2">
                /// Right-Bottom point.
                /// </param>
                /// <param name="color">
                /// Line colour of the triangle.
                /// </param>
                /// <param name="graphicsDevice">
                /// Represents the current graphics device.
                /// </param>
                public Triangle(Point p0, Point p1, Point p2, Color? color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    isEqualateral = false;
                    m_vertices = new VertexPositionColor[4];
                    m_vertices[0] = new VertexPositionColor(new Vector3(p0.X, p0.Y, 0), color.Value);
                    m_vertices[1] = new VertexPositionColor(new Vector3(p1.X, p1.Y, 0), color.Value);
                    m_vertices[2] = new VertexPositionColor(new Vector3(p2.X, p2.Y, 0), color.Value);
                    m_vertices[3] = m_vertices[0];
                    if (graphicsDevice != null) {
                        init(m_vertices.Length);
                    }
                }

                /// <summary>
                /// Creates an Equilateral Triangle.
                /// </summary>
                /// <param name="centre">
                /// TYhe central position of the triangle.
                /// </param>
                /// <param name="length">
                /// The length of each side of the triangle.
                /// </param>
                /// <param name="color">
                /// Line colour of the triangle.
                /// </param>
                /// <param name="graphicsDevice">
                /// Represents the current graphics device.
                /// </param>
                public Triangle(Vector2 centre, int length, Color? color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_length = length;
                    isEqualateral = true;
                    m_vertices = new VertexPositionColor[4];

                    // Get the height
                    double h = (Math.Sqrt(3) / 2) * length;

                    // Now set the vertices
                    m_vertices[0] = new VertexPositionColor(new Vector3(
                        centre.X, (float)(centre.Y - (2 * (h / 3))), 0), color.Value);
                    m_vertices[1] = new VertexPositionColor(new Vector3(
                        (float)(centre.X - (length / 2)), (float)(centre.Y + (h / 3)), 0), color.Value);
                    m_vertices[2] = new VertexPositionColor(new Vector3(
                        (float)(centre.X + (length / 2)), (float)(centre.Y + (h / 3)), 0), color.Value);
                    m_vertices[3] = m_vertices[0];

                    if (m_graphicsDevice != null)
                        init(m_vertices.Length);
                }

                #endregion

                #region Destructor

                ~Triangle() {
                    dispose(false);
                }

                #endregion

                #region Fields

                private bool isEqualateral;
                private int m_length, m_vertexIndex;

                #endregion

                #region Properties

                /// <summary>
                /// Gets and sets the first Vertex in the triangle.
                /// </summary>
                public Vector3 Vertex_A {
                    get { return m_vertices[0].Position; }
                    set {
                        m_vertices[0].Position = value;
                        resetData();
                    }
                }

                /// <summary>
                /// Gets and sets the second Vertex in the triangle.
                /// </summary>
                public Vector3 Vertex_B {
                    get { return m_vertices[1].Position; }
                    set {
                        m_vertices[1].Position = value;
                        resetData();
                    }
                }

                /// <summary>
                /// Gets and sets the third Vertex in the triangle.
                /// </summary>
                public Vector3 Vertex_C {
                    get { return m_vertices[2].Position; }
                    set {
                        m_vertices[2].Position = value;
                        resetData();
                    }
                }

                /// <summary>
                /// Returns the triangle's centroid.
                /// </summary>
                public Vector2 Centroid {
                    get {
                        Line alpha = this.createLineFrom(this.Vertex_B, null);
                        Line beta = this.createLineFrom(this.Vertex_C, null);

                        return alpha.getPointsOnIntersect(beta).Item2;
                    }
                }

                /// <summary>
                /// Returns the area of the triangle.
                /// </summary>
                public double Area {
                    get {
                        if (isEqualateral)
                            return (Math.Sqrt(3) / 4) * Math.Pow(m_length, 2);
                        else {
                            Line l = new Line(new Point((int)(m_vertices[1].Position.X), (int)m_vertices[1].Position.Y),
                                new Point((int)(m_vertices[2].Position.X), (int)(m_vertices[2].Position.Y)), Color.Transparent, null);
                            double b = l.Distance;
                            Vector2 b_mid = new Vector2(l.MidPoint.X, l.MidPoint.Y);
                            double a = b_mid.Y - m_vertices[0].Position.Y;
                            l.Dispose();

                            return (b * a) / 2;
                        }
                    }
                }

                /// <summary>
                /// Returns the perimeter of the triangle.
                /// </summary>
                public double Perimeter {
                    get {
                        if (isEqualateral)
                            return m_length * 3;
                        else {
                            Line a = new Line(new Point((int)m_vertices[0].Position.X, (int)m_vertices[0].Position.Y),
                                new Point((int)m_vertices[1].Position.X, (int)m_vertices[1].Position.Y), Color.Transparent, null);
                            Line b = new Line(new Point((int)m_vertices[1].Position.X, (int)m_vertices[1].Position.Y),
                                new Point((int)m_vertices[2].Position.X, (int)m_vertices[2].Position.Y), Color.Transparent, null);
                            Line c = new Line(new Point((int)m_vertices[2].Position.X, (int)m_vertices[2].Position.Y),
                                new Point((int)m_vertices[0].Position.X, (int)m_vertices[0].Position.Y), Color.Transparent, null);

                            return a.Distance + b.Distance + c.Distance;
                        }
                    }
                }

                #endregion

                #region Functions

                /// <summary>
                /// Resets the vertciers in the Vertex Buffer after any modifications.
                /// </summary>
                private void resetData() {
                    m_vertexBuffer.SetData<VertexPositionColor>(m_vertices);
                }

                /// <summary>
                /// Creates a line from the specified vertex to the opposing side of the triangle.
                /// </summary>
                /// <param name="vertex">
                /// The vertex to start the line from on the triangle.
                /// </param>
                /// <returns>
                /// A new line (real or imaginary, depending whether a color os given or not).
                /// </returns>
                public Line createLineFrom(Vector3 vertex, Color? color) {
                    Line opposite;
                    if (vertex == this.Vertex_A) {
                        opposite = new Line(new Point((int)m_vertices[1].Position.X, (int)m_vertices[1].Position.Y),
                            new Point((int)m_vertices[2].Position.X, (int)m_vertices[2].Position.Y), Color.Transparent, null);
                    }
                    else if (vertex == this.Vertex_B) {
                        opposite = new Line(new Point((int)m_vertices[2].Position.X, (int)m_vertices[2].Position.Y),
                            new Point((int)m_vertices[0].Position.X, (int)m_vertices[0].Position.Y), Color.Transparent, null);
                    }
                    else if (vertex == this.Vertex_C) {
                        opposite = new Line(new Point((int)m_vertices[0].Position.X, (int)m_vertices[0].Position.Y),
                            new Point((int)m_vertices[1].Position.X, (int)m_vertices[1].Position.Y), Color.Transparent, null);
                    }
                    else return null;

                    Vector3 mid = opposite.MidPoint;

                    if (color.HasValue) {
                        return new Line(new Point((int)vertex.X, (int)vertex.Y), new Point((int)mid.X, (int)mid.Y),
                            color, m_graphicsDevice);
                    }
                    else {
                        return new Line(new Point((int)vertex.X, (int)vertex.Y), new Point((int)mid.X, (int)mid.Y),
                            Color.Transparent, null);
                    }
                }

                /// <summary>
                /// Function that gets the median of an imaginary line from a specified corner of the
                /// triangle to it's opposing side. 
                /// </summary>
                /// <param name="vertex">
                /// The vertex to start the imaginary line from.
                /// </param>
                /// <returns>
                /// Returns a vector representing the midpoint of the imaginary line.
                /// </returns>
                public Vector3 getMedian(Vector3 vertex) {
                    return this.createLineFrom(vertex, null).MidPoint;
                }

                /// <summary>
                /// Enables rotation from the centre oif the triangle.
                /// </summary>
                public override void enableRotationFromCentre() {
                    base.enableRotationFromCentre();
                    m_origin = new Vector3(this.Centroid, 0);
                }

                /// <summary>
                /// Enables rotation around one of the triangles vertices.
                /// </summary>
                /// <param name="vertexIndex"></param>
                public override void enableRotationFromVertex(int vertexIndex) {
                    base.enableRotationFromVertex(vertexIndex);
                    m_vertexIndex = vertexIndex;
                }

                /// <summary>
                /// Creates an array of lines from the edges of two triangles.
                /// </summary>
                /// <param name="other">
                /// The other triangle.
                /// </param>
                /// <returns>
                /// An array of lines.
                /// </returns>
                private Line[] createTriLinesArray(Triangle other) {
                    Line[] triLines = new Line[6];
                    triLines[0] = new Line(new Point((int)this.m_vertices[0].Position.X, (int)this.m_vertices[0].Position.Y),
                        new Point((int)this.m_vertices[1].Position.X, (int)this.m_vertices[1].Position.Y),
                        Color.Transparent, null);
                    triLines[1] = new Line(new Point((int)this.m_vertices[1].Position.X, (int)this.m_vertices[1].Position.Y),
                        new Point((int)this.m_vertices[2].Position.X, (int)this.m_vertices[2].Position.Y),
                        Color.Transparent, null);
                    triLines[2] = new Line(new Point((int)this.m_vertices[2].Position.X, (int)this.m_vertices[2].Position.Y),
                        new Point((int)this.m_vertices[0].Position.X, (int)this.m_vertices[0].Position.Y),
                        Color.Transparent, null);
                    triLines[3] = new Line(new Point((int)other.m_vertices[0].Position.X, (int)other.m_vertices[0].Position.Y),
                        new Point((int)other.m_vertices[1].Position.X, (int)other.m_vertices[1].Position.Y),
                        Color.Transparent, null);
                    triLines[4] = new Line(new Point((int)other.m_vertices[1].Position.X, (int)other.m_vertices[1].Position.Y),
                        new Point((int)other.m_vertices[2].Position.X, (int)other.m_vertices[2].Position.Y),
                        Color.Transparent, null);
                    triLines[5] = new Line(new Point((int)other.m_vertices[2].Position.X, (int)other.m_vertices[2].Position.Y),
                        new Point((int)other.m_vertices[0].Position.X, (int)other.m_vertices[0].Position.Y),
                        Color.Transparent, null);

                    return triLines;
                }

                /// <summary>
                /// Function provides a mechanism for checking whether two triangles intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="other">
                /// The other Triangle to check against.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two triangles.
                /// </returns>
                public Tuple<bool, List<Vector2>> getPointsOnIntersect(Triangle other) {
                    bool bIntersects = false;

                    // First make lines from all sides of the triangles
                    Line[] tris = createTriLinesArray(other);

                    // Create a list to add the points of intersection to
                    List<Vector2> isecs = new List<Vector2>();

                    for (int a = 0; a < (tris.Length / 2); ++a) {
                        for (int b = (tris.Length / 2); b < tris.Length; ++b) {
                            if (tris[a].getPointsOnIntersect(tris[b]).Item1) {
                                bIntersects = true;
                                isecs.Add(tris[a].getPointsOnIntersect(tris[b]).Item2);
                            }
                        }
                    }

                    return Tuple.Create(bIntersects, isecs);
                }

                /// <summary>
                /// Function that draws the triangle.--
                /// </summary>
                /// <param name="gameTime">
                /// Represents the current game time.
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

            /// <summary>
            /// Class that represents a Oblong (or Rectangle).
            /// Called Oblong to stop clashes with Microsoft.Xna.Framework.Rectangle,
            /// Oblong will act pretty much the same, except it will support rotations
            /// unike MS version :D
            /// </summary>
            public class Oblong : Primitive {

                #region Constructors

                /// <summary>
                /// Default Constructor.
                /// </summary>
                public Oblong() { ; }

                /// <summary>
                /// Creates an Oblong using a top left corner, width and height.
                /// </summary>
                /// <param name="leftTop">
                /// A point to start the Oblong from.
                /// </param>
                /// <param name="width">
                /// The width of the Oblong.
                /// </param>
                /// <param name="height">
                /// The height of the Oblong.
                /// </param>
                /// <param name="color">
                /// The colour of the Oblong.
                /// </param>
                /// <param name="graphicsDevice">
                /// Represents the current GraphicsDevice.
                /// </param>
                public Oblong(Point leftTop, double width, double height, Color color, GraphicsDevice graphicsDevice) {
                    m_graphicsDevice = graphicsDevice;
                    m_width = width;
                    m_height = height;
                    m_vertices = new VertexPositionColor[5];
                    m_vertices[0] = new VertexPositionColor(new Vector3(leftTop.X, leftTop.Y, 0), color);
                    m_vertices[1] = new VertexPositionColor(new Vector3(leftTop.X, (float)(leftTop.Y + m_height), 0), color);
                    m_vertices[2] = new VertexPositionColor(new Vector3((float)(leftTop.X + m_width),
                        (float)(leftTop.Y + m_height), 0), color);
                    m_vertices[3] = new VertexPositionColor(new Vector3((float)(leftTop.X + m_width), leftTop.Y, 0), color);
                    m_vertices[4] = m_vertices[0];
                    m_origin = Centre;

                    if (m_graphicsDevice != null)
                        init(m_vertices.Length);
                }

                #endregion

                #region Destructor

                ~Oblong() {
                    dispose(false);
                }

                #endregion

                #region Fields

                private double m_width, m_height;
                private int m_vertexIndex;

                #endregion

                #region Properties

                /// <summary>
                /// Gets and sets the Width of the Oblong.
                /// </summary>
                public double Width {
                    get { return m_width; }
                    set { m_width = value; }
                }

                /// <summary>
                /// Gets and sets the Height of the Oblong.
                /// </summary>
                public double Height {
                    get { return m_height; }
                    set { m_height = value; }
                }

                /// <summary>
                /// Gets the Centre of the Oblong.
                /// </summary>
                public Vector3 Centre {
                    get {
                        float x = (float)(m_vertices[3].Position.X - (m_width / 2));
                        float y = (float)(m_vertices[1].Position.Y - (m_height / 2));

                        return new Vector3(x, y, 0);
                    }
                }

                /// <summary>
                /// Gets the top of the Oblong at its creation time.
                /// </summary>
                public double Top {
                    get { return m_vertices[0].Position.Y; }
                }

                /// <summary>
                /// Gets the bottom of the Oblong at its creation time.
                /// </summary>
                public double Bottom {
                    get { return m_vertices[1].Position.Y; }
                }

                /// <summary>
                /// Gets the left-hand-side of the Oblong at its creation time.
                /// </summary>
                public double Left {
                    get { return m_vertices[0].Position.X; }
                }

                /// <summary>
                /// Gets the right-hand-side of the Oblong at its creation time.
                /// </summary>
                public double Right {
                    get { return m_vertices[3].Position.X; }
                }

                /// <summary>
                /// Gets the area of the Oblong.
                /// </summary>
                public double Area {
                    get { return (m_width * m_height); }
                }

                /// <summary>
                /// Gets the length from one corner of the Oblong to its opposite diagonal corner.
                /// </summary>
                public double DiagonalLength {
                    get {
                        Line diagonal = new Line(new Point((int)m_vertices[0].Position.X, (int)m_vertices[0].Position.Y),
                            new Point((int)m_vertices[2].Position.X, (int)m_vertices[2].Position.Y), Color.Transparent, null);

                        return diagonal.Distance;
                    }
                }

                #endregion

                #region Overloads

                /// <summary>
                /// Provides the possibility of explicitly casting an instance
                /// of type Oblong to Rectangle.
                /// </summary>
                /// <param name="oblong">
                /// Parameter represents an instance of Oblong.
                /// </param>
                /// <returns>
                /// An instance of Rectangle.
                /// </returns>
                public static explicit operator Rectangle(Oblong oblong) {
                    return new Rectangle(
                        (int)oblong.Left, (int)oblong.Top, (int)oblong.Width, (int)oblong.Height);
                }

                #endregion

                #region Functions

                /// <summary>
                /// Enables rotation form the Centre of the Oblong.
                /// </summary>
                public override void enableRotationFromCentre() {
                    base.enableRotationFromCentre();
                    m_origin = this.Centre;
                }

                /// <summary>
                /// Enables rotation around one of the Oblong's vertices.
                /// </summary>
                /// <param name="vertexIndex">
                /// The index of the vertex to rotate around.
                /// </param>
                public override void enableRotationFromVertex(int vertexIndex) {
                    base.enableRotationFromVertex(vertexIndex);
                    m_vertexIndex = vertexIndex;
                }

                /// <summary>
                /// Creates an array of lines from two Oblongs, to aid intersection checks.
                /// </summary>
                /// <param name="other">
                /// The other Oblong.
                /// </param>
                /// <returns>
                /// Returns an array of lines.
                /// </returns>
                private Line[] createLineArrayFromOblongs(Oblong other) {
                    Line[] oLines = new Line[8];

                    // This Oblong
                    oLines[0] = new Line(new Point((int)this.m_vertices[0].Position.X, (int)this.m_vertices[0].Position.Y),
                        new Point((int)this.m_vertices[1].Position.X, (int)this.m_vertices[1].Position.Y),
                        Color.Transparent, null);
                    oLines[1] = new Line(new Point((int)this.m_vertices[1].Position.X, (int)this.m_vertices[1].Position.Y),
                        new Point((int)this.m_vertices[2].Position.X, (int)this.m_vertices[2].Position.Y),
                        Color.Transparent, null);
                    oLines[2] = new Line(new Point((int)this.m_vertices[2].Position.X, (int)this.m_vertices[2].Position.Y),
                        new Point((int)this.m_vertices[3].Position.X, (int)this.m_vertices[3].Position.Y),
                        Color.Transparent, null);
                    oLines[3] = new Line(new Point((int)this.m_vertices[3].Position.X, (int)this.m_vertices[3].Position.Y),
                        new Point((int)this.m_vertices[0].Position.X, (int)this.m_vertices[0].Position.Y),
                        Color.Transparent, null);

                    // Other Oblong
                    oLines[4] = new Line(new Point((int)other.m_vertices[0].Position.X, (int)other.m_vertices[0].Position.Y),
                        new Point((int)other.m_vertices[1].Position.X, (int)other.m_vertices[1].Position.Y),
                        Color.Transparent, null);
                    oLines[5] = new Line(new Point((int)other.m_vertices[1].Position.X, (int)other.m_vertices[1].Position.Y),
                        new Point((int)other.m_vertices[2].Position.X, (int)other.m_vertices[2].Position.Y),
                        Color.Transparent, null);
                    oLines[6] = new Line(new Point((int)other.m_vertices[2].Position.X, (int)other.m_vertices[2].Position.Y),
                        new Point((int)other.m_vertices[3].Position.X, (int)other.m_vertices[3].Position.Y),
                        Color.Transparent, null);
                    oLines[7] = new Line(new Point((int)other.m_vertices[3].Position.X, (int)other.m_vertices[3].Position.Y),
                        new Point((int)other.m_vertices[0].Position.X, (int)other.m_vertices[0].Position.Y),
                        Color.Transparent, null);

                    return oLines;
                }

                /// <summary>
                /// Function provides a mechanism for checking whether two oblongs intersect,
                /// and also retrieves the points of intersection.
                /// </summary>
                /// <param name="other">
                /// The other Oblong to check against.
                /// </param>
                /// <returns>
                /// Returns a Tuple containing a bool (true on an intersection), and a list of
                /// vectors containing the points of intersection between the two oblongs.
                /// </returns>
                public Tuple<bool, List<Vector2>> getPointsOnIntersect(Oblong other) {
                    bool bIntersects = false;
                    List<Vector2> isecs = new List<Vector2>();
                    Line[] oLines = createLineArrayFromOblongs(other);

                    for (int a = 0; a < (oLines.Length / 2); ++a) {
                        for (int b = (oLines.Length / 2); b < oLines.Length; ++b) {
                            if (oLines[a].getPointsOnIntersect(oLines[b]).Item1) {
                                bIntersects = true;
                                isecs.Add(oLines[a].getPointsOnIntersect(oLines[b]).Item2);
                            }
                        }
                    }

                    return Tuple.Create(bIntersects, isecs);
                }

                /// <summary>
                /// Draws the Oblong.
                /// </summary>
                /// <param name="gameTime">
                /// Represents the current game time.
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

            #region Fields

            public const double EPSILON = 0.000001;

            #endregion

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

        /// <summary>
        /// Provides functions that aid Vector analyzation.
        /// </summary>
        public class VectorHelper {

            #region Functions

            /// <summary>
            /// Gets the minimum X-coordinate from a list of Vector2.
            /// </summary>
            /// <param name="vecs">
            /// Vector2 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum X-coordinate.
            /// </returns>
            public static float getMinX(List<Vector2> vecs) {
                List<float> x_coords = new List<float>();
                foreach (Vector2 v in vecs)
                    x_coords.Add(v.X);

                return x_coords.Min();
            }

            /// <summary>
            /// Gets the minimum X-coordinate from a list of Vector3.
            /// </summary>
            /// <param name="vecs">
            /// Vector3 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum X-coordinate.
            /// </returns>
            public static float getMinX(List<Vector3> vecs) {
                List<float> x_coords = new List<float>();
                foreach (Vector3 v in vecs)
                    x_coords.Add(v.X);

                return x_coords.Min();
            }

            /// <summary>
            /// Gets the minimum X-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum X-coordinate.
            /// </returns>
            public static float getMinX(List<Vector4> vecs) {
                List<float> x_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    x_coords.Add(v.X);

                return x_coords.Min();
            }

            /// <summary>
            /// Gets the maximum X-coordinate from a list of Vector2.
            /// </summary>
            /// <param name="vecs">
            /// Vector2 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum X-coordinate.
            /// </returns>
            public static float getMaxX(List<Vector2> vecs) {
                List<float> x_coords = new List<float>();
                foreach (Vector2 v in vecs)
                    x_coords.Add(v.X);

                return x_coords.Max();
            }

            /// <summary>
            /// Gets the maximum X-coordinate from a list of Vector3.
            /// </summary>
            /// <param name="vecs">
            /// Vector3 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum X-coordinate.
            /// </returns>
            public static float getMaxX(List<Vector3> vecs) {
                List<float> x_coords = new List<float>();
                foreach (Vector3 v in vecs)
                    x_coords.Add(v.X);

                return x_coords.Max();
            }

            /// <summary>
            /// Gets the maximum X-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum X-coordinate.
            /// </returns>
            public static float getMaxX(List<Vector4> vecs) {
                List<float> x_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    x_coords.Add(v.X);

                return x_coords.Max();
            }

            /// <summary>
            /// Gets the minimum Y-coordinate from a list of Vector2.
            /// </summary>
            /// <param name="vecs">
            /// Vector2 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum Y-coordinate.
            /// </returns>
            public static float getMinY(List<Vector2> vecs) {
                List<float> y_coords = new List<float>();
                foreach (Vector2 v in vecs)
                    y_coords.Add(v.Y);

                return y_coords.Min();
            }

            /// <summary>
            /// Gets the minimum Y-coordinate from a list of Vector3.
            /// </summary>
            /// <param name="vecs">
            /// Vector3 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum Y-coordinate.
            /// </returns>
            public static float getMinY(List<Vector3> vecs) {
                List<float> y_coords = new List<float>();
                foreach (Vector3 v in vecs)
                    y_coords.Add(v.Y);

                return y_coords.Min();
            }

            /// <summary>
            /// Gets the minimum Y-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum Y-coordinate.
            /// </returns>
            public static float getMinY(List<Vector4> vecs) {
                List<float> y_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    y_coords.Add(v.Y);

                return y_coords.Min();
            }

            /// <summary>
            /// Gets the maximum Y-coordinate from a list of Vector2.
            /// </summary>
            /// <param name="vecs">
            /// Vector2 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum Y-coordinate.
            /// </returns>
            public static float getMaxY(List<Vector2> vecs) {
                List<float> y_coords = new List<float>();
                foreach (Vector2 v in vecs)
                    y_coords.Add(v.Y);

                return y_coords.Max();
            }

            /// <summary>
            /// Gets the maximum Y-coordinate from a list of Vector3.
            /// </summary>
            /// <param name="vecs">
            /// Vector3 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum Y-coordinate.
            /// </returns>
            public static float getMaxY(List<Vector3> vecs) {
                List<float> y_coords = new List<float>();
                foreach (Vector3 v in vecs)
                    y_coords.Add(v.Y);

                return y_coords.Max();
            }

            /// <summary>
            /// Gets the maximum Y-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum Y-coordinate.
            /// </returns>
            public static float getMaxY(List<Vector4> vecs) {
                List<float> y_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    y_coords.Add(v.Y);

                return y_coords.Max();
            }

            /// <summary>
            /// Gets the minimum Z-coordinate from a list of Vector3.
            /// </summary>
            /// <param name="vecs">
            /// Vector3 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum Z-coordinate.
            /// </returns>
            public static float getMinZ(List<Vector3> vecs) {
                List<float> z_coords = new List<float>();
                foreach (Vector3 v in vecs)
                    z_coords.Add(v.Z);

                return z_coords.Min();
            }

            /// <summary>
            /// Gets the minimum Z-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum Z-coordinate.
            /// </returns>
            public static float getMinZ(List<Vector4> vecs) {
                List<float> z_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    z_coords.Add(v.Z);

                return z_coords.Min();
            }

            /// <summary>
            /// Gets the maximum Z-coordinate from a list of Vector3.
            /// </summary>
            /// <param name="vecs">
            /// Vector3 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum Z-coordinate.
            /// </returns>
            public static float getMaxZ(List<Vector3> vecs) {
                List<float> z_coords = new List<float>();
                foreach (Vector3 v in vecs)
                    z_coords.Add(v.Z);

                return z_coords.Max();
            }

            /// <summary>
            /// Gets the maximum Z-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum Z-coordinate.
            /// </returns>
            public static float getMaxZ(List<Vector4> vecs) {
                List<float> z_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    z_coords.Add(v.Z);

                return z_coords.Max();
            }

            /// <summary>
            /// Gets the minimum W-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the minimum W-coordinate.
            /// </returns>
            public static float getMinW(List<Vector4> vecs) {
                List<float> w_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    w_coords.Add(v.W);

                return w_coords.Min();
            }

            /// <summary>
            /// Gets the maximum W-coordinate from a list of Vector4.
            /// </summary>
            /// <param name="vecs">
            /// Vector4 List.
            /// </param>
            /// <returns>
            /// A float representing the maximum W-coordinate.
            /// </returns>
            public static float getMaxW(List<Vector4> vecs) {
                List<float> w_coords = new List<float>();
                foreach (Vector4 v in vecs)
                    w_coords.Add(v.W);

                return w_coords.Max();
            }

            #endregion
        }

        /// <summary>
        /// Provides miscellaneous Mathematical functions.
        /// </summary>
        public class Misc {

            #region Functions

            /// <summary>
            /// Function that gets the percent of a given value.
            /// </summary>
            /// <param name="percent">
            /// The percent desired.
            /// </param>
            /// <param name="val">
            /// The value to gain a percentage from.
            /// </param>
            /// <returns>
            /// A double representing the percentage.
            /// </returns>
            public static double percentOf(double percent, double val) {
                double hundredth = (val / 100);
                return (percent * hundredth);
            }

            #endregion
        }

        #endregion
    }
}

#endregion