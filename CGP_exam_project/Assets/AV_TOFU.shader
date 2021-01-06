Shader "Reactive Fluid Effect" {
    Properties {
      tint("Colour Tint", Color) = (1,1,1,1)
      speed("Speed", Range(1,100)) = 10
      scale1("Scale 1", Range(0.1,2)) = 2
      scale2("Scale 2", Range(0.1,2)) = 2
      scale3("Scale 3", Range(0.1,2)) = 2
      scale4("Scale 4", Range(0.1,2)) = 2
    }
    SubShader {
      
      CGPROGRAM
      #pragma surface surf Lambert
      
      struct Input {
          float2 uv_MainTex;
          float3 worldPos;
      };
      
      float4 tint;
      float speed;
      float scale1;
      float scale2;
      float scale3;
      float scale4;

      void surf (Input IN, inout SurfaceOutput o) {
          const float PI = 3.14159265;
          float t = _Time.x * speed;
          
          //vertical
          float c = sin(IN.worldPos.x * scale1 + t);

          
          //horizontal
          c += sin(IN.worldPos.z * scale2 + t);

          //diagonal
          c += sin(scale3*(IN.worldPos.x*sin(t/2) + IN.worldPos.z*cos(t/3))+t);

          //circular
          float c1 = pow(IN.worldPos.x + 0.5 * sin(t/5),2);
          float c2 = pow(IN.worldPos.z + 0.5 * cos(t/3),2);
          c += sin(sqrt(scale4*(c1 + c2)+1+t));

          o.Albedo.r = sin(c/3.0*PI);
          o.Albedo.g = sin(c/3.0*PI + 2*PI/3);
          o.Albedo.b = sin(c/3.0*PI + 3*PI/3);
         
          o.Albedo *= tint;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }