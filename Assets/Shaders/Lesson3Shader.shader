    Shader "Course/Lesson3Shader"
{
    Properties
    {
        _MainTex("Ana Texture",2D)="red"{}
        _Color("Tint Color",COLOR)=(0,1,0,1)
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("SrcFactor",Float)=5
                 [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("DstFactor",Float) = 10
                             [Enum(UnityEngine.Rendering.BlendOp)]
        _BlendOp("Blend Op",Float) = 0

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
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
                float2 uv:TEXCOORD0;
            };

           sampler2D _MainTex;
           sampler2D _ChildTex;
           //Tiling and offset
            float4 _MainTex_ST;
            float4 _ChildTex_ST;
            fixed4 _Color;
            v2f vert (appdata v)
            {
                //Vertex2fragment
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=TRANSFORM_TEX(v.uv,_MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = fixed4(0,0,1,0.5);
                fixed4 textr = tex2D(_MainTex, i.uv);
                //Color multiplied for tint
                return textr;
            }
            ENDCG
        }
    }
}
