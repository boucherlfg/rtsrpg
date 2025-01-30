using UnityEngine;

namespace ProcGen
{
    public static class Noise
    {
        public static float[,] GenerateNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
        {
            var rng = new System.Random(seed);
            var offsets = new Vector2[octaves];
            for (int i = 0; i < octaves; i++)
            {
                var offsetX = rng.Next(-100000, 100000) + offset.x;
                var offsetY = rng.Next(-100000, 100000) + offset.y;
                offsets[i] = new Vector2(offsetX, offsetY);
            }
            if (scale <= 0)
            {
                scale = 0.0001f;
            }

            var maxNoise = float.MinValue;
            var minNoise = float.MaxValue;
            var centerX = width / 2f;
            var centerY = height / 2f;

            var noiseMap = new float[width, height];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (var i = 0; i < octaves; i++)
                    {
                        var sampleX = (x - centerX) / scale * frequency + offsets[i].x;
                        var sampleY = (y - centerY) / scale * frequency + offsets[i].y;

                        var perlin = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseHeight += perlin * amplitude;

                        amplitude *= persistance;
                        frequency *= lacunarity;
                    }

                    maxNoise = Mathf.Max(noiseHeight, maxNoise);
                    minNoise = Mathf.Min(noiseHeight, minNoise);
                    noiseMap[x, y] = noiseHeight;
                }
            
            }

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(minNoise, maxNoise, noiseMap[x, y]);
                }
            }
            return noiseMap;
        }
    }
}