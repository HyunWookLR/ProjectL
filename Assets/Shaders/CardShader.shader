Shader "Custom/CardShader"
{
    Properties
    {
        _MainTex("Character Texture", 2D) = "white" {}
		_FrontTex("Front Cover Texture", 2D) = "white" {}
		_FrontBGTex("Front Background Texture", 2D) = "white" {}
		_InnerTex("Inner Texture", 2D) = "white" {}
		_BackTex("Back Cover Texture", 2D) = "white" {}
		_Offset("Offset", Vector) = (0.0,0.0,0.0,0.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {
			Cull Back
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
			sampler2D _FrontTex;
			float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
                fixed4 mainCol = tex2D(_MainTex, i.uv);
				fixed4 frontCol = tex2D(_FrontTex, i.uv);
				clip(frontCol.a - 1.0);
                return frontCol;
            }
            ENDCG
        }

		Pass
		{
			Cull Back
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
			sampler2D _InnerTex;
			sampler2D _FrontBGTex;
			float4 _InnerTex_ST;
			float4 _Offset;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _InnerTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 innerCol = tex2D(_InnerTex, i.uv);
				fixed4 bgCol = tex2D(_FrontBGTex, (i.uv + float2(_Offset.x, _Offset.y)) * float2(_Offset.z, _Offset.w));
				fixed4 mainTex = tex2D(_MainTex, (i.uv + float2(_Offset.x, _Offset.y)) * float2(_Offset.z, _Offset.w));
				clip(innerCol.a - 1.0);
				innerCol.rgb = lerp(bgCol.rgb, mainTex.rgb, mainTex.a);
				return innerCol;
			}
			ENDCG
		}

		Pass
		{
			Cull Front
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

			sampler2D _BackTex;
			float4 _BackTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _BackTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_BackTex, i.uv);
				return col;
			}
			ENDCG
		}
    }
}