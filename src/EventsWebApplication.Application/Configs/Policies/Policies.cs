namespace EventsWebApplication.Application.Configs.Policies
{
    public static class Policies
    {
        public const string AdminOnlyActions = "AdminOnlyActions";
        public const string AuthorizedUserActions = "AuthorizedUserActions";

        public const string CreateEvents = "CreateEvents";
        public const string UpdateEvents = "UpdateEvents";
        public const string DeleteEvents = "DeleteEvents";

        public const string ViewEvents = "ViewEvents";
        public const string RegisterForEvent = "RegisterForEvent";
        public const string UnregisterFromEvent = "UnregisterFromEvent";
    }
}