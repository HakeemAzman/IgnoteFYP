
Shader "APOLLO/Advanced/Refractive" {



    Properties {


    _Trans ("Translucency",Range(0.1,1)) = 0.5
    _NormL ("Light in Normals",Range(0.1,1)) = 0.5
    _ModBr ("Light Power",Range(0.5,1)) = 1.0

    _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}

	 _Blur2 ("Roughness",Range(0.01,1)) = 0.01
	 _SpecularPower ("Specular Power", Range (0.0, 1)) = 0.5
	 _RimPower ("R Fresnel", Range(0,3.0)) = 0.0



        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _TrTex ("Transparency Map (A)", 2D) = "white" {}
        _Transparency ("Transparency", Range (0.0, 1)) = 1
         _RimPowerTr ("Transparency Fresnel", Range(-1,1.0)) = 0.0

        [MaterialToggle] Sp ("Use Specular Highlights", Float) = 0
	_SpecColor ("Specular Color", Color) = (0.1,0.1,0.1,1)
	_Shininess ("Shininess / SSR", Range (0.01, 1)) = 1


        _SpeTex ("Specularity Map (A)", 2D) = "white" { }

        _BumpMap ("Normalmap", 2D) = "bump" {}
        _DistAmt  ("Distortion", range (0,2)) = 0.5

        	_Blur ("Refraction Blur",Range(0,1)) = 0
        	_Gr ("Mesh Refraction",  Range (0, 1)) = 0.1


        	[MaterialToggle] Prometheus ("Use Prometheus", Float) = 0
	       _PrPower("Prometheus Value",Range(0,10)) = 5


    }
    SubShader {
        GrabPass { }
       
   
Tags { "Queue"="Transparent" "RenderType"="Transparent" }



        CGPROGRAM
        #pragma vertex vert
        #pragma surface surf  CSLambert  addshadow fullforwardshadows
        #include "UnityCG.cginc"
        #pragma target 3.0
        #pragma multi_compile _ PROMETHEUS_ON SP_ON TRS_ON

        float4 _Color;
        float _Transparency;
        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _GrabTexture;
        sampler2D _SpeTex;
        sampler2D _TrTex;
        samplerCUBE _Cube;
        float _RimPower;
        float _RimPowerTr;
        float _TranS;

        fixed4 _ShadowColor;
        float _SpecularPower;
        float _Con;
        float _LightAdd;
        float _PrPower;
        half _Shininess;

        float _DistAmt;
        float4 _GrabTexture_TexelSize;
        float _Gr;
        float _Trans;
        float _NormL;

        float _Blur2;
        float _Blur;
        float _ModBr;

        struct Input {
            float2 uv_MainTex;
            float2 uv_SpeTex;

            float2 uv_BumpMap;
            float4 proj : TEXCOORD;
            float3 viewDir;
            float3 worldRefl;
            INTERNAL_DATA

        };


   


fixed4 LightingCSLambert(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten) {



fixed NdotL = dot (s.Normal, lightDir);
fixed diff = NdotL * 10*_NormL + 10*_NormL + NdotL + 10*_Trans + 10*_Trans;

half3 h = normalize(lightDir + viewDir);
float NdotH = dot(s.Normal, h);

  #if defined(SP_ON)
fixed4 c;
float spec = pow(max(NdotH, 0), s.Specular*_Shininess * 500.0)*s.Albedo;
fixed3 specColor = _SpecColor.rgb *100* _LightColor0.rgb*_Shininess*s.Albedo;


c.rgb = s.Albedo * _LightColor0.rgb* (diff * atten)* _ModBr;
c.rgb *= atten * 0.1 + (specColor * spec);

#else

fixed4 c;
c.rgb = s.Albedo * _LightColor0.rgb* (diff * atten)* _ModBr;
c.rgb *= atten * 0.1;

#endif

c.a = s.Alpha ;
return c;
}





        void vert (inout appdata_full v, out Input o) {

        UNITY_INITIALIZE_OUTPUT(Input,o);
            float4 oPos = UnityObjectToClipPos(v.vertex);
            #if UNITY_UV_STARTS_AT_TOP
                float scale = -1.0;
            #else
                float scale = 1.0;
            #endif
            o.proj.xy = (float2(oPos.x, oPos.y*scale) + oPos.w) * 0.5;
            o.proj.zw = oPos.zw;
        }





        void surf (Input IN, inout SurfaceOutput o) {


         fixed4 s = tex2D(_SpeTex, IN.uv_SpeTex);
 
         half3 nor = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            half rim = 1 - saturate(dot (normalize(IN.viewDir), nor));
             half rim2 = 0.5 + saturate(dot (normalize(IN.viewDir), nor));

          //// Reflection

           float3 reflectedDir = WorldReflectionVector (IN, nor);
           fixed4 hdrReflection = texCUBElod (_Cube, float4(reflectedDir,_Blur2*10/s.a));



            //// Refraction
            float2 offset = nor * _DistAmt*1500 * _GrabTexture_TexelSize.xy;


         
             IN.proj.xy = offset * IN.proj.xyz + IN.proj.xyz+ reflectedDir*_Gr;


              //// Refraction Blur

            half4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj))*_ModBr;
                     
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj+0.01)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj+0.02)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj+0.03)*s.a*_Blur*50);

              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj-0.01)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj-0.02)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj-0.03)*s.a*_Blur*50);

              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj+0.015)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj+0.025)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj+0.035)*s.a*_Blur*50);

              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj-0.015)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj-0.025)*s.a*_Blur*50);
              col += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj-0.035)*s.a*_Blur*50);


                      

           float no = 1 * _Transparency/2+0.5;

            half4 tex = tex2D(_MainTex, IN.uv_MainTex)*0.5*no*_Color;
            fixed4 m = tex2D(_TrTex, IN.uv_MainTex);


         half3 mat = m.a/m.a-m.a*_Transparency;

	half3 met =   pow (rim2,_RimPowerTr)*tex.rgb*col.rgb*0.06*m.a*_Transparency;



	  o.Albedo = tex.rgb;

	  // LB Lighting
    o.Albedo += _LightAdd;

	o.Albedo *= met*mat*col.rgb*0.06*_Transparency* pow (rim2,_RimPowerTr) ;
                      
	o.Albedo += tex.rgb*mat+met;

	        /////Calculate only Specular shading
	        o.Albedo += hdrReflection.rgb*2*tex.a*_SpecularPower*0.2* pow (rim,_RimPower);


	        o.Albedo += 0.001/col.a;

            o.Normal = nor.rgb;



            o.Emission -= o.Albedo* _Con*0.1;

  #if defined(SP_ON)
o.Gloss = s.a*_Shininess;
o.Specular = tex.a*_Shininess;
  #endif


      #if defined(PROMETHEUS_ON)

      o.Albedo *= o.Albedo*_PrPower*6;

      #endif

       

            o.Alpha = tex.a ;
        }
        ENDCG
    }




    FallBack "Transparent"
}