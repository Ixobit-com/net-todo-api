namespace Todo.Data.DB.Constants {
    public static class DefaultIds {
        public static class Client {
            public static readonly Guid MasterId = new Guid("1D4254F3-23AC-4A90-82AC-DA236555F209");
        }

        public static class Scope {
            // Scope
            public static readonly Guid ScopeReadId = new Guid("A9A26B48-B3DC-4D48-90CE-56C164FA18E6");

            // Role
            public static readonly Guid RoleReadId = new Guid("36163DFC-37B8-4BC7-8D30-899024B71E6C");

            // Client
            public static readonly Guid ClientCreateId = new Guid("3747BA91-5EB5-4047-801F-245272B54417");
            public static readonly Guid ClientReadId = new Guid("25919D92-5CCA-4024-9C0A-AEC46CA8867B");
            public static readonly Guid ClientUpdateId = new Guid("3E9604E1-0109-4674-A5CA-5887BC33BEE6");
            public static readonly Guid ClientDeleteId = new Guid("78ABB407-5C11-40F7-8376-C4E7564CEF23");
            public static readonly Guid ClientResetId = new Guid("CF57362B-EF9C-40C2-9B94-624809C0CAC7");

            // User
            public static readonly Guid UserCreateId = new Guid("BBE3D5B4-6235-4087-BACE-836FDF8A230F");
            public static readonly Guid UserReadId = new Guid("0EA1AF57-D77D-4081-A92A-A248EC3779AD");
            public static readonly Guid UserUpdateId = new Guid("5B089C48-1FEB-4029-8394-930783EFE845");
            public static readonly Guid UserDeleteId = new Guid("117A96C0-8663-458F-AC3C-A052DDD752A4");

            // Sprint
            public static readonly Guid SprintCreateId = new Guid("9C959169-16C7-4E76-85F7-6E6B7F9AA659");
            public static readonly Guid SprintReadId = new Guid("04B30906-E855-4833-922F-6E4F5393077A");
            public static readonly Guid SprintUpdateId = new Guid("6337E4D5-9004-403B-BCBD-3E07DD3A86C8");
            public static readonly Guid SprintDeleteId = new Guid("C31B7D80-AA73-44F0-81C6-027A99B05A13");

            // Label
            public static readonly Guid LabelCreateId = new Guid("C28736EE-8BDC-42CC-95F7-6F24834B9538");
            public static readonly Guid LabelReadId = new Guid("130D7C29-225A-41A6-BE5D-4436743C7093");
            public static readonly Guid LabelUpdateId = new Guid("0DAF98DB-C828-4543-9582-42242980F526");
            public static readonly Guid LabelDeleteId = new Guid("A3EB3599-2BD8-43D1-AE38-A6D3AE7A58AE");

            // Ticket
            public static readonly Guid TicketCreateId = new Guid("EF0182E1-023A-44BA-A981-58C192EF3A39");
            public static readonly Guid TicketReadId = new Guid("4CDC4430-7353-4772-A34A-FFF87A708F73");
            public static readonly Guid TicketUpdateId = new Guid("B341BC42-40C5-480E-B460-D1BD36714FDF");
            public static readonly Guid TicketDeleteId = new Guid("3FF9AC83-0781-4EB1-8B7C-4AED65DC190B");
        }

        public static class Role {
            public static readonly Guid AdministratorId = new Guid("39D28B66-957A-4B4B-9342-68168F2E8803");
            public static readonly Guid UserId = new Guid("EBC079FA-6804-44AD-8B08-F635B8F1D59E");
        }
    }
}