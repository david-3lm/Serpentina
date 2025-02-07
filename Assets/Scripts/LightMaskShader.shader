Shader "Custom/LightMaskPostProcess"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightMask ("Light Mask", 2D) = "white" {}
        _Darkness ("Darkness Level", Range(0,1)) = 0.8
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _LightMask;
            float _Darkness;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 sceneColor = tex2D(_MainTex, i.uv);
                half4 mask = tex2D(_LightMask, i.uv);
                
                // Aplicar el efecto de oscuridad fuera del área iluminada
                half visibility = lerp(_Darkness, 1, mask.r);
                return sceneColor * visibility;
            }
            ENDCG
        }
    }
}
