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
#pragma once

namespace SlimDX
{
	namespace Direct3D11
	{
		public enum class DebugMessageId : System::Int32
		{
			///<summary>Unknown message.</summary>
			Unknown = D3D11_MESSAGE_ID_UNKNOWN,

			///<summary>DeviceIASetVertexBuffersHazard message.</summary>
			DeviceIASetVertexBuffersHazard = D3D11_MESSAGE_ID_DEVICE_IASETVERTEXBUFFERS_HAZARD,

			///<summary>DeviceIASetIndexBufferHazard message.</summary>
			DeviceIASetIndexBufferHazard = D3D11_MESSAGE_ID_DEVICE_IASETINDEXBUFFER_HAZARD,

			///<summary>DeviceVSSetShaderResourcesHazard message.</summary>
			DeviceVSSetShaderResourcesHazard = D3D11_MESSAGE_ID_DEVICE_VSSETSHADERRESOURCES_HAZARD,

			///<summary>DeviceVSSetConstantBuffersHazard message.</summary>
			DeviceVSSetConstantBuffersHazard = D3D11_MESSAGE_ID_DEVICE_VSSETCONSTANTBUFFERS_HAZARD,

			///<summary>DeviceGSSetShaderResourcesHazard message.</summary>
			DeviceGSSetShaderResourcesHazard = D3D11_MESSAGE_ID_DEVICE_GSSETSHADERRESOURCES_HAZARD,

			///<summary>DeviceGSSetConstantBuffersHazard message.</summary>
			DeviceGSSetConstantBuffersHazard = D3D11_MESSAGE_ID_DEVICE_GSSETCONSTANTBUFFERS_HAZARD,

			///<summary>DevicePSSetShaderResourcesHazard message.</summary>
			DevicePSSetShaderResourcesHazard = D3D11_MESSAGE_ID_DEVICE_PSSETSHADERRESOURCES_HAZARD,

			///<summary>DevicePSSetConstantBuffersHazard message.</summary>
			DevicePSSetConstantBuffersHazard = D3D11_MESSAGE_ID_DEVICE_PSSETCONSTANTBUFFERS_HAZARD,

			///<summary>DeviceOMSetRenderTargetsHazard message.</summary>
			DeviceOMSetRenderTargetsHazard = D3D11_MESSAGE_ID_DEVICE_OMSETRENDERTARGETS_HAZARD,

			///<summary>DeviceSOSetTargetsHazard message.</summary>
			DeviceSOSetTargetsHazard = D3D11_MESSAGE_ID_DEVICE_SOSETTARGETS_HAZARD,

			///<summary>StringFromApplication message.</summary>
			StringFromApplication = D3D11_MESSAGE_ID_STRING_FROM_APPLICATION,

			///<summary>CorruptedThis message.</summary>
			CorruptedThis = D3D11_MESSAGE_ID_CORRUPTED_THIS,

			///<summary>CorruptedParameter1 message.</summary>
			CorruptedParameter1 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER1,

			///<summary>CorruptedParameter2 message.</summary>
			CorruptedParameter2 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER2,

			///<summary>CorruptedParameter3 message.</summary>
			CorruptedParameter3 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER3,

			///<summary>CorruptedParameter4 message.</summary>
			CorruptedParameter4 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER4,

			///<summary>CorruptedParameter5 message.</summary>
			CorruptedParameter5 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER5,

			///<summary>CorruptedParameter6 message.</summary>
			CorruptedParameter6 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER6,

			///<summary>CorruptedParameter7 message.</summary>
			CorruptedParameter7 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER7,

			///<summary>CorruptedParameter8 message.</summary>
			CorruptedParameter8 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER8,

			///<summary>CorruptedParameter9 message.</summary>
			CorruptedParameter9 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER9,

			///<summary>CorruptedParameter10 message.</summary>
			CorruptedParameter10 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER10,

			///<summary>CorruptedParameter11 message.</summary>
			CorruptedParameter11 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER11,

			///<summary>CorruptedParameter12 message.</summary>
			CorruptedParameter12 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER12,

			///<summary>CorruptedParameter13 message.</summary>
			CorruptedParameter13 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER13,

			///<summary>CorruptedParameter14 message.</summary>
			CorruptedParameter14 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER14,

			///<summary>CorruptedParameter15 message.</summary>
			CorruptedParameter15 = D3D11_MESSAGE_ID_CORRUPTED_PARAMETER15,

			///<summary>CorruptedMultithreading message.</summary>
			CorruptedMultithreading = D3D11_MESSAGE_ID_CORRUPTED_MULTITHREADING,

			///<summary>MessageReportingOutOfMemory message.</summary>
			MessageReportingOutOfMemory = D3D11_MESSAGE_ID_MESSAGE_REPORTING_OUTOFMEMORY,

			///<summary>IASetInputLayoutUnbindDeletingObject message.</summary>
			IASetInputLayoutUnbindDeletingObject = D3D11_MESSAGE_ID_IASETINPUTLAYOUT_UNBINDDELETINGOBJECT,

			///<summary>IASetVertexBuffersUnbindDeletingObject message.</summary>
			IASetVertexBuffersUnbindDeletingObject = D3D11_MESSAGE_ID_IASETVERTEXBUFFERS_UNBINDDELETINGOBJECT,

			///<summary>IASetIndexBufferUnbindDeletingObject message.</summary>
			IASetIndexBufferUnbindDeletingObject = D3D11_MESSAGE_ID_IASETINDEXBUFFER_UNBINDDELETINGOBJECT,

			///<summary>VSSetShaderUnbindDeletingObject message.</summary>
			VSSetShaderUnbindDeletingObject = D3D11_MESSAGE_ID_VSSETSHADER_UNBINDDELETINGOBJECT,

			///<summary>VSSetShaderResourcesUnbindDeletingObject message.</summary>
			VSSetShaderResourcesUnbindDeletingObject = D3D11_MESSAGE_ID_VSSETSHADERRESOURCES_UNBINDDELETINGOBJECT,

			///<summary>VSSetConstantBuffersUnbindDeletingObject message.</summary>
			VSSetConstantBuffersUnbindDeletingObject = D3D11_MESSAGE_ID_VSSETCONSTANTBUFFERS_UNBINDDELETINGOBJECT,

			///<summary>VSSetSamplersUnbindDeletingObject message.</summary>
			VSSetSamplersUnbindDeletingObject = D3D11_MESSAGE_ID_VSSETSAMPLERS_UNBINDDELETINGOBJECT,

			///<summary>GSSetShaderUnbindDeletingObject message.</summary>
			GSSetShaderUnbindDeletingObject = D3D11_MESSAGE_ID_GSSETSHADER_UNBINDDELETINGOBJECT,

			///<summary>GSSetShaderResourcesUnbindDeletingObject message.</summary>
			GSSetShaderResourcesUnbindDeletingObject = D3D11_MESSAGE_ID_GSSETSHADERRESOURCES_UNBINDDELETINGOBJECT,

			///<summary>GSSetConstantBuffersUnbindDeletingObject message.</summary>
			GSSetConstantBuffersUnbindDeletingObject = D3D11_MESSAGE_ID_GSSETCONSTANTBUFFERS_UNBINDDELETINGOBJECT,

			///<summary>GSSetSamplersUnbindDeletingObject message.</summary>
			GSSetSamplersUnbindDeletingObject = D3D11_MESSAGE_ID_GSSETSAMPLERS_UNBINDDELETINGOBJECT,

			///<summary>SOSetTargetsUnbindDeletingObject message.</summary>
			SOSetTargetsUnbindDeletingObject = D3D11_MESSAGE_ID_SOSETTARGETS_UNBINDDELETINGOBJECT,

			///<summary>PSSetShaderUnbindDeletingObject message.</summary>
			PSSetShaderUnbindDeletingObject = D3D11_MESSAGE_ID_PSSETSHADER_UNBINDDELETINGOBJECT,

			///<summary>PSSetShaderResourcesUnbindDeletingObject message.</summary>
			PSSetShaderResourcesUnbindDeletingObject = D3D11_MESSAGE_ID_PSSETSHADERRESOURCES_UNBINDDELETINGOBJECT,

			///<summary>PSSetConstantBuffersUnbindDeletingObject message.</summary>
			PSSetConstantBuffersUnbindDeletingObject = D3D11_MESSAGE_ID_PSSETCONSTANTBUFFERS_UNBINDDELETINGOBJECT,

			///<summary>PSSetSamplersUnbindDeletingObject message.</summary>
			PSSetSamplersUnbindDeletingObject = D3D11_MESSAGE_ID_PSSETSAMPLERS_UNBINDDELETINGOBJECT,

			///<summary>RSSetStateUnbindDeletingObject message.</summary>
			RSSetStateUnbindDeletingObject = D3D11_MESSAGE_ID_RSSETSTATE_UNBINDDELETINGOBJECT,

			///<summary>OMSetBlendStateUnbindDeletingObject message.</summary>
			OMSetBlendStateUnbindDeletingObject = D3D11_MESSAGE_ID_OMSETBLENDSTATE_UNBINDDELETINGOBJECT,

			///<summary>OMSetDepthStencilStateUnbindDeletingObject message.</summary>
			OMSetDepthStencilStateUnbindDeletingObject = D3D11_MESSAGE_ID_OMSETDEPTHSTENCILSTATE_UNBINDDELETINGOBJECT,

			///<summary>OMSetRenderTargetsUnbindDeletingObject message.</summary>
			OMSetRenderTargetsUnbindDeletingObject = D3D11_MESSAGE_ID_OMSETRENDERTARGETS_UNBINDDELETINGOBJECT,

			///<summary>SetPredicationUnbindDeletingObject message.</summary>
			SetPredicationUnbindDeletingObject = D3D11_MESSAGE_ID_SETPREDICATION_UNBINDDELETINGOBJECT,

			///<summary>GetPrivateDataMoreData message.</summary>
			GetPrivateDataMoreData = D3D11_MESSAGE_ID_GETPRIVATEDATA_MOREDATA,

			///<summary>SetPrivateDataInvalidFreeData message.</summary>
			SetPrivateDataInvalidFreeData = D3D11_MESSAGE_ID_SETPRIVATEDATA_INVALIDFREEDATA,

			///<summary>SetPrivateDataInvalidIUnknown message.</summary>
			SetPrivateDataInvalidIUnknown = D3D11_MESSAGE_ID_SETPRIVATEDATA_INVALIDIUNKNOWN,

			///<summary>SetPrivateDataInvalidFlags message.</summary>
			SetPrivateDataInvalidFlags = D3D11_MESSAGE_ID_SETPRIVATEDATA_INVALIDFLAGS,

			///<summary>SetPrivateDataChangingParameters message.</summary>
			SetPrivateDataChangingParameters = D3D11_MESSAGE_ID_SETPRIVATEDATA_CHANGINGPARAMS,

			///<summary>SetPrivateDataOutOfMemory message.</summary>
			SetPrivateDataOutOfMemory = D3D11_MESSAGE_ID_SETPRIVATEDATA_OUTOFMEMORY,

			///<summary>CreateBufferUnrecognizedFormat message.</summary>
			CreateBufferUnrecognizedFormat = D3D11_MESSAGE_ID_CREATEBUFFER_UNRECOGNIZEDFORMAT,

			///<summary>CreateBufferInvalidSamples message.</summary>
			CreateBufferInvalidSamples = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDSAMPLES,

			///<summary>CreateBufferUnrecognizedUsage message.</summary>
			CreateBufferUnrecognizedUsage = D3D11_MESSAGE_ID_CREATEBUFFER_UNRECOGNIZEDUSAGE,

			///<summary>CreateBufferUnrecognizedBindFlags message.</summary>
			CreateBufferUnrecognizedBindFlags = D3D11_MESSAGE_ID_CREATEBUFFER_UNRECOGNIZEDBINDFLAGS,

			///<summary>CreateBufferUnrecognizedCPUAccessFlags message.</summary>
			CreateBufferUnrecognizedCPUAccessFlags = D3D11_MESSAGE_ID_CREATEBUFFER_UNRECOGNIZEDCPUACCESSFLAGS,

			///<summary>CreateBufferUnrecognizedMiscellaneousFlags message.</summary>
			CreateBufferUnrecognizedMiscellaneousFlags = D3D11_MESSAGE_ID_CREATEBUFFER_UNRECOGNIZEDMISCFLAGS,

			///<summary>CreateBufferInvalidCPUAccessFlags message.</summary>
			CreateBufferInvalidCPUAccessFlags = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDCPUACCESSFLAGS,

			///<summary>CreateBufferInvalidBindFlags message.</summary>
			CreateBufferInvalidBindFlags = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDBINDFLAGS,

			///<summary>CreateBufferInvalidInitialData message.</summary>
			CreateBufferInvalidInitialData = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDINITIALDATA,

			///<summary>CreateBufferInvalidDimensions message.</summary>
			CreateBufferInvalidDimensions = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDDIMENSIONS,

			///<summary>CreateBufferInvalidMipLevels message.</summary>
			CreateBufferInvalidMipLevels = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDMIPLEVELS,

			///<summary>CreateBufferInvalidMiscellaneousFlags message.</summary>
			CreateBufferInvalidMiscellaneousFlags = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDMISCFLAGS,

			///<summary>CreateBufferInvalidArgumentReturn message.</summary>
			CreateBufferInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDARG_RETURN,

			///<summary>CreateBufferOutOfMemoryReturn message.</summary>
			CreateBufferOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATEBUFFER_OUTOFMEMORY_RETURN,

			///<summary>CreateBufferNullDescription message.</summary>
			CreateBufferNullDescription = D3D11_MESSAGE_ID_CREATEBUFFER_NULLDESC,

			///<summary>CreateBufferInvalidConstantBufferBindings message.</summary>
			CreateBufferInvalidConstantBufferBindings = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDCONSTANTBUFFERBINDINGS,

			///<summary>CreateBufferLargeAllocation message.</summary>
			CreateBufferLargeAllocation = D3D11_MESSAGE_ID_CREATEBUFFER_LARGEALLOCATION,

			///<summary>CreateTexture1DUnrecognizedFormat message.</summary>
			CreateTexture1DUnrecognizedFormat = D3D11_MESSAGE_ID_CREATETEXTURE1D_UNRECOGNIZEDFORMAT,

			///<summary>CreateTexture1DUnsupportedFormat message.</summary>
			CreateTexture1DUnsupportedFormat = D3D11_MESSAGE_ID_CREATETEXTURE1D_UNSUPPORTEDFORMAT,

			///<summary>CreateTexture1DInvalidSamples message.</summary>
			CreateTexture1DInvalidSamples = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDSAMPLES,

			///<summary>CreateTexture1DUnrecognizedUsage message.</summary>
			CreateTexture1DUnrecognizedUsage = D3D11_MESSAGE_ID_CREATETEXTURE1D_UNRECOGNIZEDUSAGE,

			///<summary>CreateTexture1DUnrecognizedBindFlags message.</summary>
			CreateTexture1DUnrecognizedBindFlags = D3D11_MESSAGE_ID_CREATETEXTURE1D_UNRECOGNIZEDBINDFLAGS,

			///<summary>CreateTexture1DUnrecognizedCPUAccessFlags message.</summary>
			CreateTexture1DUnrecognizedCPUAccessFlags = D3D11_MESSAGE_ID_CREATETEXTURE1D_UNRECOGNIZEDCPUACCESSFLAGS,

			///<summary>CreateTexture1DUnrecognizedMiscellaneousFlags message.</summary>
			CreateTexture1DUnrecognizedMiscellaneousFlags = D3D11_MESSAGE_ID_CREATETEXTURE1D_UNRECOGNIZEDMISCFLAGS,

			///<summary>CreateTexture1DInvalidCPUAccessFlags message.</summary>
			CreateTexture1DInvalidCPUAccessFlags = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDCPUACCESSFLAGS,

			///<summary>CreateTexture1DInvalidBindFlags message.</summary>
			CreateTexture1DInvalidBindFlags = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDBINDFLAGS,

			///<summary>CreateTexture1DInvalidInitialData message.</summary>
			CreateTexture1DInvalidInitialData = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDINITIALDATA,

			///<summary>CreateTexture1DInvalidDimensions message.</summary>
			CreateTexture1DInvalidDimensions = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDDIMENSIONS,

			///<summary>CreateTexture1DInvalidMipLevels message.</summary>
			CreateTexture1DInvalidMipLevels = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDMIPLEVELS,

			///<summary>CreateTexture1DInvalidMiscellaneousFlags message.</summary>
			CreateTexture1DInvalidMiscellaneousFlags = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDMISCFLAGS,

			///<summary>CreateTexture1DInvalidArgumentReturn message.</summary>
			CreateTexture1DInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATETEXTURE1D_INVALIDARG_RETURN,

			///<summary>CreateTexture1DOutOfMemoryReturn message.</summary>
			CreateTexture1DOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATETEXTURE1D_OUTOFMEMORY_RETURN,

			///<summary>CreateTexture1DNullDescription message.</summary>
			CreateTexture1DNullDescription = D3D11_MESSAGE_ID_CREATETEXTURE1D_NULLDESC,

			///<summary>CreateTexture1DLargeAllocation message.</summary>
			CreateTexture1DLargeAllocation = D3D11_MESSAGE_ID_CREATETEXTURE1D_LARGEALLOCATION,

			///<summary>CreateTexture2DUnrecognizedFormat message.</summary>
			CreateTexture2DUnrecognizedFormat = D3D11_MESSAGE_ID_CREATETEXTURE2D_UNRECOGNIZEDFORMAT,

			///<summary>CreateTexture2DUnsupportedFormat message.</summary>
			CreateTexture2DUnsupportedFormat = D3D11_MESSAGE_ID_CREATETEXTURE2D_UNSUPPORTEDFORMAT,

			///<summary>CreateTexture2DInvalidSamples message.</summary>
			CreateTexture2DInvalidSamples = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDSAMPLES,

			///<summary>CreateTexture2DUnrecognizedUsage message.</summary>
			CreateTexture2DUnrecognizedUsage = D3D11_MESSAGE_ID_CREATETEXTURE2D_UNRECOGNIZEDUSAGE,

			///<summary>CreateTexture2DUnrecognizedBindFlags message.</summary>
			CreateTexture2DUnrecognizedBindFlags = D3D11_MESSAGE_ID_CREATETEXTURE2D_UNRECOGNIZEDBINDFLAGS,

			///<summary>CreateTexture2DUnrecognizedCPUAccessFlags message.</summary>
			CreateTexture2DUnrecognizedCPUAccessFlags = D3D11_MESSAGE_ID_CREATETEXTURE2D_UNRECOGNIZEDCPUACCESSFLAGS,

