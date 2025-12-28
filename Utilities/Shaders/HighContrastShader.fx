// High contrast shader in HLSL

// Texture to sample from the scene
Texture2D inputTexture : register(t0);

// Sampler state for the texture
SamplerState samplerState : register(s0);

// Constants for contrast and brightness adjustments
float contrast : register(c0); // contrast factor (user-controlled)
float brightness : register(c1); // brightness adjustment (optional)

// Main shader entry point for the pixel shader
float4 main(float2 texCoord : TEXCOORD) : SV_Target
{
    // Sample the color from the texture at texCoord
    float4 color = inputTexture.Sample(samplerState, texCoord);

    // Convert to grayscale using a luminance formula
    float grayscale = dot(color.rgb, float3(0.299f, 0.587f, 0.114f));

    // Apply contrast adjustment
    grayscale = (grayscale - 0.5f) * contrast + 0.5f;

    // Apply brightness adjustment
    grayscale += brightness;

    // Ensure the value stays within valid [0, 1] range
    grayscale = saturate(grayscale);

    // Return the modified grayscale color (preserving original alpha)
    return float4(grayscale, grayscale, grayscale, color.a);
}

// Define the technique
technique HighContrastEffect
{
    pass Pass1
    {
        // Pixel Shader Stage
        PixelShader = compile ps_2_0 main();
    }
}