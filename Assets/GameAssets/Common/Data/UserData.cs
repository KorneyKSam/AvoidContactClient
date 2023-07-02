using System;

namespace Common.Data
{
    [Serializable]
    public class UserData
    {
        public bool IsAutomaticAuthorization { get; set; }

        public UserData()
        {
            //Do nothing
        }
    }
}