			///<summary>CreateTexture2DUnrecognizedMiscellaneousFlags message.</summary>
			CreateTexture2DUnrecognizedMiscellaneousFlags = D3D11_MESSAGE_ID_CREATETEXTURE2D_UNRECOGNIZEDMISCFLAGS,

			///<summary>CreateTexture2DInvalidCPUAccessFlags message.</summary>
			CreateTexture2DInvalidCPUAccessFlags = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDCPUACCESSFLAGS,

			///<summary>CreateTexture2DInvalidBindFlags message.</summary>
			CreateTexture2DInvalidBindFlags = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDBINDFLAGS,

			///<summary>CreateTexture2DInvalidInitialData message.</summary>
			CreateTexture2DInvalidInitialData = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDINITIALDATA,

			///<summary>CreateTexture2DInvalidDimensions message.</summary>
			CreateTexture2DInvalidDimensions = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDDIMENSIONS,

			///<summary>CreateTexture2DInvalidMipLevels message.</summary>
			CreateTexture2DInvalidMipLevels = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDMIPLEVELS,

			///<summary>CreateTexture2DInvalidMiscellaneousFlags message.</summary>
			CreateTexture2DInvalidMiscellaneousFlags = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDMISCFLAGS,

			///<summary>CreateTexture2DInvalidArgumentReturn message.</summary>
			CreateTexture2DInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATETEXTURE2D_INVALIDARG_RETURN,

			///<summary>CreateTexture2DOutOfMemoryReturn message.</summary>
			CreateTexture2DOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATETEXTURE2D_OUTOFMEMORY_RETURN,

			///<summary>CreateTexture2DNullDescription message.</summary>
			CreateTexture2DNullDescription = D3D11_MESSAGE_ID_CREATETEXTURE2D_NULLDESC,

			///<summary>CreateTexture2DLargeAllocation message.</summary>
			CreateTexture2DLargeAllocation = D3D11_MESSAGE_ID_CREATETEXTURE2D_LARGEALLOCATION,

			///<summary>CreateTexture3DUnrecognizedFormat message.</summary>
			CreateTexture3DUnrecognizedFormat = D3D11_MESSAGE_ID_CREATETEXTURE3D_UNRECOGNIZEDFORMAT,

			///<summary>CreateTexture3DUnsupportedFormat message.</summary>
			CreateTexture3DUnsupportedFormat = D3D11_MESSAGE_ID_CREATETEXTURE3D_UNSUPPORTEDFORMAT,

			///<summary>CreateTexture3DInvalidSamples message.</summary>
			CreateTexture3DInvalidSamples = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDSAMPLES,

			///<summary>CreateTexture3DUnrecognizedUsage message.</summary>
			CreateTexture3DUnrecognizedUsage = D3D11_MESSAGE_ID_CREATETEXTURE3D_UNRECOGNIZEDUSAGE,

			///<summary>CreateTexture3DUnrecognizedBindFlags message.</summary>
			CreateTexture3DUnrecognizedBindFlags = D3D11_MESSAGE_ID_CREATETEXTURE3D_UNRECOGNIZEDBINDFLAGS,

			///<summary>CreateTexture3DUnrecognizedCPUAccessFlags message.</summary>
			CreateTexture3DUnrecognizedCPUAccessFlags = D3D11_MESSAGE_ID_CREATETEXTURE3D_UNRECOGNIZEDCPUACCESSFLAGS,

			///<summary>CreateTexture3DUnrecognizedMiscellaneousFlags message.</summary>
			CreateTexture3DUnrecognizedMiscellaneousFlags = D3D11_MESSAGE_ID_CREATETEXTURE3D_UNRECOGNIZEDMISCFLAGS,

			///<summary>CreateTexture3DInvalidCPUAccessFlags message.</summary>
			CreateTexture3DInvalidCPUAccessFlags = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDCPUACCESSFLAGS,

			///<summary>CreateTexture3DInvalidBindFlags message.</summary>
			CreateTexture3DInvalidBindFlags = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDBINDFLAGS,

			///<summary>CreateTexture3DInvalidInitialData message.</summary>
			CreateTexture3DInvalidInitialData = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDINITIALDATA,

			///<summary>CreateTexture3DInvalidDimensions message.</summary>
			CreateTexture3DInvalidDimensions = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDDIMENSIONS,

			///<summary>CreateTexture3DInvalidMipLevels message.</summary>
			CreateTexture3DInvalidMipLevels = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDMIPLEVELS,

			///<summary>CreateTexture3DInvalidMiscellaneousFlags message.</summary>
			CreateTexture3DInvalidMiscellaneousFlags = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDMISCFLAGS,

			///<summary>CreateTexture3DInvalidArgumentReturn message.</summary>
			CreateTexture3DInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATETEXTURE3D_INVALIDARG_RETURN,

			///<summary>CreateTexture3DOutOfMemoryReturn message.</summary>
			CreateTexture3DOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATETEXTURE3D_OUTOFMEMORY_RETURN,

			///<summary>CreateTexture3DNullDescription message.</summary>
			CreateTexture3DNullDescription = D3D11_MESSAGE_ID_CREATETEXTURE3D_NULLDESC,

			///<summary>CreateTexture3DLargeAllocation message.</summary>
			CreateTexture3DLargeAllocation = D3D11_MESSAGE_ID_CREATETEXTURE3D_LARGEALLOCATION,

			///<summary>CreateShaderResourceViewUnrecognizedFormat message.</summary>
			CreateShaderResourceViewUnrecognizedFormat = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_UNRECOGNIZEDFORMAT,

			///<summary>CreateShaderResourceViewInvalidDescription message.</summary>
			CreateShaderResourceViewInvalidDescription = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_INVALIDDESC,

			///<summary>CreateShaderResourceViewInvalidFormat message.</summary>
			CreateShaderResourceViewInvalidFormat = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_INVALIDFORMAT,

			///<summary>CreateShaderResourceViewInvalidDimensions message.</summary>
			CreateShaderResourceViewInvalidDimensions = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_INVALIDDIMENSIONS,

			///<summary>CreateShaderResourceViewInvalidResource message.</summary>
			CreateShaderResourceViewInvalidResource = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_INVALIDRESOURCE,

			///<summary>CreateShaderResourceViewTooManyObjects message.</summary>
			CreateShaderResourceViewTooManyObjects = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_TOOMANYOBJECTS,

			///<summary>CreateShaderResourceViewInvalidArgumentReturn message.</summary>
			CreateShaderResourceViewInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_INVALIDARG_RETURN,

			///<summary>CreateShaderResourceViewOutOfMemoryReturn message.</summary>
			CreateShaderResourceViewOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_OUTOFMEMORY_RETURN,

			///<summary>CreateRenderTargetViewUnrecognizedFormat message.</summary>
			CreateRenderTargetViewUnrecognizedFormat = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_UNRECOGNIZEDFORMAT,

			///<summary>CreateRenderTargetViewUnsupportedFormat message.</summary>
			CreateRenderTargetViewUnsupportedFormat = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_UNSUPPORTEDFORMAT,

			///<summary>CreateRenderTargetViewInvalidDescription message.</summary>
			CreateRenderTargetViewInvalidDescription = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_INVALIDDESC,

			///<summary>CreateRenderTargetViewInvalidFormat message.</summary>
			CreateRenderTargetViewInvalidFormat = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_INVALIDFORMAT,

			///<summary>CreateRenderTargetViewInvalidDimensions message.</summary>
			CreateRenderTargetViewInvalidDimensions = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_INVALIDDIMENSIONS,

			///<summary>CreateRenderTargetViewInvalidResource message.</summary>
			CreateRenderTargetViewInvalidResource = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_INVALIDRESOURCE,

			///<summary>CreateRenderTargetViewTooManyObjects message.</summary>
			CreateRenderTargetViewTooManyObjects = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_TOOMANYOBJECTS,

			///<summary>CreateRenderTargetViewInvalidArgumentReturn message.</summary>
			CreateRenderTargetViewInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_INVALIDARG_RETURN,

			///<summary>CreateRenderTargetViewOutOfMemoryReturn message.</summary>
			CreateRenderTargetViewOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATERENDERTARGETVIEW_OUTOFMEMORY_RETURN,

			///<summary>CreateDepthStencilViewUnrecognizedFormat message.</summary>
			CreateDepthStencilViewUnrecognizedFormat = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_UNRECOGNIZEDFORMAT,

			///<summary>CreateDepthStencilViewInvalidDescription message.</summary>
			CreateDepthStencilViewInvalidDescription = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_INVALIDDESC,

			///<summary>CreateDepthStencilViewInvalidFormat message.</summary>
			CreateDepthStencilViewInvalidFormat = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_INVALIDFORMAT,

			///<summary>CreateDepthStencilViewInvalidDimensions message.</summary>
			CreateDepthStencilViewInvalidDimensions = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_INVALIDDIMENSIONS,

			///<summary>CreateDepthStencilViewInvalidResource message.</summary>
			CreateDepthStencilViewInvalidResource = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_INVALIDRESOURCE,

			///<summary>CreateDepthStencilViewTooManyObjects message.</summary>
			CreateDepthStencilViewTooManyObjects = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_TOOMANYOBJECTS,

			///<summary>CreateDepthStencilViewInvalidArgumentReturn message.</summary>
			CreateDepthStencilViewInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_INVALIDARG_RETURN,

			///<summary>CreateDepthStencilViewOutOfMemoryReturn message.</summary>
			CreateDepthStencilViewOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_OUTOFMEMORY_RETURN,

			///<summary>CreateInputLayoutOutOfMemory message.</summary>
			CreateInputLayoutOutOfMemory = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_OUTOFMEMORY,

			///<summary>CreateInputLayoutTooManyElements message.</summary>
			CreateInputLayoutTooManyElements = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_TOOMANYELEMENTS,

			///<summary>CreateInputLayoutInvalidFormat message.</summary>
			CreateInputLayoutInvalidFormat = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_INVALIDFORMAT,

			///<summary>CreateInputLayoutIncompatibleFormat message.</summary>
			CreateInputLayoutIncompatibleFormat = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_INCOMPATIBLEFORMAT,

			///<summary>CreateInputLayoutInvalidSlot message.</summary>
			CreateInputLayoutInvalidSlot = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_INVALIDSLOT,

			///<summary>CreateInputLayoutInvalidInputSlotClass message.</summary>
			CreateInputLayoutInvalidInputSlotClass = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_INVALIDINPUTSLOTCLASS,

			///<summary>CreateInputLayoutStepRateSlotClassMismatch message.</summary>
			CreateInputLayoutStepRateSlotClassMismatch = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_STEPRATESLOTCLASSMISMATCH,

			///<summary>CreateInputLayoutInvalidSlotClassChange message.</summary>
			CreateInputLayoutInvalidSlotClassChange = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_INVALIDSLOTCLASSCHANGE,

			///<summary>CreateInputLayoutInvalidStepRateChange message.</summary>
			CreateInputLayoutInvalidStepRateChange = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_INVALIDSTEPRATECHANGE,

			///<summary>CreateInputLayoutInvalidAlignment message.</summary>
			CreateInputLayoutInvalidAlignment = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_INVALIDALIGNMENT,

			///<summary>CreateInputLayoutDuplicateSemantic message.</summary>
			CreateInputLayoutDuplicateSemantic = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_DUPLICATESEMANTIC,

			///<summary>CreateInputLayoutUnparseableInputSignature message.</summary>
			CreateInputLayoutUnparseableInputSignature = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_UNPARSEABLEINPUTSIGNATURE,

			///<summary>CreateInputLayoutNullSemantic message.</summary>
			CreateInputLayoutNullSemantic = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_NULLSEMANTIC,

			///<summary>CreateInputLayoutMissingElement message.</summary>
			CreateInputLayoutMissingElement = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_MISSINGELEMENT,

			///<summary>CreateInputLayoutNullDescription message.</summary>
			CreateInputLayoutNullDescription = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_NULLDESC,

			///<summary>CreateVertexShaderOutOfMemory message.</summary>
			CreateVertexShaderOutOfMemory = D3D11_MESSAGE_ID_CREATEVERTEXSHADER_OUTOFMEMORY,

			///<summary>CreateVertexShaderInvalidShaderBytecode message.</summary>
			CreateVertexShaderInvalidShaderBytecode = D3D11_MESSAGE_ID_CREATEVERTEXSHADER_INVALIDSHADERBYTECODE,

			///<summary>CreateVertexShaderInvalidShaderType message.</summary>
			CreateVertexShaderInvalidShaderType = D3D11_MESSAGE_ID_CREATEVERTEXSHADER_INVALIDSHADERTYPE,

			///<summary>CreateGeometryShaderOutOfMemory message.</summary>
			CreateGeometryShaderOutOfMemory = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADER_OUTOFMEMORY,

			///<summary>CreateGeometryShaderInvalidShaderBytecode message.</summary>
			CreateGeometryShaderInvalidShaderBytecode = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADER_INVALIDSHADERBYTECODE,

			///<summary>CreateGeometryShaderInvalidShaderType message.</summary>
			CreateGeometryShaderInvalidShaderType = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADER_INVALIDSHADERTYPE,

			///<summary>CreateGeometryShaderWithStreamOutputOutOfMemory message.</summary>
			CreateGeometryShaderWithStreamOutputOutOfMemory = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_OUTOFMEMORY,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidShaderBytecode message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidShaderBytecode = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDSHADERBYTECODE,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidShaderType message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidShaderType = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDSHADERTYPE,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidEntryCount message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidEntryCount = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDNUMENTRIES,

			///<summary>CreateGeometryShaderWithStreamOutputOutputStreamStrideUnused message.</summary>
			CreateGeometryShaderWithStreamOutputOutputStreamStrideUnused = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_OUTPUTSTREAMSTRIDEUNUSED,

			///<summary>CreateGeometryShaderWithStreamOutputUnexpectedDeclaration message.</summary>
			CreateGeometryShaderWithStreamOutputUnexpectedDeclaration = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_UNEXPECTEDDECL,

			///<summary>CreateGeometryShaderWithStreamOutputExpectedDeclaration message.</summary>
			CreateGeometryShaderWithStreamOutputExpectedDeclaration = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_EXPECTEDDECL,

			///<summary>CreateGeometryShaderWithStreamOutputOutputSlot0Expected message.</summary>
			CreateGeometryShaderWithStreamOutputOutputSlot0Expected = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_OUTPUTSLOT0EXPECTED,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidOutputSlot message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidOutputSlot = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDOUTPUTSLOT,

			///<summary>CreateGeometryShaderWithStreamOutputOnlyOneElementPerSlot message.</summary>
			CreateGeometryShaderWithStreamOutputOnlyOneElementPerSlot = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_ONLYONEELEMENTPERSLOT,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidComponentCount message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidComponentCount = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDCOMPONENTCOUNT,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidStartComponentAndComponentCount message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidStartComponentAndComponentCount = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDSTARTCOMPONENTANDCOMPONENTCOUNT,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidGapDefinition message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidGapDefinition = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDGAPDEFINITION,

			///<summary>CreateGeometryShaderWithStreamOutputRepeatedOutput message.</summary>
			CreateGeometryShaderWithStreamOutputRepeatedOutput = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_REPEATEDOUTPUT,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidOutputStreamStride message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidOutputStreamStride = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDOUTPUTSTREAMSTRIDE,

			///<summary>CreateGeometryShaderWithStreamOutputMissingSemantic message.</summary>
			CreateGeometryShaderWithStreamOutputMissingSemantic = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_MISSINGSEMANTIC,

			///<summary>CreateGeometryShaderWithStreamOutputMaskMismatch message.</summary>
			CreateGeometryShaderWithStreamOutputMaskMismatch = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_MASKMISMATCH,

			///<summary>CreateGeometryShaderWithStreamOutputCantHaveOnlyGaps message.</summary>
			CreateGeometryShaderWithStreamOutputCantHaveOnlyGaps = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_CANTHAVEONLYGAPS,

			///<summary>CreateGeometryShaderWithStreamOutputDeclarationTooComplex message.</summary>
			CreateGeometryShaderWithStreamOutputDeclarationTooComplex = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_DECLTOOCOMPLEX,

			///<summary>CreateGeometryShaderWithStreamOutputMissingOutputSignature message.</summary>
			CreateGeometryShaderWithStreamOutputMissingOutputSignature = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_MISSINGOUTPUTSIGNATURE,

			///<summary>CreatePixelShaderOutOfMemory message.</summary>
			CreatePixelShaderOutOfMemory = D3D11_MESSAGE_ID_CREATEPIXELSHADER_OUTOFMEMORY,

			///<summary>CreatePixelShaderInvalidShaderBytecode message.</summary>
			CreatePixelShaderInvalidShaderBytecode = D3D11_MESSAGE_ID_CREATEPIXELSHADER_INVALIDSHADERBYTECODE,

			///<summary>CreatePixelShaderInvalidShaderType message.</summary>
			CreatePixelShaderInvalidShaderType = D3D11_MESSAGE_ID_CREATEPIXELSHADER_INVALIDSHADERTYPE,

			///<summary>CreateRasterizerStateInvalidFillMode message.</summary>
			CreateRasterizerStateInvalidFillMode = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_INVALIDFILLMODE,

			///<summary>CreateRasterizerStateInvalidCullMode message.</summary>
			CreateRasterizerStateInvalidCullMode = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_INVALIDCULLMODE,

			///<summary>CreateRasterizerStateInvalidDepthBiasClamp message.</summary>
			CreateRasterizerStateInvalidDepthBiasClamp = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_INVALIDDEPTHBIASCLAMP,

			///<summary>CreateRasterizerStateInvalidSlopeScaledDepthBias message.</summary>
			CreateRasterizerStateInvalidSlopeScaledDepthBias = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_INVALIDSLOPESCALEDDEPTHBIAS,

			///<summary>CreateRasterizerStateTooManyObjects message.</summary>
			CreateRasterizerStateTooManyObjects = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_TOOMANYOBJECTS,

			///<summary>CreateRasterizerStateNullDescription message.</summary>
			CreateRasterizerStateNullDescription = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_NULLDESC,

			///<summary>CreateDepthStencilStateInvalidDepthWriteMask message.</summary>
			CreateDepthStencilStateInvalidDepthWriteMask = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDDEPTHWRITEMASK,

			///<summary>CreateDepthStencilStateInvalidDepthFunction message.</summary>
			CreateDepthStencilStateInvalidDepthFunction = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDDEPTHFUNC,

			///<summary>CreateDepthStencilStateInvalidFrontFaceStencilFailOp message.</summary>
			CreateDepthStencilStateInvalidFrontFaceStencilFailOp = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDFRONTFACESTENCILFAILOP,

