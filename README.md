
#### 起因

In unity3d, when you have unsafe code, compile with mono will halt with error, this project will resolve this problem

使用MonoDevelop编译带unsafe code的C#代码时，需要在工程属性中勾选Allow 'unsafe' code选项，否则编译时会报Error。

具体操作步骤为：

1. 选中MonoDevelop中含有unsafe code的工程
2. 打开右键菜单，点击options选项
3. 在弹出的Dialog中选择 Build-->General
4. 在Dialog右下的Language Options处，勾选Allow 'unsafe' code

这样，这个工程就可以编译通过了

不过，仍然有另外一个问题：当手动在Unity3d项目中添加、删除C#脚本，或者修改C#脚本文件名时，Unity3d会重新创建MonoDevelop工程文件，导致我们前面手动设置的内容失效，需要重新设置才能进行编译。

**本项目就是解决以上问题的**

#### 方案

1. 假定我们的Unity3d工程使用git管理
2. 在欲要支持unsafe code的Unity3d工程中，**新建一个git的subtree**，Source Path为本工程的url地址，Local Relative Path为Unity3d工程中Assets目录下的一个路径
3. 更新，done~

然后，该工程就已经支持unsafe code编译了，当Unity3d重新创建MonoDevelop工程文件后，代码会自动勾选Allow 'unsafe' code复选框

注意： 以我使用是git工具是SourceTree为例，选中Local Relative Path时，目录地址的最后的那文件夹名必须是手动敲入的，也就是说必须是一个当前不存在的文件夹，而不能是用对话框选择一个当前就存在的文件夹。

#### 测试环境

1. macOS 10.12.5
2. Unity3d 5.4.3
2. MonoDevelop 5.9.6


