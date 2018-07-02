namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IUserGrade
    {
        int AddUserGrade(UserGradeInfo userGrade);
        void DeleteUserGrade(string strID);
        List<UserGradeInfo> ReadUserGradeAllList();
        void UpdateUserGrade(UserGradeInfo userGrade);
    }
}

