// The port number must match the port of the gRPC server.
using Grpc.Net.Client;
using gRPC;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");

var greeterClient = new Greeter.GreeterClient(channel);
var reply = await greeterClient.SayHelloAsync(
    new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);

var ageReply = await greeterClient.GetAgeAsync(
    new AgeRequest { Birthday = 1990 });
Console.WriteLine("Age: " + ageReply.Age);

var taskerClient = new Tasker.TaskerClient(channel);

var addTaskReply = await taskerClient.AddTaskAsync(
    new AddTaskRequest
    {
        Task = new CustomTask
        {
            Task = "Super Task",
            Id = 1 
        }
    });

Console.WriteLine("Task added with id: " + addTaskReply.Task.Id);

var getTaskReply = await taskerClient.GetTaskAsync(
    new GetTaskRequest
    {
        Task = new CustomTask
        {
            Id = addTaskReply.Task.Id
        }
    });

Console.WriteLine("Task: " + getTaskReply.Task.Task);

Console.WriteLine("Press any key to exit...");
Console.ReadKey();