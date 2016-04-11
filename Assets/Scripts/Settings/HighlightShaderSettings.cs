using System.Collections;
using UnityEngine;

public class HighlightShaderSettings : ScriptableObject
{
	[SerializeField]
	private ShaderFloatProperty [] floatProperties;

	[SerializeField]
	private ShaderColorProperty [] colorProperties;

	public ShaderFloatProperty[] FloatProperties {
		get {
			return floatProperties;
		}
		set {
			floatProperties = value;
		}
	}

	public ShaderColorProperty[] ColorProperties {
		get {
			return colorProperties;
		}
		set {
			colorProperties = value;
		}
	}
}



