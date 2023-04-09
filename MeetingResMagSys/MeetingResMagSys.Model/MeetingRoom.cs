//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingRoom
	{	
			private int _id;
			private string _roomId;
			private string _name;
			private string _image;
			private string _officeArea;
			private string _position;
			private string _capacity;
			private string _type;
			private string _introduction;
			private string _facility;
			private string _attention;
			private string _resDepartment;
			private string _director;
			private string _useRole;
			private string _useDepartment;
			private string _available;
			private string _reason;
			private string _organizationId;
			private string _isCheck;
			private DateTime? _time;
			private string _remark;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string RoomId
			{
				get {  return _roomId;}
				set {  _roomId = value;}
			}
			public string Name
			{
				get {  return _name;}
				set {  _name = value;}
			}
			public string Image
			{
				get {  return _image;}
				set {  _image = value;}
			}
			public string OfficeArea
			{
				get {  return _officeArea;}
				set {  _officeArea = value;}
			}
			public string Position
			{
				get {  return _position;}
				set {  _position = value;}
			}
			public string Capacity
			{
				get {  return _capacity;}
				set {  _capacity = value;}
			}
			public string Type
			{
				get {  return _type;}
				set {  _type = value;}
			}
			public string Introduction
			{
				get {  return _introduction;}
				set {  _introduction = value;}
			}
			public string Facility
			{
				get {  return _facility;}
				set {  _facility = value;}
			}
			public string Attention
			{
				get {  return _attention;}
				set {  _attention = value;}
			}
			public string ResDepartment
			{
				get {  return _resDepartment;}
				set {  _resDepartment = value;}
			}
			public string Director
			{
				get {  return _director;}
				set {  _director = value;}
			}
			public string UseRole
			{
				get {  return _useRole;}
				set {  _useRole = value;}
			}
			public string UseDepartment
			{
				get {  return _useDepartment;}
				set {  _useDepartment = value;}
			}
			public string Available
			{
				get {  return _available;}
				set {  _available = value;}
			}
			public string Reason
			{
				get {  return _reason;}
				set {  _reason = value;}
			}
			public string OrganizationId
			{
				get {  return _organizationId;}
				set {  _organizationId = value;}
			}
			public string IsCheck
			{
				get {  return _isCheck;}
				set {  _isCheck = value;}
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
