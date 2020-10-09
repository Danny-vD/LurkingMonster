Shader "Custom/LinesLit"
{
    Properties
    {
        [Header(PBR settings)]
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Space]
        [KeywordEnum(X axis, Y axis, Z axis)] _LinesAxis("Lines Axis:", Float) = 0
    
        [Space]
        _Center("Origin Position", vector) = (0,0,0,0)
    
        [Space]
        _MainColor("Main color", Color) = (.83, .5, .13, 1)
        _LineColor("Line Color", Color) = (1, 1, 1, 1)
        
        [Space]
        _distanceToLine("Distance to Line", float) = 1
        _distanceBetweenLines("Distance between circles", float) = 2
        
        [Space]
        _LineSize("Line Size", float) = 1
        
        [Space]
        [MaterialToggle] _LimitLines("Limited", float) = 0
        [Integer] _LineCount("Line Amount", Int) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        
        Blend off

        CGPROGRAM
        
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha
        #pragma vertex vertex

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        
        //Variables
        half _Glossiness;
        half _Metallic;
        
        float _LinesAxis;
        
        float4 _MainColor;
        float4 _LineColor;
    
        float4 _Center;
        
        float _distanceToLine;
        float _distanceBetweenLines;
        float _LineSize;
        
        bool _LimitLines;
        int _LineCount;
        
        //Functions
        float CalculateDistance(float4 objectPosition);
        float4 DrawLines(float4 objectPosition);
        
        // Structs
        struct Input
        {
		    float4 objectPosition : TEXCOORD1;
		    float4 clipPosition: SV_POSITION;
        };

	    void vertex(inout appdata_full vInput, out Input vOutput)
	    {
	        vOutput.objectPosition = vInput.vertex;
	        
	        vOutput.clipPosition = UnityObjectToClipPos(vOutput.objectPosition);
	    }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 color = DrawLines(IN.objectPosition);
        
            // Albedo depends on the distance to the center
            o.Albedo = color.rgb;
           
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            
            o.Alpha = color.a;
        }
        
        float4 DrawLines(float4 objectPosition)
        {
            float distance = CalculateDistance(objectPosition);
             
            if (distance < _distanceToLine)
            {
                return _MainColor;
            }
            
            if (distance <= _distanceToLine + _LineSize)
            {
                return _LineColor;
            }
            
            distance -= _distanceToLine + _LineSize; // Distance is now distance from end of first circle
            
            float patternLength = _distanceBetweenLines + _LineSize; 
            
            int lineNumber = distance / patternLength + 1; // Get the number of the current circle
            distance %= patternLength; // Make sure the pattern repeats itself
            
            if (_LimitLines && lineNumber >= _LineCount)
            {
                return _MainColor;
            }
            
            if (distance < _distanceBetweenLines)
            {
                return _MainColor;
            }
            
            if (distance <= _distanceBetweenLines + _LineSize)
	        {
	            return _LineColor;
	        }
	        
	        return _MainColor;
	    }
	        
	    float CalculateDistance(float4 objectPosition)
	    {
	        float distance;
	        
	        switch(_LinesAxis)
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
    FallBack "Diffuse"
}
