var cts = new CancellationTokenSource();
var wakeUpPing = new AutoResetEvent(true);
var wakeUpPong = new AutoResetEvent(false);

var pingTask = Task.Run(() => ThreadWithSync(() => Write("ping"), wakeUpPing, wakeUpPong, cts.Token));
var pongTask = Task.Run(() => ThreadWithSync(() => Write("pong"), wakeUpPong, wakeUpPing, cts.Token));

await FinishWhenKeyPressed();

void ThreadWithSync(Action action, WaitHandle wakeUpWaiter, EventWaitHandle wakeUpNext, CancellationToken cancellatioNToken)
{
    while (!cancellatioNToken.IsCancellationRequested)
    {
        wakeUpWaiter.WaitOne();
        action?.Invoke();
        wakeUpNext.Set();
    }
}

void Write(string text)
{
    Console.WriteLine(text);
}

Task FinishWhenKeyPressed()
{
    Console.ReadKey();
    cts.Cancel();
    return Task.WhenAll(pingTask, pongTask);
}
