Shader "Custom/Colour"
{
    Properties
    {
        [KeywordEnum(ObjectPosition, WorldPosition, ScreenPosition, ClipPosition, Normal, UV, VertexColor, CameraDistance, Texture, ColorProperty)] _colorType("Get color from:", Float) = 0
        
        _Color("Tint", Color) = (.83, .13, .13, 1)
        _DistanceFactor ("DistanceFactor", Float) = 20 // DistanceFactor defines how far an object has to be to have distance 1
        
        _MainTex("Texture", 2D) = "black"
    }
    
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
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
            
            float _DistanceFactor;
            
            sampler2D _MainTex;
			float4 _MainTex_ST; //tiling(xy) and offset(zw)
			
			float4 _Color;
        
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
	        
	        vertexOutput vertex(VertexInput vInput)
	        {
	            vertexOutput vOutput;
	         
	            vOutput.objectPosition = vInput.vertex;
	            vOutput.worldPosition = mul(UNITY_MATRIX_M, vOutput.objectPosition);
	            vOutput.screenPosition = mul(UNITY_MATRIX_V, vOutput.worldPosition);
	            vOutput.clipPosition = mul(UNITY_MATRIX_P, vOutput.screenPosition); // Will be converted to NDC before fragment shader
	            
	            vOutput.normal = vInput.normal;
	            vOutput.texCoord = vInput.texCoord;
                vOutput.color = vInput.color;
	         
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
	                    
	                    // Some basic mathematics (explained under cameraDistance) to make the Z depend on distance to camera
	                    float Zpos = saturate(-fInput.screenPosition.z / _DistanceFactor);
	                    fOutput.color.z = Zpos;
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
                    case 6: // vertexColour
                        fOutput.color = fInput.color;
	                    break;
	                case 7: // cameraDistance
	                    float distance = fInput.screenPosition.z; // distance = [-∞, 0] so -distance = [0, ∞]
	                    float modifiedDistance = 1 - saturate(-distance / _DistanceFactor); // Saturate clamps to [0, 1] range
	                    fOutput.color = float4(modifiedDistance, modifiedDistance, modifiedDistance, 1);
	                    break;
	                case 8: // texture
	                    fOutput.color = tex2D(_MainTex, (fInput.texCoord.xy * _MainTex_ST.xy + _MainTex_ST.zw) % 1);
	                    break;
	                case 9: // color property
	                    fOutput.color = _Color;
	                    break;
	                default:
	                    fOutput.color = float4(1, 0, 1, 1); // Just magenta, to signify something is wrong
	                    break;
	            }
	            
	            return fOutput;
	        }
            ENDCG
        }
    }
}
