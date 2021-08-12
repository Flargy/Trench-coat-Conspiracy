Shader "Unlit/OutlineShader_2.0"
{
    Properties
    {
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        _OutlineWidth ("Outline Width", Range(0, 1)) = 1
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #define Div_Sqrt_2 0.70710678118
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _SelectionBuffer;
            float4 _MainTex_ST, _OutlineColor;
            float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //sample directions
                float2 directions[8] = {float2(1,0), float2(0,1), float2(-1,0), float2(0,-1),
                    float2(Div_Sqrt_2, Div_Sqrt_2), float2(-Div_Sqrt_2, Div_Sqrt_2),
                    float2(-Div_Sqrt_2, -Div_Sqrt_2), float2(Div_Sqrt_2, -Div_Sqrt_2)};

                float aspect = _ScreenParams.x * (_ScreenParams.w - 1); //width times 1/height
                float2 sampleDistance = float2(_OutlineWidth / aspect, _OutlineWidth);

                //generate outline
                float maxAlpha = 0;
                for(uint idx = 0; idx < 8; idx++)
                {
                    float2 sampleUv = i.uv + directions[idx] * sampleDistance;
                    maxAlpha = max(maxAlpha, tex2D(_SelectionBuffer, sampleUv).a);
                }

                //remove core
                float border = max(0, maxAlpha - tex2D(_SelectionBuffer, i.uv).a);
                
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col = lerp(col, _OutlineColor, border);
                return col;
            }
            ENDCG
        }
    }
}
