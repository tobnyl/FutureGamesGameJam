// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "JMD/SimpleOutline" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Outline ("Outline Thickness", float) = 0.05
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
	}
	
	
CGINCLUDE
#include "UnityCG.cginc"

struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};

uniform half _Outline;
uniform half4 _OutlineColor;

v2f vert(appdata v) {
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);

	float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	float2 offset = TransformViewToProjection(norm.xy);

	o.pos.xy += offset * o.pos.z * _Outline;
	o.color = _OutlineColor;
	return o;
}
ENDCG

		
SubShader {
Pass{
		
SetTexture [_MainTex] {
                combine texture
            }
		
	}

		
				Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return i.color; }
			ENDCG
		}
		
	} 
	FallBack "VertexLit"
}
