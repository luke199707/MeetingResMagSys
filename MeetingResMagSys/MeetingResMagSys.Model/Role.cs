//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class Role
	{	
			private int _id;
			private string _roleId;
			private string _roleName;
			private string _organizationId;
			private string _defaultRole;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string RoleId
			{
				get {  return _roleId;}
				set {  _roleId = value;}
			}
			public string RoleName
			{
				get {  return _roleName;}
				set {  _roleName = value;}
			}
			public string OrganizationId
			{
				get {  return _organizationId;}
				set {  _organizationId = value;}
			}
			public string DefaultRole
			{
				get {  return _defaultRole;}
				set {  _defaultRole = value;}
			}
			public string Remark
			{
				get {  return _remark;}
				set {  _remark = value;}
			}
	}
}
