namespace devspark_core_web.Helpers
{
    public static class GlobalHelpers
    {
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
