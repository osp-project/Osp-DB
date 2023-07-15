namespace Osp_DB
{
    public class DBStructs
    {
        public class DBRet
        {
            public bool status; // Success？
            public string? ret_msg; // Has anything to return？
            public string? err_msg; // If the status is false, has any errorinfo or reason to return？
        }

        public class DBNormalJson
        {
            public string type; // Such as "UserAccount" "Config" "ApplicationLog"
            public string label; // Such as "UserAccount#Mike.001#DB#LABEL" "WebArchive20230101"
            public object obj; // Any Object Before Serialize
        }
    }
}
