// 溶解图片Shader
Shader "MyShader/UIDissolve"
{
    Properties
    {
        _DissolveTex("Dissolve",2D) = "white" {}
        //临界
        _Threshold("Threshold",Range(0,1)) = 0
        // 边缘颜色
        _RimColor("RimColor",Color) = (1,1,1,1)
        // 边缘的宽
        _RimWidth("RimWidth",Range(0,0.2)) = 0
    }
    SubShader
    {
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float _Threshold;
            fixed4 _RimColor;
            fixed _RimWidth;


            fixed4 frag (v2f i) : SV_Target
            {
                // 图片颜色
                fixed4 col = tex2D(_MainTex, i.uv);
                // 溶解贴图颜色
                fixed dis = tex2D(_DissolveTex, i.uv).r;
                // 溶解差值
                fixed temp = dis - _Threshold;
                // 丢弃颜色
                clip(temp - 0);

                 //边缘光
                float rim = 1.0 - ceil(temp - _RimWidth);
                //自发光
                fixed3 rimColor = _RimColor.rgb * rim;

                return col + fixed4(rimColor,0);
            }
            ENDCG
        }
    }
}
