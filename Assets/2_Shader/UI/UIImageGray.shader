Shader "MyShader/UIImageGray"
{
    Properties
    {
       
    }
    
    SubShader
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

        Pass
        {
        
            Tags {"LightMode" = "ForwardBase"}
			ZWrite off // 关闭深度写入
			Blend SrcAlpha OneMinusSrcAlpha // alpha 混合
			
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

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed grayValue = (col.r + col.g + col.b) / 3;
                return fixed4(grayValue,grayValue,grayValue,col.a);
            }
            ENDCG
        }
    }
}
