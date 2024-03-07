using AION.BL;
using AION.BL.Adapters;
using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static DateTime CurrentQuarterTime(this DateTime dateTime)
    {
        //if(dateTime.Minute >= 45)
        //    return
        return new DateTime(dateTime.Year, dateTime.Month,
        dateTime.Day, dateTime.Hour, (dateTime.Minute / 15) * 15, 0);
    }

    public static DateTime CurrentHalfTime(this DateTime dateTime)
    {
        //if(dateTime.Minute >= 45)
        //    return
        return new DateTime(dateTime.Year, dateTime.Month,
        dateTime.Day, dateTime.Hour, (dateTime.Minute / 30) * 30, 0);
    }

    public static bool FillAllDesignatedDepartments(this List<UserIdentity> userIdentity)
    {
        for (int i = 0; i < userIdentity.Count; i++)
        {
            userIdentity[i].FillDesignatedDepartments();
        }
        return true;
    }

    public static decimal AdjustToHalfHour(this decimal? input)
	{
		double whole = (double)Math.Truncate(input.Value);
		double remainder = (double)input - whole;

		if (remainder < 0.3)
		{
			remainder = 0;
		}
		else if (remainder < 0.8)
		{
			remainder = 0.5;
		}
		else
		{
			remainder = 1;
		}

		double finalValue = whole + remainder;

		return (decimal)finalValue;
	}

	/// <summary>
	/// This is used to fill the departments based on jurisdiction for zoning and fire
	/// 
	/// </summary>
	/// <param name="userIdentity"></param>
	/// <returns></returns>
	public static UserIdentity FillDesignatedDepartments(this UserIdentity userIdentity)
    {
        if (userIdentity.DesignatedDepartments == null || userIdentity.DesignatedDepartments.Count == 0)
        {
            userIdentity.DesignatedDepartments = userIdentity.ID > 0 ?
                            new UserAdapter().GetAllDepartmentsByUserIdWSOI(userIdentity.ID) : new List<Department>();

        }
        return userIdentity;
    }
}

