Shader "Unlit/Glow"
{
    Properties
    {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _Glow ("Glow Amount", Float) = 0
		_DetailTex ("Detail Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
        
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
        // Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
            
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			float _Glow;
			sampler2D _MainTex;
			sampler2D _DetailTex;

            v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

            fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D (_MainTex, IN.texcoord);
                fixed2 uv = fixed2(_Time.x, _Time.x);
                fixed4 detail = tex2D (_DetailTex, IN.texcoord + uv);
                fixed glow = lerp(0, lerp(0, abs(_SinTime.w/2 + 1), _Glow), detail.a + 0.5f);

				c.rgb *= (c.a * glow);

				return c;
			}

            ENDCG
        }
    }
}
