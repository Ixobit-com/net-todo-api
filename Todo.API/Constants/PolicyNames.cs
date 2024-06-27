namespace Todo.API.Constants {
    public static class PolicyNames {
        public const string Cors = "cors-policy";

        public const string AuthorizedAsUser = "authorized-as-user-policy";

        public const string ScopeRead = "scope-read-policy";

        public const string RoleRead = "role-read-policy";

        public const string ClientCreate = "client-create-policy";
        public const string ClientRead = "client-read-policy";
        public const string ClientUpdate = "client-update-policy";
        public const string ClientDelete = "client-delete-policy";
        public const string ClientReset = "client-reset-policy";

        public const string UserCreate = "user-create-policy";
        public const string UserRead = "user-read-policy";
        public const string UserUpdate = "user-update-policy";
        public const string UserDelete = "user-delete-policy";

        public const string EmailTemplateRead = "email-template-read-policy";
        public const string EmailTemplateUpdate = "email-template-update-policy";
        public const string EmailTemplatePreview = "email-template-preview-policy";

        public const string LabelCreate = "label-create-policy";
        public const string LabelRead = "label-read-policy";
        public const string LabelUpdate = "label-update-policy";
        public const string LabelDelete = "label-delete-policy";

        public const string SprintCreate = "sprint-create-policy";
        public const string SprintRead = "sprint-read-policy";
        public const string SprintUpdate = "sprint-update-policy";
        public const string SprintDelete = "sprint-delete-policy";

        public const string TicketCreate = "ticket-create-policy";
        public const string TicketRead = "ticket-read-policy";
        public const string TicketUpdate = "ticket-update-policy";
        public const string TicketDelete = "ticket-delete-policy";
    }
}