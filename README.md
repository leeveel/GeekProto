# GeekProto
Super Fast Binary Serialization Library

性能高，数据量小，支持ILRuntime

## 支持列表： ##
所有基础类型
byte sbyte char bool short ushort int uint long ulong float double   
string byte[] DateTime  


枚举类型

常用容器类型：Dictionary List HashSet LinkedList

容器任意嵌套：Dictionary<int, List<int>> Dictionary<int, HashSet<int>> Dictionary<int, Dictionary<int, int>>

自定义类型(T),并可以与容器组合使用，如：Dictionary<int, Dictionary<int, T>>

Dictionary的key只能为：基础类型和枚举

## 不支持列表： ##

复杂对象Key的Dictionary

## 性能测试 ##

![](https://github.com/leeveel/GeekProto/blob/main/Doc/test.png)
  
