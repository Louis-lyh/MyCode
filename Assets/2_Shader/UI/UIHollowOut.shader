
Shader "MyShader/UIHollowOut"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _HollowOut ("HollowOut Texture", 2D) = "white" {} // 镂空图片
        _HollowOutScale("Scale",Range(0,10)) = 0.5    // 镂空缩放
    }
    SubShader
    {
        Tags{ "Queue" = "Transparent" // 渲染器队列 ： 透明
        "IgnoreProjector" = "True" // 忽略投影器
        "RenderType" = "Transparent" // 渲染类型 ： 透明
         }
        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
			ZWrite off
			Blend SrcAlpha OneMinusSrcAlpha // 开启混合
			
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
                float2 hollowOutUv : TEXCOORD1;
            };
            
            // 定义变量
            sampler2D _MainTex;
            sampler2D _HollowOut;
            float4 _HollowOut_ST;
            fixed _HollowOutScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.hollowOutUv =TRANSFORM_TEX(v.uv, _HollowOut);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 将镂空uv坐标从左下角转为中间
                i.hollowOutUv = i.hollowOutUv - float2(0.5,0.5);
                // 多镂空uv进行缩放
                i.hollowOutUv = i.hollowOutUv * pow(_HollowOutScale,4);
                // 将镂空uv转回左下角
                i.hollowOutUv = i.hollowOutUv + float2(0.5,0.5);
                // 读取镂空图片颜色
                fixed4 colHollowOut = tex2D(_HollowOut, i.hollowOutUv);
                
                // 原背景颜色
                fixed4 col = tex2D(_MainTex, i.uv);
                // 剔除镂空图片范围
                col.a = col.a * (1 - colHollowOut.a);
                return col;
            }
            ENDCG
        }
    }
}
