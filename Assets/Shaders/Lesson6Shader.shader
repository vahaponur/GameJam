Shader "Course/Lesson6Shader"
{
    Properties
    {
        _MainTex("Ana Texture",2D)="red"{}
        _SecondaryTex("İkinci Texture",2D)="red"{}
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("SrcFactor",Float)=5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("DstFactor",Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _BlendOp("Blend Op",Float) = 0
         _Cutoff("Cutoff Value",Range(0,1))=0.1
        _Feather("Feather Value",Range(0,0.1))=0.1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
        }
        LOD 100
        //         Source Value  Operation
        Blend [_SrcFactor] [_DstFactor]
        BlendOp [_BlendOp]
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                //Need uv data for texture
                float2 uv:TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv1_uv2:TEXCOORD0;
                
            };

            sampler2D _MainTex;
            //Tiling and offset
            float4 _MainTex_ST;
            sampler2D _SecondaryTex;
            float4 _SecondaryTex_ST;
            float _Cutoff,_Feather;

            v2f vert(appdata v)
            {
                //Vertex2fragment
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv1_uv2.xy = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv1_uv2.zw = TRANSFORM_TEX(v.uv,_SecondaryTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                
                fixed4 mainTex = tex2D(_MainTex, i.uv1_uv2.xy);
                fixed4 secondTex = tex2D(_SecondaryTex,i.uv1_uv2.zw);
                  float animatedDis = 0.5*sin(_Time.y)+0.5;
                float dissortion = smoothstep(secondTex.r-_Feather,secondTex.r +_Feather,animatedDis);

                fixed3 color = mainTex ;
              
                fixed alpha = dissortion*mainTex.a;
                return fixed4(color,alpha) ;
            }
            ENDCG
        }
    }
}