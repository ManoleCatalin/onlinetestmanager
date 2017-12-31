using System;

namespace OTM.UserContext
{
    public interface IUserContext
    {
        Guid? GetLogedInUserId();
    }
}
