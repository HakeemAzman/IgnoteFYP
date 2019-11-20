
Shader "APOLLO/Mobile - VR/Advanced/Refractive" {



    Properties {


    _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}

	 _Blur2 ("Roughness",Range(0.01,1)) = 0.01
	 _SpecularPower ("Specular Power", Range (0.0, 1)) = 0.2
	 _RimPower ("R Fresnel", Range(0,3.0)) = 0.0



        _MainTex ("Base (RGB)", 2D) = "white" {}
        _TrTex ("Transparency/Specularity Map (Trsn Red/Spec Blue)", 2D) = "white" {}
        _Transparency ("Transparency", Range (0.0, 1)) = 1

        _BumpMap ("Normalmap", 2D) = "bump" {}
        _DistAmt  ("Distortion", range (0,2)) = 0.5
        _Gr ("Mesh Refraction",  Range (0.0, 1)) = 0.1


        [MaterialToggle] Prometheus ("Use Prometheus", Float) = 0
	    _PrPower("Prometheus Value",Range(0,10)) = 5


    }
    SubShader {
        GrabPass { }
       
   
Tags { "Queue"="Transparent" "RenderType"="Transparent" }



        CGPROGRAM
        #pragma vertex vert
        #pragma surface surf Lambert  addshadow fullforwardshadows
        #include "UnityCG.cginc"
        #pragma target 3.0
        #pragma multi_compile _ PROMETHEUS_ON

        float _Transparency;
        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _GrabTexture;
        sampler2D _TrTex;
        samplerCUBE _Cube;
        float _RimPower;

        fixed4 _ShadowColor;
        float _SpecularPower;
        float _Con;
        float _LightAdd;
        float _PrPower;
        half _Shininess;

        float _DistAmt;
        float4 _GrabTexture_TexelSize;
        float _Gr;
 

        float _Blur2;
    
 

        struct Input {
            float2 uv_MainTex;

            float2 uv_BumpMap;
            float4 proj : TEXCOORD;
            float3 viewDir;
            float3 worldRefl;
            INTERNAL_DATA

        };


   




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


             fixed4 m = tex2D(_TrTex, IN.uv_MainTex);

         half3 nor = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
         half rim = 1 - saturate(dot (normalize(IN.viewDir), nor));

          //// Reflection

           float3 reflectedDir = WorldReflectionVector (IN, nor);
           fixed4 hdrReflection = texCUBElod (_Cube, float4(reflectedDir,_Blur2*10/m.b));



            //// Refraction
            float2 offset = nor * _DistAmt*1500 * _GrabTexture_TexelSize.xy;


         
             IN.proj.xy = offset * IN.proj.xyz + IN.proj.xyz+ reflectedDir*_Gr;


           

            half4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(IN.proj));


                      

           float no = 1 * _Transparency/2+0.5;

            half4 tex = tex2D(_MainTex, IN.uv_MainTex)*no;


            half3 mat = m.r/m.r-m.r*_Transparency;

	        half3 met =  tex.rgb*col.rgb*m.r*_Transparency;



	o.Albedo = tex.rgb;

	o.Albedo *= met*mat*col.rgb*_Transparency ;
                      
	o.Albedo += tex.rgb*mat+met;


	         /////Calculate only Specular shading
	        o.Albedo += hdrReflection.rgb*2*tex.a*_SpecularPower* pow (rim,_RimPower);
	        o.Albedo += 0.001/col.a;
            o.Normal = nor.rgb;



      #if defined(PROMETHEUS_ON)

      o.Albedo *= o.Albedo*_PrPower;

      #endif

       

            o.Alpha = tex.a ;
        }
        ENDCG
    }




    FallBack "Transparent"
}