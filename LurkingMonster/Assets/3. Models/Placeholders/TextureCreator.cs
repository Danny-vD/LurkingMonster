using UnityEngine;

namespace _3._Models.Placeholders
{
	public enum PatternType
	{
		Noise,
		Gradient,
		CheckerBoard,
		Circle,
		Custom,
		Mandelbrot,
		BrickWall,
		Split
	}

	public class TextureCreator : MonoBehaviour
	{
		public PatternType patternType;
		public float HorizontalMultiplier = 1;
		public float VerticalMultiplier = 1;
		public float HorizontalOffset = 0;
		public float VerticalOffset = 0;
		public Color color1;
		public Color color2;

		const int SIZE = 512;
		Renderer render;
		bool initialized = false;

		// Used for the Mandelbrot fractal:
		const int maxIterations = 100;
		const float escapeLengthSquared = 4;

		void Start()
		{
			render = GetComponent<Renderer>();

			// Create a new texture, with width and height equal to SIZE:
			Texture2D texture = new Texture2D(SIZE, SIZE, TextureFormat.RGBA32, false);

			// Assign it as main texture (=color data texture) to the material of the renderer:
			render.material.mainTexture = texture;

			// Fill in the color data of the texture:
			CreateTexture(texture);
			initialized = true;
		}

		void OnValidate()
		{
			if (initialized)
			{
				CreateTexture((Texture2D) render.material.mainTexture);
			}
		}

