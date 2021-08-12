Shader "Custom/OutlineShader"
{
    Properties
    {
        [HideInInspector] _Color ("Color", Color) = (1,1,1,1)
        [HideInInspector] _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [HideInInspector] [HDR] _Emission ("Emission", color) = (0,0,0)
        
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        [PowerSlider(4)] _FresnelExponent ("Fresnel Exponent", Range (0.25, 4)) = 1
        _FresnelOpacity ("Fresnel Opacity", Range(0,1)) = 1
        
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        [PowerSlider(2)] _OutlineThickness ("Outline Thickness", Range(0, 0.2)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
            INTERNAL_DATA
        };

        fixed4 _Color;
        fixed _Alpha;
        half3 _Emission;

        float3 _FresnelColor;
        float _FresnelExponent;
        half _FresnelOpacity;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input i, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, i.uv_MainTex);
            o.Albedo = c.rgb * _Color.rgb;
            o.Alpha = c.a;

            float fresnel = dot(normalize(i.worldNormal), i.viewDir);
            fresnel = pow(saturate( 1 - fresnel ), _FresnelExponent);
            float3 fresnelColor = (fresnel * _FresnelColor) * _FresnelOpacity;

            o.Emission = _Emission + fresnelColor;
            
        }
        ENDCG

        
        Pass
        {
            Cull front
            
            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            fixed4 _OutlineColor;
            float _OutlineThickness;

            struct MeshData
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
            };

            v2f vert (MeshData v)
            {
                v2f o;
                
                 if(_OutlineThickness > 0)
                     o.position = UnityObjectToClipPos(v.vertex + normalize(v.normal) * _OutlineThickness);
                
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                fixed4 c = fixed4(0,0,0,0);
                
                if(_OutlineThickness > 0)
                    c = _OutlineColor;
                
                return c;
            }
            ENDCG
        }
    }
    FallBack "Standard"
}
