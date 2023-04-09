//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class FunctionModel
	{	
			private int _id;
			private string _parentId;
			private string _modelName;
			private string _childId;
			private string _currentId;
			private string _url;
			private string _css;
			private string _target;
			private DateTime? _time;
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
			public string ModelName
			{
				get {  return _modelName;}
				set {  _modelName = value;}
			}
			public string ChildId
			{
				get {  return _childId;}
				set {  _childId = value;}
			}
			public string CurrentId
			{
				get {  return _currentId;}
				set {  _currentId = value;}
			}
			public string Url
			{
				get {  return _url;}
				set {  _url = value;}
			}
			public string Css
			{
				get {  return _css;}
				set {  _css = value;}
			}
			public string Target
			{
				get {  return _target;}
				set {  _target = value;}
			}
			public DateTime? Time
			{
				get {  return _time;}
				set {  _time = value;}
			}
			public string Remark
			{
				get {  return _remark;}
				set {  _remark = value;}
			}
	}
}
