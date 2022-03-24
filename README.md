# GeekProto
Super Fast Binary Serialization Library

## 支持列表： ##
所有基础类型 byte sbyte bool short int long float double string byte[]

枚举类型

容器类型：Dictionary List HashSet

容器单层嵌套：Dictionary<int, List<int>> Dictionary<int, HashSet<int>> Dictionary<int, Dictionary<int, int>>

自定义类型(T),并可以与容器组合使用，如：Dictionary<int, Dictionary<int, T>>

Dictionary的key只能为：基础类型和枚举

## 不支持列表： ##
容器多层嵌套

复杂对象Key的Dictionary
  
