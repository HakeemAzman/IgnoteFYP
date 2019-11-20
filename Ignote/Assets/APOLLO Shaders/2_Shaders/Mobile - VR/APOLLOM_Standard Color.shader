
Shader "APOLLO/Mobile - VR/Color" {
Properties {
	  _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
	   _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
	 _Blur ("Roughness",Range(0.01,1)) = 10
	 _SpecularPower ("Specular Power", Range (0.0, 1)) = 0.0

	 [MaterialToggle] Metal ("Calculate Metalness", Float) = 0


	 _ReflectPower ("Metal Power", Range (0.0, 1)) = 0.1


}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200


CGPROGRAM
#pragma surface surf  BlinnPhong fullforwardshadows
#pragma target 3.0
#pragma multi_compile _ METAL_ON




samplerCUBE _Cube;
fixed4 _Color;



fixed _Blur;
float _NormP;
float _AmbientC;
float _LightAdd;
float _PrPower;
float _AmbientLight;
float _Con;
float _ReflectPower;
float _SpecularPower;


struct Input {

	float3 worldRefl;
	INTERNAL_DATA
};





void surf (Input IN, inout SurfaceOutput o) {







 o.Albedo = _Color;


  
float3 reflectedDir = WorldReflectionVector (IN, o.Normal);
fixed4 hdrReflection = texCUBElod (_Cube, float4(reflectedDir,_Blur*10));



#if defined(METAL_ON)



	/////Calculate Metallic shading

	half3 mat = 1/1-1*_ReflectPower;

	half3 met = hdrReflection.rgb*_Color*_ReflectPower;


	o.Albedo *= met*mat*_ReflectPower;
	o.Albedo += mat+met;
	 o.Albedo *= _Color;

	// LB Lighting
    o.Albedo += _LightAdd;

	/////Calculate Specular shading
	o.Albedo += hdrReflection.rgb*hdrReflection.a*_Color.rgb*_SpecularPower;


	o.Emission -= o.Albedo* _Con*0.1;


	#else 

	// LB Lighting
    o.Albedo += _LightAdd;

	/////Calculate only Specular shading
	o.Albedo += hdrReflection.rgb*hdrReflection.a*_SpecularPower;



	o.Emission -= o.Albedo* _Con*0.1;

    #endif


	o.Alpha = 1;



}

 

ENDCG
}

FallBack "Legacy Shaders/Reflective/Bumped Specular"
}