			///<summary>CreateDepthStencilStateInvalidFrontFaceStencilZFailOp message.</summary>
			CreateDepthStencilStateInvalidFrontFaceStencilZFailOp = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDFRONTFACESTENCILZFAILOP,

			///<summary>CreateDepthStencilStateInvalidFrontFaceStencilPassOp message.</summary>
			CreateDepthStencilStateInvalidFrontFaceStencilPassOp = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDFRONTFACESTENCILPASSOP,

			///<summary>CreateDepthStencilStateInvalidFrontFaceStencilFunction message.</summary>
			CreateDepthStencilStateInvalidFrontFaceStencilFunction = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDFRONTFACESTENCILFUNC,

			///<summary>CreateDepthStencilStateInvalidBackFaceStencilFailOp message.</summary>
			CreateDepthStencilStateInvalidBackFaceStencilFailOp = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDBACKFACESTENCILFAILOP,

			///<summary>CreateDepthStencilStateInvalidBackFaceStencilZFailOp message.</summary>
			CreateDepthStencilStateInvalidBackFaceStencilZFailOp = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDBACKFACESTENCILZFAILOP,

			///<summary>CreateDepthStencilStateInvalidBackFaceStencilPassOp message.</summary>
			CreateDepthStencilStateInvalidBackFaceStencilPassOp = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDBACKFACESTENCILPASSOP,

			///<summary>CreateDepthStencilStateInvalidBackFaceStencilFunction message.</summary>
			CreateDepthStencilStateInvalidBackFaceStencilFunction = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_INVALIDBACKFACESTENCILFUNC,

			///<summary>CreateDepthStencilStateTooManyObjects message.</summary>
			CreateDepthStencilStateTooManyObjects = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_TOOMANYOBJECTS,

			///<summary>CreateDepthStencilStateNullDescription message.</summary>
			CreateDepthStencilStateNullDescription = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_NULLDESC,

			///<summary>CreateBlendStateInvalidSourceBlend message.</summary>
			CreateBlendStateInvalidSourceBlend = D3D11_MESSAGE_ID_CREATEBLENDSTATE_INVALIDSRCBLEND,

			///<summary>CreateBlendStateInvalidDestinationBlend message.</summary>
			CreateBlendStateInvalidDestinationBlend = D3D11_MESSAGE_ID_CREATEBLENDSTATE_INVALIDDESTBLEND,

			///<summary>CreateBlendStateInvalidBlendOperation message.</summary>
			CreateBlendStateInvalidBlendOperation = D3D11_MESSAGE_ID_CREATEBLENDSTATE_INVALIDBLENDOP,

			///<summary>CreateBlendStateInvalidSourceBlendAlpha message.</summary>
			CreateBlendStateInvalidSourceBlendAlpha = D3D11_MESSAGE_ID_CREATEBLENDSTATE_INVALIDSRCBLENDALPHA,

			///<summary>CreateBlendStateInvalidDestinationBlendAlpha message.</summary>
			CreateBlendStateInvalidDestinationBlendAlpha = D3D11_MESSAGE_ID_CREATEBLENDSTATE_INVALIDDESTBLENDALPHA,

			///<summary>CreateBlendStateInvalidBlendOperationAlpha message.</summary>
			CreateBlendStateInvalidBlendOperationAlpha = D3D11_MESSAGE_ID_CREATEBLENDSTATE_INVALIDBLENDOPALPHA,

			///<summary>CreateBlendStateInvalidRenderTargetWriteMask message.</summary>
			CreateBlendStateInvalidRenderTargetWriteMask = D3D11_MESSAGE_ID_CREATEBLENDSTATE_INVALIDRENDERTARGETWRITEMASK,

			///<summary>CreateBlendStateTooManyObjects message.</summary>
			CreateBlendStateTooManyObjects = D3D11_MESSAGE_ID_CREATEBLENDSTATE_TOOMANYOBJECTS,

			///<summary>CreateBlendStateNullDescription message.</summary>
			CreateBlendStateNullDescription = D3D11_MESSAGE_ID_CREATEBLENDSTATE_NULLDESC,

			///<summary>CreateSamplerStateInvalidFilter message.</summary>
			CreateSamplerStateInvalidFilter = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDFILTER,

			///<summary>CreateSamplerStateInvalidAddressU message.</summary>
			CreateSamplerStateInvalidAddressU = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDADDRESSU,

			///<summary>CreateSamplerStateInvalidAddressV message.</summary>
			CreateSamplerStateInvalidAddressV = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDADDRESSV,

			///<summary>CreateSamplerStateInvalidAddressW message.</summary>
			CreateSamplerStateInvalidAddressW = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDADDRESSW,

			///<summary>CreateSamplerStateInvalidMipLodBias message.</summary>
			CreateSamplerStateInvalidMipLodBias = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDMIPLODBIAS,

			///<summary>CreateSamplerStateInvalidMaxAnisotropy message.</summary>
			CreateSamplerStateInvalidMaxAnisotropy = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDMAXANISOTROPY,

			///<summary>CreateSamplerStateInvalidComparisonFunction message.</summary>
			CreateSamplerStateInvalidComparisonFunction = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDCOMPARISONFUNC,

			///<summary>CreateSamplerStateInvalidMinLod message.</summary>
			CreateSamplerStateInvalidMinLod = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDMINLOD,

			///<summary>CreateSamplerStateInvalidMaxLod message.</summary>
			CreateSamplerStateInvalidMaxLod = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_INVALIDMAXLOD,

			///<summary>CreateSamplerStateTooManyObjects message.</summary>
			CreateSamplerStateTooManyObjects = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_TOOMANYOBJECTS,

			///<summary>CreateSamplerStateNullDescription message.</summary>
			CreateSamplerStateNullDescription = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_NULLDESC,

			///<summary>CreateQueryOrPredicateInvalidQuery message.</summary>
			CreateQueryOrPredicateInvalidQuery = D3D11_MESSAGE_ID_CREATEQUERYORPREDICATE_INVALIDQUERY,

			///<summary>CreateQueryOrPredicateInvalidMiscellaneousFlags message.</summary>
			CreateQueryOrPredicateInvalidMiscellaneousFlags = D3D11_MESSAGE_ID_CREATEQUERYORPREDICATE_INVALIDMISCFLAGS,

			///<summary>CreateQueryOrPredicateUnexpectedMiscellaneousFlag message.</summary>
			CreateQueryOrPredicateUnexpectedMiscellaneousFlag = D3D11_MESSAGE_ID_CREATEQUERYORPREDICATE_UNEXPECTEDMISCFLAG,

			///<summary>CreateQueryOrPredicateNullDescription message.</summary>
			CreateQueryOrPredicateNullDescription = D3D11_MESSAGE_ID_CREATEQUERYORPREDICATE_NULLDESC,

			///<summary>DeviceIASetPrimitiveTopologyUnrecognized message.</summary>
			DeviceIASetPrimitiveTopologyUnrecognized = D3D11_MESSAGE_ID_DEVICE_IASETPRIMITIVETOPOLOGY_TOPOLOGY_UNRECOGNIZED,

			///<summary>DeviceIASetPrimitiveTopologyUndefined message.</summary>
			DeviceIASetPrimitiveTopologyUndefined = D3D11_MESSAGE_ID_DEVICE_IASETPRIMITIVETOPOLOGY_TOPOLOGY_UNDEFINED,

			///<summary>IASetVertexBuffersInvalidBuffer message.</summary>
			IASetVertexBuffersInvalidBuffer = D3D11_MESSAGE_ID_IASETVERTEXBUFFERS_INVALIDBUFFER,

			///<summary>DeviceIASetVertexBuffersOffsetTooLarge message.</summary>
			DeviceIASetVertexBuffersOffsetTooLarge = D3D11_MESSAGE_ID_DEVICE_IASETVERTEXBUFFERS_OFFSET_TOO_LARGE,

			///<summary>DeviceIASetVertexBuffersBuffersEmpty message.</summary>
			DeviceIASetVertexBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_IASETVERTEXBUFFERS_BUFFERS_EMPTY,

			///<summary>IASetIndexBufferInvalidBuffer message.</summary>
			IASetIndexBufferInvalidBuffer = D3D11_MESSAGE_ID_IASETINDEXBUFFER_INVALIDBUFFER,

			///<summary>DeviceIASetIndexBufferFormatInvalid message.</summary>
			DeviceIASetIndexBufferFormatInvalid = D3D11_MESSAGE_ID_DEVICE_IASETINDEXBUFFER_FORMAT_INVALID,

			///<summary>DeviceIASetIndexBufferOffsetTooLarge message.</summary>
			DeviceIASetIndexBufferOffsetTooLarge = D3D11_MESSAGE_ID_DEVICE_IASETINDEXBUFFER_OFFSET_TOO_LARGE,

			///<summary>DeviceIASetIndexBufferOffsetUnaligned message.</summary>
			DeviceIASetIndexBufferOffsetUnaligned = D3D11_MESSAGE_ID_DEVICE_IASETINDEXBUFFER_OFFSET_UNALIGNED,

			///<summary>DeviceVSSetShaderResourcesViewsEmpty message.</summary>
			DeviceVSSetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_VSSETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>VSSetConstantBuffersInvalidBuffer message.</summary>
			VSSetConstantBuffersInvalidBuffer = D3D11_MESSAGE_ID_VSSETCONSTANTBUFFERS_INVALIDBUFFER,

			///<summary>DeviceVSSetConstantBuffersBuffersEmpty message.</summary>
			DeviceVSSetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_VSSETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceVSSetSamplersSamplersEmpty message.</summary>
			DeviceVSSetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_VSSETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceGSSetShaderResourcesViewsEmpty message.</summary>
			DeviceGSSetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_GSSETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>GSSetConstantBuffersInvalidBuffer message.</summary>
			GSSetConstantBuffersInvalidBuffer = D3D11_MESSAGE_ID_GSSETCONSTANTBUFFERS_INVALIDBUFFER,

			///<summary>DeviceGSSetConstantBuffersBuffersEmpty message.</summary>
			DeviceGSSetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_GSSETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceGSSetSamplersSamplersEmpty message.</summary>
			DeviceGSSetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_GSSETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>SOSetTargetsInvalidBuffer message.</summary>
			SOSetTargetsInvalidBuffer = D3D11_MESSAGE_ID_SOSETTARGETS_INVALIDBUFFER,

			///<summary>DeviceSOSetTargetsOffsetUnaligned message.</summary>
			DeviceSOSetTargetsOffsetUnaligned = D3D11_MESSAGE_ID_DEVICE_SOSETTARGETS_OFFSET_UNALIGNED,

			///<summary>DevicePSSetShaderResourcesViewsEmpty message.</summary>
			DevicePSSetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_PSSETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>PSSetConstantBuffersInvalidBuffer message.</summary>
			PSSetConstantBuffersInvalidBuffer = D3D11_MESSAGE_ID_PSSETCONSTANTBUFFERS_INVALIDBUFFER,

			///<summary>DevicePSSetConstantBuffersBuffersEmpty message.</summary>
			DevicePSSetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_PSSETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DevicePSSetSamplersSamplersEmpty message.</summary>
			DevicePSSetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_PSSETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceRSSetViewportsInvalidViewport message.</summary>
			DeviceRSSetViewportsInvalidViewport = D3D11_MESSAGE_ID_DEVICE_RSSETVIEWPORTS_INVALIDVIEWPORT,

			///<summary>DeviceRSSetScissorRectanglesInvalidScissor message.</summary>
			DeviceRSSetScissorRectanglesInvalidScissor = D3D11_MESSAGE_ID_DEVICE_RSSETSCISSORRECTS_INVALIDSCISSOR,

			///<summary>ClearRenderTargetViewDenormalizeFlush message.</summary>
			ClearRenderTargetViewDenormalizeFlush = D3D11_MESSAGE_ID_CLEARRENDERTARGETVIEW_DENORMFLUSH,

			///<summary>ClearDepthStencilViewDenormalizeFlush message.</summary>
			ClearDepthStencilViewDenormalizeFlush = D3D11_MESSAGE_ID_CLEARDEPTHSTENCILVIEW_DENORMFLUSH,

			///<summary>ClearDepthStencilViewInvalid message.</summary>
			ClearDepthStencilViewInvalid = D3D11_MESSAGE_ID_CLEARDEPTHSTENCILVIEW_INVALID,

			///<summary>DeviceIAGetVertexBuffersBuffersEmpty message.</summary>
			DeviceIAGetVertexBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_IAGETVERTEXBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceVSGetShaderResourcesViewsEmpty message.</summary>
			DeviceVSGetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_VSGETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>DeviceVSGetConstantBuffersBuffersEmpty message.</summary>
			DeviceVSGetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_VSGETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceVSGetSamplersSamplersEmpty message.</summary>
			DeviceVSGetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_VSGETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceGSGetShaderResourcesViewsEmpty message.</summary>
			DeviceGSGetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_GSGETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>DeviceGSGetConstantBuffersBuffersEmpty message.</summary>
			DeviceGSGetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_GSGETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceGSGetSamplersSamplersEmpty message.</summary>
			DeviceGSGetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_GSGETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceSOGetTargetsBuffersEmpty message.</summary>
			DeviceSOGetTargetsBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_SOGETTARGETS_BUFFERS_EMPTY,

			///<summary>DevicePSGetShaderResourcesViewsEmpty message.</summary>
			DevicePSGetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_PSGETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>DevicePSGetConstantBuffersBuffersEmpty message.</summary>
			DevicePSGetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_PSGETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DevicePSGetSamplersSamplersEmpty message.</summary>
			DevicePSGetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_PSGETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceRSGetViewportsViewportsEmpty message.</summary>
			DeviceRSGetViewportsViewportsEmpty = D3D11_MESSAGE_ID_DEVICE_RSGETVIEWPORTS_VIEWPORTS_EMPTY,

			///<summary>DeviceRSGetScissorRectanglesRectsEmpty message.</summary>
			DeviceRSGetScissorRectanglesRectsEmpty = D3D11_MESSAGE_ID_DEVICE_RSGETSCISSORRECTS_RECTS_EMPTY,

			///<summary>DeviceGenerateMipsResourceInvalid message.</summary>
			DeviceGenerateMipsResourceInvalid = D3D11_MESSAGE_ID_DEVICE_GENERATEMIPS_RESOURCE_INVALID,

			///<summary>CopySubresourceRegionInvalidDestinationSubresource message.</summary>
			CopySubresourceRegionInvalidDestinationSubresource = D3D11_MESSAGE_ID_COPYSUBRESOURCEREGION_INVALIDDESTINATIONSUBRESOURCE,

			///<summary>CopySubresourceRegionInvalidSourceSubresource message.</summary>
			CopySubresourceRegionInvalidSourceSubresource = D3D11_MESSAGE_ID_COPYSUBRESOURCEREGION_INVALIDSOURCESUBRESOURCE,

			///<summary>CopySubresourceRegionInvalidSourceBox message.</summary>
			CopySubresourceRegionInvalidSourceBox = D3D11_MESSAGE_ID_COPYSUBRESOURCEREGION_INVALIDSOURCEBOX,

			///<summary>CopySubresourceRegionInvalidSource message.</summary>
			CopySubresourceRegionInvalidSource = D3D11_MESSAGE_ID_COPYSUBRESOURCEREGION_INVALIDSOURCE,

			///<summary>CopySubresourceRegionInvalidDestinationState message.</summary>
			CopySubresourceRegionInvalidDestinationState = D3D11_MESSAGE_ID_COPYSUBRESOURCEREGION_INVALIDDESTINATIONSTATE,

			///<summary>CopySubresourceRegionInvalidSourceState message.</summary>
			CopySubresourceRegionInvalidSourceState = D3D11_MESSAGE_ID_COPYSUBRESOURCEREGION_INVALIDSOURCESTATE,

			///<summary>CopyResourceInvalidSource message.</summary>
			CopyResourceInvalidSource = D3D11_MESSAGE_ID_COPYRESOURCE_INVALIDSOURCE,

			///<summary>CopyResourceInvalidDestinationState message.</summary>
			CopyResourceInvalidDestinationState = D3D11_MESSAGE_ID_COPYRESOURCE_INVALIDDESTINATIONSTATE,

			///<summary>CopyResourceInvalidSourceState message.</summary>
			CopyResourceInvalidSourceState = D3D11_MESSAGE_ID_COPYRESOURCE_INVALIDSOURCESTATE,

			///<summary>UpdateSubresourceInvalidDestinationSubresource message.</summary>
			UpdateSubresourceInvalidDestinationSubresource = D3D11_MESSAGE_ID_UPDATESUBRESOURCE_INVALIDDESTINATIONSUBRESOURCE,

			///<summary>UpdateSubresourceInvalidDestinationBox message.</summary>
			UpdateSubresourceInvalidDestinationBox = D3D11_MESSAGE_ID_UPDATESUBRESOURCE_INVALIDDESTINATIONBOX,

			///<summary>UpdateSubresourceInvalidDestinationState message.</summary>
			UpdateSubresourceInvalidDestinationState = D3D11_MESSAGE_ID_UPDATESUBRESOURCE_INVALIDDESTINATIONSTATE,

			///<summary>DeviceResolveSubresourceDestinationInvalid message.</summary>
			DeviceResolveSubresourceDestinationInvalid = D3D11_MESSAGE_ID_DEVICE_RESOLVESUBRESOURCE_DESTINATION_INVALID,

			///<summary>DeviceResolveSubresourceDestinationSubresourceInvalid message.</summary>
			DeviceResolveSubresourceDestinationSubresourceInvalid = D3D11_MESSAGE_ID_DEVICE_RESOLVESUBRESOURCE_DESTINATION_SUBRESOURCE_INVALID,

			///<summary>DeviceResolveSubresourceSourceInvalid message.</summary>
			DeviceResolveSubresourceSourceInvalid = D3D11_MESSAGE_ID_DEVICE_RESOLVESUBRESOURCE_SOURCE_INVALID,

			///<summary>DeviceResolveSubresourceSourceSubresourceInvalid message.</summary>
			DeviceResolveSubresourceSourceSubresourceInvalid = D3D11_MESSAGE_ID_DEVICE_RESOLVESUBRESOURCE_SOURCE_SUBRESOURCE_INVALID,

			///<summary>DeviceResolveSubresourceFormatInvalid message.</summary>
			DeviceResolveSubresourceFormatInvalid = D3D11_MESSAGE_ID_DEVICE_RESOLVESUBRESOURCE_FORMAT_INVALID,

