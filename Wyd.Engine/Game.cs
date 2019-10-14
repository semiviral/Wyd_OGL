#region

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Silk.NET.Input;
using Silk.NET.Input.Common;
using Silk.NET.OpenGL;
using Silk.NET.Windowing.Common;
using Silk.NET.Windowing.Desktop;
using Wyd.Engine.Graphics;
using Wyd.Engine.Logging;
using Wyd.Engine.Math;

#endregion

namespace Wyd.Engine
{
    public class Game
    {
        #region TEMP

        private static readonly double[] _vertices =
        {
            -0.5d,
            -0.5d,
            0.0d,
            0.5d,
            -0.5d,
            0.0d,
            0.0d,
            0.5d,
            0.0d
        };

        #endregion

        public delegate void FrameUpdate(TimeSpan deltaTime);

        private readonly IWindow _Window;
        private readonly IntPtr _WindowHandle;

        private GL _GL;
        private DebugProc _OnDebug;
        private uint _VertexBufferObject;
        private uint _VertexArrayObject;
        private Shader _Shader;

        private IInputContext _Input;

        public bool Running { get; private set; }
        public TimeSpan DeltaTime { get; private set; }

        public event FrameUpdate EarlyUpdate;
        public event FrameUpdate Update;
        public event FrameUpdate LateUpdate;
        public event FrameUpdate Render;

        public Game(string windowTitle = "", bool vSync = false, int2 windowSize = default)
        {
            _Window = new GlfwWindow(
                new WindowOptions(true, true, new Point(400, 400), new Size(windowSize.X, windowSize.Y), 0,
                    0, GraphicsAPI.Default, windowTitle, WindowState.Normal, WindowBorder.Resizable,
                    vSync ? VSyncMode.On : VSyncMode.Off, 5, true));
            _Window.Load += OnLoad;
            _Window.Render += OnRender;
            _Window.Resize += OnResize;
            _Window.Update += OnUpdate;

            _WindowHandle = _Window.Handle;

            Running = false;
        }

        private void End()
        {
            _GL.BindBuffer(GLEnum.ArrayBuffer, 0);
            _GL.BindVertexArray(0);
            _GL.UseProgram(0);
            _GL.DeleteBuffer(_VertexBufferObject);
            _GL.DeleteVertexArray(_VertexArrayObject);

            _Shader.Delete();
        }

        public void Run()
        {
            Running = true;

            _Window.Run();
            End();

            Running = false;
        }

        private void CheckShaderErrors(uint shader)
        {
            _GL.GetShader(shader, GLEnum.CompileStatus, out int status);

            if (status != 1)
            {
                _GL.DebugMessageInsert(GLEnum.DebugSourceApplication, GLEnum.DebugTypeError, 900110,
                    GLEnum.DebugSeverityHigh, 19u + (uint)shader.ToString().Length, $"{shader} failed to compile.");

                string str = new string(' ', 1024);
                _GL.GetShaderInfoLog(shader, 2014u, out uint length, str);
                str = str.Substring(0, (int)length);

                _GL.DebugMessageInsert(GLEnum.DebugSourceApplication, GLEnum.DebugTypeError, 900111,
                    GLEnum.DebugSeverityHigh, length, str);
            }
        }

        private void CheckProgramErrors(uint program)
        {
            _GL.GetProgram(program, GLEnum.LinkStatus, out int status);

            if (status != 1)
            {
                _GL.GetProgram(program, GLEnum.InfoLogLength, out int length2);
                _GL.DebugMessageInsert(GLEnum.DebugSourceApplication, GLEnum.DebugTypeError, 900112,
                    GLEnum.DebugSeverityHigh, 19u + (uint)program.ToString().Length,
                    $"{program} has failed to compile. " + length2);

                string str = new string(' ', 1024);
                _GL.GetProgramInfoLog(program, 1024, out uint length, str);
                str = str.Substring(0, (int)length);
                _GL.DebugMessageInsert(GLEnum.DebugSourceApplication, GLEnum.DebugTypeError, 900113,
                    GLEnum.DebugSeverityHigh, length, str);
            }
        }


        #region EVENTS

        private unsafe void OnLoad()
        {
            _GL = GL.GetApi();

            _GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            _VertexBufferObject = _GL.GenBuffer();
            _GL.BindBuffer(GLEnum.ArrayBuffer, _VertexBufferObject);

            fixed (double* vertices = _vertices)
            {
                _GL.BufferData(GLEnum.ArrayBuffer, (uint)_vertices.Length * sizeof(double), vertices,
                    GLEnum.StaticDraw);
            }

            _Shader = new Shader("Triangle.shader.vert", "Triangle.shader.frag", _GL, typeof(Game));
            _Shader.Use();

            _VertexArrayObject = _GL.GenVertexArray();
            _GL.VertexAttribPointer(0, 3, GLEnum.Double, false, 3 * sizeof(double), 0);
            _GL.EnableVertexAttribArray(0);
            _GL.BindBuffer(GLEnum.ArrayBuffer, _VertexBufferObject);

            Logger.Log(LogLevel.Information, "Made triangle?");
        }

        private void OnResize(Size size)
        {
            _GL.Viewport(0, 0, (uint)size.Width, (uint)size.Height);
            Logger.Log(LogLevel.Information, $"Resized GL viewport to {size}.");
        }

        private void OnUpdate(double delta)
        {
            // update input
            // todo extract to static interface
            _Input ??= _Window.GetInput();

            // update delta time
            DeltaTime = TimeSpan.FromSeconds(delta);

            EarlyUpdate?.Invoke(DeltaTime);
            Update?.Invoke(DeltaTime);
            LateUpdate?.Invoke(DeltaTime);
        }

        private void OnRender(double delta)
        {
            _GL.Clear((uint)GLEnum.ColorBufferBit);

            _Shader.Use();

            _GL.BindVertexArray(_VertexArrayObject);
            _GL.DrawArrays(GLEnum.Triangles, 0, 3);

            Render?.Invoke(DeltaTime);
        }

        private void OnDebug(
            GLEnum source, GLEnum type, int id, GLEnum severity,
            int length, IntPtr message, IntPtr userparam)
        {
            Logger.Log(LogLevel.Error,
                $"[{DateTime.Now} ({severity.ToString().Substring(13)} | {type.ToString().Substring(9)} / {id}] {Marshal.PtrToStringAnsi(message)}");
        }

        #endregion
    }
}
