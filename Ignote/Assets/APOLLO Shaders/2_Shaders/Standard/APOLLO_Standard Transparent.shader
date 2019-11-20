
Shader "APOLLO/Standard/Transparent" {
Properties {
	  _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}

	 _Blur ("Roughness",Range(0.01,1)) = 10
	 _SpecularPower ("Specular Power", Range (0.0, 1)) = 0.0

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
Tags {"Queue"="Transparent"  "RenderType"="Transparent"}
	LOD 200

	    // Render into depth buffer only
        Pass {
        ColorMask 0
    }
    // Render normally

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask RGB

CGPROGRAM
#pragma surface surf BlinnPhong  alpha:blend fullforwardshadows addshadow 
#pragma target 3.0
#pragma multi_compile  METAL_ON  PROMETHEUS_ON   AO_ON  CR_ON  PM_ON  SP_ON



sampler2D _SpeTex;
sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _AOTex;
samplerCUBE _Cube;
sampler2D _ParallaxMap;
sampler2D _EmTex;
sampler2D _MetTex;
fixed4 _Color;
half _Shininess;
float _Parallax;
float _ReflectPower;
fixed _Blur;
float _RimPower;
float _NormP;
float _NormSm;
float _AmbientC;
float _LightAdd;
float _PrPower;
float _AmbientLight;
float _Con;
float _EmissionPower;
float _SpecularPower;

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






half4 LightingCSLambert (SurfaceOutput s, half3 lightDir, half atten) 
{
	fixed diff = max (0, dot (s.Normal, lightDir));

	fixed4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten * 2);
	
	//shadow colorization
	c.rgb *= _Color.xyz * max(0.0,(1.0-(diff*atten*2))) * s.Alpha;
	c.a = s.Alpha;
	return c;
}



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






	 half rim = 1.5 - saturate(dot (normalize(IN.viewDir), o.Normal));








#if defined(METAL_ON)


	/////Calculate Metallic shading

	half3 mat = m.a/m.a-m.a*_ReflectPower;

	half3 met = hdrReflection.rgb*tex.rgb*m.a*_ReflectPower;

	o.Albedo *= met*mat*_ReflectPower;
	o.Albedo += tex.rgb*mat+met;


	// LB Lighting
    o.Albedo += _LightAdd;

	/////Calculate Specular shading
	o.Albedo += hdrReflection.rgb*hdrReflection.a*tex.a*s.a* pow (rim, _RimPower)*_SpecularPower;


	o.Emission -= o.Albedo* _Con*0.1;


	#else 


	// LB Lighting
    o.Albedo += _LightAdd;

	/////Calculate only Specular shading
	o.Albedo += hdrReflection.rgb*hdrReflection.a*tex.a*s.a* pow (rim, _RimPower)*_SpecularPower;



	o.Emission -= o.Albedo* _Con*0.1;

    #endif




    	     #if defined(SP_ON)
o.Gloss = s.a*_Shininess;
o.Specular = tex.a*_Shininess;


 #endif







	 
 #if defined(AO_ON)  

 o.Albedo *= d.a+o.Albedo-o.Emission/4;
  o.Albedo *= 0.5;
  o.Alpha = tex.a;

 #else
 o.Alpha = tex.a;
 o.Albedo *= 0.7;
 #endif

}

 

ENDCG
}

FallBack "VertexLit"
}
