using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Newtonsoft.Json;

public class JSONFileParser<T>
{
	public static T Parse(string filePath)
	{
		return JsonConvert.DeserializeObject<T> (File.ReadAllText (Application.dataPath + "/" + filePath));
	}
}

//	public void Parse(string filePath)
//	{
//		StreamReader reader = FileInfo.OpenText(filePath);
//
//		int lineIndex = 0;
//
//		string line;
//		while ((line = reader.ReadLine()) != null) {
//
//			if(string.IsNullOrEmpty(line)){
//				continue;
//			}
//
//			try{
//				string property;
//				string value;
//				TryParsePropertyLine(line, out property, out value);
//				switch(property){
//				case "DialogEvent" :
//					ParseDialogEvent(reader, value);
//					break;
//				default : 
//					throw new IOException("Unknown property: " + property);
//				}
//
//			} catch(IOException e){
//				throw new IOException("Line " + lineIndex + " " + e.Message);
//			}
//
//
//			lineIndex++;
//		}
//	}
//
//	public void ParseDialogEvent(StreamReader reader, string DialogEventIndex){
//
//		string line;
//		while ((line = reader.ReadLine()) != null) {
//			
//			if (string.IsNullOrEmpty (line)) {
//				continue;
//			}
//			string property;
//			string value;
//			TryParsePropertyLine(line, out property, out value);
//
//		}
//	}
//
//	public void ParseDialogEventButton(){
//
//	}
//
//	bool TryParsePropertyLine(string line, out string property, out string value){
//		int propertyNameStart = 0;
//		int valueStart;
//		
//		if(valueStart = line.IndexOf(":") == -1) {
//			throw new IOException("Could not parse expected property line: " + line);
//		}
//		
//		property = line.Substring(propertyNameStart, valueStart-propertyNameStart).Trim();
//		value = line.Substring(valueStart, line.Length-valueStart).Trim();
//
//		return true;
//	}}
