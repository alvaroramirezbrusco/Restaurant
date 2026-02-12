namespace Domain.Constants
{
    public static class StatusContraints
    {
        public const int NameMaxLength = 25;
    }
    public static class StatusIds
    {
        public const int Pending = 1;
        public const int InProgress = 2;
        public const int Ready = 3;
        public const int Delivery = 4;
        public const int Closed = 5;
    }
    public static class StatusNames
    {
        public const string Pending = "Pending";
        public const string InProgress = "In progress";
        public const string Ready = "Ready";
        public const string Delivery = "Delivery";
        public const string Closed = "Closed";
    }
}
