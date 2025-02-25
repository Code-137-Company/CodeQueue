using Code137.JsonDb.Attributes;
using Code137.JsonDb.Models;

namespace CodeQueue.Domain.Entities
{
    [JsonDbEntity("Queues")]
    public class Queue : AbstractEntity
    {
        [JsonDbProperty(notNull: true)]
        public int Consumers { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public int Order { get; set; }
    }

    public class Message
    {
        [JsonDbProperty(notNull: true)]
        public Guid Id { get; set; }
        [JsonDbProperty(notNull: true)]
        public string Payload { get; set; }
        public bool InProcessing { get; set; }

        public Message() => Id = Guid.NewGuid();
    }
}
