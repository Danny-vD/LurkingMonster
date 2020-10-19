Shader "Custom/NoCulling"
{
	Properties
	{
		[Header(Albedo)]
		[Toggle] _UseAlbedo("Toggle Albedo", Int) = 1
		[MainColor] _Tint ("Tint", Color) = (1,1,1,1)
		[MainTexture] _AlbedoMap ("Albedo (RGB)", 2D) = "white" {}
		[Toggle] _UseAlpha("Use Albedo Alpha", Int) = 0

		[Space]

		[Header(Metallic and Smoothness)]
		[Toggle] _UseMetallic("Toggle Metallic", Int) = 1
		_MetallicMap ("Metallic (R) Smoothness (A)", 2D) = "black" {}

		[Space]

		[Header(Normal)]
		[Toggle] _UseNormal("Toggle Normal", Int) = 1
		[Normal] _NormalMap ("Normal", 2D) = "bump" {}

		[Space]

		[Header(Height)]
		[Toggle] _UseHeight("Toggle Height", Int) = 1
		_HeightMap("HeightMap", 2D) = "black"
		_HeightIntensity("Height Intensity", float) = .2

		[Space]

		[Header(Occlusion)]
		[Toggle] _UseOcclusion("Toggle Occlusion", Int) = 1
		_OcclusionMap ("OcclusionMap", 2D) = "white" {}
		_OcclusionIntensity("Occlusion Intensity", float) = 0

		[Space]

		[Header(Emission)]
		[Toggle] _UseEmission("Toggle Emission", Int) = 1
		[HDR] _EmissionMap ("EmissionMap", 2D) = "black" {}

	}
	SubShader
	{
		Tags
		{
			"RenderType"="Transparent" "Queue" = "Transparent"
		}
		ZWrite off
		cull off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surface Standard fullforwardshadows alpha:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		//Variables
		fixed4 _Tint;
		sampler2D _AlbedoMap;
		bool _UseAlbedo;
		bool _UseAlpha;

		sampler2D _MetallicMap;
		bool _UseMetallic;

		sampler2D _NormalMap;
		bool _UseNormal;

		sampler2D _HeightMap;
		float _HeightIntensity;
		bool _UseHeight;

		sampler2D _OcclusionMap;
		float _OcclusionIntensity;
		bool _UseOcclusion;

		sampler2D _EmissionMap;
		bool _UseEmission;

		//Structs
		struct Input
		{
			float2 uv_AlbedoMap;
			float2 uv_MetallicMap;
			float2 uv_NormalMap;
			float2 uv_HeightMap;
			float2 uv_OcclusionMap;
			float2 uv_EmissionMap;

			float3 viewDir;
		};

		void surface(Input IN, inout SurfaceOutputStandard o)
		{
			float2 heightOffset = float2(0, 0);

			if (_UseHeight)
			{
				// Heightmap gives an offset to the UVs
				float value = tex2D(_HeightMap, IN.uv_HeightMap).rgb;
				heightOffset = ParallaxOffset(value, _HeightIntensity, IN.viewDir);
			}

			o.Alpha = _Tint.a;
			
			if (_UseAlbedo)
			{
				// Albedo comes from a albedo map tinted by color
				fixed4 color = tex2D(_AlbedoMap, IN.uv_AlbedoMap + heightOffset);
				o.Albedo = color.rgb * _Tint.rgb;

				if (_UseAlpha)
				{
					// Alpha comes from a albedo map
					o.Alpha *= color.a;
				}
			}

			if (_UseMetallic)
			{
				// Metallic and smoothness come from a metallic map
				float4 metallicData = tex2D(_MetallicMap, IN.uv_MetallicMap + heightOffset);
				o.Metallic = metallicData.r;
				o.Smoothness = metallicData.a;
			}

			if (_UseNormal)
			{
				// Normal comes from a normal map
				o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap + heightOffset));
			}

			if (_UseOcclusion)
			{
				// Occlusion comes from the occlusion map
				float4 occlusionData = tex2D(_OcclusionMap, IN.uv_OcclusionMap + heightOffset);
				o.Occlusion = occlusionData.rgb * _OcclusionIntensity;
			}

			if (_UseEmission)
			{
				// Emission comes from the emission map
				o.Emission = tex2D(_EmissionMap, IN.uv_EmissionMap + heightOffset);
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}