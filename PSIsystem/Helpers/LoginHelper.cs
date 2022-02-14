using System.Linq;
using System.Web;

namespace PSIsystem.Helpers
{
    public class LoginHelper
    {
        private const string _sessionKey = "IsLogined";
        public static bool HasLogined()//判斷是否有登入
        {
            var val = HttpContext.Current.Session[_sessionKey] as LoginInfo;
            if (val == null)
                return false;
            else
                return true;
        }
        public static bool TryLogin(string Account, string Pwd)
        {
            if (LoginHelper.HasLogined())
            {
                return true;
            }

            //讀取DB以及檢查帳號密碼是否正確/存在
            using (var context = new Models.PSIsystemModel())
            {
                
                var query = context.Users
                    .Where(item => item.Account == Account)
                    .Select(item => new { 
                    ID = item.ID,
                    Name = item.Name,
                    Level = item.Level,
                    PWD = item.PWD
                    });

                if (query.FirstOrDefault() == null ||
                    string.Compare(Pwd.Trim(), query.FirstOrDefault().PWD.Trim(), false) != 0)
                    return false;

                HttpContext.Current.Session[_sessionKey] = new LoginInfo()
                {
                    ID = query.FirstOrDefault().ID,
                    Name = query.FirstOrDefault().Name,
                    Level = (int)query.FirstOrDefault().Level,
                };

            }
            

                return true;

        }
        /// <summary> 登出目前使用者，如果還沒登入就不執行 </summary>
        public static void Logout()
        {
            if (!LoginHelper.HasLogined())
                return;

            HttpContext.Current.Session.Remove(_sessionKey);
        }

        /// <summary> 取得已登入者的資訊，如果還沒登入回傳 NULL </summary>
        /// <returns></returns>
        public static LoginInfo GetCurrentUserInfo()
        {
            if (!LoginHelper.HasLogined())
                return null;

            return HttpContext.Current.Session[_sessionKey] as LoginInfo;
        }
    }
}