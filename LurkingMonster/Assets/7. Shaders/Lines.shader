Shader "Custom/Lines"
{
    Properties
    { 
        [KeywordEnum(objectSpace, worldSpace, screenSpace)] _SpaceCalculation("Space", float) = 0
        [KeywordEnum(X axis, Y axis, Z axis)] _LineAxis("Lines Axis:", Float) = 0
    
        [Space]
        _Center("Origin Position", vector) = (0,0,0,0)
    
        [Space]
        _MainColor("Main color", Color) = (.83, .5, .13, 1)
        _LineColor("Line Color", Color) = (1, 1, 1, 1)
        
        [Space]
        _DistanceToLine("Distance to line", float) = 1
        _DistanceBetweenLines("Distance between lines", float) = 2
        
        [Space]
        _LineSize("Line Size", float) = 1
        
        [Space]
        [MaterialToggle] _LimitLines("Limited", float) = 0
        _LineCount("Lines Amount", Int) = 2
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
	        float _SpaceCalculation;
	        
            float _LineAxis;
            
			float4 _MainColor;
			float4 _LineColor;
        
            float4 _Center;
            
            float _DistanceToLine;
            float _DistanceBetweenLines;
            float _LineSize;
            
            bool _LimitLines;
            int _LineCount;
            
            // Functions
            float4 DrawLines(float4 objectPosition);
            float CalculateDistance(float4 objectPosition);
            float PingPong(float value, float length);
        
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
	            
	            float4 position;
	            switch(_SpaceCalculation)
	            {
	                case 0: // objectSpace
	                    position = fInput.objectPosition;
	                    break;
	                case 1: // worldSpace
	                    position = fInput.worldPosition;
	                    break;
	                case 2: // screenSpace
	                    position = fInput.screenPosition;
	                    break;
	                default:
	                    position = fInput.objectPosition;
	                    break;
	            }
	            
	            fOutput.color = DrawLines(position);
	            
	            return fOutput;
	        }
	        
	        float4 DrawLines(float4 objectPosition)
	        {
	            float distance = CalculateDistance(objectPosition);
	        
	            if (distance < _DistanceToLine)
	            {
	                return _MainColor;
	            }
	            
	            if (distance <= _DistanceToLine + _LineSize)
	            {
	                return _LineColor;
	            }
	            
	            distance -= _DistanceToLine + _LineSize; // Distance is now distance from end of first circle
	            
	            float patternLength = _DistanceBetweenLines + _LineSize; 
	            
	            int circleNumber = distance / patternLength + 1; // Get the number of the current circle
	            distance %= patternLength; // Make sure the pattern repeats itself
	            
	            if (_LimitLines && circleNumber >= _LineCount)
	            {
	                return _MainColor;
	            }
	            
	            if (distance < _DistanceBetweenLines)
	            {
	                return _MainColor;
	            }
	            
	            if (distance <= _DistanceBetweenLines + _LineSize)
	            {
	                return _LineColor;
	            }
	            
	            return _MainColor;
	        }
	        
	        float CalculateDistance(float4 objectPosition)
	        {
	            float distance;
	            
	            switch(_LineAxis)
	            {
	                case 0: // X axis
	                    distance = objectPosition.x - _Center.x;
	                    break;
	                case 1: // Y axis
	                    distance = objectPosition.y - _Center.y;
	                    break;
	                case 2: // Z axis
	                    distance = objectPosition.z - _Center.z;
	                    break;
	                default:
	                    distance = 0;
	                    break;
	            }
	            
	            distance = abs(distance);
	            return distance;
	        }
            ENDCG
        }
    }
}
