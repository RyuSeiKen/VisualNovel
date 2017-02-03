Shader "[PROJECT]/Narrative/TextDisplay/TextDisplayAnimation"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		_AnimationStartTime("Animation start time", Float) = 0
		_AnimationTime("Animation time", Float) = 0

		// 
		_Canvas("Canvas", Vector) = (0,0,100,100)
		_LineHight("Line Height (pixels)", Float) = 10
		_LineHightOffset("Line higt offset (pixels)", Float) = -1
		_TransitionLengh("Transition Lenghth (pixels)", Float) = 10
		_TransitionOffset("Transition offset (pixels)", Range(-1,1)) = -1
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Overlay"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Stencil
	{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
	}

		Cull Off
		Lighting Off 
		ZWrite Off
		ZTest Off
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
		
		// Math
		float smoothstep(float a, float b, float x)
		{
			float t = saturate((x - a) / (b - a));
			return t*t*(3.0 - (2.0*t));
		}

		// Old
		float _AnimationStartTime;
		float _AnimationTime;

		float GetEffectSlider(float4 vertex) {
			float y = floor((vertex.y + 16 - _ScreenParams.y/2) / 32);
			y += 0.5;
			float x = (float)vertex.x / (float)_ScreenParams.x;
			const float speed = 0.0025;
			const float lengh = 0.005;
			float slider = (_AnimationTime * speed - ((x - y) * lengh));
			slider = smoothstep(0.00, 0.001, slider);
			return slider;
		}

		// New
		float4 _Canvas;
		float _LineHight;
		float _TransitionLengh;
		float _TransitionOffset;
		float _LineHightOffset;

		float getProgress(float2 uv) {
			// Calculate signle axis position of characters's vertex
			float position = floor((uv.y + _LineHightOffset) / _LineHight);
			position *= _Canvas.z;
			position += uv.x;
			// Calculate progress with transition
			float progress = _AnimationTime - position;
			// Return progress with transition
			return (progress + lerp(0, _TransitionLengh, (_TransitionOffset + 1) / 2)) * (1 / _TransitionLengh);
		}

			struct appdata_t
		{
			float4 vertex   : POSITION; 
			float4 color    : COLOR;
			float2 texcoord : TEXCOORD0;
		};


		struct v2f
		{
			float4 vertex   : SV_POSITION;
			fixed4 color : COLOR;
			half2 texcoord  : TEXCOORD0;
		};

		fixed4 _Color;

		v2f vert(appdata_t IN)
		{
			float2 uv = float2 (
				(IN.vertex.x + _ScreenParams.x / 2 - _Canvas.x),
				(-IN.vertex.y + _ScreenParams.y / 2) - _Canvas.y
				);
			float progress = smoothstep(0, 1, getProgress(uv));
			IN.vertex += float4( 
				lerp(-10	, 0, progress),
				lerp(5 * (sin(IN.vertex.x * 0.01) - 2), 0, progress),
				0,
				0
				);

			v2f OUT;
			OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
			OUT.texcoord = IN.texcoord;
	#ifdef UNITY_HALF_TEXEL_OFFSET
			OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
	#endif
			OUT.color = IN.color * _Color;
			OUT.color.r = progress;


			return OUT;
		}

		sampler2D _MainTex;

		fixed4 frag(v2f IN) : SV_Target
		{
			half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
			color.rgb = _Color.rgb;
			//color.r = IN.color.r;
			color.a = lerp(0, color.a, IN.color.r);
			return color;
		}
			ENDCG
		}
	}
}