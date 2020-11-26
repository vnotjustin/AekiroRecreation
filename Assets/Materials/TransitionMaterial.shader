// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TransitionMaterial"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_MainTexture("Main Texture", 2D) = "white" {}
		_BlendMainColor("Blend Main Color", Range( 0 , 1)) = 1
		_MainColor("Main Color", Color) = (1,0.8796263,0.06132078,0)
		_TransitionValue("Transition Value", Range( 0 , 1)) = 0.4931016
		_OffsetBetweenColors("Offset Between Colors", Range( 0 , 0.33)) = 0.1217634
		_TransitionColor1("Transition Color 1", Color) = (0.7264151,0.3460751,0.3460751,0)
		_TransitionColor2("Transition Color 2", Color) = (0.5704551,1,0.4481132,0)
		_TransitionColor3("Transition Color 3", Color) = (0.1179245,0.1988492,1,1)
		_NoiseTiling("Noise Tiling", Vector) = (1,1,0,0)
		_TimeFactor("Time Factor", Range( 0 , 1)) = 0.2352941
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;
		uniform float4 _MainColor;
		uniform float _BlendMainColor;
		uniform float _TransitionValue;
		uniform float2 _NoiseTiling;
		uniform float _TimeFactor;
		uniform float4 _TransitionColor3;
		uniform float _OffsetBetweenColors;
		uniform float4 _TransitionColor1;
		uniform float4 _TransitionColor2;
		uniform float _Cutoff = 0.5;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float4 lerpResult14 = lerp( tex2D( _MainTexture, uv_MainTexture ) , _MainColor , _BlendMainColor);
			float temp_output_99_0 = ( _Time.y * _TimeFactor );
			float2 temp_cast_0 = (temp_output_99_0).xx;
			float2 uv_TexCoord89 = i.uv_texcoord * _NoiseTiling + temp_cast_0;
			float simplePerlin2D79 = snoise( uv_TexCoord89 );
			simplePerlin2D79 = simplePerlin2D79*0.5 + 0.5;
			float2 temp_cast_1 = (( 1.0 - temp_output_99_0 )).xx;
			float2 uv_TexCoord94 = i.uv_texcoord * _NoiseTiling + temp_cast_1;
			float simplePerlin2D93 = snoise( uv_TexCoord94 );
			simplePerlin2D93 = simplePerlin2D93*0.5 + 0.5;
			float lerpResult96 = lerp( simplePerlin2D79 , simplePerlin2D93 , 0.5);
			float temp_output_78_0 = ( lerpResult96 * 1.0 );
			float temp_output_3_0_g9 = ( _TransitionValue - temp_output_78_0 );
			float temp_output_82_0 = saturate( ( temp_output_3_0_g9 / fwidth( temp_output_3_0_g9 ) ) );
			float temp_output_3_0_g2 = ( ( _TransitionValue + _OffsetBetweenColors ) - temp_output_78_0 );
			float temp_output_69_0 = saturate( ( temp_output_3_0_g2 / fwidth( temp_output_3_0_g2 ) ) );
			float temp_output_3_0_g10 = ( ( ( _OffsetBetweenColors * 2.0 ) + _TransitionValue ) - temp_output_78_0 );
			float temp_output_68_0 = saturate( ( temp_output_3_0_g10 / fwidth( temp_output_3_0_g10 ) ) );
			float4 blendOpSrc63 = ( ( 1.0 - temp_output_68_0 ) * _TransitionColor1 );
			float4 blendOpDest63 = ( ( _TransitionColor2 * ( 1.0 - temp_output_69_0 ) ) * temp_output_68_0 );
			float4 blendOpSrc88 = ( ( _TransitionColor3 * ( 1.0 - temp_output_82_0 ) ) * temp_output_69_0 );
			float4 blendOpDest88 = ( 1.0 - ( 1.0 - blendOpSrc63 ) * ( 1.0 - blendOpDest63 ) );
			float4 blendOpSrc51 = ( lerpResult14 * temp_output_82_0 );
			float4 blendOpDest51 = ( saturate( ( 1.0 - ( 1.0 - blendOpSrc88 ) * ( 1.0 - blendOpDest88 ) ) ));
			o.Emission = ( saturate( ( 1.0 - ( 1.0 - blendOpSrc51 ) * ( 1.0 - blendOpDest51 ) ) )).rgb;
			o.Alpha = 1;
			float temp_output_23_0 = ( _OffsetBetweenColors * 3.0 );
			float temp_output_3_0_g11 = ( (-0.01 + (_TransitionValue - 0.0) * (( temp_output_23_0 + 1.0 ) - -0.01) / (( 1.0 - temp_output_23_0 ) - 0.0)) - temp_output_78_0 );
			clip( saturate( ( temp_output_3_0_g11 / fwidth( temp_output_3_0_g11 ) ) ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18707
0;0;1920;1019;4192.543;863.6131;1.400378;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;91;-3882.795,291.1702;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-4038.794,414.1703;Inherit;False;Property;_TimeFactor;Time Factor;10;0;Create;True;0;0;False;0;False;0.2352941;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-3652.795,340.1702;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;90;-3621.853,-191.4975;Inherit;False;Property;_NoiseTiling;Noise Tiling;9;0;Create;True;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.OneMinusNode;92;-3397.795,348.1702;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;94;-3257.795,202.1702;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;89;-3257.795,75.17023;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;79;-3005.335,-13.97147;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;93;-2996.795,285.1702;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;96;-2781.267,138.5604;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-2695.783,892.9996;Inherit;False;Property;_OffsetBetweenColors;Offset Between Colors;5;0;Create;True;0;0;False;0;False;0.1217634;0.08428655;0;0.33;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2641.326,698.222;Inherit;False;Property;_TransitionValue;Transition Value;4;0;Create;True;0;0;False;0;False;0.4931016;0.296;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-2077.624,289.3329;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-2081.439,409.3293;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-2249.57,-119.5836;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;69;-1389.928,239.3974;Inherit;True;Step Antialiasing;-1;;2;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-1910.571,396.2574;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;82;-1276.505,-200.2241;Inherit;True;Step Antialiasing;-1;;9;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;59;-1167.071,260.1753;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;61;-1195.789,64.37627;Inherit;False;Property;_TransitionColor2;Transition Color 2;7;0;Create;True;0;0;False;0;False;0.5704551,1,0.4481132,0;0.5704551,1,0.4481132,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;68;-1399.454,469.7184;Inherit;True;Step Antialiasing;-1;;10;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;34;-964.9054,698.9844;Inherit;False;Property;_TransitionColor1;Transition Color 1;6;0;Create;True;0;0;False;0;False;0.7264151,0.3460751,0.3460751,0;0.8018868,0.124822,0.124822,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;84;-1009.29,-160.6995;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-957.8037,212.5629;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;43;-1087.295,480.9671;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;86;-831.1896,-308.8995;Inherit;False;Property;_TransitionColor3;Transition Color 3;8;0;Create;True;0;0;False;0;False;0.1179245,0.1988492,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;83;-1048.104,-972.0635;Inherit;False;858.9406;515.9999;Main Texture;5;1;4;14;3;46;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-717.6515,361.6773;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-802.9855,458.8384;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-2141.945,1192.018;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-938.1037,-733.0635;Inherit;False;Property;_BlendMainColor;Blend Main Color;2;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-998.1036,-922.0635;Inherit;True;Property;_MainTexture;Main Texture;1;0;Create;True;0;0;False;0;False;-1;None;cbccd153d85abbc48938e6cb95cdac08;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;53;-2137.131,1291.377;Inherit;False;Constant;_ONE;ONE;8;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-939.1037,-668.0636;Inherit;False;Property;_MainColor;Main Color;3;0;Create;True;0;0;False;0;False;1,0.8796263,0.06132078,0;0.2198736,0.2638384,0.5754717,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-693.3895,-122.9995;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;-542.5873,-70.99978;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;63;-550.3671,436.773;Inherit;True;Screen;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1954.373,1170.087;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;80;-1805.48,1247.345;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;14;-630.103,-690.0635;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;52;-1506.283,1145.478;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.8;False;3;FLOAT;-0.01;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;88;-329.3831,207.1999;Inherit;False;Screen;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-424.163,-729.2252;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;70;-1264.114,1118.356;Inherit;False;Step Antialiasing;-1;;11;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;51;-104.9637,83.78634;Inherit;True;Screen;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;496.1563,120.1763;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;TransitionMaterial;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;99;0;91;0
WireConnection;99;1;98;0
WireConnection;92;0;99;0
WireConnection;94;0;90;0
WireConnection;94;1;92;0
WireConnection;89;0;90;0
WireConnection;89;1;99;0
WireConnection;79;0;89;0
WireConnection;93;0;94;0
WireConnection;96;0;79;0
WireConnection;96;1;93;0
WireConnection;56;0;15;0
WireConnection;56;1;22;0
WireConnection;29;0;22;0
WireConnection;78;0;96;0
WireConnection;69;1;78;0
WireConnection;69;2;56;0
WireConnection;30;0;29;0
WireConnection;30;1;15;0
WireConnection;82;1;78;0
WireConnection;82;2;15;0
WireConnection;59;0;69;0
WireConnection;68;1;78;0
WireConnection;68;2;30;0
WireConnection;84;0;82;0
WireConnection;62;0;61;0
WireConnection;62;1;59;0
WireConnection;43;0;68;0
WireConnection;67;0;62;0
WireConnection;67;1;68;0
WireConnection;45;0;43;0
WireConnection;45;1;34;0
WireConnection;23;0;22;0
WireConnection;85;0;86;0
WireConnection;85;1;84;0
WireConnection;87;0;85;0
WireConnection;87;1;69;0
WireConnection;63;0;45;0
WireConnection;63;1;67;0
WireConnection;26;0;23;0
WireConnection;26;1;53;0
WireConnection;80;0;53;0
WireConnection;80;1;23;0
WireConnection;14;0;1;0
WireConnection;14;1;3;0
WireConnection;14;2;4;0
WireConnection;52;0;15;0
WireConnection;52;2;80;0
WireConnection;52;4;26;0
WireConnection;88;0;87;0
WireConnection;88;1;63;0
WireConnection;46;0;14;0
WireConnection;46;1;82;0
WireConnection;70;1;78;0
WireConnection;70;2;52;0
WireConnection;51;0;46;0
WireConnection;51;1;88;0
WireConnection;0;2;51;0
WireConnection;0;10;70;0
ASEEND*/
//CHKSM=A54B5E09514ACD1B403B9A56CEA1E742901E0AB4