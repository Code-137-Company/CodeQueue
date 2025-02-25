using Code137.JsonDb;
using CodeQueue.Domain.Entities;
using CodeQueue.Domain.Models;
using CodeQueue.Service.Common;
using CodeQueue.Service.Services.SocketService;
using Newtonsoft.Json;

namespace CodeQueue.Service.Services.QueueConsumerService
{
    public class QueueConsumerService
    {
        private readonly JsonDb _jsonDb;
        private readonly SocketServer _socket;

        public QueueConsumerService(JsonDb jsonDb, SocketServer socket)
        {
            _jsonDb = jsonDb;
            _socket = socket;
        }

        public void Start()
        {
            CreateQueues();

            Task.Run(StartLoopAsync).Wait();
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

        private async Task StartLoopAsync()
        {
            while (true)
            {
                var queues = _jsonDb.GetAll<Queue>();

                foreach (var queue in queues)
                {
                    if (!queue.Messages.Any() || queue.Consumers < queue.Messages.Count(x => x.InProcessing) || !queue.Messages.Any(x => !x.InProcessing))
                        continue;

                    var message = queue.Messages.FirstOrDefault(x => !x.InProcessing);

                    /*
                     * Send Socket
                     */

                    message.InProcessing = true;

                    _jsonDb.Update(queue, out string updateMessage);
                }

                await _socket.AwaitReceive();
            }
        }
    }
}