			///<summary>BufferMapInvalidMapType message.</summary>
			BufferMapInvalidMapType = D3D11_MESSAGE_ID_BUFFER_MAP_INVALIDMAPTYPE,

			///<summary>BufferMapInvalidFlags message.</summary>
			BufferMapInvalidFlags = D3D11_MESSAGE_ID_BUFFER_MAP_INVALIDFLAGS,

			///<summary>BufferMapAlreadyMapped message.</summary>
			BufferMapAlreadyMapped = D3D11_MESSAGE_ID_BUFFER_MAP_ALREADYMAPPED,

			///<summary>BufferMapDeviceRemovedReturn message.</summary>
			BufferMapDeviceRemovedReturn = D3D11_MESSAGE_ID_BUFFER_MAP_DEVICEREMOVED_RETURN,

			///<summary>BufferUnmapNotMapped message.</summary>
			BufferUnmapNotMapped = D3D11_MESSAGE_ID_BUFFER_UNMAP_NOTMAPPED,

			///<summary>Texture1DMapInvalidMapType message.</summary>
			Texture1DMapInvalidMapType = D3D11_MESSAGE_ID_TEXTURE1D_MAP_INVALIDMAPTYPE,

			///<summary>Texture1DMapInvalidSubresource message.</summary>
			Texture1DMapInvalidSubresource = D3D11_MESSAGE_ID_TEXTURE1D_MAP_INVALIDSUBRESOURCE,

			///<summary>Texture1DMapInvalidFlags message.</summary>
			Texture1DMapInvalidFlags = D3D11_MESSAGE_ID_TEXTURE1D_MAP_INVALIDFLAGS,

			///<summary>Texture1DMapAlreadyMapped message.</summary>
			Texture1DMapAlreadyMapped = D3D11_MESSAGE_ID_TEXTURE1D_MAP_ALREADYMAPPED,

			///<summary>Texture1DMapDeviceRemovedReturn message.</summary>
			Texture1DMapDeviceRemovedReturn = D3D11_MESSAGE_ID_TEXTURE1D_MAP_DEVICEREMOVED_RETURN,

			///<summary>Texture1DUnmapInvalidSubresource message.</summary>
			Texture1DUnmapInvalidSubresource = D3D11_MESSAGE_ID_TEXTURE1D_UNMAP_INVALIDSUBRESOURCE,

			///<summary>Texture1DUnmapNotMapped message.</summary>
			Texture1DUnmapNotMapped = D3D11_MESSAGE_ID_TEXTURE1D_UNMAP_NOTMAPPED,

			///<summary>Texture2DMapInvalidMapType message.</summary>
			Texture2DMapInvalidMapType = D3D11_MESSAGE_ID_TEXTURE2D_MAP_INVALIDMAPTYPE,

			///<summary>Texture2DMapInvalidSubresource message.</summary>
			Texture2DMapInvalidSubresource = D3D11_MESSAGE_ID_TEXTURE2D_MAP_INVALIDSUBRESOURCE,

			///<summary>Texture2DMapInvalidFlags message.</summary>
			Texture2DMapInvalidFlags = D3D11_MESSAGE_ID_TEXTURE2D_MAP_INVALIDFLAGS,

			///<summary>Texture2DMapAlreadyMapped message.</summary>
			Texture2DMapAlreadyMapped = D3D11_MESSAGE_ID_TEXTURE2D_MAP_ALREADYMAPPED,

			///<summary>Texture2DMapDeviceRemovedReturn message.</summary>
			Texture2DMapDeviceRemovedReturn = D3D11_MESSAGE_ID_TEXTURE2D_MAP_DEVICEREMOVED_RETURN,

			///<summary>Texture2DUnmapInvalidSubresource message.</summary>
			Texture2DUnmapInvalidSubresource = D3D11_MESSAGE_ID_TEXTURE2D_UNMAP_INVALIDSUBRESOURCE,

			///<summary>Texture2DUnmapNotMapped message.</summary>
			Texture2DUnmapNotMapped = D3D11_MESSAGE_ID_TEXTURE2D_UNMAP_NOTMAPPED,

			///<summary>Texture3DMapInvalidMapType message.</summary>
			Texture3DMapInvalidMapType = D3D11_MESSAGE_ID_TEXTURE3D_MAP_INVALIDMAPTYPE,

			///<summary>Texture3DMapInvalidSubresource message.</summary>
			Texture3DMapInvalidSubresource = D3D11_MESSAGE_ID_TEXTURE3D_MAP_INVALIDSUBRESOURCE,

			///<summary>Texture3DMapInvalidFlags message.</summary>
			Texture3DMapInvalidFlags = D3D11_MESSAGE_ID_TEXTURE3D_MAP_INVALIDFLAGS,

			///<summary>Texture3DMapAlreadyMapped message.</summary>
			Texture3DMapAlreadyMapped = D3D11_MESSAGE_ID_TEXTURE3D_MAP_ALREADYMAPPED,

			///<summary>Texture3DMapDeviceRemovedReturn message.</summary>
			Texture3DMapDeviceRemovedReturn = D3D11_MESSAGE_ID_TEXTURE3D_MAP_DEVICEREMOVED_RETURN,

			///<summary>Texture3DUnmapInvalidSubresource message.</summary>
			Texture3DUnmapInvalidSubresource = D3D11_MESSAGE_ID_TEXTURE3D_UNMAP_INVALIDSUBRESOURCE,

			///<summary>Texture3DUnmapNotMapped message.</summary>
			Texture3DUnmapNotMapped = D3D11_MESSAGE_ID_TEXTURE3D_UNMAP_NOTMAPPED,

			///<summary>CheckFormatSupportFormatDeprecated message.</summary>
			CheckFormatSupportFormatDeprecated = D3D11_MESSAGE_ID_CHECKFORMATSUPPORT_FORMAT_DEPRECATED,

			///<summary>CheckMultisampleQualityLevelsFormatDeprecated message.</summary>
			CheckMultisampleQualityLevelsFormatDeprecated = D3D11_MESSAGE_ID_CHECKMULTISAMPLEQUALITYLEVELS_FORMAT_DEPRECATED,

			///<summary>SetExceptionModeUnrecognizedFlags message.</summary>
			SetExceptionModeUnrecognizedFlags = D3D11_MESSAGE_ID_SETEXCEPTIONMODE_UNRECOGNIZEDFLAGS,

			///<summary>SetExceptionModeInvalidArgumentReturn message.</summary>
			SetExceptionModeInvalidArgumentReturn = D3D11_MESSAGE_ID_SETEXCEPTIONMODE_INVALIDARG_RETURN,

			///<summary>SetExceptionModeDeviceRemovedReturn message.</summary>
			SetExceptionModeDeviceRemovedReturn = D3D11_MESSAGE_ID_SETEXCEPTIONMODE_DEVICEREMOVED_RETURN,

			///<summary>RefSimulatingInfinitelyFastHardware message.</summary>
			RefSimulatingInfinitelyFastHardware = D3D11_MESSAGE_ID_REF_SIMULATING_INFINITELY_FAST_HARDWARE,

			///<summary>RefThreadingMode message.</summary>
			RefThreadingMode = D3D11_MESSAGE_ID_REF_THREADING_MODE,

			///<summary>RefUmDriverException message.</summary>
			RefUmDriverException = D3D11_MESSAGE_ID_REF_UMDRIVER_EXCEPTION,

			///<summary>RefKmDriverException message.</summary>
			RefKmDriverException = D3D11_MESSAGE_ID_REF_KMDRIVER_EXCEPTION,

			///<summary>RefHardwareException message.</summary>
			RefHardwareException = D3D11_MESSAGE_ID_REF_HARDWARE_EXCEPTION,

			///<summary>RefAccessingIndexableTempOutOfRange message.</summary>
			RefAccessingIndexableTempOutOfRange = D3D11_MESSAGE_ID_REF_ACCESSING_INDEXABLE_TEMP_OUT_OF_RANGE,

			///<summary>RefProblemParsingShader message.</summary>
			RefProblemParsingShader = D3D11_MESSAGE_ID_REF_PROBLEM_PARSING_SHADER,

			///<summary>RefOutOfMemory message.</summary>
			RefOutOfMemory = D3D11_MESSAGE_ID_REF_OUT_OF_MEMORY,

			///<summary>RefInfo message.</summary>
			RefInfo = D3D11_MESSAGE_ID_REF_INFO,

			///<summary>DeviceDrawVertexPositionOverflow message.</summary>
			DeviceDrawVertexPositionOverflow = D3D11_MESSAGE_ID_DEVICE_DRAW_VERTEXPOS_OVERFLOW,

			///<summary>DeviceDrawIndexedIndexPositionOverflow message.</summary>
			DeviceDrawIndexedIndexPositionOverflow = D3D11_MESSAGE_ID_DEVICE_DRAWINDEXED_INDEXPOS_OVERFLOW,

			///<summary>DeviceDrawInstancedVertexPositionOverflow message.</summary>
			DeviceDrawInstancedVertexPositionOverflow = D3D11_MESSAGE_ID_DEVICE_DRAWINSTANCED_VERTEXPOS_OVERFLOW,

			///<summary>DeviceDrawInstancedInstancePositionOverflow message.</summary>
			DeviceDrawInstancedInstancePositionOverflow = D3D11_MESSAGE_ID_DEVICE_DRAWINSTANCED_INSTANCEPOS_OVERFLOW,

			///<summary>DeviceDrawIndexedInstancedInstancePositionOverflow message.</summary>
			DeviceDrawIndexedInstancedInstancePositionOverflow = D3D11_MESSAGE_ID_DEVICE_DRAWINDEXEDINSTANCED_INSTANCEPOS_OVERFLOW,

			///<summary>DeviceDrawIndexedInstancedIndexPositionOverflow message.</summary>
			DeviceDrawIndexedInstancedIndexPositionOverflow = D3D11_MESSAGE_ID_DEVICE_DRAWINDEXEDINSTANCED_INDEXPOS_OVERFLOW,

			///<summary>DeviceDrawVertexShaderNotSet message.</summary>
			DeviceDrawVertexShaderNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_VERTEX_SHADER_NOT_SET,

			///<summary>DeviceShaderLinkageSemanticNameNotFound message.</summary>
			DeviceShaderLinkageSemanticNameNotFound = D3D11_MESSAGE_ID_DEVICE_SHADER_LINKAGE_SEMANTICNAME_NOT_FOUND,

			///<summary>DeviceShaderLinkageRegisterIndex message.</summary>
			DeviceShaderLinkageRegisterIndex = D3D11_MESSAGE_ID_DEVICE_SHADER_LINKAGE_REGISTERINDEX,

			///<summary>DeviceShaderLinkageComponentType message.</summary>
			DeviceShaderLinkageComponentType = D3D11_MESSAGE_ID_DEVICE_SHADER_LINKAGE_COMPONENTTYPE,

			///<summary>DeviceShaderLinkageRegisterMask message.</summary>
			DeviceShaderLinkageRegisterMask = D3D11_MESSAGE_ID_DEVICE_SHADER_LINKAGE_REGISTERMASK,

			///<summary>DeviceShaderLinkageSystemValue message.</summary>
			DeviceShaderLinkageSystemValue = D3D11_MESSAGE_ID_DEVICE_SHADER_LINKAGE_SYSTEMVALUE,

			///<summary>DeviceShaderLinkageNeverWrittenAlwaysReads message.</summary>
			DeviceShaderLinkageNeverWrittenAlwaysReads = D3D11_MESSAGE_ID_DEVICE_SHADER_LINKAGE_NEVERWRITTEN_ALWAYSREADS,

			///<summary>DeviceDrawVertexBufferNotSet message.</summary>
			DeviceDrawVertexBufferNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_VERTEX_BUFFER_NOT_SET,

			///<summary>DeviceDrawInputLayoutNotSet message.</summary>
			DeviceDrawInputLayoutNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_INPUTLAYOUT_NOT_SET,

			///<summary>DeviceDrawConstantBufferNotSet message.</summary>
			DeviceDrawConstantBufferNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_CONSTANT_BUFFER_NOT_SET,

			///<summary>DeviceDrawConstantBufferTooSmall message.</summary>
			DeviceDrawConstantBufferTooSmall = D3D11_MESSAGE_ID_DEVICE_DRAW_CONSTANT_BUFFER_TOO_SMALL,

			///<summary>DeviceDrawSamplerNotSet message.</summary>
			DeviceDrawSamplerNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_SAMPLER_NOT_SET,

			///<summary>DeviceDrawShaderResourceViewNotSet message.</summary>
			DeviceDrawShaderResourceViewNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_SHADERRESOURCEVIEW_NOT_SET,

			///<summary>DeviceDrawViewDimensionMismatch message.</summary>
			DeviceDrawViewDimensionMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_VIEW_DIMENSION_MISMATCH,

			///<summary>DeviceDrawVertexBufferStrideTooSmall message.</summary>
			DeviceDrawVertexBufferStrideTooSmall = D3D11_MESSAGE_ID_DEVICE_DRAW_VERTEX_BUFFER_STRIDE_TOO_SMALL,

			///<summary>DeviceDrawVertexBufferTooSmall message.</summary>
			DeviceDrawVertexBufferTooSmall = D3D11_MESSAGE_ID_DEVICE_DRAW_VERTEX_BUFFER_TOO_SMALL,

			///<summary>DeviceDrawIndexBufferNotSet message.</summary>
			DeviceDrawIndexBufferNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_INDEX_BUFFER_NOT_SET,

			///<summary>DeviceDrawIndexBufferFormatInvalid message.</summary>
			DeviceDrawIndexBufferFormatInvalid = D3D11_MESSAGE_ID_DEVICE_DRAW_INDEX_BUFFER_FORMAT_INVALID,

			///<summary>DeviceDrawIndexBufferTooSmall message.</summary>
			DeviceDrawIndexBufferTooSmall = D3D11_MESSAGE_ID_DEVICE_DRAW_INDEX_BUFFER_TOO_SMALL,

			///<summary>DeviceDrawGSInputPrimitiveMismatch message.</summary>
			DeviceDrawGSInputPrimitiveMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_GS_INPUT_PRIMITIVE_MISMATCH,

			///<summary>DeviceDrawResourceReturnTypeMismatch message.</summary>
			DeviceDrawResourceReturnTypeMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_RESOURCE_RETURN_TYPE_MISMATCH,

			///<summary>DeviceDrawPositionNotPresent message.</summary>
			DeviceDrawPositionNotPresent = D3D11_MESSAGE_ID_DEVICE_DRAW_POSITION_NOT_PRESENT,

			///<summary>DeviceDrawOutputStreamNotSet message.</summary>
			DeviceDrawOutputStreamNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_OUTPUT_STREAM_NOT_SET,

			///<summary>DeviceDrawBoundResourceMapped message.</summary>
			DeviceDrawBoundResourceMapped = D3D11_MESSAGE_ID_DEVICE_DRAW_BOUND_RESOURCE_MAPPED,

			///<summary>DeviceDrawInvalidPrimitiveTopology message.</summary>
			DeviceDrawInvalidPrimitiveTopology = D3D11_MESSAGE_ID_DEVICE_DRAW_INVALID_PRIMITIVETOPOLOGY,

			///<summary>DeviceDrawVertexOffsetUnaligned message.</summary>
			DeviceDrawVertexOffsetUnaligned = D3D11_MESSAGE_ID_DEVICE_DRAW_VERTEX_OFFSET_UNALIGNED,

			///<summary>DeviceDrawVertexStrideUnaligned message.</summary>
			DeviceDrawVertexStrideUnaligned = D3D11_MESSAGE_ID_DEVICE_DRAW_VERTEX_STRIDE_UNALIGNED,

			///<summary>DeviceDrawIndexOffsetUnaligned message.</summary>
			DeviceDrawIndexOffsetUnaligned = D3D11_MESSAGE_ID_DEVICE_DRAW_INDEX_OFFSET_UNALIGNED,

			///<summary>DeviceDrawOutputStreamOffsetUnaligned message.</summary>
			DeviceDrawOutputStreamOffsetUnaligned = D3D11_MESSAGE_ID_DEVICE_DRAW_OUTPUT_STREAM_OFFSET_UNALIGNED,

			///<summary>DeviceDrawResourceFormatLdUnsupported message.</summary>
			DeviceDrawResourceFormatLdUnsupported = D3D11_MESSAGE_ID_DEVICE_DRAW_RESOURCE_FORMAT_LD_UNSUPPORTED,

			///<summary>DeviceDrawResourceFormatSampleUnsupported message.</summary>
			DeviceDrawResourceFormatSampleUnsupported = D3D11_MESSAGE_ID_DEVICE_DRAW_RESOURCE_FORMAT_SAMPLE_UNSUPPORTED,

			///<summary>DeviceDrawResourceFormatSampleCUnsupported message.</summary>
			DeviceDrawResourceFormatSampleCUnsupported = D3D11_MESSAGE_ID_DEVICE_DRAW_RESOURCE_FORMAT_SAMPLE_C_UNSUPPORTED,

			///<summary>DeviceDrawResourceMultisampleUnsupported message.</summary>
			DeviceDrawResourceMultisampleUnsupported = D3D11_MESSAGE_ID_DEVICE_DRAW_RESOURCE_MULTISAMPLE_UNSUPPORTED,

			///<summary>DeviceDrawSOTargetsBoundWithoutSource message.</summary>
			DeviceDrawSOTargetsBoundWithoutSource = D3D11_MESSAGE_ID_DEVICE_DRAW_SO_TARGETS_BOUND_WITHOUT_SOURCE,

			///<summary>DeviceDrawSOStrideLargerThanBuffer message.</summary>
			DeviceDrawSOStrideLargerThanBuffer = D3D11_MESSAGE_ID_DEVICE_DRAW_SO_STRIDE_LARGER_THAN_BUFFER,

			///<summary>DeviceDrawOMRenderTargetDoesNotSupportBlending message.</summary>
			DeviceDrawOMRenderTargetDoesNotSupportBlending = D3D11_MESSAGE_ID_DEVICE_DRAW_OM_RENDER_TARGET_DOES_NOT_SUPPORT_BLENDING,

			///<summary>DeviceDrawOMDualSourceBlendingCanOnlyHaveRenderTarget0 message.</summary>
			DeviceDrawOMDualSourceBlendingCanOnlyHaveRenderTarget0 = D3D11_MESSAGE_ID_DEVICE_DRAW_OM_DUAL_SOURCE_BLENDING_CAN_ONLY_HAVE_RENDER_TARGET_0,

			///<summary>DeviceRemovalProcessAtFault message.</summary>
			DeviceRemovalProcessAtFault = D3D11_MESSAGE_ID_DEVICE_REMOVAL_PROCESS_AT_FAULT,

