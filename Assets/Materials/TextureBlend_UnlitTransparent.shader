// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32525,y:32704|diff-21-OUT,custl-21-OUT,alpha-450-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33830,y:32426,ptlb:Main Texture,ptin:_MainTexture,tex:ccdda4fe08f63fe429fd603b58555e51,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4,x:33830,y:32704,ptlb:Secondary Texture,ptin:_SecondaryTexture,tex:5757aee3c67b5714995cde2453c534a9,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:13,x:33004,y:32421|A-2-RGB,B-44-OUT;n:type:ShaderForge.SFN_Multiply,id:15,x:33011,y:32683|A-425-OUT,B-4-RGB;n:type:ShaderForge.SFN_Blend,id:21,x:32807,y:32558,blmd:6,clmp:True|SRC-13-OUT,DST-15-OUT;n:type:ShaderForge.SFN_Slider,id:44,x:33444,y:32488,ptlb:Blend Factor,ptin:_BlendFactor,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_OneMinus,id:425,x:33236,y:32559|IN-44-OUT;n:type:ShaderForge.SFN_Relay,id:438,x:33322,y:32892|IN-2-A;n:type:ShaderForge.SFN_Relay,id:445,x:33266,y:33014|IN-4-A;n:type:ShaderForge.SFN_Multiply,id:447,x:33011,y:32993|A-425-OUT,B-445-OUT;n:type:ShaderForge.SFN_Multiply,id:448,x:33011,y:32845|A-449-OUT,B-438-OUT;n:type:ShaderForge.SFN_Relay,id:449,x:33266,y:32845|IN-44-OUT;n:type:ShaderForge.SFN_Add,id:450,x:32807,y:32924|A-448-OUT,B-447-OUT;proporder:4-44-2;pass:END;sub:END;*/

Shader "Custom/TextureBlend_UnlitTransparent" {
    Properties {
        _SecondaryTexture ("Secondary Texture", 2D) = "black" {}
        _BlendFactor ("Blend Factor", Range(0, 1)) = 0.5
        _MainTexture ("Main Texture", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _SecondaryTexture; uniform float4 _SecondaryTexture_ST;
            uniform float _BlendFactor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float2 node_494 = i.uv0;
                float4 node_2 = tex2D(_MainTexture,TRANSFORM_TEX(node_494.rg, _MainTexture));
                float node_425 = (1.0 - _BlendFactor);
                float4 node_4 = tex2D(_SecondaryTexture,TRANSFORM_TEX(node_494.rg, _SecondaryTexture));
                float3 node_21 = saturate((1.0-(1.0-(node_2.rgb*_BlendFactor))*(1.0-(node_425*node_4.rgb))));
                float3 finalColor = node_21;
/// Final Color:
                return fixed4(finalColor,((_BlendFactor*node_2.a)+(node_425*node_4.a)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
