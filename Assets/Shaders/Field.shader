Shader "Custom/Field"
{
    	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Emission ("Emission (Lightmapper)", Float) = 1.0
		[NoScaleOffset] _FlowMap ("Flow (RG, A noise)", 2D) = "black" {}
	}
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque"  "ForceNoShadowCasting" = "True"} 
		ZWrite Off
		Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		CGPROGRAM

		#include "UnityCG.cginc"
		#pragma surface surf Standard fullforwardshadows alpha:fade
		#pragma target 3.0
		fixed _Emission;
		fixed4 _Color;
		struct Input {
			float2 uv_MainTex;
		};
		sampler2D _MainTex, _FlowMap;

		float3 FlowUVW (float2 uv, float2 flowVector, float time, bool flowB) {
			float phaseOffset = flowB ? 0.5 : 0;
			float progress = frac(time + phaseOffset);
			float3 uvw;
			uvw.xy = uv - flowVector * progress + phaseOffset;
			uvw.z = 1 - abs(1 - 2 * progress);
			return uvw;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg * 2 - 1;
			float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
			float time = _Time.y + noise;

			float3 uvwA = FlowUVW(IN.uv_MainTex, flowVector, time, false);
			float3 uvwB = FlowUVW(IN.uv_MainTex, flowVector, time, true);

			fixed4 texA = tex2D(_MainTex, uvwA.xy) * uvwA.z;
			fixed4 texB = tex2D(_MainTex, uvwB.xy) * uvwB.z;

			fixed4 c = (texA + texB) * _Color;
			o.Albedo = c.rgb;
			float alpha = c.a * _Color.a;
			o.Alpha = alpha;
			o.Emission = c.rgb;
			o.Emission *= _Emission.rrr;
		}
		ENDCG
	}


	FallBack "Diffuse"
}
