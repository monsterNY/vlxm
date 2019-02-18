using System;
using System.Collections.Generic;
using System.Text;

namespace NoSafe.CusInterface
{
  public interface IMemoryBlockCopier
  {
    void Copy<T>(T[] source, T[] destination);
    void Copy<T>(T[] source, int sourceStartIndex, T[] destination, int destinationStartIndex, int elementsCount);
    unsafe void Copy<T>(void* source, void* destination, int elementsCount);
    unsafe void Copy<T>(void* source, int sourceStartIndex, void* destination, int destinationStartIndex, int elementsCount);
    unsafe void Copy<T>(void* source, int sourceLength, T[] destination);
    unsafe void Copy<T>(void* source, int sourceStartIndex, T[] destination, int destinationStartIndex, int elementsCount);
  }
}
