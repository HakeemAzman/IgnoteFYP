
Shader "APOLLO/RTAL/Standard" {
Properties {

     _Saturation("Ambient Saturation",Range(0.0,1)) = 0.5
	 _LightP("Ambient Power",Range(0.0,5)) = 2




	 _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
	


	 _Blur ("Roughness",Range(0.01,1)) = 0
	 _SpecularPower ("Specular Power", Range (0.0, 1)) = 0.5

	 [MaterialToggle] Metal ("Calculate Metalness", Float) = 0
	 _ReflectPower ("Metal Power", Range (0.0, 1)) = 0.1



	 _RimPower ("R Fresnel", Range(0,3.0)) = 1.0

	 	

    _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
	_MainTex ("Main Texture (Albedo)", 2D) = "white" { }

	[MaterialToggle] Sp ("Use Specular Highlights", Float) = 0
	_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1.)
	_Shininess ("Shininess / SSR", Range (0.01, 1)) = 0.078125
	_SpeTex ("Specularity Map (A)", 2D) = "white" { }
	_MetTex ("Metalic Map (A)", 2D) = "white" { }


	[MaterialToggle] Ao ("Use Ambient Occlusion", Float) = 0
    _AOTex ("AO Map  (A)", 2D) = "white" { }

	_NormP ("Normalmap Power",Range(0.01,3)) = 1
	_BumpMap ("Normalmap", 2D) = "bump" { }

	_EmissionPower ("Emission Power",Range(0,20)) = 0
	_EmTex ("Emission Map  (A)", 2D) = "white" { }

	[MaterialToggle] PM ("Use Parallax Map", Float) = 0
	_Parallax ("Height", Range (0.01, 0.08)) = 0.02
	_ParallaxMap ("Heightmap (A)", 2D) = "black" {}


	[MaterialToggle] Prometheus ("Use Prometheus", Float) = 0
	_PrPower("Prometheus Value",Range(0,1)) = 1

}
SubShader {
	Tags { "RenderType"="Opaque"}
	LOD 200


CGPROGRAM
#pragma surface surf BlinnPhong  fullforwardshadows noforwardadd noambient
#pragma target 3.0
#pragma multi_compile _ METAL_ON  PROMETHEUS_ON  AO_ON  CR_ON  PM_ON  SP_ON 



sampler2D _SpeTex;
sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _AOTex;
samplerCUBE _Cube;
sampler2D _ParallaxMap;
sampler2D _EmTex;
sampler2D _MetTex;
fixed4 _Color;

half _Saturation;
half _LightP;

half _Shininess;
half _Parallax;
half _ReflectPower;
half _Blur;
half _RimPower;
half _NormP;
half _NormSm;
half _AmbientC;
half _LightAdd;
half _PrPower;
half _AmbientLight;
half _Con;
half _EmissionPower;
half _SpecularPower;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_SpeTex;
	float2 uv_AOTex;
	float2 uv_EmTex;
	float2 uv_MetTex;
	float3 worldRefl;
	float3 viewDir;
	INTERNAL_DATA
};










void surf (Input IN, inout SurfaceOutput o) {

    #if defined(PM_ON)

	half h = tex2D (_ParallaxMap, IN.uv_BumpMap).w;
	float2 offset = ParallaxOffset (h, _Parallax, IN.viewDir);
	IN.uv_MainTex += offset;
	IN.uv_BumpMap += offset;

		#endif


	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex)*_Color;
	fixed4 s = tex2D(_SpeTex, IN.uv_SpeTex);
	fixed4 d= tex2D(_AOTex, IN.uv_AOTex);
	fixed4 e = tex2D(_EmTex, IN.uv_EmTex);
	fixed4 m = tex2D(_MetTex, IN.uv_MetTex);




 o.Albedo = tex.rgb*tex.a;

 o.Emission =  e.rgb*_Color*_EmissionPower;



       #if defined(PROMETHEUS_ON)

      o.Albedo *= o.Albedo*_PrPower*3;

      #endif


     

    fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)); 
    normal.z = normal.z/_NormP; o.Normal = normalize(normal); 



    float3 reflectedDir = WorldReflectionVector (IN, o.Normal);
    fixed4 hdrReflection = texCUBElod (_Cube, float4(reflectedDir,_Blur*10/s.a));

    //RTAL HDRI
    fixed4 hdrReflection2 = texCUBElod (_Cube, float4(reflectedDir+2,1000));





	 half rim = 1.5 - saturate(dot (normalize(IN.viewDir), o.Normal));


	//Saturation

float greyscale = dot(hdrReflection2.rgb, fixed3(.222, .707, .071));  // Convert to greyscale numbers with magic luminance numbers
hdrReflection2.rgb = lerp(float3(greyscale, greyscale, greyscale), hdrReflection2.rgb, _Saturation);


	 //Real Time Ambient Lighting
     o.Emission += hdrReflection2.rgb*_LightP*tex.rgb;






#if defined(METAL_ON)


	/////Calculate Metallic shading

	half3 mat = m.a/m.a-m.a*_ReflectPower;

	half3 met = hdrReflection.rgb*tex.rgb*m.a*_ReflectPower;

	o.Albedo *= met*mat*_ReflectPower;
	o.Albedo += tex.rgb*mat+met;



	// LB Lighting
    o.Albedo += _LightAdd;

	/////Calculate Specular shading
	o.Emission += hdrReflection.rgb*hdrReflection.a*tex.a*s.a* pow (rim, _RimPower)*_SpecularPower;



	o.Emission -= o.Albedo* _Con*0.1;


	#else 

	// LB Lighting
    o.Albedo += _LightAdd;

	/////Calculate only Specular shading
	o.Emission += hdrReflection.rgb*hdrReflection.a*tex.a*s.a* pow (rim, _RimPower)*_SpecularPower;



	o.Emission -= o.Albedo* _Con*0.1;

    #endif





 #if defined(SP_ON)
o.Gloss = s.a*_Shininess;
o.Specular = tex.a*_Shininess;


 #endif









o.Alpha = 1;

	 
 #if defined(AO_ON)  

 o.Albedo *= d.a+o.Albedo-o.Emission/4;
  o.Albedo *= 0.5;
 #else
 o.Albedo *= 0.7;
 #endif

}

 

ENDCG
}

FallBack "Legacy Shaders/Reflective/Bumped Specular"
}
