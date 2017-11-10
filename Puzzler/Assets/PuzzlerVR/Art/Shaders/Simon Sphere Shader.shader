// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Simon Sphere Shader"
{
	Properties
	{
		[PerRendererData]_Intensity("Intensity", Range( 0 , 2)) = 0.1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow noambient novertexlights nodynlightmap nofog nometa noforwardadd 
		struct Input
		{
			fixed filler;
		};

		uniform float _Intensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _SphereColor = float4(0.7426471,0.3276384,0.6796802,0);
			o.Albedo = _SphereColor.rgb;
			o.Emission = ( _SphereColor * _Intensity ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13701
1543;29;1522;788;761;399;1;True;True
Node;AmplifyShaderEditor.ColorNode;1;-386,-251;Float;False;Constant;_SphereColor;Sphere Color;0;0;0.7426471,0.3276384,0.6796802,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;2;-411,133;Float;False;Property;_Intensity;Intensity;0;1;[PerRendererData];0.1;0;2;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-140,3;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;21,-115;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Simon Sphere Shader;False;False;False;False;True;True;False;True;False;True;True;True;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;0;0;1;0
WireConnection;0;2;3;0
ASEEND*/
//CHKSM=697F9E10219EAFCB77D0636B3ED3294FC7FCBAC2