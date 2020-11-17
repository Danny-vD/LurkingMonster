Shader "Custom/Water"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		[Toggle]_UseAbsolute("Absolute", Int) = 0

		_WaveHeight("MaximumHeight", float) = 1
		_WaveSpeed("Speed", float) = 1
		_WaveLength("Length", float) = 1

		_Color("Tint", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
		}
		LOD 100

		Pass
		{
			CGPROGRAM
            #pragma vertex vertex
            #pragma fragment fragment

			//Constants
			uniform float PI = 3.14159265358979;
			
			//Variables
            sampler2D _MainTex;
			float4 _MainTex_ST; //tiling(xy) and offset(zw)

			float4 _Color;

			bool _UseAbsolute;
			
			float _WaveSpeed;
			float _WaveHeight;
			float _WaveLength;
            
            ///Structs
            struct VertexInput
            {
                float4 vertex : POSITION;
				float4 texCoord : TEXCOORD0;
				float4 normal : NORMAL;
                
                float4 color : COLOR;
            };
            
            struct vertexOutput
			{
			    float4 normal : NORMAL;
			    
			    float4 objectPosition : TEXCOORD1;
			    float4 worldPosition : TEXCOORD2;
				float4 screenPosition : TEXCOORD3;
				float4 clipPosition: SV_POSITION;
				
				float4 texCoord : TEXCOORD0;
                
                float4 color : COLOR;
			};
	
	        struct fragmentOutput
	        {
	            float4 color : COLOR;
	        };
	        
	        vertexOutput vertex(VertexInput v)
	        {
	            vertexOutput o;
	        	
	            o.objectPosition = v.vertex;
	            o.worldPosition = mul(UNITY_MATRIX_M, o.objectPosition);
	            o.screenPosition = mul(UNITY_MATRIX_V, o.worldPosition);

				float4 modifiedPosition = v.vertex;
				float speed = _Time.y * _WaveSpeed;
				float frequency = _WaveLength;

				if (_UseAbsolute)
				{
					modifiedPosition.y += abs(sin(frequency * o.worldPosition.x + speed)) * _WaveHeight;
	        		modifiedPosition.y += abs(sin(frequency * o.worldPosition.z + speed)) * _WaveHeight;	
				}
	        	else
	        	{
	        		modifiedPosition.y += sin(frequency * o.worldPosition.x + speed) * _WaveHeight;
	        		modifiedPosition.y += sin(frequency * o.worldPosition.z + speed) * _WaveHeight;
	        	}
	        	
				o.clipPosition = UnityObjectToClipPos(modifiedPosition);
	        	
	            o.normal = v.normal;
	            o.texCoord = v.texCoord;
                o.color = v.color;
	        	
	            return o;
	        }
	        
	        fragmentOutput fragment(vertexOutput i)
	        {
	            fragmentOutput o;

				float2 uv = i.texCoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
	       
	        	o.color = tex2D(_MainTex, uv) * _Color;
	        	
	            return o;
	        }
            
            ENDCG
		}
	}
}