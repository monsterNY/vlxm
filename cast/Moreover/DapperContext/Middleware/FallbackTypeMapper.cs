﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;

namespace DapperContext.Middleware
{
  /// <summary>
  /// @desc : FallbackTypeMapper  
  /// @author :mons
  /// @create : 2019/3/12 10:06:00 
  /// @source : 
  /// </summary>
  public class FallbackTypeMapper : SqlMapper.ITypeMap
  {
    private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

    public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
    {
      _mappers = mappers;
    }


    public ConstructorInfo FindConstructor(string[] names, Type[] types)
    {
      foreach (var mapper in _mappers)
      {
        try
        {
          ConstructorInfo result = mapper.FindConstructor(names, types);
          if (result != null)
          {
            return result;
          }
        }
        catch (NotImplementedException)
        {
        }
      }
      return null;
    }

    public ConstructorInfo FindExplicitConstructor()
    {
      return _mappers
        .Select(mapper => mapper.FindExplicitConstructor())
        .FirstOrDefault(result => result != null);
    }

    public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
    {
      foreach (var mapper in _mappers)
      {
        try
        {
          var result = mapper.GetConstructorParameter(constructor, columnName);
          if (result != null)
          {
            return result;
          }
        }
        catch (NotImplementedException)
        {
        }
      }
      return null;
    }

    public SqlMapper.IMemberMap GetMember(string columnName)
    {
      foreach (var mapper in _mappers)
      {
        try
        {
          var result = mapper.GetMember(columnName);
          if (result != null)
          {
            return result;
          }
        }
        catch (NotImplementedException)
        {
        }
      }
      return null;
    }
  }
}
