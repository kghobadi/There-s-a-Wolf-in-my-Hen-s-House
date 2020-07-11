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
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace OikosTools {
[CustomEditor(typeof(ConstantMovement))]
public class ConstantMovementEditor : Editor {
	
	public override void OnInspectorGUI ()
	{
		
		//base.OnInspectorGUI ();
		
		ConstantMovement t = target as ConstantMovement;
		
		EditorGUI.BeginChangeCheck();
		Undo.RecordObject(t, "Change value");
		

		t.type = (ConstantMovement.Type)EditorGUILayout.EnumPopup("Type", t.type);
		t.velocity = EditorGUILayout.Vector3Field("Velocity", t.velocity);
		t.space = (Space)EditorGUILayout.EnumPopup("According to", t.space);
		if (t.type == ConstantMovement.Type.MoveBySinewave || t.type == ConstantMovement.Type.RotateBySinewave || t.type == ConstantMovement.Type.ScaleBySinewave) {
			t.sineFrequency = EditorGUILayout.FloatField("Sinewave speed", t.sineFrequency);
		}
		
		if (EditorGUI.EndChangeCheck()) {
			EditorUtility.SetDirty(t);
		}
		
	}
}
}