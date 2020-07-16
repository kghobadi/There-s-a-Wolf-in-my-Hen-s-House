Shader "Custom/ChickenNecks"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Rotation ("Rotation", Vector) = (0, 0, 0, 0)
        _WorldPoint ("Look at Point", Vector) = (0, 0, 0, 0)
        _ChickenPosition ("Current Chicken Position", Vector) = (0, 0, 0, 0)
        _ChickenRotation ("Current Chicken Rotation", float) = 0
        //_RotationMask ("Rotation Mask", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert addshadow fullforwardshadows nolightmap

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        
        

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _Rotation, _WorldPoint, _ChickenPosition;
        float _ChickenRotation;
        
        /*
        Y
        [cos(D) 0 sin(D) 0]
        [0 1 0 0]
        [-sin(D) 0 cos(D) 0]
        [0 0 0 1]
        */
        
        float3 rotationY(float3 vertex, float rotation){
            float4 vert = float4(vertex, 1);
            float4x4 mat;
            mat[0] = float4(cos(rotation), 0, sin(rotation), 0);
            mat[1] = float4(0, 1, 0, 0);
            mat[2] = float4(-sin(rotation), 0, cos(rotation), 0);
            mat[3] = float4(0, 0, 0, 1);
            return mul(mat, vert).xyz;
        }
        
        float3 rotationX(float3 vertex, float rotation){
            float4 vert = float4(vertex, 1);
            float4x4 mat;
            mat[0] = float4(1, 0, 0, 0);
            mat[1] = float4(0, cos(rotation), -sin(rotation), 0);
            mat[2] = float4(0, sin(rotation), cos(rotation), 0);
            mat[3] = float4(0, 0, 0, 1);
            return mul(mat, vert).xyz;
        }
        
        void vert(inout appdata_full v){

            float headMask = smoothstep(0.62, 0.65, v.texcoord.y);
           
            v.vertex.xyz = rotationY(v.vertex.xyz, headMask * _Rotation.y);
            v.vertex.xyz = rotationX(v.vertex.xyz, headMask * _Rotation.x);
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