			///<summary>DeviceRemovalProcessPossiblyAtFault message.</summary>
			DeviceRemovalProcessPossiblyAtFault = D3D11_MESSAGE_ID_DEVICE_REMOVAL_PROCESS_POSSIBLY_AT_FAULT,

			///<summary>DeviceRemovalProcessNotAtFault message.</summary>
			DeviceRemovalProcessNotAtFault = D3D11_MESSAGE_ID_DEVICE_REMOVAL_PROCESS_NOT_AT_FAULT,

			///<summary>DeviceOpenSharedResourceInvalidArgumentReturn message.</summary>
			DeviceOpenSharedResourceInvalidArgumentReturn = D3D11_MESSAGE_ID_DEVICE_OPEN_SHARED_RESOURCE_INVALIDARG_RETURN,

			///<summary>DeviceOpenSharedResourceOutOfMemoryReturn message.</summary>
			DeviceOpenSharedResourceOutOfMemoryReturn = D3D11_MESSAGE_ID_DEVICE_OPEN_SHARED_RESOURCE_OUTOFMEMORY_RETURN,

			///<summary>DeviceOpenSharedResourceBadInterfaceReturn message.</summary>
			DeviceOpenSharedResourceBadInterfaceReturn = D3D11_MESSAGE_ID_DEVICE_OPEN_SHARED_RESOURCE_BADINTERFACE_RETURN,

			///<summary>DeviceDrawViewportNotSet message.</summary>
			DeviceDrawViewportNotSet = D3D11_MESSAGE_ID_DEVICE_DRAW_VIEWPORT_NOT_SET,

			///<summary>CreateInputLayoutTrailingDigitInSemantic message.</summary>
			CreateInputLayoutTrailingDigitInSemantic = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_TRAILING_DIGIT_IN_SEMANTIC,

			///<summary>CreateGeometryShaderWithStreamOutputTrailingDigitInSemantic message.</summary>
			CreateGeometryShaderWithStreamOutputTrailingDigitInSemantic = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_TRAILING_DIGIT_IN_SEMANTIC,

			///<summary>DeviceRSSetViewportsDenormalizeFlush message.</summary>
			DeviceRSSetViewportsDenormalizeFlush = D3D11_MESSAGE_ID_DEVICE_RSSETVIEWPORTS_DENORMFLUSH,

			///<summary>OMSetRenderTargetsInvalidView message.</summary>
			OMSetRenderTargetsInvalidView = D3D11_MESSAGE_ID_OMSETRENDERTARGETS_INVALIDVIEW,

			///<summary>DeviceSetTextFilterSizeInvalidDimensions message.</summary>
			DeviceSetTextFilterSizeInvalidDimensions = D3D11_MESSAGE_ID_DEVICE_SETTEXTFILTERSIZE_INVALIDDIMENSIONS,

			///<summary>DeviceDrawSamplerMismatch message.</summary>
			DeviceDrawSamplerMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_SAMPLER_MISMATCH,

			///<summary>CreateInputLayoutTypeMismatch message.</summary>
			CreateInputLayoutTypeMismatch = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_TYPE_MISMATCH,

			///<summary>BlendStateGetDescriptionLegacy message.</summary>
			BlendStateGetDescriptionLegacy = D3D11_MESSAGE_ID_BLENDSTATE_GETDESC_LEGACY,

			///<summary>ShaderResourceViewGetDescriptionLegacy message.</summary>
			ShaderResourceViewGetDescriptionLegacy = D3D11_MESSAGE_ID_SHADERRESOURCEVIEW_GETDESC_LEGACY,

			///<summary>CreateQueryOutOfMemoryReturn message.</summary>
			CreateQueryOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATEQUERY_OUTOFMEMORY_RETURN,

			///<summary>CreatePredicateOutOfMemoryReturn message.</summary>
			CreatePredicateOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATEPREDICATE_OUTOFMEMORY_RETURN,

			///<summary>CreateCounterOutOfRangeCounter message.</summary>
			CreateCounterOutOfRangeCounter = D3D11_MESSAGE_ID_CREATECOUNTER_OUTOFRANGE_COUNTER,

			///<summary>CreateCounterSimultaneousActiveCountersExhausted message.</summary>
			CreateCounterSimultaneousActiveCountersExhausted = D3D11_MESSAGE_ID_CREATECOUNTER_SIMULTANEOUS_ACTIVE_COUNTERS_EXHAUSTED,

			///<summary>CreateCounterUnsupportedWellKnownCounter message.</summary>
			CreateCounterUnsupportedWellKnownCounter = D3D11_MESSAGE_ID_CREATECOUNTER_UNSUPPORTED_WELLKNOWN_COUNTER,

			///<summary>CreateCounterOutOfMemoryReturn message.</summary>
			CreateCounterOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATECOUNTER_OUTOFMEMORY_RETURN,

			///<summary>CreateCounterNonexclusiveReturn message.</summary>
			CreateCounterNonexclusiveReturn = D3D11_MESSAGE_ID_CREATECOUNTER_NONEXCLUSIVE_RETURN,

			///<summary>CreateCounterNullDescription message.</summary>
			CreateCounterNullDescription = D3D11_MESSAGE_ID_CREATECOUNTER_NULLDESC,

			///<summary>CheckCounterOutOfRangeCounter message.</summary>
			CheckCounterOutOfRangeCounter = D3D11_MESSAGE_ID_CHECKCOUNTER_OUTOFRANGE_COUNTER,

			///<summary>CheckCounterUnsupportedWellKnownCounter message.</summary>
			CheckCounterUnsupportedWellKnownCounter = D3D11_MESSAGE_ID_CHECKCOUNTER_UNSUPPORTED_WELLKNOWN_COUNTER,

			///<summary>SetPredicationInvalidPredicateState message.</summary>
			SetPredicationInvalidPredicateState = D3D11_MESSAGE_ID_SETPREDICATION_INVALID_PREDICATE_STATE,

			///<summary>QueryBeginUnsupported message.</summary>
			QueryBeginUnsupported = D3D11_MESSAGE_ID_QUERY_BEGIN_UNSUPPORTED,

			///<summary>PredicateBeginDuringPredication message.</summary>
			PredicateBeginDuringPredication = D3D11_MESSAGE_ID_PREDICATE_BEGIN_DURING_PREDICATION,

			///<summary>QueryBeginDuplicate message.</summary>
			QueryBeginDuplicate = D3D11_MESSAGE_ID_QUERY_BEGIN_DUPLICATE,

			///<summary>QueryBeginAbandoningPreviousResults message.</summary>
			QueryBeginAbandoningPreviousResults = D3D11_MESSAGE_ID_QUERY_BEGIN_ABANDONING_PREVIOUS_RESULTS,

			///<summary>PredicateEndDuringPredication message.</summary>
			PredicateEndDuringPredication = D3D11_MESSAGE_ID_PREDICATE_END_DURING_PREDICATION,

			///<summary>QueryEndAbandoningPreviousResults message.</summary>
			QueryEndAbandoningPreviousResults = D3D11_MESSAGE_ID_QUERY_END_ABANDONING_PREVIOUS_RESULTS,

			///<summary>QueryEndWithoutBegin message.</summary>
			QueryEndWithoutBegin = D3D11_MESSAGE_ID_QUERY_END_WITHOUT_BEGIN,

			///<summary>QueryGetDataInvalidDataSize message.</summary>
			QueryGetDataInvalidDataSize = D3D11_MESSAGE_ID_QUERY_GETDATA_INVALID_DATASIZE,

			///<summary>QueryGetDataInvalidFlags message.</summary>
			QueryGetDataInvalidFlags = D3D11_MESSAGE_ID_QUERY_GETDATA_INVALID_FLAGS,

			///<summary>QueryGetDataInvalidCall message.</summary>
			QueryGetDataInvalidCall = D3D11_MESSAGE_ID_QUERY_GETDATA_INVALID_CALL,

			///<summary>DeviceDrawPSOutputTypeMismatch message.</summary>
			DeviceDrawPSOutputTypeMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_PS_OUTPUT_TYPE_MISMATCH,

			///<summary>DeviceDrawResourceFormatGatherUnsupported message.</summary>
			DeviceDrawResourceFormatGatherUnsupported = D3D11_MESSAGE_ID_DEVICE_DRAW_RESOURCE_FORMAT_GATHER_UNSUPPORTED,

			///<summary>DeviceDrawInvalidUseOfCenterMultisamplePattern message.</summary>
			DeviceDrawInvalidUseOfCenterMultisamplePattern = D3D11_MESSAGE_ID_DEVICE_DRAW_INVALID_USE_OF_CENTER_MULTISAMPLE_PATTERN,

			///<summary>DeviceIASetVertexBuffersStrideTooLarge message.</summary>
			DeviceIASetVertexBuffersStrideTooLarge = D3D11_MESSAGE_ID_DEVICE_IASETVERTEXBUFFERS_STRIDE_TOO_LARGE,

			///<summary>DeviceIASetVertexBuffersInvalidRange message.</summary>
			DeviceIASetVertexBuffersInvalidRange = D3D11_MESSAGE_ID_DEVICE_IASETVERTEXBUFFERS_INVALIDRANGE,

			///<summary>CreateInputLayoutEmptyLayout message.</summary>
			CreateInputLayoutEmptyLayout = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_EMPTY_LAYOUT,

			///<summary>DeviceDrawResourceSampleCountMismatch message.</summary>
			DeviceDrawResourceSampleCountMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_RESOURCE_SAMPLE_COUNT_MISMATCH,

			///<summary>CreateDepthStencilStateStencilNoTwoSided message.</summary>
			CreateDepthStencilStateStencilNoTwoSided = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILSTATE_STENCIL_NO_TWO_SIDED,

			///<summary>CreateRasterizerStateDepthBiasClampNotSupported message.</summary>
			CreateRasterizerStateDepthBiasClampNotSupported = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_DepthBiasClamp_NOT_SUPPORTED,

			///<summary>CreateSamplerStateNoComparisonSupport message.</summary>
			CreateSamplerStateNoComparisonSupport = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_NO_COMPARISON_SUPPORT,

			///<summary>CreateSamplerStateExcessiveAnisotropy message.</summary>
			CreateSamplerStateExcessiveAnisotropy = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_EXCESSIVE_ANISOTROPY,

			///<summary>CreateSamplerStateBorderOutOfRange message.</summary>
			CreateSamplerStateBorderOutOfRange = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_BORDER_OUT_OF_RANGE,

			///<summary>VSSetSamplersNotSupported message.</summary>
			VSSetSamplersNotSupported = D3D11_MESSAGE_ID_VSSETSAMPLERS_NOT_SUPPORTED,

			///<summary>VSSetSamplersTooManySamplers message.</summary>
			VSSetSamplersTooManySamplers = D3D11_MESSAGE_ID_VSSETSAMPLERS_TOO_MANY_SAMPLERS,

			///<summary>PSSetSamplersTooManySamplers message.</summary>
			PSSetSamplersTooManySamplers = D3D11_MESSAGE_ID_PSSETSAMPLERS_TOO_MANY_SAMPLERS,

			///<summary>CreateResourceNoArrays message.</summary>
			CreateResourceNoArrays = D3D11_MESSAGE_ID_CREATERESOURCE_NO_ARRAYS,

			///<summary>CreateResourceNoVBAndIBBind message.</summary>
			CreateResourceNoVBAndIBBind = D3D11_MESSAGE_ID_CREATERESOURCE_NO_VB_AND_IB_BIND,

			///<summary>CreateResourceNoTexture1D message.</summary>
			CreateResourceNoTexture1D = D3D11_MESSAGE_ID_CREATERESOURCE_NO_TEXTURE_1D,

			///<summary>CreateResourceDimensionOutOfRange message.</summary>
			CreateResourceDimensionOutOfRange = D3D11_MESSAGE_ID_CREATERESOURCE_DIMENSION_OUT_OF_RANGE,

			///<summary>CreateResourceNotBindableAsShaderResource message.</summary>
			CreateResourceNotBindableAsShaderResource = D3D11_MESSAGE_ID_CREATERESOURCE_NOT_BINDABLE_AS_SHADER_RESOURCE,

			///<summary>OMSetRenderTargetsTooManyRenderTargets message.</summary>
			OMSetRenderTargetsTooManyRenderTargets = D3D11_MESSAGE_ID_OMSETRENDERTARGETS_TOO_MANY_RENDER_TARGETS,

			///<summary>OMSetRenderTargetsNoDifferingBitDepths message.</summary>
			OMSetRenderTargetsNoDifferingBitDepths = D3D11_MESSAGE_ID_OMSETRENDERTARGETS_NO_DIFFERING_BIT_DEPTHS,

			///<summary>IASetVertexBuffersBadBufferIndex message.</summary>
			IASetVertexBuffersBadBufferIndex = D3D11_MESSAGE_ID_IASETVERTEXBUFFERS_BAD_BUFFER_INDEX,

			///<summary>DeviceRSSetViewportsTooManyViewports message.</summary>
			DeviceRSSetViewportsTooManyViewports = D3D11_MESSAGE_ID_DEVICE_RSSETVIEWPORTS_TOO_MANY_VIEWPORTS,

			///<summary>DeviceIASetPrimitiveTopologyAdjacencyUnsupported message.</summary>
			DeviceIASetPrimitiveTopologyAdjacencyUnsupported = D3D11_MESSAGE_ID_DEVICE_IASETPRIMITIVETOPOLOGY_ADJACENCY_UNSUPPORTED,

			///<summary>DeviceRSSetScissorRectanglesTooManyScissors message.</summary>
			DeviceRSSetScissorRectanglesTooManyScissors = D3D11_MESSAGE_ID_DEVICE_RSSETSCISSORRECTS_TOO_MANY_SCISSORS,

			///<summary>CopyResourceOnlyTexture2DWithinGpuMemory message.</summary>
			CopyResourceOnlyTexture2DWithinGpuMemory = D3D11_MESSAGE_ID_COPYRESOURCE_ONLY_TEXTURE_2D_WITHIN_GPU_MEMORY,

			///<summary>CopyResourceNoTexture3DReadBack message.</summary>
			CopyResourceNoTexture3DReadBack = D3D11_MESSAGE_ID_COPYRESOURCE_NO_TEXTURE_3D_READBACK,

			///<summary>CopyResourceNoTextureOnlyReadBack message.</summary>
			CopyResourceNoTextureOnlyReadBack = D3D11_MESSAGE_ID_COPYRESOURCE_NO_TEXTURE_ONLY_READBACK,

			///<summary>CreateInputLayoutUnsupportedFormat message.</summary>
			CreateInputLayoutUnsupportedFormat = D3D11_MESSAGE_ID_CREATEINPUTLAYOUT_UNSUPPORTED_FORMAT,

			///<summary>CreateBlendStateNoAlphaToCoverage message.</summary>
			CreateBlendStateNoAlphaToCoverage = D3D11_MESSAGE_ID_CREATEBLENDSTATE_NO_ALPHA_TO_COVERAGE,

			///<summary>CreateRasterizerStateDepthClipEnableMustBeTrue message.</summary>
			CreateRasterizerStateDepthClipEnableMustBeTrue = D3D11_MESSAGE_ID_CREATERASTERIZERSTATE_DepthClipEnable_MUST_BE_TRUE,

			///<summary>DrawIndexedStartIndexLocationMustBePositive message.</summary>
			DrawIndexedStartIndexLocationMustBePositive = D3D11_MESSAGE_ID_DRAWINDEXED_STARTINDEXLOCATION_MUST_BE_POSITIVE,

			///<summary>CreateShaderResourceViewMustUseLowestLod message.</summary>
			CreateShaderResourceViewMustUseLowestLod = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_MUST_USE_LOWEST_LOD,

			///<summary>CreateSamplerStateMinLodMustNotBeFractional message.</summary>
			CreateSamplerStateMinLodMustNotBeFractional = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_MINLOD_MUST_NOT_BE_FRACTIONAL,

			///<summary>CreateSamplerStateMaxLodMustBeFltMax message.</summary>
			CreateSamplerStateMaxLodMustBeFltMax = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_MAXLOD_MUST_BE_FLT_MAX,

			///<summary>CreateShaderResourceViewFirstArraySliceMustBeZero message.</summary>
			CreateShaderResourceViewFirstArraySliceMustBeZero = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_FIRSTARRAYSLICE_MUST_BE_ZERO,

			///<summary>CreateShaderResourceViewCubesMustHave6Sides message.</summary>
			CreateShaderResourceViewCubesMustHave6Sides = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_CUBES_MUST_HAVE_6_SIDES,

			///<summary>CreateResourceNotBindableAsRenderTarget message.</summary>
			CreateResourceNotBindableAsRenderTarget = D3D11_MESSAGE_ID_CREATERESOURCE_NOT_BINDABLE_AS_RENDER_TARGET,

			///<summary>CreateResourceNoDwordIndexBuffer message.</summary>
			CreateResourceNoDwordIndexBuffer = D3D11_MESSAGE_ID_CREATERESOURCE_NO_DWORD_INDEX_BUFFER,

			///<summary>CreateResourceMsaaPrecludesShaderResource message.</summary>
			CreateResourceMsaaPrecludesShaderResource = D3D11_MESSAGE_ID_CREATERESOURCE_MSAA_PRECLUDES_SHADER_RESOURCE,

			///<summary>CreateResourcePresentationPrecludesShaderResource message.</summary>
			CreateResourcePresentationPrecludesShaderResource = D3D11_MESSAGE_ID_CREATERESOURCE_PRESENTATION_PRECLUDES_SHADER_RESOURCE,

			///<summary>CreateBlendStateNoIndependentBlendEnable message.</summary>
			CreateBlendStateNoIndependentBlendEnable = D3D11_MESSAGE_ID_CREATEBLENDSTATE_NO_INDEPENDENT_BLEND_ENABLE,

			///<summary>CreateBlendStateNoIndependentWriteMasks message.</summary>
			CreateBlendStateNoIndependentWriteMasks = D3D11_MESSAGE_ID_CREATEBLENDSTATE_NO_INDEPENDENT_WRITE_MASKS,

			///<summary>CreateResourceNoStreamOut message.</summary>
			CreateResourceNoStreamOut = D3D11_MESSAGE_ID_CREATERESOURCE_NO_STREAM_OUT,

			///<summary>CreateResourceOnlyVbIbForBuffers message.</summary>
			CreateResourceOnlyVbIbForBuffers = D3D11_MESSAGE_ID_CREATERESOURCE_ONLY_VB_IB_FOR_BUFFERS,

			///<summary>CreateResourceNoAutogenForVolumes message.</summary>
			CreateResourceNoAutogenForVolumes = D3D11_MESSAGE_ID_CREATERESOURCE_NO_AUTOGEN_FOR_VOLUMES,

