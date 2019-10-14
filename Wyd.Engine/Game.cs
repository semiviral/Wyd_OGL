#region

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Silk.NET.Input;
using Silk.NET.Input.Common;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Common;
using Wyd.Engine.Graphics;
using Wyd.Engine.Logging;
using Wyd.Engine.Math;

#endregion

namespace Wyd.Engine
{
    public class Game
    {
        #region TEMP

        private static readonly double[] _Vertices =
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

        private Type _Caller;
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
            _Caller = new StackFrame(1).GetType();
            
            _Window = Window.Create(WindowOptions.Default);
            _Window.Title = windowTitle;
            _Window.VSync = vSync ? VSyncMode.On : VSyncMode.Off;
            _Window.Size = new Size(windowSize.X, windowSize.Y);
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


        #region EVENTS

        private unsafe void OnLoad()
        {
            _GL = GL.GetApi();

            _GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            _VertexBufferObject = _GL.GenBuffer();
            _GL.BindBuffer(GLEnum.ArrayBuffer, _VertexBufferObject);

            fixed (double* vertices = _Vertices)
            {
                _GL.BufferData(GLEnum.ArrayBuffer, (uint)_Vertices.Length * sizeof(double), vertices,
                    GLEnum.StaticDraw);
            }

            _Shader = new Shader("Wyd.shader.vert", "Wyd.shader.frag", _GL, _Caller);
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
