using Grpc.Core;
using gRPC;

namespace gRPC.Services;

public class TaskerService : Tasker.TaskerBase
{
    private static List<CustomTask> _tasks = new();

    private readonly ILogger<TaskerService> _logger;

    public TaskerService(ILogger<TaskerService> logger)
    {
        _logger = logger;
    }

    public override Task<AddTaskReply> AddTask(AddTaskRequest request, ServerCallContext context)
    {
        _tasks.Add(request.Task);

        return Task.FromResult(new AddTaskReply
        {
            Task = new CustomTask()
            {
                Id = _tasks.IndexOf(request.Task),
            }
        });
    }
    
    public override Task<GetTaskReply> GetTask(GetTaskRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetTaskReply
        {
            Task = _tasks[request.Task.Id]
        });
    }
}