			///<summary>CreateResourceDxgiFormatR8G8B8A8CannotBeShared message.</summary>
			CreateResourceDxgiFormatR8G8B8A8CannotBeShared = D3D11_MESSAGE_ID_CREATERESOURCE_DXGI_FORMAT_R8G8B8A8_CANNOT_BE_SHARED,

			///<summary>VSShaderResourcesNotSupported message.</summary>
			VSShaderResourcesNotSupported = D3D11_MESSAGE_ID_VSSHADERRESOURCES_NOT_SUPPORTED,

			///<summary>GeometryShaderNotSupported message.</summary>
			GeometryShaderNotSupported = D3D11_MESSAGE_ID_GEOMETRY_SHADER_NOT_SUPPORTED,

			///<summary>StreamOutNotSupported message.</summary>
			StreamOutNotSupported = D3D11_MESSAGE_ID_STREAM_OUT_NOT_SUPPORTED,

			///<summary>TextFilterNotSupported message.</summary>
			TextFilterNotSupported = D3D11_MESSAGE_ID_TEXT_FILTER_NOT_SUPPORTED,

			///<summary>CreateBlendStateNoSeparateAlphaBlend message.</summary>
			CreateBlendStateNoSeparateAlphaBlend = D3D11_MESSAGE_ID_CREATEBLENDSTATE_NO_SEPARATE_ALPHA_BLEND,

			///<summary>CreateBlendStateNoMrtBlend message.</summary>
			CreateBlendStateNoMrtBlend = D3D11_MESSAGE_ID_CREATEBLENDSTATE_NO_MRT_BLEND,

			///<summary>CreateBlendStateOperationNotSupported message.</summary>
			CreateBlendStateOperationNotSupported = D3D11_MESSAGE_ID_CREATEBLENDSTATE_OPERATION_NOT_SUPPORTED,

			///<summary>CreateSamplerStateNoMirrorOnce message.</summary>
			CreateSamplerStateNoMirrorOnce = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_NO_MIRRORONCE,

			///<summary>DrawInstancedNotSupported message.</summary>
			DrawInstancedNotSupported = D3D11_MESSAGE_ID_DRAWINSTANCED_NOT_SUPPORTED,

			///<summary>DrawIndexedInstancedNotSupportedBelow93 message.</summary>
			DrawIndexedInstancedNotSupportedBelow93 = D3D11_MESSAGE_ID_DRAWINDEXEDINSTANCED_NOT_SUPPORTED_BELOW_9_3,

			///<summary>DrawIndexedPointListUnsupported message.</summary>
			DrawIndexedPointListUnsupported = D3D11_MESSAGE_ID_DRAWINDEXED_POINTLIST_UNSUPPORTED,

			///<summary>SetBlendStateSampleMaskCannotBeZero message.</summary>
			SetBlendStateSampleMaskCannotBeZero = D3D11_MESSAGE_ID_SETBLENDSTATE_SAMPLE_MASK_CANNOT_BE_ZERO,

			///<summary>CreateResourceDimensionExceedsFeatureLevelDefinition message.</summary>
			CreateResourceDimensionExceedsFeatureLevelDefinition = D3D11_MESSAGE_ID_CREATERESOURCE_DIMENSION_EXCEEDS_FEATURE_LEVEL_DEFINITION,

			///<summary>CreateResourceOnlySingleMipLevelDepthStencilSupported message.</summary>
			CreateResourceOnlySingleMipLevelDepthStencilSupported = D3D11_MESSAGE_ID_CREATERESOURCE_ONLY_SINGLE_MIP_LEVEL_DEPTH_STENCIL_SUPPORTED,

			///<summary>DeviceRSSetScissorRectanglesNegativeScissor message.</summary>
			DeviceRSSetScissorRectanglesNegativeScissor = D3D11_MESSAGE_ID_DEVICE_RSSETSCISSORRECTS_NEGATIVESCISSOR,

			///<summary>SlotZeroMustBeD3D10InputPerVertexData message.</summary>
			SlotZeroMustBeD3D10InputPerVertexData = D3D11_MESSAGE_ID_SLOT_ZERO_MUST_BE_D3D10_INPUT_PER_VERTEX_DATA,

			///<summary>CreateResourceNonPow2MipMap message.</summary>
			CreateResourceNonPow2MipMap = D3D11_MESSAGE_ID_CREATERESOURCE_NON_POW_2_MIPMAP,

			///<summary>CreateSamplerStateBorderNotSupported message.</summary>
			CreateSamplerStateBorderNotSupported = D3D11_MESSAGE_ID_CREATESAMPLERSTATE_BORDER_NOT_SUPPORTED,

			///<summary>OMSetRenderTargetsNoSrgbMrt message.</summary>
			OMSetRenderTargetsNoSrgbMrt = D3D11_MESSAGE_ID_OMSETRENDERTARGETS_NO_SRGB_MRT,

			///<summary>CreateDepthStencilViewInvalidFlags message.</summary>
			CreateDepthStencilViewInvalidFlags = D3D11_MESSAGE_ID_CREATEDEPTHSTENCILVIEW_INVALIDFLAGS,

			///<summary>CreateVertexShaderInvalidClassLinkage message.</summary>
			CreateVertexShaderInvalidClassLinkage = D3D11_MESSAGE_ID_CREATEVERTEXSHADER_INVALIDCLASSLINKAGE,

			///<summary>CreateGeometryShaderInvalidClassLinkage message.</summary>
			CreateGeometryShaderInvalidClassLinkage = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADER_INVALIDCLASSLINKAGE,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidStreamCount message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidStreamCount = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDNUMSTREAMS,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidStreamToRasterizer message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidStreamToRasterizer = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDSTREAMTORASTERIZER,

			///<summary>CreateGeometryShaderWithStreamOutputUnexpectedStreams message.</summary>
			CreateGeometryShaderWithStreamOutputUnexpectedStreams = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_UNEXPECTEDSTREAMS,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidClassLinkage message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidClassLinkage = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDCLASSLINKAGE,

			///<summary>CreatePixelShaderInvalidClassLinkage message.</summary>
			CreatePixelShaderInvalidClassLinkage = D3D11_MESSAGE_ID_CREATEPIXELSHADER_INVALIDCLASSLINKAGE,

			///<summary>CreateDeferredContextInvalidCommandListFlags message.</summary>
			CreateDeferredContextInvalidCommandListFlags = D3D11_MESSAGE_ID_CREATEDEFERREDCONTEXT_INVALID_COMMANDLISTFLAGS,

			///<summary>CreateDeferredContextSingleThreaded message.</summary>
			CreateDeferredContextSingleThreaded = D3D11_MESSAGE_ID_CREATEDEFERREDCONTEXT_SINGLETHREADED,

			///<summary>CreateDeferredContextInvalidArgumentReturn message.</summary>
			CreateDeferredContextInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATEDEFERREDCONTEXT_INVALIDARG_RETURN,

			///<summary>CreateDeferredContextInvalidCallReturn message.</summary>
			CreateDeferredContextInvalidCallReturn = D3D11_MESSAGE_ID_CREATEDEFERREDCONTEXT_INVALID_CALL_RETURN,

			///<summary>CreateDeferredContextOutOfMemoryReturn message.</summary>
			CreateDeferredContextOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATEDEFERREDCONTEXT_OUTOFMEMORY_RETURN,

			///<summary>FinishDisplayListOnImmediateContext message.</summary>
			FinishDisplayListOnImmediateContext = D3D11_MESSAGE_ID_FINISHDISPLAYLIST_ONIMMEDIATECONTEXT,

			///<summary>FinishDisplayListOutOfMemoryReturn message.</summary>
			FinishDisplayListOutOfMemoryReturn = D3D11_MESSAGE_ID_FINISHDISPLAYLIST_OUTOFMEMORY_RETURN,

			///<summary>FinishDisplayListInvalidCallReturn message.</summary>
			FinishDisplayListInvalidCallReturn = D3D11_MESSAGE_ID_FINISHDISPLAYLIST_INVALID_CALL_RETURN,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidStream message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidStream = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDSTREAM,

			///<summary>CreateGeometryShaderWithStreamOutputUnexpectedEntries message.</summary>
			CreateGeometryShaderWithStreamOutputUnexpectedEntries = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_UNEXPECTEDENTRIES,

			///<summary>CreateGeometryShaderWithStreamOutputUnexpectedStrides message.</summary>
			CreateGeometryShaderWithStreamOutputUnexpectedStrides = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_UNEXPECTEDSTRIDES,

			///<summary>CreateGeometryShaderWithStreamOutputInvalidStrideCount message.</summary>
			CreateGeometryShaderWithStreamOutputInvalidStrideCount = D3D11_MESSAGE_ID_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_INVALIDNUMSTRIDES,

			///<summary>DeviceHSSetShaderResourcesHazard message.</summary>
			DeviceHSSetShaderResourcesHazard = D3D11_MESSAGE_ID_DEVICE_HSSETSHADERRESOURCES_HAZARD,

			///<summary>DeviceHSSetConstantBuffersHazard message.</summary>
			DeviceHSSetConstantBuffersHazard = D3D11_MESSAGE_ID_DEVICE_HSSETCONSTANTBUFFERS_HAZARD,

			///<summary>HSSetShaderResourcesUnbindDeletingObject message.</summary>
			HSSetShaderResourcesUnbindDeletingObject = D3D11_MESSAGE_ID_HSSETSHADERRESOURCES_UNBINDDELETINGOBJECT,

			///<summary>HSSetConstantBuffersUnbindDeletingObject message.</summary>
			HSSetConstantBuffersUnbindDeletingObject = D3D11_MESSAGE_ID_HSSETCONSTANTBUFFERS_UNBINDDELETINGOBJECT,

			///<summary>CreateHullShaderInvalidCall message.</summary>
			CreateHullShaderInvalidCall = D3D11_MESSAGE_ID_CREATEHULLSHADER_INVALIDCALL,

			///<summary>CreateHullShaderOutOfMemory message.</summary>
			CreateHullShaderOutOfMemory = D3D11_MESSAGE_ID_CREATEHULLSHADER_OUTOFMEMORY,

			///<summary>CreateHullShaderInvalidShaderBytecode message.</summary>
			CreateHullShaderInvalidShaderBytecode = D3D11_MESSAGE_ID_CREATEHULLSHADER_INVALIDSHADERBYTECODE,

			///<summary>CreateHullShaderInvalidShaderType message.</summary>
			CreateHullShaderInvalidShaderType = D3D11_MESSAGE_ID_CREATEHULLSHADER_INVALIDSHADERTYPE,

			///<summary>CreateHullShaderInvalidClassLinkage message.</summary>
			CreateHullShaderInvalidClassLinkage = D3D11_MESSAGE_ID_CREATEHULLSHADER_INVALIDCLASSLINKAGE,

			///<summary>DeviceHSSetShaderResourcesViewsEmpty message.</summary>
			DeviceHSSetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_HSSETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>HSSetConstantBuffersInvalidBuffer message.</summary>
			HSSetConstantBuffersInvalidBuffer = D3D11_MESSAGE_ID_HSSETCONSTANTBUFFERS_INVALIDBUFFER,

			///<summary>DeviceHSSetConstantBuffersBuffersEmpty message.</summary>
			DeviceHSSetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_HSSETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceHSSetSamplersSamplersEmpty message.</summary>
			DeviceHSSetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_HSSETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceHSGetShaderResourcesViewsEmpty message.</summary>
			DeviceHSGetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_HSGETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>DeviceHSGetConstantBuffersBuffersEmpty message.</summary>
			DeviceHSGetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_HSGETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceHSGetSamplersSamplersEmpty message.</summary>
			DeviceHSGetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_HSGETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceDSSetShaderResourcesHazard message.</summary>
			DeviceDSSetShaderResourcesHazard = D3D11_MESSAGE_ID_DEVICE_DSSETSHADERRESOURCES_HAZARD,

			///<summary>DeviceDSSetConstantBuffersHazard message.</summary>
			DeviceDSSetConstantBuffersHazard = D3D11_MESSAGE_ID_DEVICE_DSSETCONSTANTBUFFERS_HAZARD,

			///<summary>DSSetShaderResourcesUnbindDeletingObject message.</summary>
			DSSetShaderResourcesUnbindDeletingObject = D3D11_MESSAGE_ID_DSSETSHADERRESOURCES_UNBINDDELETINGOBJECT,

			///<summary>DSSetConstantBuffersUnbindDeletingObject message.</summary>
			DSSetConstantBuffersUnbindDeletingObject = D3D11_MESSAGE_ID_DSSETCONSTANTBUFFERS_UNBINDDELETINGOBJECT,

			///<summary>CreateDomainShaderInvalidCall message.</summary>
			CreateDomainShaderInvalidCall = D3D11_MESSAGE_ID_CREATEDOMAINSHADER_INVALIDCALL,

			///<summary>CreateDomainShaderOutOfMemory message.</summary>
			CreateDomainShaderOutOfMemory = D3D11_MESSAGE_ID_CREATEDOMAINSHADER_OUTOFMEMORY,

			///<summary>CreateDomainShaderInvalidShaderBytecode message.</summary>
			CreateDomainShaderInvalidShaderBytecode = D3D11_MESSAGE_ID_CREATEDOMAINSHADER_INVALIDSHADERBYTECODE,

			///<summary>CreateDomainShaderInvalidShaderType message.</summary>
			CreateDomainShaderInvalidShaderType = D3D11_MESSAGE_ID_CREATEDOMAINSHADER_INVALIDSHADERTYPE,

			///<summary>CreateDomainShaderInvalidClassLinkage message.</summary>
			CreateDomainShaderInvalidClassLinkage = D3D11_MESSAGE_ID_CREATEDOMAINSHADER_INVALIDCLASSLINKAGE,

			///<summary>DeviceDSSetShaderResourcesViewsEmpty message.</summary>
			DeviceDSSetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_DSSETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>DSSetConstantBuffersInvalidBuffer message.</summary>
			DSSetConstantBuffersInvalidBuffer = D3D11_MESSAGE_ID_DSSETCONSTANTBUFFERS_INVALIDBUFFER,

			///<summary>DeviceDSSetConstantBuffersBuffersEmpty message.</summary>
			DeviceDSSetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_DSSETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceDSSetSamplersSamplersEmpty message.</summary>
			DeviceDSSetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_DSSETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceDSGetShaderResourcesViewsEmpty message.</summary>
			DeviceDSGetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_DSGETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>DeviceDSGetConstantBuffersBuffersEmpty message.</summary>
			DeviceDSGetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_DSGETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceDSGetSamplersSamplersEmpty message.</summary>
			DeviceDSGetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_DSGETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceDrawHsXorDsMismatch message.</summary>
			DeviceDrawHsXorDsMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_HS_XOR_DS_MISMATCH,

			///<summary>DeferredContextRemovalProcessAtFault message.</summary>
			DeferredContextRemovalProcessAtFault = D3D11_MESSAGE_ID_DEFERRED_CONTEXT_REMOVAL_PROCESS_AT_FAULT,

			///<summary>DeviceDrawIndirectInvalidArgBuffer message.</summary>
			DeviceDrawIndirectInvalidArgBuffer = D3D11_MESSAGE_ID_DEVICE_DRAWINDIRECT_INVALID_ARG_BUFFER,

			///<summary>DeviceDrawIndirectOffsetUnaligned message.</summary>
			DeviceDrawIndirectOffsetUnaligned = D3D11_MESSAGE_ID_DEVICE_DRAWINDIRECT_OFFSET_UNALIGNED,

			///<summary>DeviceDrawIndirectOffsetOverflow message.</summary>
			DeviceDrawIndirectOffsetOverflow = D3D11_MESSAGE_ID_DEVICE_DRAWINDIRECT_OFFSET_OVERFLOW,

			///<summary>ResourceMapInvalidMapType message.</summary>
			ResourceMapInvalidMapType = D3D11_MESSAGE_ID_RESOURCE_MAP_INVALIDMAPTYPE,

			///<summary>ResourceMapInvalidSubresource message.</summary>
			ResourceMapInvalidSubresource = D3D11_MESSAGE_ID_RESOURCE_MAP_INVALIDSUBRESOURCE,

			///<summary>ResourceMapInvalidFlags message.</summary>
			ResourceMapInvalidFlags = D3D11_MESSAGE_ID_RESOURCE_MAP_INVALIDFLAGS,

			///<summary>ResourceMapAlreadyMapped message.</summary>
			ResourceMapAlreadyMapped = D3D11_MESSAGE_ID_RESOURCE_MAP_ALREADYMAPPED,

			///<summary>ResourceMapDeviceRemovedReturn message.</summary>
			ResourceMapDeviceRemovedReturn = D3D11_MESSAGE_ID_RESOURCE_MAP_DEVICEREMOVED_RETURN,

			///<summary>ResourceMapOutOfMemoryReturn message.</summary>
			ResourceMapOutOfMemoryReturn = D3D11_MESSAGE_ID_RESOURCE_MAP_OUTOFMEMORY_RETURN,

			///<summary>ResourceMapWithoutInitialDiscard message.</summary>
			ResourceMapWithoutInitialDiscard = D3D11_MESSAGE_ID_RESOURCE_MAP_WITHOUT_INITIAL_DISCARD,

			///<summary>ResourceUnmapInvalidSubresource message.</summary>
			ResourceUnmapInvalidSubresource = D3D11_MESSAGE_ID_RESOURCE_UNMAP_INVALIDSUBRESOURCE,

			///<summary>ResourceUnmapNotMapped message.</summary>
			ResourceUnmapNotMapped = D3D11_MESSAGE_ID_RESOURCE_UNMAP_NOTMAPPED,

			///<summary>DeviceDrawRasterizingControlPoints message.</summary>
			DeviceDrawRasterizingControlPoints = D3D11_MESSAGE_ID_DEVICE_DRAW_RASTERIZING_CONTROL_POINTS,

			///<summary>DeviceIASetPrimitiveTopologyUnsupported message.</summary>
			DeviceIASetPrimitiveTopologyUnsupported = D3D11_MESSAGE_ID_DEVICE_IASETPRIMITIVETOPOLOGY_TOPOLOGY_UNSUPPORTED,

			///<summary>DeviceDrawHsDsSignatureMismatch message.</summary>
			DeviceDrawHsDsSignatureMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_HS_DS_SIGNATURE_MISMATCH,

			///<summary>DeviceDrawHullShaderInputTopologyMismatch message.</summary>
			DeviceDrawHullShaderInputTopologyMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_HULL_SHADER_INPUT_TOPOLOGY_MISMATCH,

