float4x4 view;
float4x4 proj;
float4x4 world;
Texture2D model_texture;
float transparency = 0.0;
float3 sun = float3(.5, 0, -.5);

SamplerState ModelTextureSampler {
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};

struct VS_IN {
	float4 pos : POSITION;
	float3 normal : NORMAL;
	float2 texCoord : TEXCOORD;
};

struct PS_IN {
	float4 pos : SV_POSITION;
	float3 normal : NORMAL;
	float2 texCoord : TEXCOORD;
};

PS_IN VS( VS_IN input ) {
	PS_IN output = (PS_IN)0;
	
	float4x4 worldViewProj = mul(mul(world, view), proj);
	output.pos = mul(input.pos, worldViewProj);
	output.normal = mul(input.normal, worldViewProj);
	output.texCoord = input.texCoord;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target {
	float lightAmount = max(.125,dot(normalize(input.normal),normalize(sun)));
	return model_texture.Sample(ModelTextureSampler, input.texCoord) * lightAmount * (1 - float4(transparency, transparency, transparency, transparency));
}

technique10 RenderSolid {
	pass P0 {
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VS() ) );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}
}