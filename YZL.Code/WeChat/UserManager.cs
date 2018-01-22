using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZL.Code.WeChat
{
    public class UserManager
    {
        private string access_token ;

        public UserManager()
        {
            access_token=new AccessToken().Get();
        }

        
    }
}
