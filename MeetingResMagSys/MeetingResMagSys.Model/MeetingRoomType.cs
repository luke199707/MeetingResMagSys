//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingRoomType
	{	
			private int _id;
			private string _roomTypeId;
			private string _name;
			private string _introduction;
			private string _organizationId;
			private string _remark;
			private string _c1;
			private string _c2;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string RoomTypeId
			{
				get {  return _roomTypeId;}
				set {  _roomTypeId = value;}
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
			public string OrganizationId
			{
				get {  return _organizationId;}
				set {  _organizationId = value;}
			}
			public string Remark
			{
				get {  return _remark;}
				set {  _remark = value;}
			}
			public string C1
			{
				get {  return _c1;}
				set {  _c1 = value;}
			}
			public string C2
			{
				get {  return _c2;}
				set {  _c2 = value;}
			}
			
	}
}
