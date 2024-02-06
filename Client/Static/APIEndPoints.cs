namespace Client.Static
{
    public static class APIEndPoints
    {
        //Server URL
        public const string ServerBaseUrl = "https://localhost:7184";


        //API EndPoints
        public const string GetAllRolesUrl = $"{ServerBaseUrl}/allRoles";

        public const string GetAllDegreesUrl = $"{ServerBaseUrl}/allDegrees";

        public const string GetAllStudentsUrl = $"{ServerBaseUrl}/students";

        public const string AddStudentUrl = $"{ServerBaseUrl}/student/Add";

        public static string DeleteStudentUrl(int rollNo) => $"{ServerBaseUrl}/studentDelete/{rollNo}";
    }
}
