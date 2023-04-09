//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class OfficeArea
	{	
			private int _id;
			private string _officeAreaId;
			private string _name;
			private string _superiorArea;
			private string _address;
			private string _phone;
			private string _serviceDirector;
			private string _introduction;
			private string _organizationId;
			private string _organizationName;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string OfficeAreaId
			{
				get {  return _officeAreaId;}
				set {  _officeAreaId = value;}
			}
			public string Name
			{
				get {  return _name;}
				set {  _name = value;}
			}
			public string SuperiorArea
			{
				get {  return _superiorArea;}
				set {  _superiorArea = value;}
			}
			public string Address
			{
				get {  return _address;}
				set {  _address = value;}
			}
			public string Phone
			{
				get {  return _phone;}
				set {  _phone = value;}
			}
			public string ServiceDirector
			{
				get {  return _serviceDirector;}
				set {  _serviceDirector = value;}
			}
			public string Introduction
			{
				get {  return _introduction;}
				set {  _introduction = value;}
			}
			public string OrganizationId
			{
				get {  return _organizationId;}
				set {  _organizationId = value;}
			}
			public string OrganizationName
			{
				get {  return _organizationName;}
				set {  _organizationName = value;}
			}
			public string Remark
			{
				get {  return _remark;}
				set {  _remark = value;}
			}
	}
}
