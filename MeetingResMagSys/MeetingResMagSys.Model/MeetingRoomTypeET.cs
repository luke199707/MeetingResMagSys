//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingRoomTypeET
	{	
			private int _id;
			private string _organizationId;
			private string _roomTypeId;
			private string _cname;
			private string _value;
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
			public string RoomTypeId
			{
				get {  return _roomTypeId;}
				set {  _roomTypeId = value;}
			}
			public string Cname
			{
				get {  return _cname;}
				set {  _cname = value;}
			}
			public string Value
			{
				get {  return _value;}
				set {  _value = value;}
			}
	}
}
