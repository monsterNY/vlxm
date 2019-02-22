using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;

namespace NoSafe.SiteDemo
{

  /// <summary>
  /// @source:https://blog.marcgravell.com/2018/07/pipe-dreams-part-1.html
  /// </summary>
  public class PipDemo
  {


    /**
     *  Pipe与MemoryStream大致相当，除了不能多次回放，数据更简单地是“先进先出”队列
     *
     *  管道通常是异步操作
     *
     */
    public async void Demo()
    {

      Pipe pipe = new Pipe();
      // write something
      await WriteSomeDataAsync(pipe.Writer);
      // signal that there won't be anything else written
      pipe.Writer.Complete();
      // consume it
      await ReadSomeDataAsync(pipe.Reader);

    }

    async ValueTask WriteSomeDataAsync(PipeWriter writer)
    {
      // use an oversized size guess
      Memory<byte> workspace = writer.GetMemory(20);
      // write the data to the workspace
      int bytes = Encoding.ASCII.GetBytes(
        "hello, world!", workspace.Span);
      // tell the pipe how much of the workspace
      // we actually want to commit
      writer.Advance(bytes);
      // this is **not** the same as Stream.Flush!
      await writer.FlushAsync();
    }

    async ValueTask ReadSomeDataAsync(PipeReader reader)
    {
      while (true)
      {
        // await some data being available
        ReadResult read = await reader.ReadAsync();
        ReadOnlySequence<byte> buffer = read.Buffer;
        // check whether we've reached the end
        // and processed everything
        if (buffer.IsEmpty && read.IsCompleted)
          break; // exit loop

        // process what we received
        foreach (ReadOnlyMemory<byte> segment in buffer)
        {
          string s = Encoding.ASCII.GetString(
            segment.Span);
          Console.Write(s);
        }
        // tell the pipe that we used everything
        reader.AdvanceTo(buffer.End);
      }
    }

    #region stream - demo

    public void StreamDemo()
    {
      using (MemoryStream ms = new MemoryStream())
      {
        // write something
        WriteSomeData(ms);
        // rewind - MemoryStream works like a tape
        ms.Position = 0;
        // consume it
        ReadSomeData(ms);
      }
    }

    void WriteSomeData(Stream stream)
    {
      byte[] bytes = Encoding.ASCII.GetBytes("hello, world!");
      stream.Write(bytes, 0, bytes.Length);
      stream.Flush();
    }

    void ReadSomeData(Stream stream)
    {
      int bytesRead;
      // note that the caller usually can't know much about
      // the size; .Length is not usually usable
      byte[] buffer = new byte[256];
      do
      {
        bytesRead = stream.Read(buffer, 0, buffer.Length);
        if (bytesRead > 0)
        {   // note this only works for single-byte encodings
          string s = Encoding.ASCII.GetString(
            buffer, 0, bytesRead);
          Console.Write(s);
        }
      } while (bytesRead > 0);
    }

    #endregion

  }
}
