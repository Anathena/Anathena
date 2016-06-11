struct VS_IN
{
	float4 pos : POSITION;
	float4 col : COLOR;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float2 uv: TEXCOORD0;
};

SamplerState sampLinearWrap
{
	Filter = MIN_MAG_LINEAR_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

#define NUM_SAMPLES 35

float2 lightPosSS;
float2 resolutionInv;
texture2D tex;

//on the fly full screen quad
PS_IN VS(uint id : SV_VertexID)
{
	PS_IN ret;
	ret.uv = float2( id & 1, (id & 2) >> 1 );
	ret.pos = float4( ret.uv * float2( 2.0f, -2.0f ) + float2( -1.0f, 1.0f), 0.0f, 1.0f );
	return ret;
}


//This shader is basicly little utilized example from GPU GEMS3:
//http://http.developer.nvidia.com/GPUGems3/gpugems3_ch13.html
float4 PS( PS_IN input ) : SV_Target
{
    float3 background = float3(0.286, 0.262, 0.231);
    float3 cellColor = float3(0, 0.890, 0.996);
    float3 lightColor = float3(0.019, 0.101, 0.133);
    
	float3 color = background;
	float isAlive = tex.SampleLevel(sampLinearWrap, input.uv , 0).r ;
	color = cellColor * isAlive + background * (1.0 - isAlive);
	
	float2 texCoord = input.uv;
	float Decay = 0.99;
	float2 ScreenLightPos = float2(0.5,0.5);
	float Density = 0.3;
	float2 deltaTexCoord = (input.uv - lightPosSS.xy);  
	deltaTexCoord *= 1.0f / NUM_SAMPLES * Density;    
	float illuminationDecay = 1.0f;  
	for (int i = 0; i < NUM_SAMPLES; i++)  
	{  
		texCoord -= deltaTexCoord;  
	    float3 sample = float3(tex.SampleLevel(sampLinearWrap, texCoord , 0).rrr) * lightColor * 0.3;  
		sample *= illuminationDecay * 0.9; 
		color += sample;  
		illuminationDecay *= Decay;  
	 }  
	return float4 (color,1);
}

technique11 Render
{
	pass P0
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_5_0, VS() ) );
		SetPixelShader( CompileShader( ps_5_0, PS() ) );
	}
}