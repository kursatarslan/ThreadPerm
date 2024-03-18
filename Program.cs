class Program{
    static Mutex mutex = new Mutex();
    static Semaphore semaphore = new Semaphore(2, 2); // Initial count and maximum thread count
    static int threadCount = 0;
    private static readonly object _lockObj = new object();
    private static readonly object _monitorObj = new object();

    static void Main() {
        Console.WriteLine("Hello, World!");
       
        for(int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(DoWork);
            thread.Start();
        }

        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(DoWorkSemaphore);
            thread.Start();
        }

                
        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(DoWorkLock);
            thread.Start();
        }

        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(DoWorkMonitor);
            thread.Start();
        }

       
        

}
    static void DoWorkLock() {
        lock(_lockObj){
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} waiting for Lock");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} acquired the Lock.");
        //TODO
            threadCount = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} releasing the Lock.");

        }
        

    }
    static void DoWorkMonitor() {
        Monitor.Enter(_monitorObj);
        try{
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} waiting for Monitor");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} acquired the Monitor.");
        //TODO
            threadCount = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} releasing the Monitor.");

        }finally {
            Monitor.Exit(_monitorObj);
        }
        

    }
    static void DoWorkSemaphore() {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} waiting for Semaphore");
        semaphore.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} acquired the Semaphore.");
        //TODO
        threadCount = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} releasing the Semaphore.");
        semaphore.Release(); // Release the Mutex


    }
    static void DoWork() {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} waiting for mutex");
        mutex.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} acquired the mutex.");
        //TODO
        threadCount = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} releasing the mutex.");
        mutex.ReleaseMutex(); // Release the Mutex

    }
}
