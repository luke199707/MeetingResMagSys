//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingReservation
	{	
			private int _id;
			private string _meetingId;
			private string _title;
			private string _meetingRoom;
			private string _introduction;
			private string _time;
			private string _startTime;
			private string _endTime;
			private string _booker;
			private string _department;
			private string _state;
			private string _reviewer;
			private string _organizationId;
			private string _orderTime;
			private string _remark;
			private string _refuseReason;
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
			public string Title
			{
				get {  return _title;}
				set {  _title = value;}
			}
			public string MeetingRoom
			{
				get {  return _meetingRoom;}
				set {  _meetingRoom = value;}
			}
			public string Introduction
			{
				get {  return _introduction;}
				set {  _introduction = value;}
			}
			public string Time
			{
				get {  return _time;}
				set {  _time = value;}
			}
			public string StartTime
			{
				get {  return _startTime;}
				set {  _startTime = value;}
			}
			public string EndTime
			{
				get {  return _endTime;}
				set {  _endTime = value;}
			}
			public string Booker
			{
				get {  return _booker;}
				set {  _booker = value;}
			}
			public string Department
			{
				get {  return _department;}
				set {  _department = value;}
			}
			public string State
			{
				get {  return _state;}
				set {  _state = value;}
			}
			public string Reviewer
			{
				get {  return _reviewer;}
				set {  _reviewer = value;}
			}
			public string OrganizationId
			{
				get {  return _organizationId;}
				set {  _organizationId = value;}
			}
			public string OrderTime
			{
				get {  return _orderTime;}
				set {  _orderTime = value;}
			}
			public string Remark
			{
				get {  return _remark;}
				set {  _remark = value;}
			}
			public string RefuseReason
			{
				get {  return _refuseReason;}
				set {  _refuseReason = value;}
			}
	}
}
