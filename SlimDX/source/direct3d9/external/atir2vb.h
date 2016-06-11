//-----------------------------------------------------------------------------
// File: atir2vb.h
// Copyright (c) 2005 ATI Technologies Inc. All rights reserved.
//-----------------------------------------------------------------------------



#ifndef __ATIR2VB_H__
#define __ATIR2VB_H__

// FourCC code exported by D3D driver to indicate R2VB support
// (not intended for resources creation)
#define R2VB_FOURCC_R2VB        MAKEFOURCC('R','2','V','B')

// R2VB command ids
//
#define R2VB_GLB_ENA_CMD        0x0
#define R2VB_VS2SM_CMD          0x1

// R2VB mask/shifts
//

// R2VB Command Token
#define R2VB_TOK_CMD_SHFT       24
#define R2VB_TOK_CMD_MSK        0x0F000000
#define R2VB_TOK_CMD_MAG        0x70FF0000
#define R2VB_TOK_CMD_MAT        0xFFFF0000
#define R2VB_TOK_PLD_MSK        0x0000FFFF


// R2VB_GLB_ENA_CMD 
#define R2VB_GLB_ENA_MSK        0x1

// R2VB_VS2SM_CMD 
#define R2VB_VS2SM_STRM_MSK     0xF
#define R2VB_VS2SM_SMP_SHFT     0x4
#define R2VB_VS2SM_SMP_MSK      0x7

// R2VB enums
//

#define R2VB_VSMP_OVR_DMAP   0          // override stream with dmap sampler
#define R2VB_VSMP_OVR_VTX0   1          // override stream with vertex texture 0 sampler
#define R2VB_VSMP_OVR_VTX1   2          // override stream with vertex texture 1 sampler
#define R2VB_VSMP_OVR_VTX2   3          // override stream with vertex texture 2 sampler
#define R2VB_VSMP_OVR_VTX3   4          // override stream with vertex texture 3 sampler
#define R2VB_VSMP_OVR_DIS    5          // disable stream override
#define R2VB_VSMP_OVR_NUM    6          //
#define R2VB_VSMP_NUM        5          // 5 available texture samplers

//
// R2VB Inlines

// Commnad Token Processing 
//
__inline DWORD r2vbToken_Set(DWORD cmd, DWORD payload){
	DWORD cmd_token = (cmd << R2VB_TOK_CMD_SHFT) & R2VB_TOK_CMD_MSK;
	DWORD pld_data = payload & R2VB_TOK_PLD_MSK;
	return (R2VB_TOK_CMD_MAG | cmd_token | pld_data);
}

// R2VB_GLB_ENA_CMD
//
__inline DWORD r2vbGlbEnable_Set(BOOL ena){
	DWORD payload = ena & R2VB_GLB_ENA_MSK;
	DWORD dw = r2vbToken_Set(R2VB_GLB_ENA_CMD, payload);
	return dw;
}

// R2VB_VS2SM_CMD
//
__inline DWORD r2vbVStrm2SmpMap_Set(DWORD str, DWORD smp){
	DWORD sampler = (smp & R2VB_VS2SM_SMP_MSK) << R2VB_VS2SM_SMP_SHFT;
	DWORD stream = (str & R2VB_VS2SM_STRM_MSK);
	DWORD payload = sampler | stream;
	DWORD dw = r2vbToken_Set(R2VB_VS2SM_CMD, payload);
	return dw;
}

#endif  // __ATIR2VB_H__
