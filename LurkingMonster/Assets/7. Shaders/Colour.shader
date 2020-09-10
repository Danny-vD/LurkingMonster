Shader "Custom/Colour"
{
    Properties
    {
        [KeywordEnum(ObjectPosition, WorldPosition, ScreenPosition, ClipPosition, normal, UV)] _colorType("Get color from:", Float) = 0
    }
    
    SubShader
    {
        Blend srcAlpha OneMinusSrcAlpha
        ZTest Less
    
        Tags
        {
            "Queue" = "Geometry"
        }    
    
        Pass
        {
            CGPROGRAM
            
            #pragma vertex vertex
			#pragma fragment fragment
            
            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0
        
	        ///Variables
            float _colorType;
        
            ///Structs
            struct VertexInput
            {
                float4 vertex : POSITION;
				float4 texCoord : TEXCOORD0;
				float4 normal : NORMAL;
            };
            
            struct vertexOutput
			{
			    float4 normal : NORMAL;
			    
			    float4 objectPosition : TEXCOORD1;
			    float4 worldPosition : TEXCOORD2;
				float4 screenPosition : TEXCOORD3;
				float4 clipPosition : SV_POSITION;
				
				float4 texCoord : TEXCOORD0;
			};
	
	        struct fragmentOutput
	        {
	            float4 color : COLOR;
	        };
	        
	        vertexOutput vertex(VertexInput vInput)
	        {
	            // Apply scaling and offset
	            vInput.texCoord.xy = vInput.texCoord.xy;// * _MainTex_ST.xy + _MainTex_ST.zw;
	        
	            vertexOutput vOutput;
	         
	            vOutput.objectPosition = vInput.vertex;
	            vOutput.worldPosition = mul(UNITY_MATRIX_M, vOutput.objectPosition);
	            vOutput.screenPosition = mul(UNITY_MATRIX_V, vOutput.worldPosition);
	            vOutput.clipPosition = mul(UNITY_MATRIX_P, vOutput.screenPosition);
	            
	            vOutput.normal = vInput.normal;
	            
	            vOutput.texCoord = vInput.texCoord;
	         
	            return vOutput;  
	        }
	        
	        fragmentOutput fragment(vertexOutput fInput)
	        {
	            fragmentOutput fOutput;
	            
	            switch(_colorType)
	            {
	                case 0: // objectPosition
	                    fOutput.color = fInput.objectPosition;
	                    break;
	                case 1: // worldPosition
	                    fOutput.color = fInput.worldPosition;
	                    break;
	                case 2: // screenPosition
	                    fOutput.color = fInput.screenPosition;
	                    break;
	                case 3: // clipPosition
	                    fOutput.color = fInput.clipPosition;
	                    break;
	                case 4: // normal
	                    fOutput.color = fInput.normal;
	                    break;
	                case 5: // uv
	                    fOutput.color = fInput.texCoord;
	                    break;
	                default:
	                    fOutput.color = float4(1, 0, 1, 1);
	                    break;
	            }
	            
	            return fOutput;
	        }
            ENDCG
        }
    }
}