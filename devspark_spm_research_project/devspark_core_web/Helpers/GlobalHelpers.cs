namespace devspark_core_web.Helpers
{
    public static class GlobalHelpers
    {
        public static string LearnerPortalDataProtectorSecret = "PBJ9SalsV9Q0vgNL3FUtXOD1s0TPLSp5";

        public static void SetBool(this ISession session, string key, bool value)
        {
            session.SetInt32(key, value ? 1 : 0);
        }

        public static bool? GetBool(this ISession session, string key)
        {
            var value = session.GetInt32(key);

            return value.HasValue ? value == 1 : (bool?)null;
        }
    }
}
