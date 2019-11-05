Shader "Unlit/TileOverlay"
{
    Properties
    {        
        _Transparency ("Transparency", float) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {                
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;            
            float _Transparency;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture                
                fixed4 col = float4(0,0,0,_Transparency);
                return col;
            }
            ENDCG
        }
    }
}