		void CreateTexture(Texture2D tex)
		{
			Color[] cols = tex.GetPixels();

			for (int x = 0; x < tex.width; x++)
			{
				for (int y = 0; y < tex.height; y++)
				{
					float u = x * 1f / tex.width;
					float v = y * 1f / tex.height;
					int index = x + y * tex.width;

					switch (patternType)
					{
						case PatternType.Noise:
							// DONE: Use a better noise type here (Perlin?)
							float lightness = Mathf.PerlinNoise(u * HorizontalMultiplier + HorizontalOffset,
								v * VerticalMultiplier + VerticalOffset);

							// black / white:
							//cols [index] = lightness * Color.white;
							// linear interpolation between two given colors:
							cols[index] = lightness * color1 + (1 - lightness) * color2;
							break;
						case PatternType.Gradient:
							// from black to white:
							//cols [index] = u * Color.white;
							// linear interpolation between two given colors:
							//cols [index] = u * color1 + (1-u) * color2;
							// using the ColorGradient method below:
							cols[index] = ColorGradient(360f * u);

							//testing using 3 gradients at once
							//float testred = u * 2 + 1;
							//Color colorred = Mathf.PingPong(testred, 1) * Color.red;

							//float testGreen = u + 0.3f;
							//Color colorgreen = Mathf.PingPong(testGreen, 1) * Color.green;

							//float testBlue = u + 0.7f;
							//Color colorblue = Mathf.PingPong(testBlue, 1) * Color.blue;

							//cols[index] = colorred + colorgreen + colorblue;
							break;
						case PatternType.CheckerBoard:
							// DONE: Create a two colored checkerboard here
							cols[index] =
								((Mathf.Floor(u * HorizontalMultiplier) + Mathf.Floor(v * VerticalMultiplier)) % 2 == 0)
									? color1
									: color2;

							//cols[index] =
							//    color1 * (Mathf.Floor(u * HorizontalMultiplier) / HorizontalMultiplier) +
							//    color2 * (Mathf.Floor(v * VerticalMultiplier) / VerticalMultiplier);
							break;
						case PatternType.Circle:
							// DONE: create an actual circle here
							float dx = u - 0.5f;
							float dy = v - 0.5f;
							cols[index] = color1 * Mathf.Max(0, 1 - 2 * Mathf.Sqrt(dx * dx + dy * dy));

							//cols[index] = color1 * (1 - 2 * Mathf.Abs(u - 0.5f) - 2 * Mathf.Abs(v - 0.5f));
							break;
						case PatternType.Mandelbrot:
							cols[index] = Mandelbrot(
								(u - 0.5f) * HorizontalMultiplier + HorizontalOffset,
								(v - 0.5f) * VerticalMultiplier + VerticalOffset
							);
							break;
						case PatternType.BrickWall:
							float brickWidth = 0.2f;
							float brickHeight = 0.1f;
							float space = 0.03f;

							if (v > 0.99) //had to force a line at the top
							{
								cols[index] = color2;
								continue;
							}

							u *= HorizontalMultiplier;
							v *= VerticalMultiplier;

							while (u > brickWidth + space)
							{
								u -= (brickWidth + space);
							}

							while (v > brickHeight * 2 + space)
							{
								v -= brickHeight * 2;
							}

							if (u < brickWidth)
							{
								cols[index] = color1;
							}
							else
							{
								cols[index] = color2;
							}

							if (v > brickHeight + space && v < brickHeight * 2 + space)
							{
								if (u < (brickWidth / 2 - space / 2) || u > (brickWidth / 2) + space / 2)
								{
									cols[index] = color1;
								}
								else
								{
									cols[index] = color2;
								}
							}

							if (v > brickHeight && v < brickHeight + space ||
								v > brickHeight * 2 && v < brickHeight * 2 + space)
							{
								cols[index] = color2;
							}

							break;
						case PatternType.Custom:
							// TODO: experiment with different patterns here
							//cols[index] = color1 * Mathf.PingPong(v * HorizontalMultiplier, 1);

							//cols[index] = color1 * ((u * HorizontalMultiplier + v * VerticalMultiplier) % 1);

							//A real chessboard:
							cols[index] =
								(Mathf.Floor(u * HorizontalMultiplier) + Mathf.Floor(v * VerticalMultiplier)) % 2 == 1
									? color1
									: color2;
							break;

						case PatternType.Split:
							//brick wall:

							if (u <= 0.5f)
							{
								Color red = new Color(0.5f, 0, 0);
								brickWidth  = 0.4f;
								brickHeight = 0.2f;
								float white = 0.03f;

								if (v > 0.99) //had to force a white line at the top
								{
									cols[index] = Color.white;
									continue;
								}

								u *= HorizontalMultiplier;
								v *= VerticalMultiplier;

								// short:
								u = u % (brickWidth + white);

								// long:
								//while (u > brickWidth + white)
								//{
								//    u -= (brickWidth + white);
								//}

								while (v > brickHeight * 2 + white)
								{
									v -= brickHeight * 2;
								}

								if (u < brickWidth)
								{
									cols[index] = red;
								}
								else
								{
									cols[index] = Color.white;
								}

								if (v > brickHeight + white && v < brickHeight * 2 + white)
								{
									if (u < (brickWidth / 2 - white / 2) || u > (brickWidth / 2) + white / 2)
									{
										cols[index] = red;
									}
									else
									{
										cols[index] = Color.white;
									}
								}

								if (v > brickHeight && v < brickHeight + white ||
									v > brickHeight * 2 && v < brickHeight * 2 + white)
								{
									cols[index] = Color.white;
								}
							}
							else
							{
								/**/ //second brick wall, bigger
								brickWidth = 0.8f;
								brickHeight = 0.4f;
								space = 0.03f;

								if (v > 0.99) //had to force a line at the top
								{
									cols[index] = color2;
									continue;
								}

								u *= HorizontalMultiplier;
								v *= VerticalMultiplier;

								while (u > brickWidth + space)
								{
									u -= (brickWidth + space);
								}

								while (v > brickHeight * 2 + space)
								{
									v -= brickHeight * 2;
								}

								if (u < brickWidth)
								{
									cols[index] = color1;
								}
								else
								{
									cols[index] = color2;
								}

								if (v > brickHeight + space && v < brickHeight * 2 + space)
								{
									if (u < (brickWidth / 2 - space / 2) || u > (brickWidth / 2) + space / 2)
									{
										cols[index] = color1;
									}
									else
									{
										cols[index] = color2;
									}
								}

								if (v > brickHeight && v < brickHeight + space || v > brickHeight * 2 && v < brickHeight * 2 + space)
								{
									cols[index] = color2;
								}
								/**/

								/** //uuuuh...... a gradient apparently
								Vector3 col1 = ToHSV(color1);
								Vector3 col2 = ToHSV(color2);

								col1.x += u;
								col2.x -= v;

								float dot = Vector3.Dot(col1, col2);

								Vector3 colour = col1 + col2;
								colour.x -= dot;

								cols[index] = ToRGB(colour);
								/**/
							}

							break;
					}
				}
			}

			tex.SetPixels(cols);
			tex.Apply();
		}

