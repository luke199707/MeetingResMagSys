//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingMember
	{	
			private int _id;
			private string _meetingId;
			private string _userId;
			private string _organizationId;
			private string _organizationName;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string MeetingId
			{
				get {  return _meetingId;}
				set {  _meetingId = value;}
			}
			public string UserId
			{
				get {  return _userId;}
				set {  _userId = value;}
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
