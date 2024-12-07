Shader "Unlit/DepthFog"
{
    
    
    Properties
    {
        _FogColor ("Fog Color", Color) = (0.5, 0.5, 0.5, 1)
    _FogDensity ("Fog Density", Float) = 0.02
    }
    SubShader
    {
         ...
    CGPROGRAM
    #pragma surface surf Standard
    #include "UnityCG.cginc"

    // Объявление переменных
    uniform fixed4 _FogColor;
    uniform float _FogDensity;
    ...
    ENDCG
        
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Fog.cginc"
            
// Функция для обработки цвета тумана
fixed3 BlendFog(fixed3 color, float fogCoord)
{
    float fogFactor = saturate(exp2(-fogCoord * fogCoord * _FogDensity * _FogDensity * 1.442695f)); // Exponential squared fog
    return lerp(_FogColor.rgb, color, fogFactor);
}


            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col.rgb = BlendFog(col.rgb, i.fogCoord);
                return col;
            }
            ENDCG
        }
    }
}
