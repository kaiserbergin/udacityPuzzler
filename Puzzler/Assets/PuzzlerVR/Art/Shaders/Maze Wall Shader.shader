// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Maze Wall Shader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_WallPatternColor("Wall Pattern Color", Color) = (0.04249566,0.9251356,0.9632353,0)
		_WallPattern("Wall Pattern", 2D) = "white" {}
		[IntRange]_WallPatternSize("Wall Pattern Size", Range( 1 , 20)) = 5
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

		uniform float4 _WallPatternColor;
		uniform sampler2D _WallPattern;
		uniform float _WallPatternSize;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 appendResult16 = float2( _WallPatternSize , _WallPatternSize );
			o.texcoord_0.xy = v.texcoord.xy * appendResult16 + float2( 0,0 );
		}

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = ( _WallPatternColor * tex2D( _WallPattern, i.texcoord_0 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13201
1543;29;1522;788;2766.601;990.4485;2.551983;True;True
Node;AmplifyShaderEditor.RangedFloatNode;17;-1529.494,-363.3484;Float;False;Property;_WallPatternSize;Wall Pattern Size;6;1;[IntRange];5;1;20;0;1;FLOAT
Node;AmplifyShaderEditor.AppendNode;16;-1189.494,-339.3484;Float;False;FLOAT2;0;0;0;0;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-979.8508,-198.3064;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;8;-683.7423,-213.7026;Float;True;Property;_WallPattern;Wall Pattern;1;0;Assets/UdacityVR/Art/Textures/walls.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;10;-662.6383,-404.4719;Float;False;Property;_WallPatternColor;Wall Pattern Color;0;0;0.04249566,0.9251356,0.9632353,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-308.3423,-245.7024;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;12;-20,-236;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Maze Wall Shader;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;False;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;False;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;17;0
WireConnection;16;1;17;0
WireConnection;15;0;16;0
WireConnection;8;1;15;0
WireConnection;9;0;10;0
WireConnection;9;1;8;0
WireConnection;12;2;9;0
ASEEND*/
//CHKSM=1AD150D6A5FA640B7B7D3CC1A6860E3EBDFE58B6