#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Silk.NET.OpenGL;

#endregion

namespace Wyd.Engine.Graphics
{
    namespace SampleBase
    {
        public class Shader
        {
            public readonly uint Handle;

            private readonly Dictionary<string, int> _UniformLocations;
            private readonly GL _GL;

            public Shader(string vertPath, string fragPath, GL gl, Type type)
            {
                _GL = gl;
                string shaderSource = LoadEmbeddedResource(vertPath, type);

                uint vertexShader = _GL.CreateShader(GLEnum.VertexShader);

                _GL.ShaderSource(vertexShader, shaderSource);

                CompileShader(vertexShader);

                shaderSource = LoadEmbeddedResource(fragPath, type);
                uint fragmentShader = _GL.CreateShader(GLEnum.FragmentShader);
                _GL.ShaderSource(fragmentShader, shaderSource);
                CompileShader(fragmentShader);

                Handle = _GL.CreateProgram();

                _GL.AttachShader(Handle, vertexShader);
                _GL.AttachShader(Handle, fragmentShader);

                LinkProgram(Handle);

                _GL.DetachShader(Handle, vertexShader);
                _GL.DetachShader(Handle, fragmentShader);
                _GL.DeleteShader(fragmentShader);
                _GL.DeleteShader(vertexShader);

                _GL.GetProgram(Handle, GLEnum.ActiveUniforms, out int numberOfUniforms);

                _UniformLocations = new Dictionary<string, int>();

                for (uint i = 0u; i < numberOfUniforms; i++)
                {
                    void key = _GL.GetActiveUniform(Handle, i, out _, out _);

                    int location = _GL.GetUniformLocation(Handle, key);

                    _UniformLocations.Add(key, location);
                }
            }

            private string LoadEmbeddedResource(string path, Type type)
            {
                using (Stream s = type.Assembly.GetManifestResourceStream(path))
                {
                    using (StreamReader sr = new StreamReader(s))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }

            private void CompileShader(uint shader)
            {
                _GL.CompileShader(shader);

                _GL.GetShader(shader, GLEnum.CompileStatus, out int code);
                if (code != (int) GLEnum.True)
                {
                    throw new Exception($"Error occurred whilst compiling Shader({shader})");
                }
            }

            private void LinkProgram(uint program)
            {
                _GL.LinkProgram(program);

                _GL.GetProgram(program, GLEnum.LinkStatus, out int code);
                if (code != (int) GLEnum.True)
                {
                    throw new Exception($"Error occurred whilst linking Program({program})");
                }
            }

            public void Use()
            {
                _GL.UseProgram(Handle);
            }

            public int GetAttribLocation(string attribName) => _GL.GetAttribLocation(Handle, attribName);

            public void SetInt(string name, int data)
            {
                _GL.UseProgram(Handle);
                _GL.Uniform1(_UniformLocations[name], data);
            }

            public void SetFloat(string name, float data)
            {
                _GL.UseProgram(Handle);
                _GL.Uniform1(_UniformLocations[name], data);
            }

            public unsafe void SetMatrix4(string name, Matrix4x4 data)
            {
                _GL.UseProgram(Handle);
                _GL.UniformMatrix4(_UniformLocations[name], Handle, true, (float*) &data);
            }

            public void SetVector3(string name, Vector3 data)
            {
                _GL.UseProgram(Handle);
                _GL.Uniform3(_UniformLocations[name], data);
            }
        }
    }
}
