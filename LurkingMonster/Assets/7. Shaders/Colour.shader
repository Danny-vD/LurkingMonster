Shader "Custom/Colour"
{
    Properties
    {
        [KeywordEnum(ObjectPosition, WorldPosition, ScreenPosition, ClipPosition, Normal, UV, CameraDistance)] _colorType("Get color from:", Float) = 0
        _DistanceFactor ("DistanceFactor", Float) = 20 // DistanceFactor defines how far an object has to be to have distance 1
    }
    
    SubShader
    {
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
				float4 clipPosition: SV_POSITION;
				
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
	            vOutput.clipPosition = mul(UNITY_MATRIX_P, vOutput.screenPosition); // Will be converted to NDC before fragment shader
	            
	            vOutput.normal = vInput.normal;
	            
	            vOutput.texCoord = vInput.texCoord;
	         
	            return vOutput;  
	        }
	        
	        float _DistanceFactor;
	        
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
	                case 6: // cameraDistance
	                    float distance = fInput.screenPosition.z; // distance = [0, -1] so -distance = [0, 1]
	                    float modifiedDistance = 1 - saturate(-distance / _DistanceFactor);
	                    fOutput.color = float4(modifiedDistance, modifiedDistance, modifiedDistance, 1);
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