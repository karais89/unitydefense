
/***************************************************************************************************************

	This script contains the code for support and contact information
	Please dont modify this script


****************************************************************************************************************/

using UnityEngine;
using UnityEditor;

using System;

using System.Collections;
using System.Collections.Generic;

public class AboutWindow : EditorWindow {

	private static AboutWindow window;
	
	public static void Init () {
		window = (AboutWindow)EditorWindow.GetWindow(typeof (AboutWindow));
		//window.minSize=new Vector2(375, 449);
	}
	
	void OnGUI () {
		if(window==null) Init();
		
		float startX=5;
		float startY=5;
		float spaceX=70;
		float spaceY=18;
		float width=230;
		float height=17;
		
		GUIStyle style=new GUIStyle("Label");
		style.fontSize=16;
		style.fontStyle=FontStyle.Bold;
		
		GUIContent cont=new GUIContent("Tower Defense ToolKit (TDTK)");
		EditorGUI.LabelField(new Rect(startX, startY, 300, 30), cont, style);
		
		EditorGUI.LabelField(new Rect(startX, startY+8, 300, height), "_______________________________________");
		
		startY+=30;
		EditorGUI.LabelField(new Rect(startX, startY, width, height), " - Version:");
		EditorGUI.LabelField(new Rect(startX+spaceX, startY, width, height), "3.0 free");
		EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), " - Release:");
		EditorGUI.LabelField(new Rect(startX+spaceX, startY, width, height), "19 November 2014");
		
		startY+=15;
		
		EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), "Developed by K.Song Tan");
		
		EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), " - Email:");
		EditorGUI.TextField(new Rect(startX+spaceX, startY, width, height), "k.songtan@gmail.com");
		EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), " - Twitter:");
		EditorGUI.TextField(new Rect(startX+spaceX, startY, width, height), "SongTan@SongGameDev");
		
		EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), " - Website:");
		EditorGUI.TextField(new Rect(startX+spaceX, startY, width, height), "http://song-gamedev.blogspot.co.uk/");
		if(GUI.Button(new Rect(startX+spaceX+width+10, startY, 50, height), "Open")){
			Application.OpenURL("http://song-gamedev.blogspot.co.uk/");
		}
		
		startY+=10;
		if(GUI.Button(new Rect(startX+25, startY+=spaceY, width, height), "Get Full Version")){
			Application.OpenURL("https://www.assetstore.unity3d.com/#/content/1024");
		}
	}
	
}