			///<summary>DeviceDrawHsDsControlPointCountMismatch message.</summary>
			DeviceDrawHsDsControlPointCountMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_HS_DS_CONTROL_POINT_COUNT_MISMATCH,

			///<summary>DeviceDrawHsDsTessellatorDomainMismatch message.</summary>
			DeviceDrawHsDsTessellatorDomainMismatch = D3D11_MESSAGE_ID_DEVICE_DRAW_HS_DS_TESSELLATOR_DOMAIN_MISMATCH,

			///<summary>CreateContext message.</summary>
			CreateContext = D3D11_MESSAGE_ID_CREATE_CONTEXT,

			///<summary>LiveContext message.</summary>
			LiveContext = D3D11_MESSAGE_ID_LIVE_CONTEXT,

			///<summary>DestroyContext message.</summary>
			DestroyContext = D3D11_MESSAGE_ID_DESTROY_CONTEXT,

			///<summary>CreateBuffer message.</summary>
			CreateBuffer = D3D11_MESSAGE_ID_CREATE_BUFFER,

			///<summary>LiveBuffer message.</summary>
			LiveBuffer = D3D11_MESSAGE_ID_LIVE_BUFFER,

			///<summary>DestroyBuffer message.</summary>
			DestroyBuffer = D3D11_MESSAGE_ID_DESTROY_BUFFER,

			///<summary>CreateTexture1D message.</summary>
			CreateTexture1D = D3D11_MESSAGE_ID_CREATE_TEXTURE1D,

			///<summary>LiveTexture1D message.</summary>
			LiveTexture1D = D3D11_MESSAGE_ID_LIVE_TEXTURE1D,

			///<summary>DestroyTexture1D message.</summary>
			DestroyTexture1D = D3D11_MESSAGE_ID_DESTROY_TEXTURE1D,

			///<summary>CreateTexture2D message.</summary>
			CreateTexture2D = D3D11_MESSAGE_ID_CREATE_TEXTURE2D,

			///<summary>LiveTexture2D message.</summary>
			LiveTexture2D = D3D11_MESSAGE_ID_LIVE_TEXTURE2D,

			///<summary>DestroyTexture2D message.</summary>
			DestroyTexture2D = D3D11_MESSAGE_ID_DESTROY_TEXTURE2D,

			///<summary>CreateTexture3D message.</summary>
			CreateTexture3D = D3D11_MESSAGE_ID_CREATE_TEXTURE3D,

			///<summary>LiveTexture3D message.</summary>
			LiveTexture3D = D3D11_MESSAGE_ID_LIVE_TEXTURE3D,

			///<summary>DestroyTexture3D message.</summary>
			DestroyTexture3D = D3D11_MESSAGE_ID_DESTROY_TEXTURE3D,

			///<summary>CreateShaderResourceView message.</summary>
			CreateShaderResourceView = D3D11_MESSAGE_ID_CREATE_SHADERRESOURCEVIEW,

			///<summary>LiveShaderResourceView message.</summary>
			LiveShaderResourceView = D3D11_MESSAGE_ID_LIVE_SHADERRESOURCEVIEW,

			///<summary>DestroyShaderResourceView message.</summary>
			DestroyShaderResourceView = D3D11_MESSAGE_ID_DESTROY_SHADERRESOURCEVIEW,

			///<summary>CreateRenderTargetView message.</summary>
			CreateRenderTargetView = D3D11_MESSAGE_ID_CREATE_RENDERTARGETVIEW,

			///<summary>LiveRenderTargetView message.</summary>
			LiveRenderTargetView = D3D11_MESSAGE_ID_LIVE_RENDERTARGETVIEW,

			///<summary>DestroyRenderTargetView message.</summary>
			DestroyRenderTargetView = D3D11_MESSAGE_ID_DESTROY_RENDERTARGETVIEW,

			///<summary>CreateDepthStencilView message.</summary>
			CreateDepthStencilView = D3D11_MESSAGE_ID_CREATE_DEPTHSTENCILVIEW,

			///<summary>LiveDepthStencilView message.</summary>
			LiveDepthStencilView = D3D11_MESSAGE_ID_LIVE_DEPTHSTENCILVIEW,

			///<summary>DestroyDepthStencilView message.</summary>
			DestroyDepthStencilView = D3D11_MESSAGE_ID_DESTROY_DEPTHSTENCILVIEW,

			///<summary>CreateVertexShader message.</summary>
			CreateVertexShader = D3D11_MESSAGE_ID_CREATE_VERTEXSHADER,

			///<summary>LiveVertexShader message.</summary>
			LiveVertexShader = D3D11_MESSAGE_ID_LIVE_VERTEXSHADER,

			///<summary>DestroyVertexShader message.</summary>
			DestroyVertexShader = D3D11_MESSAGE_ID_DESTROY_VERTEXSHADER,

			///<summary>CreateHullShader message.</summary>
			CreateHullShader = D3D11_MESSAGE_ID_CREATE_HULLSHADER,

			///<summary>LiveHullShader message.</summary>
			LiveHullShader = D3D11_MESSAGE_ID_LIVE_HULLSHADER,

			///<summary>DestroyHullShader message.</summary>
			DestroyHullShader = D3D11_MESSAGE_ID_DESTROY_HULLSHADER,

			///<summary>CreateDomainShader message.</summary>
			CreateDomainShader = D3D11_MESSAGE_ID_CREATE_DOMAINSHADER,

			///<summary>LiveDomainShader message.</summary>
			LiveDomainShader = D3D11_MESSAGE_ID_LIVE_DOMAINSHADER,

			///<summary>DestroyDomainShader message.</summary>
			DestroyDomainShader = D3D11_MESSAGE_ID_DESTROY_DOMAINSHADER,

			///<summary>CreateGeometryShader message.</summary>
			CreateGeometryShader = D3D11_MESSAGE_ID_CREATE_GEOMETRYSHADER,

			///<summary>LiveGeometryShader message.</summary>
			LiveGeometryShader = D3D11_MESSAGE_ID_LIVE_GEOMETRYSHADER,

			///<summary>DestroyGeometryShader message.</summary>
			DestroyGeometryShader = D3D11_MESSAGE_ID_DESTROY_GEOMETRYSHADER,

			///<summary>CreatePixelShader message.</summary>
			CreatePixelShader = D3D11_MESSAGE_ID_CREATE_PIXELSHADER,

			///<summary>LivePixelShader message.</summary>
			LivePixelShader = D3D11_MESSAGE_ID_LIVE_PIXELSHADER,

			///<summary>DestroyPixelShader message.</summary>
			DestroyPixelShader = D3D11_MESSAGE_ID_DESTROY_PIXELSHADER,

			///<summary>CreateInputLayout message.</summary>
			CreateInputLayout = D3D11_MESSAGE_ID_CREATE_INPUTLAYOUT,

			///<summary>LiveInputLayout message.</summary>
			LiveInputLayout = D3D11_MESSAGE_ID_LIVE_INPUTLAYOUT,

			///<summary>DestroyInputLayout message.</summary>
			DestroyInputLayout = D3D11_MESSAGE_ID_DESTROY_INPUTLAYOUT,

			///<summary>CreateSampler message.</summary>
			CreateSampler = D3D11_MESSAGE_ID_CREATE_SAMPLER,

			///<summary>LiveSampler message.</summary>
			LiveSampler = D3D11_MESSAGE_ID_LIVE_SAMPLER,

			///<summary>DestroySampler message.</summary>
			DestroySampler = D3D11_MESSAGE_ID_DESTROY_SAMPLER,

			///<summary>CreateBlendState message.</summary>
			CreateBlendState = D3D11_MESSAGE_ID_CREATE_BLENDSTATE,

			///<summary>LiveBlendState message.</summary>
			LiveBlendState = D3D11_MESSAGE_ID_LIVE_BLENDSTATE,

			///<summary>DestroyBlendState message.</summary>
			DestroyBlendState = D3D11_MESSAGE_ID_DESTROY_BLENDSTATE,

			///<summary>CreateDepthStencilState message.</summary>
			CreateDepthStencilState = D3D11_MESSAGE_ID_CREATE_DEPTHSTENCILSTATE,

			///<summary>LiveDepthStencilState message.</summary>
			LiveDepthStencilState = D3D11_MESSAGE_ID_LIVE_DEPTHSTENCILSTATE,

			///<summary>DestroyDepthStencilState message.</summary>
			DestroyDepthStencilState = D3D11_MESSAGE_ID_DESTROY_DEPTHSTENCILSTATE,

			///<summary>CreateRasterizerState message.</summary>
			CreateRasterizerState = D3D11_MESSAGE_ID_CREATE_RASTERIZERSTATE,

			///<summary>LiveRasterizerState message.</summary>
			LiveRasterizerState = D3D11_MESSAGE_ID_LIVE_RASTERIZERSTATE,

			///<summary>DestroyRasterizerState message.</summary>
			DestroyRasterizerState = D3D11_MESSAGE_ID_DESTROY_RASTERIZERSTATE,

			///<summary>CreateQuery message.</summary>
			CreateQuery = D3D11_MESSAGE_ID_CREATE_QUERY,

			///<summary>LiveQuery message.</summary>
			LiveQuery = D3D11_MESSAGE_ID_LIVE_QUERY,

			///<summary>DestroyQuery message.</summary>
			DestroyQuery = D3D11_MESSAGE_ID_DESTROY_QUERY,

			///<summary>CreatePredicate message.</summary>
			CreatePredicate = D3D11_MESSAGE_ID_CREATE_PREDICATE,

			///<summary>LivePredicate message.</summary>
			LivePredicate = D3D11_MESSAGE_ID_LIVE_PREDICATE,

			///<summary>DestroyPredicate message.</summary>
			DestroyPredicate = D3D11_MESSAGE_ID_DESTROY_PREDICATE,

			///<summary>CreateCounter message.</summary>
			CreateCounter = D3D11_MESSAGE_ID_CREATE_COUNTER,

			///<summary>LiveCounter message.</summary>
			LiveCounter = D3D11_MESSAGE_ID_LIVE_COUNTER,

			///<summary>DestroyCounter message.</summary>
			DestroyCounter = D3D11_MESSAGE_ID_DESTROY_COUNTER,

			///<summary>CreateCommandList message.</summary>
			CreateCommandList = D3D11_MESSAGE_ID_CREATE_COMMANDLIST,

			///<summary>LiveCommandList message.</summary>
			LiveCommandList = D3D11_MESSAGE_ID_LIVE_COMMANDLIST,

			///<summary>DestroyCommandList message.</summary>
			DestroyCommandList = D3D11_MESSAGE_ID_DESTROY_COMMANDLIST,

			///<summary>CreateClassInstance message.</summary>
			CreateClassInstance = D3D11_MESSAGE_ID_CREATE_CLASSINSTANCE,

			///<summary>LiveClassInstance message.</summary>
			LiveClassInstance = D3D11_MESSAGE_ID_LIVE_CLASSINSTANCE,

			///<summary>DestroyClassInstance message.</summary>
			DestroyClassInstance = D3D11_MESSAGE_ID_DESTROY_CLASSINSTANCE,

			///<summary>CreateClassLinkage message.</summary>
			CreateClassLinkage = D3D11_MESSAGE_ID_CREATE_CLASSLINKAGE,

			///<summary>LiveClassLinkage message.</summary>
			LiveClassLinkage = D3D11_MESSAGE_ID_LIVE_CLASSLINKAGE,

			///<summary>DestroyClassLinkage message.</summary>
			DestroyClassLinkage = D3D11_MESSAGE_ID_DESTROY_CLASSLINKAGE,

			///<summary>LiveDevice message.</summary>
			LiveDevice = D3D11_MESSAGE_ID_LIVE_DEVICE,

			///<summary>LiveObjectSummary message.</summary>
			LiveObjectSummary = D3D11_MESSAGE_ID_LIVE_OBJECT_SUMMARY,

			///<summary>CreateComputeShader message.</summary>
			CreateComputeShader = D3D11_MESSAGE_ID_CREATE_COMPUTESHADER,

			///<summary>LiveComputeShader message.</summary>
			LiveComputeShader = D3D11_MESSAGE_ID_LIVE_COMPUTESHADER,

			///<summary>DestroyComputeShader message.</summary>
			DestroyComputeShader = D3D11_MESSAGE_ID_DESTROY_COMPUTESHADER,

			///<summary>CreateUnorderedAccessView message.</summary>
			CreateUnorderedAccessView = D3D11_MESSAGE_ID_CREATE_UNORDEREDACCESSVIEW,

			///<summary>LiveUnorderedAccessView message.</summary>
			LiveUnorderedAccessView = D3D11_MESSAGE_ID_LIVE_UNORDEREDACCESSVIEW,

			///<summary>DestroyUnorderedAccessView message.</summary>
			DestroyUnorderedAccessView = D3D11_MESSAGE_ID_DESTROY_UNORDEREDACCESSVIEW,

			///<summary>DeviceSetShaderInterfacesFeatureLevel message.</summary>
			DeviceSetShaderInterfacesFeatureLevel = D3D11_MESSAGE_ID_DEVICE_SETSHADER_INTERFACES_FEATURELEVEL,

			///<summary>DeviceSetShaderInterfaceCountMismatch message.</summary>
			DeviceSetShaderInterfaceCountMismatch = D3D11_MESSAGE_ID_DEVICE_SETSHADER_INTERFACE_COUNT_MISMATCH,

			///<summary>DeviceSetShaderInvalidInstance message.</summary>
			DeviceSetShaderInvalidInstance = D3D11_MESSAGE_ID_DEVICE_SETSHADER_INVALID_INSTANCE,

			///<summary>DeviceSetShaderInvalidInstanceIndex message.</summary>
			DeviceSetShaderInvalidInstanceIndex = D3D11_MESSAGE_ID_DEVICE_SETSHADER_INVALID_INSTANCE_INDEX,

			///<summary>DeviceSetShaderInvalidInstanceType message.</summary>
			DeviceSetShaderInvalidInstanceType = D3D11_MESSAGE_ID_DEVICE_SETSHADER_INVALID_INSTANCE_TYPE,

			///<summary>DeviceSetShaderInvalidInstanceData message.</summary>
			DeviceSetShaderInvalidInstanceData = D3D11_MESSAGE_ID_DEVICE_SETSHADER_INVALID_INSTANCE_DATA,

			///<summary>DeviceSetShaderUnboundInstanceData message.</summary>
			DeviceSetShaderUnboundInstanceData = D3D11_MESSAGE_ID_DEVICE_SETSHADER_UNBOUND_INSTANCE_DATA,

			///<summary>DeviceSetShaderInstanceDataBindings message.</summary>
			DeviceSetShaderInstanceDataBindings = D3D11_MESSAGE_ID_DEVICE_SETSHADER_INSTANCE_DATA_BINDINGS,

			///<summary>DeviceCreateShaderClassLinkageFull message.</summary>
			DeviceCreateShaderClassLinkageFull = D3D11_MESSAGE_ID_DEVICE_CREATESHADER_CLASSLINKAGE_FULL,

			///<summary>DeviceCheckFeatureSupportUnrecognizedFeature message.</summary>
			DeviceCheckFeatureSupportUnrecognizedFeature = D3D11_MESSAGE_ID_DEVICE_CHECKFEATURESUPPORT_UNRECOGNIZED_FEATURE,

			///<summary>DeviceCheckFeatureSupportMismatchedDataSize message.</summary>
			DeviceCheckFeatureSupportMismatchedDataSize = D3D11_MESSAGE_ID_DEVICE_CHECKFEATURESUPPORT_MISMATCHED_DATA_SIZE,

			///<summary>DeviceCheckFeatureSupportInvalidArgumentReturn message.</summary>
			DeviceCheckFeatureSupportInvalidArgumentReturn = D3D11_MESSAGE_ID_DEVICE_CHECKFEATURESUPPORT_INVALIDARG_RETURN,

			///<summary>DeviceCSSetShaderResourcesHazard message.</summary>
			DeviceCSSetShaderResourcesHazard = D3D11_MESSAGE_ID_DEVICE_CSSETSHADERRESOURCES_HAZARD,

			///<summary>DeviceCSSetConstantBuffersHazard message.</summary>
			DeviceCSSetConstantBuffersHazard = D3D11_MESSAGE_ID_DEVICE_CSSETCONSTANTBUFFERS_HAZARD,

			///<summary>CSSetShaderResourcesUnbindDeletingObject message.</summary>
			CSSetShaderResourcesUnbindDeletingObject = D3D11_MESSAGE_ID_CSSETSHADERRESOURCES_UNBINDDELETINGOBJECT,

			///<summary>CSSetConstantBuffersUnbindDeletingObject message.</summary>
			CSSetConstantBuffersUnbindDeletingObject = D3D11_MESSAGE_ID_CSSETCONSTANTBUFFERS_UNBINDDELETINGOBJECT,

			///<summary>CreateComputeShaderInvalidCall message.</summary>
			CreateComputeShaderInvalidCall = D3D11_MESSAGE_ID_CREATECOMPUTESHADER_INVALIDCALL,

			///<summary>CreateComputeShaderOutOfMemory message.</summary>
			CreateComputeShaderOutOfMemory = D3D11_MESSAGE_ID_CREATECOMPUTESHADER_OUTOFMEMORY,

			///<summary>CreateComputeShaderInvalidShaderBytecode message.</summary>
			CreateComputeShaderInvalidShaderBytecode = D3D11_MESSAGE_ID_CREATECOMPUTESHADER_INVALIDSHADERBYTECODE,

			///<summary>CreateComputeShaderInvalidShaderType message.</summary>
			CreateComputeShaderInvalidShaderType = D3D11_MESSAGE_ID_CREATECOMPUTESHADER_INVALIDSHADERTYPE,

			///<summary>CreateComputeShaderInvalidClassLinkage message.</summary>
			CreateComputeShaderInvalidClassLinkage = D3D11_MESSAGE_ID_CREATECOMPUTESHADER_INVALIDCLASSLINKAGE,

			///<summary>DeviceCSSetShaderResourcesViewsEmpty message.</summary>
			DeviceCSSetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_CSSETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>CSSetConstantBuffersInvalidBuffer message.</summary>
			CSSetConstantBuffersInvalidBuffer = D3D11_MESSAGE_ID_CSSETCONSTANTBUFFERS_INVALIDBUFFER,

			///<summary>DeviceCSSetConstantBuffersBuffersEmpty message.</summary>
			DeviceCSSetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_CSSETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceCSSetSamplersSamplersEmpty message.</summary>
			DeviceCSSetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_CSSETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceCSGetShaderResourcesViewsEmpty message.</summary>
			DeviceCSGetShaderResourcesViewsEmpty = D3D11_MESSAGE_ID_DEVICE_CSGETSHADERRESOURCES_VIEWS_EMPTY,

