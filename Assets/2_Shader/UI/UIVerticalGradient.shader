// 图片渐变shader
Shader "MyShader/UITransparentGradient"
{
    Properties
    {
        _FillAmount("Fill Amount",Range(0,1)) = 0
        _Multiple("Multiple",Range(1,10)) = 0
        [MaterialToggle]_Vertical("Vertical",int) = 0
        [MaterialToggle]_VerReverse("Vertical Reverse",int) = 0
        [MaterialToggle]_Horizontal("Horizontal",int) = 0
        [MaterialToggle]_HorReverse("Horizontal Reverse",int) = 0
    }
    SubShader
    {
     
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
        Pass
        {
            //深度写入
            ZWrite On
            //掩码遮罩
            ColorMask 0
        }

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
			ZWrite off
			Blend SrcAlpha OneMinusSrcAlpha

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
            
            sampler2D _MainTex;
            half _FillAmount;
            half _Multiple;
            int _Vertical;
            int _VerReverse;
            int _Horizontal;
            int _HorReverse;

            v2f vert (appdata v)
            {
                v2f o;
                // 计算裁剪空间顶点
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                // 图片颜色
                fixed4 col = tex2D(_MainTex, i.uv);

                // 处理渐变进度
                _FillAmount = (_FillAmount - 0.5f) * 2;

                // 计算uv垂直轴的方向
                half ver = -((_VerReverse - 0.5f) * 2) * i.uv.y +  _VerReverse;
                // 垂直透明度
                half alphaVer = min((ver + _FillAmount) * _Multiple,1);
                alphaVer = max(alphaVer,0);
                // 开关
                _Vertical = 1 -_Vertical;
                alphaVer = max(alphaVer,_Vertical);

                // 计算uv水平轴的方向
                half hor = -((_HorReverse - 0.5f) * 2) * i.uv.x +  _HorReverse;
                // 水平透明度
                half alphaHor = min((hor + _FillAmount) * _Multiple,1);
                alphaHor = max(alphaHor,0);
                // 开关
                _Horizontal = 1 -_Horizontal;
                alphaHor = max(alphaHor,_Horizontal);

                // 选择最小透明度
                half alpha = min(alphaHor,alphaVer);
                
                col.a = min(alpha , col.a) ;
                
                return col;
            }
            ENDCG
        }
    }
}
