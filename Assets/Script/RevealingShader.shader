Shader "Custom/RevealUnderLight_URP_WithEdges"
{
    Properties
    {
        _BaseMap ("Albedo", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _HidenColor ("Hiden Color", Color) = (1,1,1,1)
        _EdgeColor ("Edge Highlight Color", Color) = (1,0,0,1) // New property for edge color
        _EdgeWidth ("Edge Width", Range(0, 1)) = 0.02 // Controls how thick the edge highlight is
        _Gloss ("Smoothness", Range(0,1)) = 0.5
        _Metal ("Metallic", Range(0,1)) = 0.0
        _LightPos("Light Position", Vector) = (0,0,0,0)
        _LightDir("Light Direction", Vector) = (0,0,1,0)
        _Angle ("Light Angle", Range(0,180)) = 45
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
            float4 _HidenColor;
            float4 _EdgeColor; // New edge color
            float _EdgeWidth; // New edge width
            float4 _LightPos;
            float4 _LightDir;
            float _Angle;
            float _Strength;
            float _Gloss;
            float _Metal;
            float _Sections;
            float _Speed;

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL; // Added for edge detection
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 worldNormal : TEXCOORD2; // Added for edge detection
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.uv = v.uv;
                o.worldPos = TransformObjectToWorld(v.positionOS);
                o.worldNormal = TransformObjectToWorldNormal(v.normalOS); // Transform normal to world space
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float4 albedo = _HidenColor * SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);
            
                // Compute light-based reveal strength
                float3 direction = normalize(_LightPos.xyz - i.worldPos);
                float scale = dot(direction, _LightDir.xyz);
                float strength = scale - cos(_Angle * (3.14 / 360.0));
                strength = saturate(strength * _Strength);
            
                // Stripe effect only for hidden areas
                float stripeMask = clamp(abs(tan((i.uv.x + _Time.x * _Speed) * _Sections)), 0, 1);
            
                // Final color blending
                float3 hiddenColor = _Color.rgb * stripeMask;
                float hiddenAlpha = _Color.a * stripeMask;
            
                float3 finalColor = lerp(albedo.rgb, hiddenColor, strength);
                float finalAlpha = lerp(albedo.a, hiddenAlpha, strength);
                
                // Edge detection based on normal facing
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float edge = 1 - abs(dot(i.worldNormal, viewDir));
                edge = smoothstep(1 - _EdgeWidth, 1.0, edge);
                
                // Combine edge with final color
                finalColor = lerp(finalColor, _EdgeColor.rgb, edge * _EdgeColor.a);
                
                return half4(finalColor, finalAlpha);
            }

            ENDHLSL
        }
    }
}