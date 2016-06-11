/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using SlimDX;
using SlimDX.D3DCompiler;
using DXGI = SlimDX.DXGI;
using D3D = SlimDX.Direct3D11;


namespace BasicComputeShader
{
    class Program
    {
        static void Main(string[] args)
        {
            const int elementCount = 16;
            const int bufferSizeInBytes = elementCount * sizeof(float);

            D3D.Device device = new D3D.Device(D3D.DriverType.Hardware, D3D.DeviceCreationFlags.Debug);

            // The input to the computation will be a constant buffer containing
            // integers (in floating point representation) from 1 to numberOfElements,
            // inclusive. The compute shader itself will double these values and write
            // them to the output buffer.
            D3D.BufferDescription inputBufferDescription = new D3D.BufferDescription
            {
                BindFlags = D3D.BindFlags.ConstantBuffer,
                CpuAccessFlags = D3D.CpuAccessFlags.Write,
                OptionFlags = D3D.ResourceOptionFlags.None,
                SizeInBytes = bufferSizeInBytes,
                StructureByteStride = sizeof(float),
                Usage = D3D.ResourceUsage.Dynamic,
            };
            D3D.Buffer inputBuffer = new D3D.Buffer(device, inputBufferDescription);
            DataBox input = device.ImmediateContext.MapSubresource(inputBuffer, D3D.MapMode.WriteDiscard, D3D.MapFlags.None);
            for (int value = 1; value <= elementCount; ++value)
                input.Data.Write((float)value);
            device.ImmediateContext.UnmapSubresource(inputBuffer, 0);

            // A staging buffer is used to copy data between the CPU and GPU; the output
            // buffer (which gets the computation results) cannot be mapped directly.
            D3D.BufferDescription stagingBufferDescription = new D3D.BufferDescription
            {
                BindFlags = D3D.BindFlags.None,
                CpuAccessFlags = D3D.CpuAccessFlags.Read,
                OptionFlags = D3D.ResourceOptionFlags.StructuredBuffer,
                SizeInBytes = bufferSizeInBytes,
                StructureByteStride = sizeof(float),
                Usage = D3D.ResourceUsage.Staging,
            };
            D3D.Buffer stagingBuffer = new D3D.Buffer(device, stagingBufferDescription);

            // The output buffer itself, and the view required to bind it to the pipeline.
            D3D.BufferDescription outputBufferDescription = new D3D.BufferDescription
            {
                BindFlags = D3D.BindFlags.UnorderedAccess | D3D.BindFlags.ShaderResource,
                OptionFlags = D3D.ResourceOptionFlags.StructuredBuffer,
                SizeInBytes = bufferSizeInBytes,
                StructureByteStride = sizeof(float),
                Usage = D3D.ResourceUsage.Default,
            };
            D3D.Buffer outputBuffer = new D3D.Buffer(device, outputBufferDescription);
            D3D.UnorderedAccessViewDescription outputViewDescription = new D3D.UnorderedAccessViewDescription
            {
                ElementCount = elementCount,
                Format = DXGI.Format.Unknown,
                Dimension = D3D.UnorderedAccessViewDimension.Buffer
            };
            D3D.UnorderedAccessView outputView = new D3D.UnorderedAccessView(device, outputBuffer, outputViewDescription);

            // Compile the shader.
            ShaderBytecode computeShaderCode = ShaderBytecode.CompileFromFile("BasicComputeShader.hlsl", "main", "cs_4_0", ShaderFlags.None, EffectFlags.None);
            D3D.ComputeShader computeShader = new D3D.ComputeShader(device, computeShaderCode);

            device.ImmediateContext.ComputeShader.Set(computeShader);
            device.ImmediateContext.ComputeShader.SetUnorderedAccessView(outputView, 0);
            device.ImmediateContext.ComputeShader.SetConstantBuffer(inputBuffer, 0);

            // Compute shaders execute on multiple threads at the same time. Those execution
            // threads are grouped; Dispatch() indicates how many groups in the X, Y and Z
            // dimension will be utilized. The shader itself specified how many threads per
            // group (also in X, Y and Z dimensions) to use via the [numthreads] attribute.
            // In this sample, one thread group will be used with 16 threads, each thread
            // will process one element of the input data.
            device.ImmediateContext.Dispatch(1, 1, 1);

            device.ImmediateContext.CopyResource(outputBuffer, stagingBuffer);
            DataBox output = device.ImmediateContext.MapSubresource(stagingBuffer, D3D.MapMode.Read, D3D.MapFlags.None);

            Console.Write("Results:");
            for (int index = 0; index < elementCount; ++index)
                Console.Write(" {0}", output.Data.Read<float>());
            device.ImmediateContext.UnmapSubresource(outputBuffer, 0);
            Console.WriteLine();

            computeShader.Dispose();
            outputView.Dispose();
            outputBuffer.Dispose();
            stagingBuffer.Dispose();
            inputBuffer.Dispose();
            device.Dispose();
        }
    }
}
