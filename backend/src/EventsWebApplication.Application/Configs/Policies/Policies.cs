namespace EventsWebApplication.Application.Configs.Policies
{
    public static class Policies
    {
        public const string CORS = "AllowOrigin";

        public const string AdminOnlyActions = "AdminOnlyActions";

        public const string CreateEvents = "CreateEvents";
        public const string UpdateEvents = "UpdateEvents";
        public const string DeleteEvents = "DeleteEvents";

        public const string RegisterForEvent = "RegisterForEvent";
        public const string UnregisterFromEvent = "UnregisterFromEvent";
    }
}