		// Returns a color that changes smoothly as degrees increases from 0 to 360.
		// (If done correctly, degrees and degrees+360 give the same color.)
		Color ColorGradient(float degrees)
		{
			// TODO: insert a better gradient here
			/**
			return new Color(
				Mathf.PingPong(degrees / 180 + 1, 1),
				Mathf.PingPong(degrees / 60, 1),
				Mathf.PingPong(degrees / 90, 1)
			);

			/**/
			// DONE: improve this color gradient (follow the definition of *hue*), and possibly simplify the formula

			while (degrees < 0)
			{
				degrees += 360;
			}

			while (degrees > 360)
			{
				degrees -= 360;
			}

			if (degrees < 60)
			{
				// increase green:
				return new Color(1, degreesToRGBValue(degrees, 0), 0);
			}
			else if (degrees < 120)
			{
				// decrease red:
				return new Color(1 - degreesToRGBValue(degrees, 60), 1, 0);
			}
			else if (degrees < 180)
			{
				// increase blue:
				return new Color(0, 1, degreesToRGBValue(degrees, 120));
			}
			else if (degrees < 240)
			{
				// decrease green:
				return new Color(0, 1 - degreesToRGBValue(degrees, 180), 1);
			}
			else if (degrees < 300)
			{
				// increase red:
				return new Color(degreesToRGBValue(degrees, 240), 0, 1);
			}
			else if (degrees < 360)
			{
				// decrease blue:
				return new Color(1, 0, 1 - degreesToRGBValue(degrees, 300));
			}
			else
			{
				// return red:
				return new Color(1, 0, 0);
			}

			/**/
		}

		Color Mandelbrot(float cReal, float cImaginary)
		{
			int iteration = 0;

			float zReal = 0;
			float zImaginary = 0;

			while (zReal * zReal + zImaginary * zImaginary < escapeLengthSquared && iteration < maxIterations)
			{
				// Use Mandelbrot's magic iteration formula: z := z^2 + c 
				// (using complex number multiplication & addition - 
				//   see https://mathbitsnotebook.com/Algebra2/ComplexNumbers/CPArithmeticASM.html)
				float newZr = zReal * zReal - zImaginary * zImaginary + cReal;
				zImaginary = 2 * zReal * zImaginary + cImaginary;
				zReal      = newZr;
				iteration++;
			}

			// Return a color value based on the number of iterations that were needed to "escape the circle":
			return ColorGradient(360f * iteration / maxIterations);
		}

		private float degreesToRGBValue(float degrees, int pStartpoint)
		{
			return (degrees - pStartpoint) / 60f;
		}

		public Vector3 ToHSV(Color pColor)
		{
			float Max = Mathf.Max(pColor.r, pColor.g, pColor.b);
			float Min = Mathf.Min(pColor.r, pColor.g, pColor.b);
			float Diff = Max - Min;
			float Brightness = Max;
			float Saturation = (Max > 0) ? Diff / Max : 0;
			float Hue = 0;

			if (Diff == 0)
			{
				Hue = 0;
				return new Vector3(Hue, Saturation, Brightness);
			}

			if (Max == pColor.r && Min == pColor.b)
			{
				// increase green:
				Hue = 0 + 60 * (pColor.g - Min) / Diff;
				return new Vector3(Hue, Saturation, Brightness);
			}
			else if (Max == pColor.g && Min == pColor.b)
			{
				// decrease red:
				Hue = 60 + 60 * (Max - pColor.r) / Diff;
				return new Vector3(Hue, Saturation, Brightness);
			}
			else if (Max == pColor.g && Min == pColor.r)
			{
				// increase blue:
				Hue = 120 + 60 * (pColor.b - Min) / Diff;
				return new Vector3(Hue, Saturation, Brightness);
			}
			else if (Max == pColor.b && Min == pColor.r)
			{
				// decrease green:
				Hue = 180 + 60 * (Max - pColor.g) / Diff;
				return new Vector3(Hue, Saturation, Brightness);
			}
			else if (Max == pColor.b && Min == pColor.g)
			{
				// increase red:
				Hue = 240 + 60 * (pColor.r - Min) / Diff;
				return new Vector3(Hue, Saturation, Brightness);
			}
			else if (Max == pColor.r && Min == pColor.g)
			{
				// decrease blue:
				Hue = 300 + 60 * (Max - pColor.b) / Diff;
				return new Vector3(Hue, Saturation, Brightness);
			}
			else
			{
				// return red:
				Hue = 0;
			}

			return new Vector3(Hue, Saturation, Brightness);
		}

