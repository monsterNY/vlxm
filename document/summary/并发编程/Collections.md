
#### ArrayList ####

> ArrayList是否是线程安全类?

测试代码:
  	
	ArrayList list = new ArrayList();
	for (int i = 0; i < 10; i++)
	{
		Task.Run((() =>
		{
		  Thread.Sleep(10);
		  list.Add(1);
		  list.Add(2);
		
		  foreach (var item in list)
		    Console.Write(item);
		
		  Console.WriteLine();
		  Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId} 执行完毕！");
		
		}));
	}

执行结果:



> 在Add 时 出错:
> 
> System.IndexOutOfRangeException:“Index was outside the bounds of the array.”

----------
since:5/22/2019 4:34:52 PM 

direction:Collections