using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnRoad
{
    public class DbUtils
    {
        public static OnRoadEntities Db;
        static DbUtils()
        {
            try
            {
                Db = new OnRoadEntities();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка");
            }
        }
        public static bool Authorization(string Login, string Password)
        {
            if (!DbUtils.Db.Employee.Any(x => x.Login == Login))
                return false;
            var User = DbUtils.Db.Employee.Single(x => x.Login == Login);
            return User.Password == Password;
        }   
    }
}
