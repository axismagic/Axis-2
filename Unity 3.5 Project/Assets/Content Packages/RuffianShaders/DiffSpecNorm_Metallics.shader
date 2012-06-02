Shader "RuffianShaders/DiffSpecNorm_Metallics"
{
	Properties 
	{
_Color("Colour Multiplier", Color) = (1,1,1,1)
_MainTex("Diffuse Texture", 2D) = "gray" {}
_FresnelPow("FresnelBias(0 = off)", Range(0,6) ) = 3.546798
_Glossiness("Gloss Multiplier", Range(0,3) ) = 0.5
_SpecularColor("Tint Overall Spec", Color) = (1,1,1,1)
_SpecularMap("Specular Map", 2D) = "white" {}
_FresnelIntensity("Fresnel Effect Boost", Range(1,25) ) = 1
_NormalsMap("Normal Map", 2D) = "bump" {}
_EnvSpecColour("Spec Colour Cubemap (optional)", Cube) = "black" {}
_GlowMap("_GlowMap", 2D) = "black" {}
_DiffuseFresnelPower("_DiffuseFresnelPower", Range(1,25) ) = 1
_DiffuseFresnelColor("_DiffuseFresnelColor", Color) = (1,1,1,1)
_ReflectionAmount("_ReflectionAmount", Color) = (1,1,1,1)

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  addshadow vertex:vert
#pragma target 3.0


float4 _Color;
sampler2D _MainTex;
float _FresnelPow;
float _Glossiness;
float4 _SpecularColor;
sampler2D _SpecularMap;
float _FresnelIntensity;
sampler2D _NormalsMap;
samplerCUBE _EnvSpecColour;
sampler2D _GlowMap;
float _DiffuseFresnelPower;
float4 _DiffuseFresnelColor;
float4 _ReflectionAmount;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float4 color : COLOR;
float2 uv_MainTex;
float3 viewDir;
float2 uv_NormalsMap;
float2 uv_GlowMap;
float2 uv_SpecularMap;
float3 sWorldNormal;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);

o.sWorldNormal = mul((float3x3)_Object2World, SCALED_NORMAL);

			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_MainTex,(IN.uv_MainTex.xyxy).xy);
float4 Multiply0=_Color * Tex2D0;
float4 Multiply6=IN.color * Multiply0;
float4 Fresnel1_1_NoInput = float4(0,0,1,1);
float4 Fresnel1=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel1_1_NoInput.xyz ) )).xxxx;
float4 Pow1=pow(Fresnel1,_DiffuseFresnelPower.xxxx);
float4 Multiply7=_DiffuseFresnelColor * Pow1;
float4 Invert0= float4(1.0, 1.0, 1.0, 1.0) - Multiply7;
float4 Multiply8=Multiply6 * Invert0;
float4 Tex2D3=tex2D(_NormalsMap,(IN.uv_NormalsMap.xyxy).xy);
float4 UnpackNormal1=float4(UnpackNormal(Tex2D3).xyz, 1.0);
float4 Tex2D4=tex2D(_GlowMap,(IN.uv_GlowMap.xyxy).xy);
float4 Tex2D1=tex2D(_SpecularMap,(IN.uv_SpecularMap.xyxy).xy);
float4 TexCUBE0=texCUBE(_EnvSpecColour,float4( IN.sWorldNormal.x, IN.sWorldNormal.y,IN.sWorldNormal.z,1.0 ));
float4 Multiply4=Tex2D1 * TexCUBE0;
float4 Saturate0=saturate(Multiply4);
float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( UnpackNormal1.xyz ) )).xxxx;
float4 Pow0=pow(Fresnel0,_FresnelPow.xxxx);
float4 Multiply5=_FresnelIntensity.xxxx * Pow0;
float4 Multiply3=Saturate0 * Multiply5;
float4 Multiply9=_ReflectionAmount * Multiply3;
float4 Add0=Tex2D4 + Multiply9;
float4 Pow2=pow(Tex2D0.aaaa,_Glossiness.xxxx);
float4 Multiply2=_SpecularColor * Multiply3;
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Multiply8;
o.Normal = UnpackNormal1;
o.Emission = Add0;
o.Specular = Pow2;
o.Gloss = Multiply2;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}