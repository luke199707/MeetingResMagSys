//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class AllUser
	{	
			private int _id;
			private string _userId;
			private string _name;
			private string _pwd;
			private string _organizationId;
			private string _organizationName;
			private string _departmentName;
			private string _email;
			private string _phone;
			private string _role;
			private string _available;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string UserId
			{
				get {  return _userId;}
				set {  _userId = value;}
			}
			public string Name
			{
				get {  return _name;}
				set {  _name = value;}
			}
			public string Pwd
			{
				get {  return _pwd;}
				set {  _pwd = value;}
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
			public string DepartmentName
			{
				get {  return _departmentName;}
				set {  _departmentName = value;}
			}
			public string Email
			{
				get {  return _email;}
				set {  _email = value;}
			}
			public string Phone
			{
				get {  return _phone;}
				set {  _phone = value;}
			}
			public string Role
			{
				get {  return _role;}
				set {  _role = value;}
			}
			public string Available
			{
				get {  return _available;}
				set {  _available = value;}
			}
			public string Remark
			{
				get {  return _remark;}
				set {  _remark = value;}
			}
	}
}
