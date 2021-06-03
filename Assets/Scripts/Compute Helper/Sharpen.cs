using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComputeShaderUtility
{
	public static class Sharpen
	{
		static ComputeShader sharpenCompute;

		public static void SharpenEffect(RenderTexture texture, int iterations = 1)
		{
			if (sharpenCompute == null)
			{
				sharpenCompute = (ComputeShader)Resources.Load("Sharpen");
			}

			RenderTexture resultTexture = new RenderTexture(texture.descriptor);
			sharpenCompute.SetTexture(0, "Source", texture);
			sharpenCompute.SetTexture(0, "Result", resultTexture);
			sharpenCompute.SetInt("width", texture.width);
			sharpenCompute.SetInt("height", texture.height);

			for (int i = 0; i < iterations; i++)
			{

				ComputeHelper.Dispatch(sharpenCompute, texture.width, texture.height);
				ComputeHelper.CopyRenderTexture(resultTexture, texture);
			}
			resultTexture.Release();

		}
	}
}