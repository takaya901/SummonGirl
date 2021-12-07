Shader "Transfer/Face"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold("Threshold", Range(-1,1.5)) = 1
        _LazerTex("LazerTexture", 2D) = "white"{}
        _LazerHeight("LazerHeight", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100
        Cull off
        Blend SrcAlpha OneMinusSrcAlpha 

		Pass{
  		  ZWrite ON
  		  ColorMask 0
		}
		
        CGPROGRAM
        #pragma surface surf Standard alpha:fade
        #pragma target 3.0
        
        sampler2D _MainTex;

        struct Input {
            float3 worldNormal;
      		float3 viewDir;
        };
        
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o) 
        {
			o.Albedo = fixed4(1, 1, 1, 1);
			float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
     		o.Alpha =  alpha*1.5f;
        }
        ENDCG		
		
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
                float2 uv : TEXCOORD0;
                float3 oPos: TEXCOORD1;
            };

            sampler2D _MainTex;
            float _Threshold;
            sampler2D _LazerTex;
            float _LazerHeight;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.oPos = v.vertex;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                //float diff = i.oPos.y - _Threshold;
                float diff = _Threshold - i.oPos.y;
                clip(diff);
                //if(diff < _LazerHeight){
                //    fixed4 lazerCol = tex2D(_LazerTex, diff / _LazerHeight);
                //    col.rgb = col.rgb * (1 - lazerCol.a) + lazerCol.rgb * col.a;
                //}

                return col;
            }
            ENDCG
        }
    }
}