//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class DataDictionary
	{	
			private int _id;
			private string _parentId;
			private string _name;
			private string _code;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string ParentId
			{
				get {  return _parentId;}
				set {  _parentId = value;}
			}
			public string Name
			{
				get {  return _name;}
				set {  _name = value;}
			}
			public string Code
			{
				get {  return _code;}
				set {  _code = value;}
			}
			public string Remark
			{
				get {  return _remark;}
				set {  _remark = value;}
			}
	}
}
