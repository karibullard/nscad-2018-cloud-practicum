using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace API.GraphHelper
{
    public interface IAuthProvider
    {
        Task<string> GetUserAccessTokenAsync();
    }
}