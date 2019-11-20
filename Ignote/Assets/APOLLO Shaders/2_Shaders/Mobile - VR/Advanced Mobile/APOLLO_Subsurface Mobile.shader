
//This shader is a creation of Rispat Momit 2019




Shader "APOLLO/Mobile - VR/Advanced/Subsurface" {
Properties {

    _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
	 
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" { }

    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_ShininessP ("Shininess Power", Range (0.0, 1)) = 0.078125

	_SpeTex ("Specular Map Gloss (A)", 2D) = "white" { }
    
	_Blur ("reflection blur",Range(0.01,10)) = 1
	_ReflectPower ("Reflection Power", Range (0.0, 2)) = 0.5
	_RimPower ("Fresnel Power", Range(0,3.0)) = 1.0

	_TranslucencyColor ("Translucency Color", Color) = (0.73,0.85,0.41,1)  
    _RimPower2 ("Translucency Fresnel", Range(0,1.0)) = 0.5
 
	_TrPower ("Translucency Power", Range(0,10)) = 1.0

	_TranslucencyViewDependency ("View dependency", Range(0,1)) = 0
	_ShadowStrength("Shadow Strength", Range(0,1)) = 1
    
	[MaterialToggle] Ao ("Ambient Occlusion", Float) = 0


	_NormP ("Normalmap Power",Range(0.01,3)) = 1
	_BumpMap ("Normalmap", 2D) = "bump" { }
    

	[MaterialToggle] PM ("Parallax Map", Float) = 0
	_Parallax ("Height", Range (0.0, 0.08)) = 0.02
	_ParallaxMap ("Heightmap (A)", 2D) = "black" {}



	[HideInInspector] _TreeInstanceColor ("TreeInstanceColor", Vector) = (1,1,1,1)
	[HideInInspector] _TreeInstanceScale ("TreeInstanceScale", Vector) = (1,1,1,1)
	[HideInInspector] _SquashAmount ("Squash", Float) = 1


}
SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	 LOD 400

 
CGPROGRAM
#pragma surface surf  TreeLeaf   vertex:TreeVertLeaf  alphatest:_Cutoff fullforwardshadows addshadow 
#include "UnityBuiltin3xTreeLibrary.cginc"


#pragma target 3.0
#pragma multi_compile _ AO_ON
#pragma multi_compile _ CR_ON
#pragma multi_compile _ PM_ON

sampler2D _SpeTex;
sampler2D _MainTex;
sampler2D _BumpMap;
float _AddLght;
samplerCUBE _Cube;
float _ShininessP;
float _Con;
half _Shininess;
float _ReflectPower;
fixed _Blur;
float _AOpower;
float _RimPower;
float _RimPower2;
float _NormP;
float _NormSm;
float _AmbientC;
float _TrPower;
float _RimTh;
float _LightAdd;

struct Input {
	float2 uv_MainTex;
   	fixed4 color : COLOR; 

	float3 worldRefl;
	float3 viewDir;
	INTERNAL_DATA
};



void surf (Input IN, inout LeafSurfaceOutput o) {






	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);

	fixed4 sa = tex2D(_SpeTex, IN.uv_MainTex);

 



    o.Albedo = tex.rgb;

    
  
    fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
    normal.z = normal.z/_NormP*2; o.Normal = normalize(normal); 

    // LB Lighting
    o.Albedo += _LightAdd;
          

	float3 reflectedDir = WorldReflectionVector (IN, o.Normal);

    fixed4 hdrReflection = texCUBElod (_Cube, float4(reflectedDir,_Blur/sa.a));


         

	 half rim = 1.5 - saturate(dot (normalize(IN.viewDir), o.Normal));
	 half rim2 = 1 - saturate(dot (normalize(IN.viewDir), o.Normal));
 
  	o.Emission -= o.Albedo* _Con*0.1;


	o.Albedo += hdrReflection.rgb*hdrReflection.a* pow (rim, _RimPower*2)*_ReflectPower;
	o.Albedo += _AddLght;
 
	o.Translucency =  tex.rgb*_TrPower* pow (rim2, _RimPower2)+_TranslucencyColor; 
    o.Translucency += o.Translucency+5;
        
	
    o.Alpha = tex.a;

     o.Gloss = _ShininessP*hdrReflection.rgb*hdrReflection.a*tex.a*_Shininess*20*sa.a*_Blur;

    o.Specular = _Shininess*sa.a+0.001 ;

  
     o.Albedo *= o.Albedo;
     o.Albedo *= 0.7;
  
   
 }

 

ENDCG

	
	
}

Dependency "BillboardShader" = "Hidden/Nature/Tree Creator Leaves Rendertex"

}