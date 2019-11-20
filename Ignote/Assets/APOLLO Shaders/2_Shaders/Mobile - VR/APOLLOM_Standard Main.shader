
Shader "APOLLO/Mobile - VR/Main" {
Properties {
	  _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}

	 _Blur ("Roughness",Range(0.01,1)) = 10
	 _SpecularPower ("Specular Power", Range (0.0, 1)) = 0.0

	 [MaterialToggle] Metal ("Calculate Metalness", Float) = 0


	 _ReflectPower ("Metal Power", Range (0.0, 1)) = 0.1


	_MainTex ("Base (RGB) ", 2D) = "white" { }

	_SpeTex ("Specular Map (A)", 2D) = "white" { }
	_MetTex ("Metal Map (A)", 2D) = "white" { }



	[MaterialToggle] Sp ("Use Specular Highlights", Float) = 0
	_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1.)
	_Shininess ("Shininess / SSR", Range (0.01, 1)) = 0.078125


	_NormP ("Normalmap Power",Range(0.01,3)) = 1

	_BumpMap ("Normalmap", 2D) = "bump" { }


}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200


CGPROGRAM
#pragma surface surf  BlinnPhong fullforwardshadows
#pragma target 3.0
#pragma multi_compile _ METAL_ON
#pragma multi_compile _ SP_ON


sampler2D _SpeTex;
sampler2D _MainTex;
sampler2D _MetTex;

sampler2D _BumpMap;
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
half _Shininess;


struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_SpeTex;
	float2 uv_MetTex;

	float3 worldRefl;
	INTERNAL_DATA
};





void surf (Input IN, inout SurfaceOutput o) {




fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
fixed4 s = tex2D(_SpeTex, IN.uv_SpeTex);
fixed4 m = tex2D(_MetTex, IN.uv_MetTex);



 o.Albedo = tex.rgb;







fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)); 
normal.z = normal.z/_NormP; o.Normal = normalize(normal); 
  
float3 reflectedDir = WorldReflectionVector (IN, o.Normal);
fixed4 hdrReflection = texCUBElod (_Cube, float4(reflectedDir,_Blur*10/s.a));









#if defined(METAL_ON)
	

	/////Calculate Metallic shading

	half3 mat = m.a/m.a-m.a*_ReflectPower;

	half3 met = hdrReflection.rgb*tex.rgb*m.a*_ReflectPower;

	o.Albedo *= met*mat*_ReflectPower;
	o.Albedo += tex.rgb*mat+met;
	o.Albedo *= tex.rgb;


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

	    	     #if defined(SP_ON)
o.Gloss = s.a*_Shininess;
o.Specular = tex.a*_Shininess;


 #endif


}

 

ENDCG
}

FallBack "Legacy Shaders/Reflective/Bumped Specular"
}
