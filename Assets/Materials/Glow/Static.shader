Shader "Custom/Static"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}

			// Highlighting stuff
		_HighlightTex("Highlight Texture", 2D) = "white" {}
		_HighlightColor("Highlight Color", Color) = (0.0, 0.9, 0.1, 1.0)
		_BorderStrength("Border Strength", Range(0.75, 1.25)) = 1.144
		_DistortionTex("Distortion Texture", 2D) = "white" {}
		_Freq("Frequency", Float) = 0.005
		_Amp("Amplitude", Float) = 90
		_Direction("Direction", Vector) = (25.0, 100.0, 0.0, 0.0)

		// Blending state
		[HideInInspector] _Mode ("__mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("__src", Float) = 1.0
		[HideInInspector] _DstBlend ("__dst", Float) = 0.0
		[HideInInspector] _ZWrite ("__zw", Float) = 1.0
	}

	CGINCLUDE
		#define UNITY_SETUP_BRDF_INPUT MetallicSetup
	ENDCG

	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PerformanceChecks"="False" }
		LOD 300
// ---------------------------------------------------------------------------- //
//			Highlight Shader Pass												//
// ---------------------------------------------------------------------------- //	
		Pass
		{
			Name "HIGHLIGHT"

			Blend One OneMinusSrcAlpha
			ZWrite OFF
			Lighting ON

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float distance(fixed4 v)
			{
				return v.x * v.y * v.z;
			}

			struct v2f {
				float4 pos : SV_POSITION;
				float4 texcoord : TEXCOORD0;
			};

			float _BorderScale = 0.5;
			float _BorderStrength;

			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			fixed4 _HighlightColor;
			sampler2D _HighlightTex;
			sampler2D _DistortionTex;
			float _Freq;
			float _Amp;
			fixed4 _Direction;

			fixed4 frag (v2f IN) : SV_Target
			{
				//Distortion value
				float2 distortcoord = IN.texcoord;
				distortcoord.x += _Time.y * _Freq * _Direction.x;
				distortcoord.x -= floor(distortcoord.x);
				distortcoord.y += _Time.y * _Freq * _Direction.y;
				distortcoord.y -= floor(distortcoord.y);

				float4 distortcol = tex2D(_DistortionTex, distortcoord);
				distortcoord.x = (2 * distortcol.r - 1) / _Amp + IN.texcoord.x;
				distortcoord.y = (2 * distortcol.g - 1) / _Amp + IN.texcoord.y;

				float4 col = tex2D(_HighlightTex, distortcoord);
				//Used to be col = col * _HighlightColor
				//Created Black <-> Highlight Fade
				//Double invert allows for White <-> Highlight Fade
				col.rgb = (1 - col * (1 - _HighlightColor)); 
				col.a *= _HighlightColor.a;
				return col;
			}

			ENDCG
		}





				}
}
