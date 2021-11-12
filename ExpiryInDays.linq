<Query Kind="Statements" />

// expiry in days
var limitDays = 17 + 0.99;
limitDays.Dump("Limit Days");
var enrollDate = new DateTime(2021, 8, 1);
enrollDate.Dump("Enrolled Date");
var expiryDate = enrollDate.AddDays(limitDays);
expiryDate.Dump("Expiration Date");
DateTime.Today.Dump("Today");
var diffTimespan = DateTime.Now.Subtract(enrollDate);
var onboardDays = diffTimespan.TotalHours / 24;
onboardDays.Dump("onboard days");

// has it been too many days?
var isExpired = DateTime.Today >= expiryDate;
isExpired.Dump("Is Expired");