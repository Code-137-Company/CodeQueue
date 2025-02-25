using Code137.JsonDb.Attributes;
using Code137.JsonDb.Models;

namespace CodeQueue.Domain.Entities
{
    [JsonDbEntity("Messages")]
    public class Message : AbstractEntity
    {
        [JsonDbProperty(notNull: true)]
        public string Payload { get; set; }
    }
}
