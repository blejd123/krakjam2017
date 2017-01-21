// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Wave"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_RaysTex("Rays Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_MaxRange("MaxRange", float) = 1.0
		_CurrentRange("CurrentRange", float) = 1.0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "False"
		}

	Cull Off
	Lighting Off
	ZWrite Off
	Blend One OneMinusSrcAlpha

	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 2.0
		#pragma multi_compile _ PIXELSNAP_ON
		#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
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
			//float4 wpos : TEXCOORD1;
			fixed4 color : COLOR;
			float2 texcoord  : TEXCOORD0;
		};

		#define PI 3.14159265359

		fixed4 _Color;
		sampler2D _MainTex;
		sampler2D _AlphaTex;
		sampler2D _RaysTex;
		float _MaxRange;
		float _CurrentRange;

		v2f vert(appdata_t IN)
		{
			v2f OUT;
			OUT.vertex = UnityObjectToClipPos(IN.vertex);
			OUT.texcoord = IN.texcoord;
			OUT.color = IN.color * _Color;
	#ifdef PIXELSNAP_ON
			OUT.vertex = UnityPixelSnap(OUT.vertex);
	#endif

			return OUT;
		}
				
		fixed4 SampleSpriteTexture(float2 uv)
		{	
			fixed4 color = tex2D(_MainTex, uv);
			
	#if ETC1_EXTERNAL_ALPHA
			// get the color from an external texture (usecase: Alpha support for ETC1 on android)
			color.a = tex2D(_AlphaTex, uv).r;
	#endif //ETC1_EXTERNAL_ALPHA

			return color;
		}

		float GetAngle(float2 v)
		{
			return fmod(atan2(v.y, v.x) - PI / 2 + 2 * PI, 2 * PI) / (2 * PI);
		}

		fixed4 frag(v2f IN) : SV_Target
		{
			float2 uv = IN.texcoord;
			float2 centeredPos = uv - float2(0.5, 0.5);
			float angle = GetAngle(centeredPos);
			float4 ray = tex2D(_RaysTex, float2(angle, 0.0));
			float hitDist = ray.r * 255.0 + ray.g;
			float r = length(centeredPos)*2.0 * _MaxRange;
			centeredPos *= _MaxRange / _CurrentRange;
			float4 c = SampleSpriteTexture(centeredPos + float2(0.5, 0.5)) * IN.color;
			c.a *= r <= min(_CurrentRange, hitDist) ? ( r / _MaxRange > 0.9 ? (1.0 - r / _MaxRange) / 0.1 : 1.0) : 0.0;
			c.rgb *= c.a;
			return c;
		}
		ENDCG
		}
	}
}
