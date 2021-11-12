<Query Kind="Statements" />

var enroll = new DateTime(2021, 8, 28, 5, 0, 0).Date;
var today = new DateTime(2021, 8, 29, 2, 22, 0).Date;
var days = today.Subtract(enroll).TotalDays;
var onboardDays = Math.Floor(days) + 1;

enroll.Dump("enrolled");
today.Dump("today");
days.Dump("today - enrolled");
onboardDays.Dump("ceiling onboard days");

