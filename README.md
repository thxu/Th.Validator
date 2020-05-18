# Th.Validator
C#参数验证组件，模仿Java的`Hibernate-Validator`做的一个参数自动验证的组件

主要功能有

* AssertFalseAttribute	   // 验证参数必须为false
* AssertTrueAttribute        // 验证参数必须为true
* DigitsAttribute			     // 验证参数的整数位个数和小数位个数必须符合用户的要求
* EmailAttribute                 // 验证参数必须是邮箱格式（也可以自定义验证邮箱的正则表达式）
* FutureAttribute      	     // 验证参数的值必须大于客户指定的时间
* InnerValidAttribute         // 告知系统集合类型的参数需要进一步校验集合中每一个元素的参数
* MaxAttribute     		       // 验证参数的值必须大于用户指定的值
* MinAttribute     		        // 验证参数的值必须小于用户指定的值
* NotBlankAttribute     	   // 验证参数去掉首尾空格后不能为空（只适用于字符串）
* NotEmptyAttribute     	  // 验证参数不能为空集合
* NotNullAttribute     	      // 验证参数不能为null
* NullAttribute     		         // 验证参数必须为null
* PastAttribute     		        // 验证参数的值必须小于客户指定的时间
* PatternAttribute     	       // 验证参数必须符合指定的正则表达式
* PhoneAttribute     		     // 验证参数必须是手机号
* RangeAttribute     		     // 验证参数的值在用户指定范围内
* SizeAttribute 			         // 验证集合的大小在用户指定范围内

## 使用方式

这里我用**PhoneAttribute**来举例说明。

1. 先定义一个Model

   ```c#
       public class PhoneModel
       {
           [Phone("手机号格式不正确")]
           public string PhoneField { get; set; }
       }
   ```

2. 编写具体的逻辑类

   ```c#
           [ValidateParam("model")]
           public int PhoneTest(PhoneModel model)
           {
               return 100;
           }
   
           [ValidateParam("model")]
           public Result PhoneTest1(PhoneModel model)
           {
               return new Result() { IsSucceed = true, Message = "参数验证通过，执行成功" };
           }
   
           [ValidateParam("model")]
           public async Task<Result> PhoneTest2(PhoneModel model)
           {
               return await Task.Factory.StartNew((() =>
                   {
                       return new Result() { IsSucceed = true, Message = "参数验证通过，执行成功" };
                   }));
           }
   ```

3. 调用此函数，查看效果

   ```c#
   PhoneModel model = new PhoneModel
   {
       PhoneField = "13555555554"
   };
   res = new TestLogic().PhoneTest(model);
   
   model.PhoneField = "13555555554111222";
   res = new TestLogic().PhoneTest(model);
   
   var res1 = new TestLogic().PhoneTest1(model);
   
   res1 = new TestLogic().PhoneTest2(model).Result;
   ```

4. 观察结果，可以发现，

   第一个函数执行成功，返回了100，

   第二个函数抛出了`手机号格式不正确`的异常

   第三个函数和第四个函数都返回了`Result`对象，其中`Message`字段的值为`手机号格式不正确`

5. 更多使用方式请查看源码中的单元测试。

### 项目的思路和开发背景

[开发思路](https://thxu.site/2020/05/12/CSharp版本的参数自动验证框架/)