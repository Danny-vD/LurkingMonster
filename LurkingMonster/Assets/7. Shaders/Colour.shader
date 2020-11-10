Shader "Custom/Colour"
{
    Properties
    {
        [KeywordEnum(ObjectPosition, WorldPosition, ScreenPosition, ClipPosition, Normal, UV, VertexColor, CameraDistance, Texture, ColorProperty)] _colorType("Get color from:", Float) = 0
        
        _Color("Tint", Color) = (.83, .13, .13, 1)
    	[Toggle] _EnableShadows("Enable Shadows", Int) = 1
    	
    	[Header(how far an object has to be to have distance 1)]
        _DistanceFactor ("DistanceFactor", Float) = 20
    	
        _MainTex("Texture", 2D) = "black"
    }
    
    SubShader
    {
        Tags
        {
        	"RenderType" = "Opaque"
        	"LightMode" = "ForwardBase"
        }    
    
        Pass
        {
            CGPROGRAM

            #include "UnityCG.cginc"
			#include "AutoLight.cginc"
            
            #pragma vertex vertex
			#pragma fragment fragment
			#pragma multi_compile_fwdbase // For shadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0
        
	        ///Variables
            float _colorType;
            
            float _DistanceFactor;

			bool _EnableShadows;
            
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

            	// Used for shadows
            	LIGHTING_COORDS(4,5)
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
	            o.clipPosition = mul(UNITY_MATRIX_P, o.screenPosition); // Will be converted to NDC before fragment shader
	            
	            o.normal = v.normal;
	            o.texCoord = v.texCoord;
                o.color = v.color;

	        	// Used for shadows
				TRANSFER_VERTEX_TO_FRAGMENT(o);
	        	
	            return o;
	        }
	        
	        fragmentOutput fragment(vertexOutput i)
	        {
	            fragmentOutput o;
	            
	            switch(_colorType)
	            {
	                case 0: // objectPosition
	                    o.color = i.objectPosition;
	                    break;
	                case 1: // worldPosition
	                    o.color = i.worldPosition;
	                    break;
	                case 2: // screenPosition
	                    o.color = i.screenPosition;
	            	
	                    // Some basic mathematics (explained under cameraDistance) to make the Z depend on distance to camera
	                    float Zpos = saturate(-i.screenPosition.z / _DistanceFactor);
	                    o.color.z = Zpos;
	                    break;
	                case 3: // clipPosition
	                    o.color = i.clipPosition;
	                    break;
	                case 4: // normal
	                    o.color = i.normal;
	                    break;
	                case 5: // uv
	                    o.color = i.texCoord;
	                    break;
                    case 6: // vertexColour
                        o.color = i.color;
	                    break;
	                case 7: // cameraDistance
	                    float distance = i.screenPosition.z; // distance = [-∞, 0] so -distance = [0, ∞]
	                    float modifiedDistance = 1 - saturate(-distance / _DistanceFactor); // Saturate clamps to [0, 1] range
	                    o.color = float4(modifiedDistance, modifiedDistance, modifiedDistance, 1);
	                    break;
	                case 8: // texture
	                    o.color = tex2D(_MainTex, (i.texCoord.xy * _MainTex_ST.xy + _MainTex_ST.zw) % 1);
	                    break;
	                case 9: // color property
	                    o.color = _Color;
	                    break;
	                default:
	                    o.color = float4(1, 0, 1, 1); // Just magenta, to signify something is wrong
	                    break;
	            }

				float shadow = _EnableShadows ? LIGHT_ATTENUATION(i) : 1; // Sample the shadow map if shadows are enabled
	        	o.color.rgb *= shadow;
	        	
	            return o;
	        }
            ENDCG
        }
    }
    FallBack "VertexLit"
}
