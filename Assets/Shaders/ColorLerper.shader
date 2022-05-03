Shader "VShade/ColorLerper"
{
    Properties
    {
   
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("SrcFactor",Float)=5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("DstFactor",Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _BlendOp("Blend Op",Float) = 0

        _StartColor("StartColor",COLOR)=(1,1,1,1)
        _LerpFactor("LerpFactor",Range(0,1)) = 0
        _EndColor("EndColor",COLOR)=(1,1,1,1)
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
            fixed4 _StartColor;
            fixed4 _EndColor;
            float _LerpFactor;
            
   
            v2f vert(appdata v)
            {
                //Vertex2fragment
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
            

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                
             fixed4 color = lerp(_StartColor,_EndColor,_LerpFactor);
                
          
                return color ;
            }
            ENDCG
        }
    }
}