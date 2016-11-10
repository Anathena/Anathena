﻿namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface
{
    using System;

    /// <summary>
    /// A collection of virtual table flags for supported DirectX versions
    /// </summary>
    internal static class DirectXFlags
    {
        public static readonly Int32 D3D9DeviceMethodCount = Enum.GetNames(typeof(Direct3DDevice9FunctionOrdinalsEnum)).Length;

        public static readonly Int32 D3D9ExDeviceMethodCount = Enum.GetNames(typeof(Direct3DDevice9ExFunctionOrdinalsEnum)).Length;

        public static readonly Int32 D3D10DeviceMethodCount = Enum.GetNames(typeof(D3D10DeviceVirtualTableEnum)).Length;

        public static readonly Int32 D3D101DeviceMethodCount = Enum.GetNames(typeof(D3D101DeviceVirtualTableEnum)).Length;

        public static readonly Int32 D3D11DeviceMethodCount = Enum.GetNames(typeof(D3D11DeviceVirtualTableEnum)).Length;

        public static readonly Int32 DXGISwapChainMethodCount = Enum.GetNames(typeof(DXGISwapChainVirtualTableEnum)).Length;

        /// <summary>
        /// Versions of DirectX the graphic hook supports
        /// </summary>
        public enum Direct3DVersionEnum
        {
            Unknown,

            Direct3D9,

            Direct3D10,

            Direct3D10_1,

            Direct3D11,

            Direct3D11_1,
        }

        /// <summary>
        /// The full list of DXGI functions, sorted by virtual table index
        /// </summary>
        internal enum DXGISwapChainVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,

            AddRef = 1,

            Release = 2,

            //// IDXGIObject
            SetPrivateData = 3,

            SetPrivateDataInterface = 4,

            GetPrivateData = 5,

            GetParent = 6,

            //// IDXGIDeviceSubObject
            GetDevice = 7,

            //// IDXGISwapChain
            Present = 8,

            GetBuffer = 9,

            SetFullscreenState = 10,

            GetFullscreenState = 11,

            GetDesc = 12,

            ResizeBuffers = 13,

            ResizeTarget = 14,

            GetContainingOutput = 15,

            GetFrameStatistics = 16,

            GetLastPresentCount = 17,
        }
        //// End DXGISwapChainVirtualTableEnum

        /// <summary>
        /// The full list of IDirect3DDevice9 functions with the correct index
        /// </summary>
        internal enum Direct3DDevice9FunctionOrdinalsEnum : Int16
        {
            QueryInterface = 0,

            AddRef = 1,

            Release = 2,

            TestCooperativeLevel = 3,

            GetAvailableTextureMem = 4,

            EvictManagedResources = 5,

            GetDirect3D = 6,

            GetDeviceCaps = 7,

            GetDisplayMode = 8,

            GetCreationParameters = 9,

            SetCursorProperties = 10,

            SetCursorPosition = 11,

            ShowCursor = 12,

            CreateAdditionalSwapChain = 13,

            GetSwapChain = 14,

            GetNumberOfSwapChains = 15,

            Reset = 16,

            Present = 17,

            GetBackBuffer = 18,

            GetRasterStatus = 19,

            SetDialogBoxMode = 20,

            SetGammaRamp = 21,

            GetGammaRamp = 22,

            CreateTexture = 23,

            CreateVolumeTexture = 24,

            CreateCubeTexture = 25,

            CreateVertexBuffer = 26,

            CreateIndexBuffer = 27,

            CreateRenderTarget = 28,

            CreateDepthStencilSurface = 29,

            UpdateSurface = 30,

            UpdateTexture = 31,

            GetRenderTargetData = 32,

            GetFrontBufferData = 33,

            StretchRect = 34,

            ColorFill = 35,

            CreateOffscreenPlainSurface = 36,

            SetRenderTarget = 37,

            GetRenderTarget = 38,

            SetDepthStencilSurface = 39,

            GetDepthStencilSurface = 40,

            BeginScene = 41,

            EndScene = 42,

            Clear = 43,

            SetTransform = 44,

            GetTransform = 45,

            MultiplyTransform = 46,

            SetViewport = 47,

            GetViewport = 48,

            SetMaterial = 49,

            GetMaterial = 50,

            SetLight = 51,

            GetLight = 52,

            LightEnable = 53,

            GetLightEnable = 54,

            SetClipPlane = 55,

            GetClipPlane = 56,

            SetRenderState = 57,

            GetRenderState = 58,

            CreateStateBlock = 59,

            BeginStateBlock = 60,

            EndStateBlock = 61,

            SetClipStatus = 62,

            GetClipStatus = 63,

            GetTexture = 64,

            SetTexture = 65,

            GetTextureStageState = 66,

            SetTextureStageState = 67,

            GetSamplerState = 68,

            SetSamplerState = 69,

            ValidateDevice = 70,

            SetPaletteEntries = 71,

            GetPaletteEntries = 72,

            SetCurrentTexturePalette = 73,

            GetCurrentTexturePalette = 74,

            SetScissorRect = 75,

            GetScissorRect = 76,

            SetSoftwareVertexProcessing = 77,

            GetSoftwareVertexProcessing = 78,

            SetNPatchMode = 79,

            GetNPatchMode = 80,

            DrawPrimitive = 81,

            DrawIndexedPrimitive = 82,

            DrawPrimitiveUP = 83,

            DrawIndexedPrimitiveUP = 84,

            ProcessVertices = 85,

            CreateVertexDeclaration = 86,

            SetVertexDeclaration = 87,

            GetVertexDeclaration = 88,

            SetFVF = 89,

            GetFVF = 90,

            CreateVertexShader = 91,

            SetVertexShader = 92,

            GetVertexShader = 93,

            SetVertexShaderConstantF = 94,

            GetVertexShaderConstantF = 95,

            SetVertexShaderConstantI = 96,

            GetVertexShaderConstantI = 97,

            SetVertexShaderConstantB = 98,

            GetVertexShaderConstantB = 99,

            SetStreamSource = 100,

            GetStreamSource = 101,

            SetStreamSourceFreq = 102,

            GetStreamSourceFreq = 103,

            SetIndices = 104,

            GetIndices = 105,

            CreatePixelShader = 106,

            SetPixelShader = 107,

            GetPixelShader = 108,

            SetPixelShaderConstantF = 109,

            GetPixelShaderConstantF = 110,

            SetPixelShaderConstantI = 111,

            GetPixelShaderConstantI = 112,

            SetPixelShaderConstantB = 113,

            GetPixelShaderConstantB = 114,

            DrawRectPatch = 115,

            DrawTriPatch = 116,

            DeletePatch = 117,

            CreateQuery = 118,
        }
        //// End Direct3DDevice9FunctionOrdinalsEnum

        internal enum Direct3DDevice9ExFunctionOrdinalsEnum : Int16
        {
            SetConvolutionMonoKernel = 119,

            ComposeRects = 120,

            PresentEx = 121,

            GetGPUThreadPriority = 122,

            SetGPUThreadPriority = 123,

            WaitForVBlank = 124,

            CheckResourceResidency = 125,

            SetMaximumFrameLatency = 126,

            GetMaximumFrameLatency = 127,

            CheckDeviceState_ = 128,

            CreateRenderTargetEx = 129,

            CreateOffscreenPlainSurfaceEx = 130,

            CreateDepthStencilSurfaceEx = 131,

            ResetEx = 132,

            GetDisplayModeEx = 133,
        }
        //// End Direct3DDevice9ExFunctionOrdinalsEnum

        internal enum D3D10DeviceVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,
            AddRef = 1,
            Release = 2,

            //// ID3D10Device
            VSSetConstantBuffers = 3,
            PSSetShaderResources = 4,
            PSSetShader = 5,
            PSSetSamplers = 6,
            VSSetShader = 7,
            DrawIndexed = 8,
            Draw = 9,
            PSSetConstantBuffers = 10,
            IASetInputLayout = 11,
            IASetVertexBuffers = 12,
            IASetIndexBuffer = 13,
            DrawIndexedInstanced = 14,
            DrawInstanced = 15,
            GSSetConstantBuffers = 16,
            GSSetShader = 17,
            IASetPrimitiveTopology = 18,
            VSSetShaderResources = 19,
            VSSetSamplers = 20,
            SetPredication = 21,
            GSSetShaderResources = 22,
            GSSetSamplers = 23,
            OMSetRenderTargets = 24,
            OMSetBlendState = 25,
            OMSetDepthStencilState = 26,
            SOSetTargets = 27,
            DrawAuto = 28,
            RSSetState = 29,
            RSSetViewports = 30,
            RSSetScissorRects = 31,
            CopySubresourceRegion = 32,
            CopyResource = 33,
            UpdateSubresource = 34,
            ClearRenderTargetView = 35,
            ClearDepthStencilView = 36,
            GenerateMips = 37,
            ResolveSubresource = 38,
            VSGetConstantBuffers = 39,
            PSGetShaderResources = 40,
            PSGetShader = 41,
            PSGetSamplers = 42,
            VSGetShader = 43,
            PSGetConstantBuffers = 44,
            IAGetInputLayout = 45,
            IAGetVertexBuffers = 46,
            IAGetIndexBuffer = 47,
            GSGetConstantBuffers = 48,
            GSGetShader = 49,
            IAGetPrimitiveTopology = 50,
            VSGetShaderResources = 51,
            VSGetSamplers = 52,
            GetPredication = 53,
            GSGetShaderResources = 54,
            GSGetSamplers = 55,
            OMGetRenderTargets = 56,
            OMGetBlendState = 57,
            OMGetDepthStencilState = 58,
            SOGetTargets = 59,
            RSGetState = 60,
            RSGetViewports = 61,
            RSGetScissorRects = 62,
            GetDeviceRemovedReason = 63,
            SetExceptionMode = 64,
            GetExceptionMode = 65,
            GetPrivateData = 66,
            SetPrivateData = 67,
            SetPrivateDataInterface = 68,
            ClearState = 69,
            Flush = 70,
            CreateBuffer = 71,
            CreateTexture1D = 72,
            CreateTexture2D = 73,
            CreateTexture3D = 74,
            CreateShaderResourceView = 75,
            CreateRenderTargetView = 76,
            CreateDepthStencilView = 77,
            CreateInputLayout = 78,
            CreateVertexShader = 79,
            CreateGeometryShader = 80,
            CreateGemoetryShaderWithStreamOutput = 81,
            CreatePixelShader = 82,
            CreateBlendState = 83,
            CreateDepthStencilState = 84,
            CreateRasterizerState = 85,
            CreateSamplerState = 86,
            CreateQuery = 87,
            CreatePredicate = 88,
            CreateCounter = 89,
            CheckFormatSupport = 90,
            CheckMultisampleQualityLevels = 91,
            CheckCounterInfo = 92,
            CheckCounter = 93,
            GetCreationFlags = 94,
            OpenSharedResource = 95,
            SetTextFilterSize = 96,
            GetTextFilterSize = 97,
        }
        //// End D3D10DeviceVirtualTableEnum

        internal enum D3D101DeviceVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,

            AddRef = 1,

            Release = 2,

            //// ID3D10Device
            VSSetConstantBuffers = 3,

            PSSetShaderResources = 4,

            PSSetShader = 5,

            PSSetSamplers = 6,

            VSSetShader = 7,

            DrawIndexed = 8,

            Draw = 9,

            PSSetConstantBuffers = 10,

            IASetInputLayout = 11,

            IASetVertexBuffers = 12,

            IASetIndexBuffer = 13,

            DrawIndexedInstanced = 14,

            DrawInstanced = 15,

            GSSetConstantBuffers = 16,

            GSSetShader = 17,

            IASetPrimitiveTopology = 18,

            VSSetShaderResources = 19,

            VSSetSamplers = 20,

            SetPredication = 21,

            GSSetShaderResources = 22,

            GSSetSamplers = 23,

            OMSetRenderTargets = 24,

            OMSetBlendState = 25,

            OMSetDepthStencilState = 26,

            SOSetTargets = 27,

            DrawAuto = 28,

            RSSetState = 29,

            RSSetViewports = 30,

            RSSetScissorRects = 31,

            CopySubresourceRegion = 32,

            CopyResource = 33,

            UpdateSubresource = 34,

            ClearRenderTargetView = 35,

            ClearDepthStencilView = 36,

            GenerateMips = 37,

            ResolveSubresource = 38,

            VSGetConstantBuffers = 39,

            PSGetShaderResources = 40,

            PSGetShader = 41,

            PSGetSamplers = 42,

            VSGetShader = 43,

            PSGetConstantBuffers = 44,

            IAGetInputLayout = 45,

            IAGetVertexBuffers = 46,

            IAGetIndexBuffer = 47,

            GSGetConstantBuffers = 48,

            GSGetShader = 49,

            IAGetPrimitiveTopology = 50,

            VSGetShaderResources = 51,

            VSGetSamplers = 52,

            GetPredication = 53,

            GSGetShaderResources = 54,

            GSGetSamplers = 55,

            OMGetRenderTargets = 56,

            OMGetBlendState = 57,

            OMGetDepthStencilState = 58,

            SOGetTargets = 59,

            RSGetState = 60,

            RSGetViewports = 61,

            RSGetScissorRects = 62,

            GetDeviceRemovedReason = 63,

            SetExceptionMode = 64,

            GetExceptionMode = 65,

            GetPrivateData = 66,

            SetPrivateData = 67,

            SetPrivateDataInterface = 68,

            ClearState = 69,

            Flush = 70,

            CreateBuffer = 71,

            CreateTexture1D = 72,

            CreateTexture2D = 73,

            CreateTexture3D = 74,

            CreateShaderResourceView = 75,

            CreateRenderTargetView = 76,

            CreateDepthStencilView = 77,

            CreateInputLayout = 78,

            CreateVertexShader = 79,

            CreateGeometryShader = 80,

            CreateGemoetryShaderWithStreamOutput = 81,

            CreatePixelShader = 82,

            CreateBlendState = 83,

            CreateDepthStencilState = 84,

            CreateRasterizerState = 85,

            CreateSamplerState = 86,

            CreateQuery = 87,

            CreatePredicate = 88,

            CreateCounter = 89,

            CheckFormatSupport = 90,

            CheckMultisampleQualityLevels = 91,

            CheckCounterInfo = 92,

            CheckCounter = 93,

            GetCreationFlags = 94,

            OpenSharedResource = 95,

            SetTextFilterSize = 96,

            GetTextFilterSize = 97,

            //// ID3D10Device1
            CreateShaderResourceView1 = 98,

            CreateBlendState1 = 99,

            GetFeatureLevel = 100,
        }
        //// End D3D101DeviceVirtualTableEnum

        internal enum D3D11DeviceVirtualTableEnum : Int16
        {
            //// IUnknown
            QueryInterface = 0,

            AddRef = 1,

            Release = 2,

            //// ID3D11Device
            CreateBuffer = 3,

            CreateTexture1D = 4,

            CreateTexture2D = 5,

            CreateTexture3D = 6,

            CreateShaderResourceView = 7,

            CreateUnorderedAccessView = 8,

            CreateRenderTargetView = 9,

            CreateDepthStencilView = 10,

            CreateInputLayout = 11,

            CreateVertexShader = 12,

            CreateGeometryShader = 13,

            CreateGeometryShaderWithStreamOutput = 14,

            CreatePixelShader = 15,

            CreateHullShader = 16,

            CreateDomainShader = 17,

            CreateComputeShader = 18,

            CreateClassLinkage = 19,

            CreateBlendState = 20,

            CreateDepthStencilState = 21,

            CreateRasterizerState = 22,

            CreateSamplerState = 23,

            CreateQuery = 24,

            CreatePredicate = 25,

            CreateCounter = 26,

            CreateDeferredContext = 27,

            OpenSharedResource = 28,

            CheckFormatSupport = 29,

            CheckMultisampleQualityLevels = 30,

            CheckCounterInfo = 31,

            CheckCounter = 32,

            CheckFeatureSupport = 33,

            GetPrivateData = 34,

            SetPrivateData = 35,

            SetPrivateDataInterface = 36,

            GetFeatureLevel = 37,

            GetCreationFlags = 38,

            GetDeviceRemovedReason = 39,

            GetImmediateContext = 40,

            SetExceptionMode = 41,

            GetExceptionMode = 42,
        }
        //// End D3D11DeviceVirtualTableEnum
    }
    //// End class
}
//// End namespace