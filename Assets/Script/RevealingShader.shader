Shader "Custom/RevealUnderLight_URP"
{
    Properties
    {
        _BaseMap ("Albedo", 2D) = "white" {}
        _Color   ("Tint Color", Color) = (1,1,1,1)
        _Gloss   ("Smoothness", Range(0,1)) = 0.5
        _Metal   ("Metallic", Range(0,1)) = 0.0
        _LightPos("Light Position", Vector) = (0,0,0,0)
        _LightDir("Light Direction", Vector) = (0,0,1,0)
        _Angle   ("Light Angle", Range(0,180)) = 45
        _Strength("Strength", Float) = 50
        _Sections ("Sections", Range(0, 10)) = 0
        _Speed ("Speed", Range(0, 10)) = 0
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "UniversalForward"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            float4 _Color;
            float4 _LightPos;
            float4 _LightDir;
            float  _Angle;
            float  _Strength;
            float  _Gloss;
            float  _Metal;
            float _Sections;
            float _Speed;

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv         : TEXCOORD0;
                float3 worldPos   : TEXCOORD1;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.uv         = v.uv;
                o.worldPos   = TransformObjectToWorld(v.positionOS);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                // sample albedo
                float4 albedo = _Color * SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);

                // compute reveal strength
                float3 direction = normalize(_LightPos.xyz - i.worldPos);
                //float  cosAngle = dot(direction, normalize(_LightDir.xyz));
                //float  threshold = cos(_Angle * 3.14 / 180.0);
                //float  reveal = saturate((cosAngle - threshold) * _Strength);
                float scale = dot(direction, _LightDir.xyz);
			    float strength = scale - cos(_Angle * (3.14 / 360.0));
			    strength = min(max(strength * _Strength, 0), 1);

                // lighting (you could plug into URP lighting functions here if desired)
                float3 emission = albedo.rgb * albedo.a * strength;
                
                float4 tanCol = clamp(abs(tan((i.uv.x + _Time.x * _Speed) * _Sections)), 0, 1);
                tanCol *= _Color;
                return half4(albedo.rgb * strength, albedo.a * strength) * tanCol + half4(emission, 0);
            }
            ENDHLSL
        }
    }
}
