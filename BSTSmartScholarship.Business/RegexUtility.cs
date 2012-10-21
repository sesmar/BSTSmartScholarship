namespace BSTSmartScholarship.Business
{
	#region "Using Statements"

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	#endregion

	public static class RegexUtility
	{
		#region "Using Statements"

		public const string PhonePattern = "^(?<long>1)?[-| |.]?(\\({0,1}(?<area>\\d{3})\\){0,1})[-| |.]?(?<phone1>\\d{3})[-| |.]?(?<phone2>\\d{4})\\z";
		public const string FormatPhonePattern = "(${area}) ${phone1}-${phone2}";
		public const string FormatPhoneDottedPattern = "${area}.${phone1}.${phone2}";
		public const string UnFormatPhonePattern = "${area}${phone1}${phone2}";
		public const string EmailPattern = @"^[a-zA-Z0-9_\+-]+(\.[a-zA-Z0-9_\+-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.([a-zA-Z]{2,4})\z";
		public const string AddressPattern = "^(?<number>\\d+?)\\s(?<name>.+?)\\z";
		public const string BirthdayPattern = @"^((1[0-2])|(0?[1-9]))/(([0-2]?[0-9])|(3[01]))\z";

		#endregion

		#region "Format Methods"

		public static string FormatPhone(string phoneNumber)
		{
			if (!String.IsNullOrEmpty(phoneNumber))
			{
				return Regex.Replace(phoneNumber, PhonePattern, FormatPhonePattern);
			}

			return String.Empty;
		}

		public static string FormatPhoneDotted(string phoneNumber)
		{
			if (!String.IsNullOrEmpty(phoneNumber))
			{
				return Regex.Replace(phoneNumber, PhonePattern, FormatPhoneDottedPattern);
			}

			return String.Empty;
		}

		public static string UnFormatPhone(string phoneNumber)
		{
			return Regex.Replace(phoneNumber, PhonePattern, UnFormatPhonePattern);
		}

		#endregion
	}
}
