#ifndef PERLIN_NOISE_CGINC
#define PERLIN_NOISE_CGINC

//https://blog.csdn.net/candycat1992/article/details/50346469

float2 hash22(float2 p)
{
	p = float2(dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)));
	return  (-1.0 + 2.0*frac(sin(p)*43758.5453123));
}

float mix(float from, float to, float t)
{
	float f = 6 * pow(t, 5) - 15 * pow(t, 4) + 10 * pow(t, 3);
	return lerp(from, to, f);
}

float perlin_noise(float2 p)
{
	float2 pf = floor(p);
	float2 po = p - pf;

	float res = mix(
		mix(
			dot(hash22(pf), (po)),
			dot(hash22(pf + float2(1, 0)), (po - float2(1, 0))),
			po.x
		),
		mix(
			dot(hash22(pf + float2(0, 1)), (po - float2(0, 1))),
			dot(hash22(pf + float2(1, 1)), (po - float2(1, 1))),
			po.x
		),
		po.y
	);
	return res;
}

#define ADJUST(v, a) v *= 2; \
					a *= 0.5;

#define SUM(r, v, a) r += a * perlin_noise(v);

#define SUM_ABS(r, v, a) r += a * abs(perlin_noise(v));

#define FBM(r, v, a) ADJUST(v, a) \
					SUM(r, v, a)

#define FBM_ABS(r, v, a) ADJUST(v, a) \
					SUM_ABS(r, v, a)

float fbm_perlin_noise(float2 p)
{
	float2 v = p;
	float f = 0;
	float m = 1;
	SUM(f, v, m)
	FBM(f, v, m)
	FBM(f, v, m)
	FBM(f, v, m)
	FBM(f, v, m)
	return f;
}

float turbulence_perlin_noise(float2 p)
{
	float2 v = p;
	float f = 0;
	float m = 1;
	SUM_ABS(f, v, m);
	FBM_ABS(f, v, m);
	FBM_ABS(f, v, m);
	FBM_ABS(f, v, m);
	FBM_ABS(f, v, m);
	return f;
}

#endif//PERLIN_NOISE_CGINC