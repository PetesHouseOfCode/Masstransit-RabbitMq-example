namespace TestTransit.Shared
{
    public class MessageQueue
    {
        public static readonly string PublishMyMessage = "publish.mymessage";

        public static readonly string PublisherService = "mymessage.publisher.service";

        public static readonly string TrackerService = "mymessage.tracker.service";
    }
}