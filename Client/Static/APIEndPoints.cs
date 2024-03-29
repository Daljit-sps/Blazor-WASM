﻿namespace Client.Static
{
    public static class APIEndPoints
    {
        //Server URL
        public const string ServerBaseUrl = "https://localhost:7184";


        //API EndPoints
        public const string GetAllRolesUrl = $"{ServerBaseUrl}/allRoles";

        public const string GetAllDegreesUrl = $"{ServerBaseUrl}/allDegrees";

        public const string GetAllStudentsUrl = $"{ServerBaseUrl}/students";

        public static string GetAllStudentsWithPaginationUrl(int PageIndex, int PageSize) => $"{ServerBaseUrl}/pagination/{PageIndex}{PageSize}";

        public const string AddStudentUrl = $"{ServerBaseUrl}/student/Add";

        public const string LoginUrl = $"{ServerBaseUrl}/login";

        public static string UpdateStudentUrl(int rollNo) => $"{ServerBaseUrl}/student/Update/{rollNo}";

        public static string GetStudentByIdUrl(int rollNo) => $"{ServerBaseUrl}/student/{rollNo}";

        public static string DeleteStudentUrl(int rollNo) => $"{ServerBaseUrl}/studentDelete/{rollNo}";
    }
}
