﻿
// OikOS Toolkit - Visual game making tools for Unity

// Developed by Fernando Ramallo
// Copyright (C) 2017 David Kanaga

// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using UnityEngine;

namespace OikosTools {
	[AddComponentMenu("")]
	public class ImageEffect : MonoBehaviour
	{
		/// Provides a shader property that is set in the inspector
		/// and a material instantiated from the shader
		public Shader shader;
		private Material m_Material;
		
		protected virtual void Start()
		{
			// Disable if we don't support image effects
			if (!SystemInfo.supportsImageEffects)
			{
				enabled = false;
				return;
			}
			
			// Disable the image effect if the shader can't
			// run on the users graphics card
			if (!shader || !shader.isSupported)
				enabled = false;
		}
		
		protected Material material
		{
			get
			{
				if (m_Material == null)
				{
					m_Material = new Material(shader);
					m_Material.hideFlags = HideFlags.HideAndDontSave;
				}
				return m_Material;
			}
		}
		
		protected virtual void OnDisable()
		{
			if (m_Material)
			{
				DestroyImmediate(m_Material);
			}
		}
		
		
		public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Debug.LogError("error " + name, this);
			throw new System.NotImplementedException();
		}
	}
}