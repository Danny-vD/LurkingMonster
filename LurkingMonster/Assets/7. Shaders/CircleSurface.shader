Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        [Header(PBR settings)]
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Space]
        [KeywordEnum(X axis, Y axis, Z axis)] _CircleAxis("Circle Axis:", Float) = 0
    
        [Space]
        _Center("Origin Position", vector) = (0,0,0,0)
    
        [Space]
        _MainColor("Main color", Color) = (.83, .5, .13, 1)
        _CircleColor("Circle Color", Color) = (1, 1, 1, 1)
        
        [Space]
        _distanceToCircle("Distance to Circle", float) = 1
        _distanceBetweenCircles("Distance between circles", float) = 2
        
        [Space]
        _CircleSize("Circle Size", float) = 1
        
        [Space]
        [MaterialToggle] _LimitCircles("Limited", float) = 0
        [Integer] _CircleCount("Circle Amount", Int) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        #pragma vertex vertex

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        
        //Variables
        half _Glossiness;
        half _Metallic;
        
        float _CircleAxis;
        
        float4 _MainColor;
        float4 _CircleColor;
    
        float4 _Center;
        
        float _distanceToCircle;
        float _distanceBetweenCircles;
        float _CircleSize;
        
        bool _LimitCircles;
        int _CircleCount;
        
        //Functions
        float CalculateDistance(float4 objectPosition);
        float4 DrawCircles(float4 objectPosition);
        
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
            // Albedo depends on the distance to the center
            o.Albedo = DrawCircles(IN.objectPosition);
           
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        
        float4 DrawCircles(float4 objectPosition)
        {
            float distance = CalculateDistance(objectPosition);
             
            if (distance < _distanceToCircle)
            {
                return _MainColor;
            }
            
            if (distance <= _distanceToCircle + _CircleSize)
            {
                return _CircleColor;
            }
            
            distance -= _distanceToCircle + _CircleSize; // Distance is now distance from end of first circle
            
            float patternLength = _distanceBetweenCircles + _CircleSize; 
            
            int circleNumber = distance / patternLength + 1; // Get the number of the current circle
            distance %= patternLength; // Make sure the pattern repeats itself
            
            if (_LimitCircles && circleNumber >= _CircleCount)
            {
                return _MainColor;
            }
            
            if (distance < _distanceBetweenCircles)
            {
                return _MainColor;
            }
            
            if (distance <= _distanceBetweenCircles + _CircleSize)
	        {
	            return _CircleColor;
	        }
	        
	        return _MainColor;
	    }
	        
	    float CalculateDistance(float4 objectPosition)
	    {
	        float distance;
	        
	        switch(_CircleAxis)
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
