namespace Todo.Common.Constants {
    public static class ResultCodes {
        public static class Common {
            public const string OK = "OK";
            public const string ERROR = "ERROR";
            public const string INCORRECT_INPUT_DATA_ERROR = "INCORRECT_INPUT_DATA_ERROR";
            public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
        }

        public static class Authorization {
            public const string UNKNOWN_GRANT_TYPE = "UNKNOWN_GRANT_TYPE";
            public const string ACCESS_TOKEN_RETRIEVED = "ACCESS_TOKEN_RETRIEVED";
            public const string CLIENT_NOT_FOUND = "CLIENT_NOT_FOUND";
            public const string INCORRECT_USER_CREDENTIALS = "INCORRECT_USER_CREDENTIALS";
            public const string INCORRECT_REFRESH_TOKEN = "INCORRECT_REFRESH_TOKEN";
        }

        public static class Errors {
            public const string RESET_PASSWORD_ERROR = "RESET_PASSWORD_ERROR";
            public const string CHANGE_PASSWORD_ERROR = "CHANGE_PASSWORD_ERROR";
            public const string INCORRECT_EMAIL_FORMAT = "INCORRECT_EMAIL_FORMAT";
            public const string CREATE_USER_ERROR = "CREATE_USER_ERROR";
            public const string UPDATE_USER_ERROR = "UPDATE_USER_ERROR";
            public const string UPDATE_USER_ROLES_ERROR = "UPDATE_USER_ERROR";
        }

        public static class RequiredFields {
            public const string GRANT_TYPE_REQUIRED = "GRANT_TYPE_REQUIRED";
            public const string CLIENT_KEY_REQUIRED = "CLIENT_KEY_REQUIRED";
            public const string CLIENT_SECRET_REQUIRED = "CLIENT_SECRET_REQUIRED";
            public const string CLIENT_NAME_REQUIRED = "CLIENT_NAME_REQUIRED";
            public const string EMAIL_REQUIRED = "EMAIL_REQUIRED";
            public const string PASSWORD_REQUIRED = "PASSWORD_REQUIRED";
            public const string REFRESH_TOKEN_REQUIRED = "REFRESH_TOKEN_REQUIRED";
            public const string USER_EMAIL_REQUIRED = "USER_EMAIL_REQUIRED";
            public const string USER_FIRSTNAME_REQUIRED = "USER_FIRSTNAME_REQUIRED";
            public const string USER_LASTNAME_REQUIRED = "USER_LASTNAME_REQUIRED";
            public const string RESET_TOKEN_REQUIRED = "RESET_TOKEN_REQUIRED";
            public const string NEW_PASSWORD_REQUIRED = "NEW_PASSWORD_REQUIRED";
            public const string CURRENT_PASSWORD_REQUIRED = "CURRENT_PASSWORD_REQUIRED";
            public const string LABEL_NAME_REQUIRED = "LABEL_NAME_REQUIRED";
            public const string SPRINT_NAME_REQUIRED = "SPRINT_NAME_REQUIRED";
            public const string SUBJECT_NAME_REQUIRED = "SUBJECT_NAME_REQUIRED";
        }

        public static class FieldRestrictions {
            public const string MAX_LENGTH_REACHED = "MAX_LENGTH_REACHED";
            public const string START_GREATER_THAN_FINISH = "START_GREATER_THAN_FINISH";
        }
    }
}