		public Color ToRGB(float hue, float saturation, float value)
		{
			float Max = value;
			float Min = value * (1 - saturation);
			float R = 0;
			float G = 0;
			float B = 0;

			while (hue < 0)
			{
				hue += 360;
			}

			while (hue > 360)
			{
				hue -= 360;
			}

			if (hue <= 60)
			{
				// increase green:
				R = Max;
				B = Min;

				G = Min + (hue / 60) * (Max - Min);
			}
			else if (hue <= 120)
			{
				// decrease red:
				G = Max;
				B = Min;

				hue -= 60;
				R   =  Max - (hue / 60) * (Max - Min);
			}
			else if (hue <= 180)
			{
				// increase blue:
				G = Max;
				R = Min;

				hue -= 120;
				B   =  Min + (hue / 60) * (Max - Min);
			}
			else if (hue <= 240)
			{
				// decrease green:
				B = Max;
				R = Min;

				hue -= 180;
				G   =  Max - (hue / 60) * (Max - Min);
			}
			else if (hue <= 300)
			{
				// increase red:
				B = Max;
				G = Min;

				hue -= 240;
				R   =  Min + (hue / 60) * (Max - Min);
			}
			else if (hue <= 360)
			{
				// decrease blue:
				R = Max;
				G = Min;

				hue -= 300;
				B   =  Max - (hue / 60) * (Max - Min);
			}
			else
			{
				// return red:
				R = Max;
				G = Min;
				B = Min;
			}

			return new Color(R, G, B);
		}

		public Color ToRGB(Vector3 HSV)
		{
			float Max = HSV.z;
			float Min = HSV.z * (1 - HSV.y);
			float hue = HSV.x;
			float R;
			float G;
			float B;

			while (hue < 0)
			{
				hue += 360;
			}

			while (hue > 360)
			{
				hue -= 360;
			}

			if (hue <= 60)
			{
				// increase green:
				R = Max;
				B = Min;

				G = Min + (hue / 60) * (Max - Min);
				return new Color(R, G, B);
			}
			else if (hue <= 120)
			{
				// decrease red:
				G = Max;
				B = Min;

				hue -= 60;
				R   =  Max - (hue / 60) * (Max - Min);
				return new Color(R, G, B);
			}
			else if (hue <= 180)
			{
				// increase blue:
				G = Max;
				R = Min;

				hue -= 120;
				B   =  Min + (hue / 60) * (Max - Min);
				return new Color(R, G, B);
			}
			else if (hue <= 240)
			{
				// decrease green:
				B = Max;
				R = Min;

				hue -= 180;
				G   =  Max - (hue / 60) * (Max - Min);
				return new Color(R, G, B);
			}
			else if (hue <= 300)
			{
				// increase red:
				B = Max;
				G = Min;

				hue -= 240;
				R   =  Min + (hue / 60) * (Max - Min);
				return new Color(R, G, B);
			}
			else if (hue <= 360)
			{
				// decrease blue:
				R = Max;
				G = Min;

				hue -= 300;
				B   =  Max - (hue / 60) * (Max - Min);
				return new Color(R, G, B);
			}
			else
			{
				// return red:
				R = Max;
				G = Min;
				B = Min;
			}

			return new Color(R, G, B);
		}
	}
}