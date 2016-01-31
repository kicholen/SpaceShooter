Shader "Jelly/HitShader" {
	Properties {
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Base Color", Color) = (1, 1, 1, 1)
		_HitColor("Hit Color", Color) = (1, 1, 1, 1)
		_HitVector("Hit Vector4", Vector) = (0, 0, 0, 0)
		_Power("Power", float) = 0.1
		_CutOff("Alpha Cut off", float) = 0.1
	}

	SubShader {
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag          

				struct Vertex {
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct Fragment {
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				uniform sampler2D _MainTex;
				uniform fixed4 _Color;
				uniform fixed4 _HitColor;
				uniform float4 _HitVector;
				uniform float _Power;
				uniform float _CutOff;

				Fragment vert(Vertex ver) {
					Fragment fr;
					fr.vertex = mul(UNITY_MATRIX_MVP, ver.vertex);
					fr.texcoord = ver.texcoord;

					return fr;
				}

				fixed4 frag(Fragment fr) : COLOR {
					float4 texColor = tex2D(_MainTex, fr.texcoord);
					texColor.rgb = _Color;

					float xOffset = _HitVector.x - fr.texcoord.x;
					float yOffset = _HitVector.y - fr.texcoord.y;

					if (texColor.a < _CutOff) 
						discard;

					if (xOffset < _Power && xOffset > -_Power && yOffset < _Power && yOffset > -_Power)
					{
						texColor.rgb = _HitColor;
					}
					return texColor;
						
				}
			ENDCG
		}
	}
}