			///<summary>DeviceCSGetConstantBuffersBuffersEmpty message.</summary>
			DeviceCSGetConstantBuffersBuffersEmpty = D3D11_MESSAGE_ID_DEVICE_CSGETCONSTANTBUFFERS_BUFFERS_EMPTY,

			///<summary>DeviceCSGetSamplersSamplersEmpty message.</summary>
			DeviceCSGetSamplersSamplersEmpty = D3D11_MESSAGE_ID_DEVICE_CSGETSAMPLERS_SAMPLERS_EMPTY,

			///<summary>DeviceCreateVertexShaderDoubleFloatOpsNotSupported message.</summary>
			DeviceCreateVertexShaderDoubleFloatOpsNotSupported = D3D11_MESSAGE_ID_DEVICE_CREATEVERTEXSHADER_DOUBLEFLOATOPSNOTSUPPORTED,

			///<summary>DeviceCreateHullShaderDoubleFloatOpsNotSupported message.</summary>
			DeviceCreateHullShaderDoubleFloatOpsNotSupported = D3D11_MESSAGE_ID_DEVICE_CREATEHULLSHADER_DOUBLEFLOATOPSNOTSUPPORTED,

			///<summary>DeviceCreateDomainShaderDoubleFloatOpsNotSupported message.</summary>
			DeviceCreateDomainShaderDoubleFloatOpsNotSupported = D3D11_MESSAGE_ID_DEVICE_CREATEDOMAINSHADER_DOUBLEFLOATOPSNOTSUPPORTED,

			///<summary>DeviceCreateGeometryShaderDoubleFloatOpsNotSupported message.</summary>
			DeviceCreateGeometryShaderDoubleFloatOpsNotSupported = D3D11_MESSAGE_ID_DEVICE_CREATEGEOMETRYSHADER_DOUBLEFLOATOPSNOTSUPPORTED,

			///<summary>DeviceCreateGeometryShaderWithStreamOutputDoubleFloatOpsNotSupported message.</summary>
			DeviceCreateGeometryShaderWithStreamOutputDoubleFloatOpsNotSupported = D3D11_MESSAGE_ID_DEVICE_CREATEGEOMETRYSHADERWITHSTREAMOUTPUT_DOUBLEFLOATOPSNOTSUPPORTED,

			///<summary>DeviceCreatePixelShaderDoubleFloatOpsNotSupported message.</summary>
			DeviceCreatePixelShaderDoubleFloatOpsNotSupported = D3D11_MESSAGE_ID_DEVICE_CREATEPIXELSHADER_DOUBLEFLOATOPSNOTSUPPORTED,

			///<summary>DeviceCreateComputeShaderDoubleFloatOpsNotSupported message.</summary>
			DeviceCreateComputeShaderDoubleFloatOpsNotSupported = D3D11_MESSAGE_ID_DEVICE_CREATECOMPUTESHADER_DOUBLEFLOATOPSNOTSUPPORTED,

			///<summary>CreateBufferInvalidStructureStride message.</summary>
			CreateBufferInvalidStructureStride = D3D11_MESSAGE_ID_CREATEBUFFER_INVALIDSTRUCTURESTRIDE,

			///<summary>CreateShaderResourceViewInvalidFlags message.</summary>
			CreateShaderResourceViewInvalidFlags = D3D11_MESSAGE_ID_CREATESHADERRESOURCEVIEW_INVALIDFLAGS,

			///<summary>CreateUnorderedAccessViewInvalidResource message.</summary>
			CreateUnorderedAccessViewInvalidResource = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_INVALIDRESOURCE,

			///<summary>CreateUnorderedAccessViewInvalidDescription message.</summary>
			CreateUnorderedAccessViewInvalidDescription = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_INVALIDDESC,

			///<summary>CreateUnorderedAccessViewInvalidFormat message.</summary>
			CreateUnorderedAccessViewInvalidFormat = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_INVALIDFORMAT,

			///<summary>CreateUnorderedAccessViewInvalidDimensions message.</summary>
			CreateUnorderedAccessViewInvalidDimensions = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_INVALIDDIMENSIONS,

			///<summary>CreateUnorderedAccessViewUnrecognizedFormat message.</summary>
			CreateUnorderedAccessViewUnrecognizedFormat = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_UNRECOGNIZEDFORMAT,

			///<summary>DeviceOMSetRenderTargetsAndUnorderedAccessViewsHazard message.</summary>
			DeviceOMSetRenderTargetsAndUnorderedAccessViewsHazard = D3D11_MESSAGE_ID_DEVICE_OMSETRENDERTARGETSANDUNORDEREDACCESSVIEWS_HAZARD,

			///<summary>DeviceOMSetRenderTargetsAndUnorderedAccessViewsOverlappingOldSlots message.</summary>
			DeviceOMSetRenderTargetsAndUnorderedAccessViewsOverlappingOldSlots = D3D11_MESSAGE_ID_DEVICE_OMSETRENDERTARGETSANDUNORDEREDACCESSVIEWS_OVERLAPPING_OLD_SLOTS,

			///<summary>DeviceOMSetRenderTargetsAndUnorderedAccessViewsNoOp message.</summary>
			DeviceOMSetRenderTargetsAndUnorderedAccessViewsNoOp = D3D11_MESSAGE_ID_DEVICE_OMSETRENDERTARGETSANDUNORDEREDACCESSVIEWS_NO_OP,

			///<summary>CSSetUnorderedAccessViewsUnbindDeletingObject message.</summary>
			CSSetUnorderedAccessViewsUnbindDeletingObject = D3D11_MESSAGE_ID_CSSETUNORDEREDACCESSVIEWS_UNBINDDELETINGOBJECT,

			///<summary>PSSetUnorderedAccessViewsUnbindDeletingObject message.</summary>
			PSSetUnorderedAccessViewsUnbindDeletingObject = D3D11_MESSAGE_ID_PSSETUNORDEREDACCESSVIEWS_UNBINDDELETINGOBJECT,

			///<summary>CreateUnorderedAccessViewInvalidArgumentReturn message.</summary>
			CreateUnorderedAccessViewInvalidArgumentReturn = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_INVALIDARG_RETURN,

			///<summary>CreateUnorderedAccessViewOutOfMemoryReturn message.</summary>
			CreateUnorderedAccessViewOutOfMemoryReturn = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_OUTOFMEMORY_RETURN,

			///<summary>CreateUnorderedAccessViewTooManyObjects message.</summary>
			CreateUnorderedAccessViewTooManyObjects = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_TOOMANYOBJECTS,

			///<summary>DeviceCSSetUnorderedAccessViewsHazard message.</summary>
			DeviceCSSetUnorderedAccessViewsHazard = D3D11_MESSAGE_ID_DEVICE_CSSETUNORDEREDACCESSVIEWS_HAZARD,

			///<summary>ClearUnorderedAccessViewDenormalizeFlush message.</summary>
			ClearUnorderedAccessViewDenormalizeFlush = D3D11_MESSAGE_ID_CLEARUNORDEREDACCESSVIEW_DENORMFLUSH,

			///<summary>DeviceCSSetUnorderedAccessViewsEmpty message.</summary>
			DeviceCSSetUnorderedAccessViewsEmpty = D3D11_MESSAGE_ID_DEVICE_CSSETUNORDEREDACCESSS_VIEWS_EMPTY,

			///<summary>DeviceCSGetUnorderedAccessViewsEmpty message.</summary>
			DeviceCSGetUnorderedAccessViewsEmpty = D3D11_MESSAGE_ID_DEVICE_CSGETUNORDEREDACCESSS_VIEWS_EMPTY,

			///<summary>CreateUnorderedAccessViewInvalidFlags message.</summary>
			CreateUnorderedAccessViewInvalidFlags = D3D11_MESSAGE_ID_CREATEUNORDEREDACCESSVIEW_INVALIDFLAGS,

			///<summary>DeviceDispatchIndirectInvalidArgBuffer message.</summary>
			DeviceDispatchIndirectInvalidArgBuffer = D3D11_MESSAGE_ID_DEVICE_DISPATCHINDIRECT_INVALID_ARG_BUFFER,

			///<summary>DeviceDispatchIndirectOffsetUnaligned message.</summary>
			DeviceDispatchIndirectOffsetUnaligned = D3D11_MESSAGE_ID_DEVICE_DISPATCHINDIRECT_OFFSET_UNALIGNED,

			///<summary>DeviceDispatchIndirectOffsetOverflow message.</summary>
			DeviceDispatchIndirectOffsetOverflow = D3D11_MESSAGE_ID_DEVICE_DISPATCHINDIRECT_OFFSET_OVERFLOW,

			///<summary>DeviceSetResourceMinLodInvalidContext message.</summary>
			DeviceSetResourceMinLodInvalidContext = D3D11_MESSAGE_ID_DEVICE_SETRESOURCEMINLOD_INVALIDCONTEXT,

			///<summary>DeviceSetResourceMinLodInvalidResource message.</summary>
			DeviceSetResourceMinLodInvalidResource = D3D11_MESSAGE_ID_DEVICE_SETRESOURCEMINLOD_INVALIDRESOURCE,

			///<summary>DeviceSetResourceMinLodInvalidMinLod message.</summary>
			DeviceSetResourceMinLodInvalidMinLod = D3D11_MESSAGE_ID_DEVICE_SETRESOURCEMINLOD_INVALIDMINLOD,

			///<summary>DeviceGetResourceMinLodInvalidContext message.</summary>
			DeviceGetResourceMinLodInvalidContext = D3D11_MESSAGE_ID_DEVICE_GETRESOURCEMINLOD_INVALIDCONTEXT,

			///<summary>DeviceGetResourceMinLodInvalidResource message.</summary>
			DeviceGetResourceMinLodInvalidResource = D3D11_MESSAGE_ID_DEVICE_GETRESOURCEMINLOD_INVALIDRESOURCE,

			///<summary>OMSetDepthStencilUnbindDeletingObject message.</summary>
			OMSetDepthStencilUnbindDeletingObject = D3D11_MESSAGE_ID_OMSETDEPTHSTENCIL_UNBINDDELETINGOBJECT,

			///<summary>ClearDepthStencilViewDepthReadOnly message.</summary>
			ClearDepthStencilViewDepthReadOnly = D3D11_MESSAGE_ID_CLEARDEPTHSTENCILVIEW_DEPTH_READONLY,

			///<summary>ClearDepthStencilViewStencilReadOnly message.</summary>
			ClearDepthStencilViewStencilReadOnly = D3D11_MESSAGE_ID_CLEARDEPTHSTENCILVIEW_STENCIL_READONLY,

			///<summary>CheckFeatureSupportFormatDeprecated message.</summary>
			CheckFeatureSupportFormatDeprecated = D3D11_MESSAGE_ID_CHECKFEATURESUPPORT_FORMAT_DEPRECATED,

			///<summary>DeviceUnorderedAccessViewReturnTypeMismatch message.</summary>
			DeviceUnorderedAccessViewReturnTypeMismatch = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_RETURN_TYPE_MISMATCH,

			///<summary>DeviceUnorderedAccessViewNotSet message.</summary>
			DeviceUnorderedAccessViewNotSet = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_NOT_SET,

			///<summary>DeviceDrawUnorderedAccessViewRenderTargetViewOverlap message.</summary>
			DeviceDrawUnorderedAccessViewRenderTargetViewOverlap = D3D11_MESSAGE_ID_DEVICE_DRAW_UNORDEREDACCESSVIEW_RENDERTARGETVIEW_OVERLAP,

			///<summary>DeviceUnorderedAccessViewDimensionMismatch message.</summary>
			DeviceUnorderedAccessViewDimensionMismatch = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_DIMENSION_MISMATCH,

			///<summary>DeviceUnorderedAccessViewAppendUnsupported message.</summary>
			DeviceUnorderedAccessViewAppendUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_APPEND_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewAtomicsUnsupported message.</summary>
			DeviceUnorderedAccessViewAtomicsUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_ATOMICS_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewStructureStrideMismatch message.</summary>
			DeviceUnorderedAccessViewStructureStrideMismatch = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_STRUCTURE_STRIDE_MISMATCH,

			///<summary>DeviceUnorderedAccessViewBufferTypeMismatch message.</summary>
			DeviceUnorderedAccessViewBufferTypeMismatch = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_BUFFER_TYPE_MISMATCH,

			///<summary>DeviceUnorderedAccessViewRawUnsupported message.</summary>
			DeviceUnorderedAccessViewRawUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_RAW_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewFormatLdUnsupported message.</summary>
			DeviceUnorderedAccessViewFormatLdUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_FORMAT_LD_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewFormatStoreUnsupported message.</summary>
			DeviceUnorderedAccessViewFormatStoreUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_FORMAT_STORE_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewAtomicAddUnsupported message.</summary>
			DeviceUnorderedAccessViewAtomicAddUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_ATOMIC_ADD_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewAtomicBitwiseOpsUnsupported message.</summary>
			DeviceUnorderedAccessViewAtomicBitwiseOpsUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_ATOMIC_BITWISE_OPS_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewAtomicCmpStoreCmpExchangeUnsupported message.</summary>
			DeviceUnorderedAccessViewAtomicCmpStoreCmpExchangeUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_ATOMIC_CMPSTORE_CMPEXCHANGE_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewAtomicExchangeUnsupported message.</summary>
			DeviceUnorderedAccessViewAtomicExchangeUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_ATOMIC_EXCHANGE_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewAtomicSignedMinmaxUnsupported message.</summary>
			DeviceUnorderedAccessViewAtomicSignedMinmaxUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_ATOMIC_SIGNED_MINMAX_UNSUPPORTED,

			///<summary>DeviceUnorderedAccessViewAtomicUnsignedMinmaxUnsupported message.</summary>
			DeviceUnorderedAccessViewAtomicUnsignedMinmaxUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_ATOMIC_UNSIGNED_MINMAX_UNSUPPORTED,

			///<summary>DeviceDispatchBoundResourceMapped message.</summary>
			DeviceDispatchBoundResourceMapped = D3D11_MESSAGE_ID_DEVICE_DISPATCH_BOUND_RESOURCE_MAPPED,

			///<summary>DeviceDispatchThreadGroupCountOverflow message.</summary>
			DeviceDispatchThreadGroupCountOverflow = D3D11_MESSAGE_ID_DEVICE_DISPATCH_THREADGROUPCOUNT_OVERFLOW,

			///<summary>DeviceDispatchThreadGroupCountZero message.</summary>
			DeviceDispatchThreadGroupCountZero = D3D11_MESSAGE_ID_DEVICE_DISPATCH_THREADGROUPCOUNT_ZERO,

			///<summary>DeviceShaderResourceViewStructureStrideMismatch message.</summary>
			DeviceShaderResourceViewStructureStrideMismatch = D3D11_MESSAGE_ID_DEVICE_SHADERRESOURCEVIEW_STRUCTURE_STRIDE_MISMATCH,

			///<summary>DeviceShaderResourceViewBufferTypeMismatch message.</summary>
			DeviceShaderResourceViewBufferTypeMismatch = D3D11_MESSAGE_ID_DEVICE_SHADERRESOURCEVIEW_BUFFER_TYPE_MISMATCH,

			///<summary>DeviceShaderResourceViewRawUnsupported message.</summary>
			DeviceShaderResourceViewRawUnsupported = D3D11_MESSAGE_ID_DEVICE_SHADERRESOURCEVIEW_RAW_UNSUPPORTED,

			///<summary>DeviceDispatchUnsupported message.</summary>
			DeviceDispatchUnsupported = D3D11_MESSAGE_ID_DEVICE_DISPATCH_UNSUPPORTED,

			///<summary>DeviceDispatchIndirectUnsupported message.</summary>
			DeviceDispatchIndirectUnsupported = D3D11_MESSAGE_ID_DEVICE_DISPATCHINDIRECT_UNSUPPORTED,

			///<summary>CopyStructureCountInvalidOffset message.</summary>
			CopyStructureCountInvalidOffset = D3D11_MESSAGE_ID_COPYSTRUCTURECOUNT_INVALIDOFFSET,

			///<summary>CopyStructureCountLargeOffset message.</summary>
			CopyStructureCountLargeOffset = D3D11_MESSAGE_ID_COPYSTRUCTURECOUNT_LARGEOFFSET,

			///<summary>CopyStructureCountInvalidDestinationState message.</summary>
			CopyStructureCountInvalidDestinationState = D3D11_MESSAGE_ID_COPYSTRUCTURECOUNT_INVALIDDESTINATIONSTATE,

			///<summary>CopyStructureCountInvalidSourceState message.</summary>
			CopyStructureCountInvalidSourceState = D3D11_MESSAGE_ID_COPYSTRUCTURECOUNT_INVALIDSOURCESTATE,

			///<summary>CheckFormatSupportFormatNotSupported message.</summary>
			CheckFormatSupportFormatNotSupported = D3D11_MESSAGE_ID_CHECKFORMATSUPPORT_FORMAT_NOT_SUPPORTED,

			///<summary>DeviceCSSetUnorderedAccessViewsInvalidView message.</summary>
			DeviceCSSetUnorderedAccessViewsInvalidView = D3D11_MESSAGE_ID_DEVICE_CSSETUNORDEREDACCESSVIEWS_INVALIDVIEW,

			///<summary>DeviceCSSetUnorderedAccessViewsInvalidOffset message.</summary>
			DeviceCSSetUnorderedAccessViewsInvalidOffset = D3D11_MESSAGE_ID_DEVICE_CSSETUNORDEREDACCESSVIEWS_INVALIDOFFSET,

			///<summary>DeviceCSSetUnorderedAccessViewsTooManyViews message.</summary>
			DeviceCSSetUnorderedAccessViewsTooManyViews = D3D11_MESSAGE_ID_DEVICE_CSSETUNORDEREDACCESSVIEWS_TOOMANYVIEWS,

			///<summary>ClearUnorderedAccessViewFloatInvalidFormat message.</summary>
			ClearUnorderedAccessViewFloatInvalidFormat = D3D11_MESSAGE_ID_CLEARUNORDEREDACCESSVIEWFLOAT_INVALIDFORMAT,

			///<summary>DeviceUnorderedAccessViewCounterUnsupported message.</summary>
			DeviceUnorderedAccessViewCounterUnsupported = D3D11_MESSAGE_ID_DEVICE_UNORDEREDACCESSVIEW_COUNTER_UNSUPPORTED,

			///<summary>RefWarning message.</summary>
			RefWarning = D3D11_MESSAGE_ID_REF_WARNING
		};
	}
}