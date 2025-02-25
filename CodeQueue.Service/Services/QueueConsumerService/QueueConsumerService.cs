using Code137.JsonDb;
using CodeQueue.Domain.Entities;
using CodeQueue.Domain.Models;
using CodeQueue.Service.Common;
using Newtonsoft.Json;

namespace CodeQueue.Service.Services.QueueConsumerService
{
    public class QueueConsumerService
    {
        private readonly JsonDb _jsonDb;

        public QueueConsumerService(JsonDb jsonDb)
        {
            _jsonDb = jsonDb;
        }

        public void Start()
        {
            CreateQueues();

            while (true)
            {

            }
        }

        private void CreateQueues()
        {
            var pathQueues = CommonPath.GetFullPathQueues();

            var json = File.ReadAllText(pathQueues);

            var queuesModel = JsonConvert.DeserializeObject<List<QueueModel>>(json);

            var queues = new List<Queue>();

            int count = 0;

            foreach (var queueModel in queuesModel)
            {
                var queue = _jsonDb.GetOne<Queue>(x => x.Name == queueModel.Name);

                if (queue != null)
                    continue;

                _jsonDb.Insert(new Queue()
                {
                    Consumers = queueModel.Consumers,
                    Name = queueModel.Name,
                    Order = count
                }, out string message);

                count++;
            }

            var queuesDb = _jsonDb.Get<Queue>(x => !queuesModel.Select(y => y.Name).Contains(x.Name));

            foreach (var queueDb in queuesDb)
                _jsonDb.Delete<Queue>(queueDb.Id, out string deleteMessage);
        }
    }
}
