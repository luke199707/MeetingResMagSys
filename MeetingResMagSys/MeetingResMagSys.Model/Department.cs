//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class Department
	{	
			private int _id;
			private string _departmentId;
			private string _name;
			private string _introduction;
			private string _superiorDepartment;
			private string _supervisor;
			private string _officeArea;
			private string _type;
			private string _email;
			private string _organizationId;
			private string _organizationName;
			private DateTime? _time;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string DepartmentId
			{
				get {  return _departmentId;}
				set {  _departmentId = value;}
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
			public string SuperiorDepartment
			{
				get {  return _superiorDepartment;}
				set {  _superiorDepartment = value;}
			}
			public string Supervisor
			{
				get {  return _supervisor;}
				set {  _supervisor = value;}
			}
			public string OfficeArea
			{
				get {  return _officeArea;}
				set {  _officeArea = value;}
			}
			public string Type
			{
				get {  return _type;}
				set {  _type = value;}
			}
			public string Email
			{
				get {  return _email;}
				set {  _email = value;}
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
