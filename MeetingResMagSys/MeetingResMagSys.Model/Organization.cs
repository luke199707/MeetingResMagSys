//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class Organization
	{	
			private int _id;
			private string _organizationId;
			private string _name;
			private string _introduction;
			private string _logo;
			private string _reseStart;
			private string _reseEnd;
			private string _timeUnit;
			private string _signIn;
			private string _sameTimeAttend;
			private DateTime? _time;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string OrganizationId
			{
				get {  return _organizationId;}
				set {  _organizationId = value;}
			}
			public string Name
			{
				get {  return _name;}
				set {  _name = value;}
			}
			public string Introduction
			{
				get {  return _introduction;}
				set {  _introduction = value;}
			}
			public string Logo
			{
				get {  return _logo;}
				set {  _logo = value;}
			}
			public string ReseStart
			{
				get {  return _reseStart;}
				set {  _reseStart = value;}
			}
			public string ReseEnd
			{
				get {  return _reseEnd;}
				set {  _reseEnd = value;}
			}
			public string TimeUnit
			{
				get {  return _timeUnit;}
				set {  _timeUnit = value;}
			}
			public string SignIn
			{
				get {  return _signIn;}
				set {  _signIn = value;}
			}
			public string SameTimeAttend
			{
				get {  return _sameTimeAttend;}
				set {  _sameTimeAttend = value;}
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
