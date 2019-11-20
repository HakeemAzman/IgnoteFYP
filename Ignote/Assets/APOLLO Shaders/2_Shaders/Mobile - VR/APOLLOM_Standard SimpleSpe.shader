
Shader "APOLLO/Mobile - VR/Simple+SpecMap" {
Properties {
	  _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}

	 _Blur ("Roughness",Range(0.01,1)) = 10
	 _SpecularPower ("Specular Power", Range (0.0, 1)) = 0.0

	 [MaterialToggle] Metal ("Calculate Metalness", Float) = 0


	 _ReflectPower ("Metal Power", Range (0.0, 1)) = 0.1


	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" { }

	_SpeTex ("Specular Map Gloss (A)", 2D) = "white" { }




}
SubShader {
	Tags { "RenderType"="Opaque" }
	//LOD 200


CGPROGRAM
#pragma surface surf  BlinnPhong fullforwardshadows
#pragma target 3.0
#pragma multi_compile _ METAL_ON


sampler2D _SpeTex;
sampler2D _MainTex;


samplerCUBE _Cube;



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
	float2 uv_MainTex;
	float2 uv_SpeTex;


	float3 worldRefl;
	INTERNAL_DATA
};





void surf (Input IN, inout SurfaceOutput o) {




fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
fixed4 s = tex2D(_SpeTex, IN.uv_SpeTex);



 o.Albedo = tex.rgb;
 o.Albedo += _LightAdd;



float3 reflectedDir = WorldReflectionVector (IN, o.Normal);
fixed4 hdrReflection = texCUBElod (_Cube, float4(reflectedDir,_Blur*10/s.a));



#if defined(METAL_ON)
	


	/////Calculate Metallic shading

	half3 mat = 1/1-1*_ReflectPower;

	half3 met = hdrReflection.rgb*tex.rgb*_ReflectPower;

	o.Albedo *= met*mat*_ReflectPower;
	o.Albedo += tex.rgb*mat+met;

	// LB Lighting
    o.Albedo += _LightAdd;

	/////Calculate Specular shading
	o.Albedo += hdrReflection.rgb*hdrReflection.a*tex.a*s.a*_SpecularPower;

	o.Emission -= o.Albedo* _Con*0.2;
	

	  
	#else 


	// LB Lighting
    o.Albedo += _LightAdd;
  
	/////Calculate Specular shading
	o.Albedo += hdrReflection.rgb*hdrReflection.a*tex.a*s.a*_SpecularPower;

	o.Emission -= o.Albedo* _Con*0.2;
	
    #endif


	o.Alpha = 1;



}

 

ENDCG
}

FallBack "Legacy Shaders/Reflective/Bumped Specular"
}
