using System;

namespace PEIS.Common
{
	public enum EnumUserLoginState
	{
		登录成功,
		验证码输入错误,
		用户名不存在,
		密码输入错误请重新输入,
		该用户名已被禁用,
		该用户名已被删除,
        连续登录错误次数超过3次请与管理员联系,
		验证码读取失败请与管理员联系,
		该用户登录名称存在异常请与管理员联系,
		数据库连接失败
	}
}
