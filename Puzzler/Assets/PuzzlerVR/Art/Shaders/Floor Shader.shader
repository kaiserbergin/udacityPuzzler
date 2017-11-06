// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Floor Shader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color1("Color 1", Color) = (0.04249566,0.9251356,0.9632353,0)
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		[IntRange]_Float1("Float 1", Range( 1 , 20)) = 5
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd vertex:vertexDataFunc 
		struct Input
		{
			float2 texcoord_0;
		};

		uniform float4 _Color1;
		uniform sampler2D _TextureSample1;
		uniform float _Float1;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 appendResult23 = float2( _Float1 , _Float1 );
			o.texcoord_0.xy = v.texcoord.xy * appendResult23 + float2( 0,0 );
		}

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = ( _Color1 * tex2D( _TextureSample1, i.texcoord_0 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13201
1543;29;1522;788;1801.485;229.6466;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;22;-1490.284,-34.91339;Float;False;Property;_Float1;Float 1;6;1;[IntRange];5;1;20;0;1;FLOAT
Node;AmplifyShaderEditor.AppendNode;23;-1150.284,-10.91339;Float;False;FLOAT2;0;0;0;0;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-940.6408,130.1286;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;5;-573.2866,-161.3522;Float;False;Property;_Color1;Color 1;0;0;0.04249566,0.9251356,0.9632353,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;25;-644.5323,114.7324;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Assets/UdacityVR/Art/Textures/walls.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-271.6843,82.7326;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Floor Shader;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;22;0
WireConnection;23;1;22;0
WireConnection;24;0;23;0
WireConnection;25;1;24;0
WireConnection;26;0;5;0
WireConnection;26;1;25;0
WireConnection;0;2;26;0
ASEEND*/
//CHKSM=07321F903BFB7F0EB09A951D59667CDB5C6A5068